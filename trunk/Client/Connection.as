package
{
  import flash.events.Event;
  import flash.events.IOErrorEvent;
  import flash.events.SecurityErrorEvent;
  import flash.events.ProgressEvent;
  import flash.net.Socket;
  import flash.utils.ByteArray;

  import Client.UpdateToServer;
  import Server.UpdatePacket;
  import Server.MessageType;
  import Server.NoteType;

  public class Connection
  {
    public static function init(newGame : Game) : void
    {
      game = newGame;
      conn = new Socket();
      conn.addEventListener(Event.CLOSE, onClose);
      conn.addEventListener(Event.CONNECT, onConnect);
      conn.addEventListener(IOErrorEvent.IO_ERROR, onIoError);
      conn.addEventListener(SecurityErrorEvent.SECURITY_ERROR, onSecurityError);
      conn.addEventListener(ProgressEvent.SOCKET_DATA, onData);
      game.addChat("Client: Socket created");
      conn.connect("localhost", 1701);
      length = -1;
    }

    static function onClose(event : Event) : void
    {
      game.addChat("Client: Socket closed");
    }

    static function onConnect(event : Event) : void
    {
      game.addChat("Client: Socket Connected");
    }

    static function onIoError(event : IOErrorEvent) : void
    {
      game.addChat("Client: Socket IO Error");
    }

    static function onSecurityError(event : SecurityErrorEvent) : void
    {
      game.addChat("Client: Socket Security Error");
    }

    static function onData(event : ProgressEvent) : void
    {
      if (length == -1)
      {
        if (conn.bytesAvailable >= 4)
        {
          length = 0;
          for (var i = 0; i < 4; ++i)
          {
            var current = conn.readUnsignedByte();
            length |= (current << (i*8));
          }
        }
      }
      if (length != -1)
      {
        if (conn.bytesAvailable >= length)
        {
          var message = new ByteArray();
          conn.readBytes(message, 0, length);
          var update = new Server.UpdatePacket();
          update.readExternal(message);
          processUpdate(update);
          length = -1;
        }
      }
    }

    static function processUpdate(update : Server.UpdatePacket) : void
    {
      for each (var updateMessage in update.Messages)
      {
        var urgency = "";
        if (updateMessage.Type == MessageType.System)
        {
          urgency = "SYSTEM: ";
        }
        else if (updateMessage.Type == MessageType.Error)
        {
          urgency = "ERROR: ";
        }
        game.addChat(urgency + updateMessage.Text);
      }
      game.setController(update.ControllingEntityId);
      for each (var entity in update.Entities)
      {
        game.updateEntity(entity);
      }
      for each (var note in update.Notes)
      {
        if (note.Type == NoteType.EntityRemoved)
        {
          game.removeEntity(note.Target);
        }
      }
    }

    public static function sendMessage(message : Client.UpdateToServer) : void
    {
      var outBuffer = new ByteArray();
      message.writeExternal(outBuffer);
      var lenBuffer = encodeLength(outBuffer.length);
      conn.writeBytes(lenBuffer, 0, lenBuffer.length);
      conn.writeBytes(outBuffer, 0, outBuffer.length);
      conn.flush();
    }

    static function encodeLength(value : int) : ByteArray
    {
      var result = new ByteArray();
      for (var i = 0; i < 4; ++i)
      {
        result.writeByte((value >> (i*8)) & 0xff);
      }
      return result;
    }

    public static function sendChat(newChat : String) : void
    {
      var message = new Client.UpdateToServer();
      message.Messages = [newChat];
      sendMessage(message);
    }

    static var game : Game;
    static var conn : Socket;
    static var length : int;
  }
}


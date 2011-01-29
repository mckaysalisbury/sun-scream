package
{
  import flash.events.Event;
  import flash.events.IOErrorEvent;
  import flash.events.SecurityErrorEvent;
  import flash.events.ProgressEvent;
  import flash.net.Socket;
  import flash.utils.ByteArray;

  import Client.UpdatePacket;
  import Server.UpdatePacket;
  import Server.MessageType;

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
      trace("Socket created\n");
      conn.connect("localhost", 1701);
      length = -1;
    }

    static function onClose(event : Event) : void
    {
      trace("Socket closed\n");
    }

    static function onConnect(event : Event) : void
    {
      trace("Socket Connected\n");
    }

    static function onIoError(event : IOErrorEvent) : void
    {
      trace("Socket IO Error\n");
    }

    static function onSecurityError(event : SecurityErrorEvent) : void
    {
      trace("Socket Security Error\n");
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
          for each (var updateMessage in update.Messages)
          {
            var urgency = "System";
            if (updateMessage.Type == MessageType.Chat)
            {
              urgency = "Chat";
            }
            else if (updateMessage.Type == MessageType.Error)
            {
              urgency = "ERROR";
            }
            trace(urgency + ": " + updateMessage.Text + "\n");
          }
          game.setController(update.ControllingEntityId);
          for each (var entity in update.Entities)
          {
            game.updateEntity(entity);
          }
          length = -1;
        }
      }
    }

    public static function sendMessage(message : Client.UpdatePacket) : void
    {
      var outBuffer = new ByteArray();
      message.writeExternal(outBuffer);
      var lenBuffer = encodeLength(outBuffer.length);
      conn.writeBytes(lenBuffer, 0, lenBuffer.length);
      conn.writeBytes(outBuffer, 0, outBuffer.length);
      conn.flush();
      trace("sendMessage");
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

    static var game : Game;
    static var conn : Socket;
    static var length : int;
  }
}


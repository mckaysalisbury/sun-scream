package
{
  import flash.events.Event;
  import flash.events.IOErrorEvent;
  import flash.events.SecurityErrorEvent;
  import flash.events.ProgressEvent;
  import flash.events.AsyncErrorEvent;
  import flash.events.NetStatusEvent;
  import flash.events.SyncEvent;
  import flash.net.SharedObject;
  import flash.net.Socket;
  import flash.utils.ByteArray;

  import lib.ui.ButtonList;

  import ui.View;

  import Client.UpdateToServer;
  import Server.UpdatePacket;
  import Server.MessageType;
  import Server.NoteType;

  public class Connection
  {
    public static function init(newGame : Game) : void
    {
      game = newGame;
      name = "John Doe";
      host = "localhost";
      disk = SharedObject.getLocal("sun-scream");
      disk.addEventListener(flash.events.AsyncErrorEvent.ASYNC_ERROR,
                            asyncHandler);
      disk.addEventListener(flash.events.NetStatusEvent.NET_STATUS,
                            netStatusHandler);
      disk.addEventListener(flash.events.SyncEvent.SYNC,
                            syncHandler);
      load();
      conn = null;
      menu = null;
      menuButtons = null;
      disconnect();
    }

    static function asyncHandler(event : AsyncErrorEvent) : void
    {
      game.addChat("Client: SharedObject async error");
    }

    static function netStatusHandler(event : NetStatusEvent) : void
    {
      game.addChat("Client: SharedObject net status error");
    }

    static function syncHandler(event : SyncEvent) : void
    {
      game.addChat("Client: SharedObject sync error");
    }

    static function onClose(event : Event) : void
    {
      game.addChat("Client: Socket closed");
      isConnected = false;
    }

    static function onConnect(event : Event) : void
    {
      game.addChat("Client: Socket Connected");
      isConnected = true;
      sendChat("/setname " + name);
//      sendChat("/setfaction " + Math.floor(Math.random() * 4));
    }

    static function onIoError(event : IOErrorEvent) : void
    {
      game.addChat("Client: Socket IO Error");
      disconnect();
    }

    static function onSecurityError(event : SecurityErrorEvent) : void
    {
      game.addChat("Client: Socket Security Error");
      disconnect();
    }

    static function load() : void
    {
      try
      {
        if (disk.data.done == true && disk.data.version == 0)
        {
          if (disk.data.host != null && disk.data.host != "")
          {
            host = disk.data.host;
          }
          else
          {
            disk.data.host = host;
          }
          if (disk.data.name != null && disk.data.name != "")
          {
            name = disk.data.name;
          }
          else
          {
            disk.data.name = name;
          }
        }
        else if (! disk.data.done)
        {
          save();
        }
      }
      catch (e : Error)
      {
        save();
      }
    }

    static function save() : void
    {
      try
      {
        disk.data.done = false;
        disk.data.version = 0;
        disk.data.name = name;
        disk.data.host = host;
        disk.data.done = true;
        disk.flush();
      }
      catch (e : Error)
      {
      }
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
//          urgency = "SYSTEM: ";
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
      if (isConnected)
      {
        var outBuffer = new ByteArray();
        message.writeExternal(outBuffer);
        var lenBuffer = encodeLength(outBuffer.length);
        conn.writeBytes(lenBuffer, 0, lenBuffer.length);
        conn.writeBytes(outBuffer, 0, outBuffer.length);
        conn.flush();
      }
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
      if (isConnected)
      {
        var message = new Client.UpdateToServer();
        message.Messages = [newChat];
        sendMessage(message);
      }
    }

    static function connect() : void
    {
      conn = new Socket();
      conn.addEventListener(Event.CLOSE, onClose);
      conn.addEventListener(Event.CONNECT, onConnect);
      conn.addEventListener(IOErrorEvent.IO_ERROR, onIoError);
      conn.addEventListener(SecurityErrorEvent.SECURITY_ERROR, onSecurityError);
      conn.addEventListener(ProgressEvent.SOCKET_DATA, onData);
      game.addChat("Client: Socket created");
      conn.connect(host, 1701);
      if (menu != null)
      {
        menuButtons.cleanup();
        menuButtons = null;
        menu.parent.removeChild(menu);
        menu = null;
      }
    }

    static function disconnect() : void
    {
      isConnected = false;
      length = -1;
      if (conn != null)
      {
        conn.removeEventListener(Event.CLOSE, onClose);
        conn.removeEventListener(Event.CONNECT, onConnect);
        conn.removeEventListener(IOErrorEvent.IO_ERROR, onIoError);
        conn.removeEventListener(SecurityErrorEvent.SECURITY_ERROR,
                                 onSecurityError);
        conn.removeEventListener(ProgressEvent.SOCKET_DATA, onData);
        conn = null;
      }
      menu = new ConnectMenu();
      Main.root.addChild(menu);
      menu.x = (View.WIDTH - menu.width)/2;
      menu.y = (View.HEIGHT - menu.height)/2;
      menuButtons = new ButtonList([menu.ok]);
      menuButtons.setActions(click, menuButtons.frameOver,
                             menuButtons.frameOut);
      menu.hostField.text = host;
      menu.nameField.text = name;
    }

    static function click(choice : int) : void
    {
      host = menu.hostField.text;
      name = menu.nameField.text;
      save();
      connect();
    }

    static var game : Game;
    static var conn : Socket;
    static var length : int;
    static var isConnected : Boolean;
    static var menu : ConnectMenu;
    static var menuButtons : ButtonList;
    static var host : String;
    static var name : String;
    static var disk : SharedObject;
  }
}

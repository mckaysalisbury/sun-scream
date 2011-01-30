package ui
{
  import flash.display.DisplayObjectContainer;
  import flash.utils.Dictionary;
  import flash.events.KeyboardEvent;
  import flash.ui.Keyboard;

  import lib.Point;
  import lib.ui.ImageList;
  import lib.ui.Window;
  import lib.ui.WindowBorder;

  import Client.UpdateToServer;
  import Client.ThrustUpdate;
  import Server.EntityUpdate;
  import Server.EntityUpdateType;

  public class View
  {
    public function View(newParent : DisplayObjectContainer)
    {
      parent = newParent;
      background = new WindowBackgroundClip();
      parent.addChild(background);
      images = new ImageList();
      windowBorder = new WindowBorder();
      window = new Window(parent, new Point(WIDTH, WINDOW_HEIGHT),
                          null,
                          ImageConfig.layerCount,
                          images, windowBorder,
                          background, 0);
      windowBorder.init(parent);
      controller = 0;
      entities = new Dictionary();
      chat = new Chat(parent);
      window.addDragBeginCommand(beginThrust);
      window.addDragEndCommand(endThrust);
      frameCount = 1;
//      window.addClickCommand(click);
      isThrusting = false;
      shouldEndThrust = false;
      parent.stage.addEventListener(KeyboardEvent.KEY_UP, keyDown);
    }

    public function cleanup() : void
    {
      parent.stage.removeEventListener(KeyboardEvent.KEY_UP, keyDown);
      chat.cleanup();
      for each (var entity in entities)
      {
        entity.cleanup();
      }
      parent.removeChild(background);
      window.cleanup();
      windowBorder.cleanup();
      images.cleanup();
    }

    public function enterFrame() : void
    {
      images.update(window);
      ++frameCount;
      if (frameCount % 3 == 0)
      {
        if (isThrusting)
        {
          var pos = new Point(Math.floor(parent.stage.mouseX),
                              Math.floor(parent.stage.mouseY));
          var offset = window.getOffset();
          var absolute = new Point(pos.x + offset.x,
                                   pos.y + offset.y);
          var ship = entities[controller];
          if (ship != null)
          {
            var relative = new Point(absolute.x - ship.getPos().x,
                                     absolute.y - ship.getPos().y);
            var message = new Client.UpdateToServer();
            message.Thrust = new Client.ThrustUpdate();
            var angle = Math.floor(Math.atan2(relative.y, relative.x)*100);
            message.Thrust.Angle = angle;
            var dist = Math.floor(Math.sqrt(relative.x*relative.x
                                            + relative.y*relative.y));
            if (dist > WINDOW_HEIGHT/2)
            {
              message.Thrust.Distance = 100;
            }
            else
            {
              message.Thrust.Distance = Math.floor(dist/(WINDOW_HEIGHT/2)*100);
            }
//            trace("Thrust Angle: " + message.Thrust.Angle + ", Dist: "
//                  + message.Thrust.Distance);
            Connection.sendMessage(message);
          }
        }
        if (shouldEndThrust)
        {
          shouldEndThrust = false;
          isThrusting = false;
        }
      }
    }

    public function getParent() : DisplayObjectContainer
    {
      return parent;
    }

    function beginThrust(pos : Point) : void
    {
      isThrusting = true;
    }

    function endThrust(pos : Point) : void
    {
      shouldEndThrust = true;
    }

    public function updateEntity(update : EntityUpdate) : void
    {
      var pos = new Point(Math.floor(update.LocationX/SCALE),
                          Math.floor(update.LocationY/SCALE));
      var rotation = (update.Rotation / 100) * (180 / Math.PI) - 90;
      if (update.Id == controller)
      {
        window.setCenter(pos);
//        trace("orig: " + update.Rotation + ", transform: " + rotation);
      }
      if (entities[update.Id] == null)
      {
//        trace(String(update.Type) + ": " + pos.toString());
        entities[update.Id] = new Entity(images, update.Type);
      }
      var entity = entities[update.Id];
      entity.changePos(pos);
      entity.changeRotation(rotation);
    }

    public function removeEntity(target : int) : void
    {
      var oldEntity = entities[target];
      if (oldEntity != null)
      {
        oldEntity.cleanup();
        delete entities[oldEntity];
      }
    }

    public function setController(newController : int) : void
    {
      controller = newController;
    }

    public function addChat(newMessage : String) : void
    {
      chat.addChat(newMessage);
    }

    function keyDown(event : KeyboardEvent) : void
    {
      if (! chat.isActive())
      {
        var ch = String.fromCharCode(event.charCode);
        if (event.keyCode == Keyboard.PAGE_DOWN)
        {
          SCALE = SCALE*2;
          chat.addChat("Client: Scale=" + SCALE);
        }
        else if (event.keyCode == Keyboard.PAGE_UP)
        {
          SCALE = SCALE/2;
          chat.addChat("Client: Scale=" + SCALE);
        }
        else if (event.keyCode == Keyboard.TAB || ch == "t" || ch == " ")
        {
          Connection.sendChat("/tractor");
        }
        else if (ch == "r")
        {
          Connection.sendChat("/release");
        }
        else if (ch == "0")
        {
          Connection.sendChat("/setfaction 0");
        }
        else if (ch == "1")
        {
          Connection.sendChat("/setfaction 1");
        }
        else if (ch == "2")
        {
          Connection.sendChat("/setfaction 2");
        }
        else if (ch == "3")
        {
          Connection.sendChat("/setfaction 3");
        }
        else if (ch == "b")
        {
          Connection.sendChat("/build");
        }
      }
    }

    var parent : DisplayObjectContainer;
    var background : WindowBackgroundClip;
    var images : ImageList;
    var windowBorder : WindowBorder;
    var window : Window;
    var controller : int;
    var entities : Dictionary;
    var chat : Chat;
    var frameCount : int;
    var isThrusting : Boolean;
    var shouldEndThrust : Boolean;

    public static var SCALE : Number = 6000;
    public static var WIDTH = 1024;
    public static var HEIGHT = 768;
    public static var WINDOW_HEIGHT = HEIGHT - 40;
  }
}

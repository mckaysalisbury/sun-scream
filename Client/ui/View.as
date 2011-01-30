package ui
{
  import flash.display.DisplayObjectContainer;
  import flash.display.Shape;
  import flash.display.Sprite;
  import flash.utils.Dictionary;
  import flash.events.KeyboardEvent;
  import flash.events.MouseEvent;
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
      oldRange = 1;
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
      tractorParent = new Sprite();
      parent.addChild(tractorParent);
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
      parent.stage.addEventListener(MouseEvent.MOUSE_WHEEL, mouseWheel);
      range = new Shape();
      parent.addChild(range);
    }

    public function cleanup() : void
    {
      range.parent.removeChild(range);
      parent.stage.removeEventListener(KeyboardEvent.KEY_UP, keyDown);
      parent.stage.removeEventListener(MouseEvent.MOUSE_WHEEL, mouseWheel);
      chat.cleanup();
      for each (var entity in entities)
      {
        entity.cleanup();
      }
      parent.removeChild(tractorParent);
      window.cleanup();
      windowBorder.cleanup();
      parent.removeChild(background);
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
      if (! tractorParent.visible)
      {
        tractorParent.visible = true;
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
        for each (var towed in update.Towed)
        {
          var current = entities[towed];
          if (current != null)
          {
            current.setTowed(tractorParent);
          }
        }
      }
      if (entities[update.Id] == null)
      {
//        trace(String(update.Type) + ": " + pos.toString());
        entities[update.Id] = new Entity(images, update.Type);
      }
      var entity = entities[update.Id];
      entity.changePos(pos, window);
      entity.changeRotation(rotation);
      entity.changeScale(Math.floor(update.Size/SCALE));
      if (update.Id != controller)
      {
        entity.setName(update.Name);
      }
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

    public function updateRange(newRange : int) : void
    {
      var newRadius = Math.floor(newRange/SCALE);
      if (newRadius != oldRange)
      {
        oldRange = newRadius;
        range.graphics.clear();
        range.graphics.lineStyle(0, 0xaaaaaa);
        range.graphics.drawCircle(WIDTH/2, WINDOW_HEIGHT/2, oldRange);
      }
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
        else if (ch == "b")
        {
          Connection.sendChat("/build");
        }
      }
    }

    function mouseWheel(event : MouseEvent) : void
    {
//      trace(event.delta);
      if (event.delta > 0)
      {
        SCALE = SCALE/1.5;
//        chat.addChat("Client: Scale=" + SCALE);
      }
      else
      {
        SCALE = SCALE*1.5;
//        chat.addChat("Client: Scale=" + SCALE);
      }
      tractorParent.visible = false;
    }

    var parent : DisplayObjectContainer;
    var background : WindowBackgroundClip;
    var images : ImageList;
    var windowBorder : WindowBorder;
    var window : Window;
    var tractorParent : Sprite;
    var controller : int;
    var entities : Dictionary;
    var chat : Chat;
    var frameCount : int;
    var isThrusting : Boolean;
    var shouldEndThrust : Boolean;
    var range : Shape;
    var oldRange : int;

    public static var SCALE : Number = 12000;
    public static var WIDTH = 1024;
    public static var HEIGHT = 768;
    public static var WINDOW_HEIGHT = HEIGHT - 40;
  }
}

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
                          new Point(2048, 2048),
                          ImageConfig.layerCount,
                          images, windowBorder,
                          background, 0);
      windowBorder.init(parent);
      controller = 0;
      entities = new Dictionary();
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
            trace("Thrust Angle: " + message.Thrust.Angle + ", Dist: "
                  + message.Thrust.Distance);
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
      var pos = new Point(Math.floor(update.LocationX/SCALE) + 1024,
                          Math.floor(update.LocationY/SCALE) + 1024);
      if (update.Id == controller)
      {
        window.setCenter(pos);
      }
      if (entities[update.Id] == null)
      {
//        trace(String(update.Type) + ": " + pos.toString());
        entities[update.Id] = new Entity(images, update.Type);
      }
      entities[update.Id].changePos(pos);
    }

    public function setController(newController : int) : void
    {
      controller = newController;
    }

    function keyDown(event : KeyboardEvent) : void
    {
      if (event.keyCode == Keyboard.PAGE_DOWN)
      {
        SCALE = SCALE*2;
      }
      else if (event.keyCode == Keyboard.PAGE_UP)
      {
        SCALE = SCALE/2;
      }
      trace(SCALE);
    }

    var parent : DisplayObjectContainer;
    var background : WindowBackgroundClip;
    var images : ImageList;
    var windowBorder : WindowBorder;
    var window : Window;
    var controller : int;
    var entities : Dictionary;
    var frameCount : int;
    var isThrusting : Boolean;
    var shouldEndThrust : Boolean;

    public static var SCALE : Number = 100000;
    public static var WIDTH = 1024;
    public static var HEIGHT = 768;
    public static var WINDOW_HEIGHT = HEIGHT - 30;
  }
}

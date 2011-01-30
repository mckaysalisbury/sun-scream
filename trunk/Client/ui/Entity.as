package ui
{
  import flash.display.DisplayObjectContainer;
  import flash.display.Shape;

  import lib.Point;
  import lib.ui.ImageText;
  import lib.ui.ImageList;
  import lib.ui.Window;

  import Server.EntityUpdateType;

  public class Entity
  {
    public function Entity(newImages : ImageList, newType : int) : void
    {
      images = newImages;
      type = 0;
      if (newType > 0 && newType < ImageConfig.entities.length)
      {
        type = newType;
      }
      sprite = new ImageText(ImageConfig.entities[type]);
      images.add(sprite);
      pos = new Point(0, 0);
      tractor = null;
      isTowed = false;
    }

    public function cleanup() : void
    {
      cleanupTractor();
      images.remove(sprite);
      sprite.cleanup();
    }

    function cleanupTractor() : void
    {
      if (tractor != null)
      {
        tractor.parent.removeChild(tractor);
        tractor = null;
      }
    }

    public function getPos() : Point
    {
      return pos;
    }

    public function changePos(newPos : Point, window : Window) : void
    {
      pos = newPos.clone();
      sprite.setPos(newPos);
      if (isTowed)
      {
        var source = window.toRelative(pos);
        tractor.graphics.clear();
        tractor.graphics.lineStyle(0, 0x55ff55);
        tractor.graphics.moveTo(source.x, source.y);
        tractor.graphics.lineTo(View.WIDTH/2, View.WINDOW_HEIGHT/2);
      }
      else
      {
        cleanupTractor();
      }
      isTowed = false;
    }

    public function changeRotation(newRotation : int) : void
    {
      if (type != Server.EntityUpdateType.Planet)
      {
        sprite.setRotation(newRotation);
      }
    }

    public function changeScale(newRadius : int) : void
    {
      var size = ImageConfig.entities[type].size;
      var most = size.x;
      if (size.y > size.x)
      {
        most = size.y;
      }
      var scale = newRadius*2/most;
      if (scale < 0.3 && (type == Server.EntityUpdateType.BuilderShip
                          || type == Server.EntityUpdateType.DestroyerShip
                          || type == Server.EntityUpdateType.GuideShip))
      {
        scale = 0.3;
      }
      else if (scale < 0.1)
      {
        scale = 0.1;
      }
      sprite.setScale(scale);
    }

    public function setTowed(parent : DisplayObjectContainer) : void
    {
      if (tractor == null)
      {
        tractor = new Shape();
        parent.addChild(tractor);
      }
      isTowed = true;
    }

    public function setName(name : String) : void
    {
      if (name != null
          && (type == Server.EntityUpdateType.BuilderShip
              || type == Server.EntityUpdateType.DestroyerShip
              || type == Server.EntityUpdateType.GuideShip
              || type == Server.EntityUpdateType.Planet))
      {
        sprite.setText(name);
      }
    }

    var sprite : ImageText;
    var images : ImageList;
    var type : int;
    var pos : Point;
    var tractor : Shape;
    var isTowed : Boolean;
  }
}

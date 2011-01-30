package ui
{
  import lib.Point;
  import lib.ui.Image;
  import lib.ui.ImageList;
  import lib.ui.Window;

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
      sprite = new Image(ImageConfig.entities[type]);
      images.add(sprite);
      pos = new Point(0, 0);
    }

    public function cleanup() : void
    {
      images.remove(sprite);
      sprite.cleanup();
    }

    public function getPos() : Point
    {
      return pos;
    }

    public function changePos(newPos : Point) : void
    {
      pos = newPos.clone();
      sprite.setPos(newPos);
    }

    public function changeRotation(newRotation : int) : void
    {
      sprite.setRotation(newRotation);
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
      if (scale < 0.1)
      {
        scale = 0.1;
      }
      sprite.setScale(scale);
    }

    var sprite : Image;
    var images : ImageList;
    var type : int;
    var pos : Point;
  }
}

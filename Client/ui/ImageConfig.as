package ui
{
  import lib.Point;
  import lib.ui.ImageType;

  public class ImageConfig
  {
    public static var STAR_LAYER = 0;
    public static var ASTEROID_LAYER = 1;
    public static var SHIP_LAYER = 2;
    public static var layerCount = 3;

    public static var entities = [new ImageType("EmptyClip", SHIP_LAYER,
                                                new Point(4, 4)),
                                  new ImageType("ShipClip", SHIP_LAYER,
                                                new Point(72, 65)),
                                  new ImageType("ShipClip", SHIP_LAYER,
                                                new Point(72, 65)),
                                  new ImageType("ShipClip", SHIP_LAYER,
                                                new Point(72, 65)),
                                  new ImageType("PlanetClip", STAR_LAYER,
                                                new Point(30, 30)),
                                  new ImageType("AsteroidClip", ASTEROID_LAYER,
                                                new Point(90, 90)),
                                  new ImageType("HitchClip", ASTEROID_LAYER,
                                                new Point(30, 30))];
  }
}

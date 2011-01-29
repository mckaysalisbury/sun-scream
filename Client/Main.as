package
{
  import flash.display.DisplayObjectContainer;

  public class Main
  {
    public static function init(parent : DisplayObjectContainer) : void
    {
      root = parent;
      var settings = new GameSettings();
      var newGame = new Game(root, settings);
      state = newGame
      Connection.init(newGame);
    }

    static var root : DisplayObjectContainer;
    static var state : MainState;
  }
}

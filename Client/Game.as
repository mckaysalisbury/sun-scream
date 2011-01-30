package
{
  import flash.display.DisplayObjectContainer;
  import flash.events.Event;

  import ui.View;
  import logic.Model;
  import Server.EntityUpdate;

  public class Game implements MainState
  {
    public function Game(newParent : DisplayObjectContainer,
                         newSettings : GameSettings) : void
    {
      parent = newParent;
      settings = newSettings;
      view = new ui.View(parent);
      model = new logic.Model(settings, view);

      parent.addEventListener(Event.ENTER_FRAME, enterFrame);
    }

    public function cleanup() : void
    {
      model.cleanup();
      view.cleanup();
    }

    function enterFrame(event : Event) : void
    {
      model.enterFrame();
      view.enterFrame();
    }

    public function updateEntity(update : EntityUpdate) : void
    {
      view.updateEntity(update);
    }

    public function removeEntity(target : int) : void
    {
      view.removeEntity(target);
    }

    public function setController(newController : int) : void
    {
      view.setController(newController);
    }

    public function updateRange(newRange : int) : void
    {
      view.updateRange(newRange);
    }

    public function addChat(newMessage : String) : void
    {
      view.addChat(newMessage);
    }

    var parent : DisplayObjectContainer;
    var settings : GameSettings;
    var view : ui.View;
    var model : logic.Model;
  }
}

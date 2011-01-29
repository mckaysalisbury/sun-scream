package logic
{
  import lib.ActorList;
  import lib.ChangeList;
  import ui.View;

  public class Model
  {
    public function Model(newSettings : GameSettings, newView : ui.View) : void
    {
      settings = newSettings;
      view = newView;
      actors = new lib.ActorList();
      changes = new lib.ChangeList();
    }

    public function cleanup() : void
    {
    }

    public function enterFrame() : void
    {
      actors.enterFrame(changes);
      changes.execute(this, view);
    }

    var settings : GameSettings;
    var view : ui.View;
    var actors : lib.ActorList;
    var changes : lib.ChangeList;
  }
}

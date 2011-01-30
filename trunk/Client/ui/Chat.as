package ui
{
  import flash.display.DisplayObjectContainer;
  import flash.events.KeyboardEvent;
  import flash.events.MouseEvent;
  import flash.ui.Keyboard;

  import lib.ui.ButtonList;

  public class Chat
  {
    public function Chat(parent : DisplayObjectContainer) : void
    {
      clip = new ChatClip();
      parent.addChild(clip);
      clip.x = 0;
      clip.y = View.WINDOW_HEIGHT;
      clip.history.mouseEnabled = false;
      clip.send.text.text = "Send";
      var buttons = new ButtonList([clip.send]);
      buttons.setActions(clickSend, buttons.frameOver, buttons.frameOut);
      clip.stage.addEventListener(KeyboardEvent.KEY_UP, keyPress);
      clip.mouseEnabled = false;
    }

    public function cleanup() : void
    {
      buttons.cleanup();
      clip.stage.removeEventListener(KeyboardEvent.KEY_UP, keyPress);
      clip.parent.removeChild(clip);
    }

    function keyPress(event : KeyboardEvent) : void
    {
      if (event.keyCode == Keyboard.ENTER)
      {
        if (isActive())
        {
          clickSend(0);
        }
        else
        {
          clip.stage.focus = clip.input;
        }
      }
    }

    function clickSend(choice : int) : void
    {
      if (clip.input.text != "")
      {
        Connection.sendChat(clip.input.text);
        clip.input.text = "";
      }
      clip.stage.focus = null;
    }

    public function addChat(message : String) : void
    {
      clip.history.appendText(message + "\n");
      clip.history.scrollV = clip.history.maxScrollV;
    }

    function isActive() : Boolean
    {
      return clip.stage.focus == clip.input;
    }

    var clip : ChatClip;
    var buttons : ButtonList;
  }
}

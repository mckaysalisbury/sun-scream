package ui
{
  import flash.display.DisplayObjectContainer;
  import flash.events.KeyboardEvent;
  import flash.events.MouseEvent;
  import flash.ui.Keyboard;

  public class Chat
  {
    public function Chat(parent : DisplayObjectContainer) : void
    {
      clip = new ChatClip();
      parent.addChild(clip);
      clip.x = 0;
      clip.y = View.WINDOW_HEIGHT;
      clip.history.mouseEnabled = false;
      clip.send.addEventListener(MouseEvent.CLICK, clickSend);
      clip.stage.addEventListener(KeyboardEvent.KEY_UP, keyPress);
      clip.mouseEnabled = false;
    }

    public function cleanup() : void
    {
      clip.send.removeEventListener(MouseEvent.CLICK, clickSend);
      clip.stage.removeEventListener(KeyboardEvent.KEY_UP, keyPress);
      clip.parent.removeChild(clip);
    }

    function keyPress(event : KeyboardEvent) : void
    {
      if (event.keyCode == Keyboard.ENTER)
      {
        if (clip.stage.focus == clip.input)
        {
          clickSend(null);
        }
        else
        {
          clip.stage.focus = clip.input;
        }
      }
    }

    function clickSend(event : MouseEvent) : void
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

    var clip : ChatClip;
  }
}

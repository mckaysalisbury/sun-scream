package ui
{

  public class Sound
  {
    public static var sounds = [null, new Sound1(), new Sound2(),
                                new Sound3(), new Sound4(), new Sound5(),
                                new Sound6()];

    public static function play(index : int) : void
    {
      if (index > 0 && index < sounds.length && index != 3)
      {
        sounds[index].play();
      }
    }
  }
}

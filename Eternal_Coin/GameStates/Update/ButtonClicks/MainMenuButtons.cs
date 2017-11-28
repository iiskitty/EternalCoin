using System;

namespace Eternal_Coin
{
  public class MainMenuButtons
  {
    public static void ExitGame()
    {
      GVar.exitAfterFade = true;
      Colours.drawBlackFade = true;
      Colours.fadeIn = true;
    }

    public static void ChangeToChooseCharacter()
    {
      GVar.changeToCreateCharacter = true;
      Colours.drawBlackFade = true;
      Colours.fadeIn = true;
    }
  }
}

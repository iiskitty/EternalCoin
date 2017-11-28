using System;

namespace Eternal_Coin
{
  public class UniversalButtons
  {
    public static void ChangeToMainMenu()
    {
      GVar.changeToMainMenu = true;
      Colours.drawBlackFade = true;
      Colours.fadeIn = true;
      Lists.mainMenuButtons.Clear();
    }

    public static void ChangeToOptions()
    {
      GVar.changeToOptions = true;
      Colours.drawBlackFade = true;
      Colours.fadeIn = true;
    }
  }
}

using System;
using System.Xml;

namespace Eternal_Coin
{
  public class OptionsButtons
  {
    

    public static void BackToGame()
    {
      GVar.changeBackToGame = true;
      Colours.drawBlackFade = true;
      Colours.fadeIn = true;
    }

    public static void ToggleFullScreen()
    {
      GVar.toggleFullScreen = true;
    }

    public static void ToggleDebugLog()
    {
      GVar.debugLogEnabled = !GVar.debugLogEnabled;
      if (GVar.debugLogEnabled)
        GVar.CreateDebugLog();
      XmlDocument options = new XmlDocument();
      options.Load("./Content/Options.xml");
      XmlNode optionsNode = options.DocumentElement.SelectSingleNode("/options/enabledebuglog");
      optionsNode.InnerText = Convert.ToString(GVar.debugLogEnabled);
      options.Save("./Content/Options.xml");
    }
  }
}

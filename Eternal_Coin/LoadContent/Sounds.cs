using Microsoft.Xna.Framework.Audio;
using System.Xml;
using Microsoft.Xna.Framework.Content;

namespace Eternal_Coin
{
  public class Sounds
  {
    public static void LoadSounds(ContentManager Content)
    {
      XmlDocument loadSoundDoc = new XmlDocument();
      loadSoundDoc.Load("./Content/LoadData/Sounds.xml");
      XmlNodeList soundList = loadSoundDoc.SelectNodes("/sounds/load/sound");

      foreach (XmlNode sound in soundList)
      {
        Dictionaries.sounds.Add(sound["id"].InnerText, Content.Load<SoundEffect>(sound["filepath"].InnerText));
        Lists.soundIDs.Add(sound["id"].InnerText);
      }

      XmlDocument setSoundDoc = new XmlDocument();
      setSoundDoc.Load("./Content/LoadData/Sounds.xml");
      XmlNode setSound = setSoundDoc.SelectSingleNode("/sounds/set");
      SetSounds(setSound);
    }

    private static void SetSounds(XmlNode setSound)
    {
      GVar.SoundIDs.clickcoin = setSound["clickcoin"].InnerText;
      GVar.SoundIDs.clicklocnode = setSound["clicklocnode"].InnerText;
      GVar.SoundIDs.clickbutton = setSound["clickbutton"].InnerText;
      GVar.SoundIDs.buttonmouseover = setSound["buttonmouseover"].InnerText;
    }
  }
}

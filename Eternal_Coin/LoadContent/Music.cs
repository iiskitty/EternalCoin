
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Xml;

namespace Eternal_Coin
{
  class Music
  {
    public static Song menuMusic;

    public static void LoadMusic(ContentManager Content)
    {
      XmlDocument music = new XmlDocument();
      music.Load("./Content/LoadData/Music.xml");

      XmlNodeList songs = music.SelectNodes("/music/load/song");

      foreach (XmlNode S in songs)
        Dictionaries.music.Add(S["id"].InnerText, Content.Load<Song>(S["filepath"].InnerText));

      XmlNode setMusic = music.SelectSingleNode("/music/set");

      menuMusic = Dictionaries.music[setMusic["menumusic"].InnerText];

    }
  }
}

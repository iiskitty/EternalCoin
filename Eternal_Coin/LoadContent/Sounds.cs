using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Eternal_Coin
{
    public class Sounds
    {
        public static void LoadSounds(ContentManager Content)
        {
            XmlDocument loadSoundDoc = new XmlDocument();
            loadSoundDoc.Load("./Content/LoadData/LoadSounds.xml");
            XmlNodeList soundList = loadSoundDoc.SelectNodes("/load/sound");

            foreach (XmlNode sound in soundList)
            {
                Dictionaries.sounds.Add(sound["id"].InnerText, Content.Load<SoundEffect>(sound["filepath"].InnerText));
                Lists.soundIDs.Add(sound["id"].InnerText);
            }

            XmlDocument setSoundDoc = new XmlDocument();
            setSoundDoc.Load("./Content/LoadData/SetSounds.xml");
            XmlNode setSound = setSoundDoc.SelectSingleNode("/set");
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

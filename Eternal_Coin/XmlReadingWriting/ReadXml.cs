using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using System.Text;

namespace Eternal_Coin
{
    public class ReadXml
    {
        public static void ReadLocationXmlFile(Entity player, Node locationNode)
        {
            string fileDir = "Content/GameFiles/" + player.Name + "/" + locationNode.LocatoinFilePath;
            try
            {
                GVar.curLocNode.Load(fileDir);
                XmlNode locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");

                GVar.location = new Location(locNode[GVar.XmlTags.LocationTags.name].InnerText, locNode[GVar.XmlTags.LocationTags.description].InnerText, Convert.ToBoolean(locNode[GVar.XmlTags.LocationTags.searched].InnerText), Convert.ToBoolean(locNode[GVar.XmlTags.LocationTags.hasnpc].InnerText), Convert.ToBoolean(locNode[GVar.XmlTags.LocationTags.hasshop].InnerText), Convert.ToBoolean(locNode[GVar.XmlTags.LocationTags.hasenemy].InnerText));
                locationNode.Searched = Convert.ToBoolean(locNode[GVar.XmlTags.LocationTags.searched].InnerText);

            }
            catch
            {
                GVar.location = null;
            }
        }
    }
}

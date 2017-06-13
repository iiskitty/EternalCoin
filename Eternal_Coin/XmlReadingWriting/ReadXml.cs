using System;
using System.Xml;

namespace Eternal_Coin
{
    public class ReadXml
    {
        /// <summary>
        /// Reads the xml file of a location that is passed in.
        /// </summary>
        /// <param name="player">the player for the players name.</param>
        /// <param name="locationNode">current location.</param>
        public static void ReadLocationXmlFile(Entity player, Node locationNode)
        {
            string fileDir = "Content/GameFiles/" + player.Name + "/" + locationNode.LocatoinFilePath;//set string to xml file's directory location.
            try
            {
                GVar.curLocNode.Load(fileDir);//load the xml file with the created string.
                XmlNode locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");//grab the location tag from the loaded xml file.

                GVar.location = new Location(locNode[GVar.XmlTags.LocationTags.name].InnerText, locNode[GVar.XmlTags.LocationTags.description].InnerText, Convert.ToBoolean(locNode[GVar.XmlTags.LocationTags.searched].InnerText), Convert.ToBoolean(locNode[GVar.XmlTags.LocationTags.hasnpc].InnerText), Convert.ToBoolean(locNode[GVar.XmlTags.LocationTags.hasshop].InnerText), Convert.ToBoolean(locNode[GVar.XmlTags.LocationTags.hasenemy].InnerText));//create a location with the data from the xml file.
                locationNode.Searched = Convert.ToBoolean(locNode[GVar.XmlTags.LocationTags.searched].InnerText);

            }
            catch
            {
                GVar.location = null;
            }
        }
    }
}

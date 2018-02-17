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
    public static void ReadLocationXmlFile(Node locationNode)
    {
      string fileDir = GVar.gameFilesLocation + GVar.player.Name + "/" + locationNode.LocatoinFilePath;//set string to xml file's directory location.
      try
      {
        GVar.curLocNode.Load(fileDir);//load the xml file with the created string.
        XmlNode locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");//grab the location tag from the loaded xml file.
        
      }
      catch
      {

      }
    }

    public static void ReadCurrentLocation(Player player)
    {
      string fileDir = GVar.gameFilesLocation + player.Name + "/" + player.CurrentLocation.LocatoinFilePath;//set string to xml file's directory location.

      try
      {
        GVar.curLocNode.Load(fileDir);

        XmlNode currLocNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");
        
        player.CurrentLocation.Description = Text.WrapText(Fonts.lucidaConsole16Regular, currLocNode["description"].InnerText, 600);
        player.CurrentLocation.HasNPC = Convert.ToBoolean(currLocNode["hasnpc"].InnerText);
        player.CurrentLocation.HasShop = Convert.ToBoolean(currLocNode["hasshop"].InnerText);
        player.CurrentLocation.HasEnemy = Convert.ToBoolean(currLocNode["hasenemy"].InnerText);
        player.CurrentLocation.Searched = Convert.ToBoolean(currLocNode["searched"].InnerText);
      }
      catch
      {

      }
    }


  }
}

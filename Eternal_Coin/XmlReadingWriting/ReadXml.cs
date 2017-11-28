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

    public static void ReadCurrentLocation()
    {
      string fileDir = GVar.gameFilesLocation + GVar.player.Name + "/" + GVar.player.CurrentLocation.LocatoinFilePath;//set string to xml file's directory location.

      try
      {
        GVar.curLocNode.Load(fileDir);

        XmlNode currLocNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");
        
        GVar.player.CurrentLocation.Description = Text.WrapText(Fonts.lucidaConsole16Regular, currLocNode["description"].InnerText, 600);
        GVar.player.CurrentLocation.HasNPC = Convert.ToBoolean(currLocNode["hasnpc"].InnerText);
        GVar.player.CurrentLocation.HasShop = Convert.ToBoolean(currLocNode["hasshop"].InnerText);
        GVar.player.CurrentLocation.HasEnemy = Convert.ToBoolean(currLocNode["hasenemy"].InnerText);
        
      }
      catch
      {

      }
    }


  }
}

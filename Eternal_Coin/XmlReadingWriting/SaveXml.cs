using System;
using System.Xml;

namespace Eternal_Coin
{
    public class SaveXml
    {
        /// <summary>
        /// Saves a locations xml file after being modified.
        /// </summary>
        /// <param name="player">pass in player for the players name.</param>
        /// <param name="locationNode">pass in any location's file path to be saved.</param>
        public static void SaveLocationXmlFile(Entity player, Node locationNode)
        {
            string fileDir = GVar.gameFilesLocation + player.Name + "/" + locationNode.LocatoinFilePath;//set xml files directory.
            GVar.curLocNode.Save(fileDir);//save.
        }

        /// <summary>
        /// creates an xml element for items(saving players or shops inventory.
        /// </summary>
        /// <param name="item">the item being saved.</param>
        /// <param name="itemDoc">the xml document item is being saved to.</param>
        /// <returns></returns>
        public static XmlElement CreateItemXmlElement(Item item, XmlDocument itemDoc)
        {
            XmlElement itemElement = itemDoc.CreateElement(string.Empty, "item", string.Empty);//create base item tag.

            XmlElement itemName = itemDoc.CreateElement(string.Empty, "itemname", string.Empty);//create child itemname tag.
            XmlText itemNameInner = itemDoc.CreateTextNode(item.ItemName);//create innertext for itemname tag.
            itemName.AppendChild(itemNameInner);//append innertext to itemname.
            itemElement.AppendChild(itemName);//append itemname to item.
            
            //if the item being saved is of class jewellry.
            if (item.ItemClass == GVar.ItemClassName.jewellry)
            {
                try
                {
                    Jewellry jewl = (Jewellry)item;//cast item to Jewellry.
                    XmlElement eternalCoin = itemDoc.CreateElement(string.Empty, "eternalcoin", string.Empty);//create eternalcoin tag.
                    XmlText eternalCoinInner = itemDoc.CreateTextNode(jewl.eternalCoinSlot.item.ItemName);//create innertext for eternalcoin tag.
                    eternalCoin.AppendChild(eternalCoinInner);//append innertext to eternalcoin.
                    itemElement.AppendChild(eternalCoin);//append eternalcoin to item.
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e  + "]", 1);//this will happen if the jewellry doesn't have an eternal coin or something else went wrong, check debug logs.
                }
            }

            try
            {
                XmlElement itemInventorySlot = itemDoc.CreateElement(string.Empty, GVar.XmlTags.ItemTags.inventoryslot, string.Empty);//create inventoryslot tag.
                XmlText itemPlayerInventorySlotInner = itemDoc.CreateTextNode(item.PlayerInventorySlot.ToString());//create innertext for inventoryslot tag.
                itemInventorySlot.AppendChild(itemPlayerInventorySlotInner);//append innertext to inventoryslot.
                itemElement.AppendChild(itemInventorySlot);//append inventoryslot to item.
            }
            catch
            {
                GVar.LogDebugInfo("!No PlayerInventorySlot!", 2);//i dont remember how this would happen.
            }

            return itemElement;
        }
    }
}

using System;
using System.Xml;

namespace Eternal_Coin
{
    public class SaveXml
    {
        public static void SaveLocationXmlFile(Entity player, Node locationNode)
        {
            string fileDir = GVar.gameFilesLocation + player.Name + "/" + locationNode.LocatoinFilePath;
            GVar.curLocNode.Save(fileDir);
        }

        public static XmlElement CreateItemXmlElement(Item item, XmlDocument itemDoc)
        {
            XmlElement itemElement = itemDoc.CreateElement(string.Empty, "item", string.Empty);

            XmlElement itemName = itemDoc.CreateElement(string.Empty, "itemname", string.Empty);
            XmlText itemNameInner = itemDoc.CreateTextNode(item.ItemName);
            itemName.AppendChild(itemNameInner);
            itemElement.AppendChild(itemName);
            
            if (item.ItemClass == GVar.ItemClassName.jewellry)
            {
                try
                {
                    Jewellry jewl = (Jewellry)item;
                    XmlElement eternalCoin = itemDoc.CreateElement(string.Empty, "eternalcoin", string.Empty);
                    XmlText eternalCoinInner = itemDoc.CreateTextNode(jewl.eternalCoinSlot.item.ItemName);
                    eternalCoin.AppendChild(eternalCoinInner);
                    itemElement.AppendChild(eternalCoin);
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e  + "]", 1);
                }
            }

            try
            {
                XmlElement itemInventorySlot = itemDoc.CreateElement(string.Empty, GVar.XmlTags.ItemTags.inventoryslot, string.Empty);
                XmlText itemPlayerInventorySlotInner = itemDoc.CreateTextNode(item.PlayerInventorySlot.ToString());
                itemInventorySlot.AppendChild(itemPlayerInventorySlotInner);
                itemElement.AppendChild(itemInventorySlot);
            }
            catch
            {
                GVar.LogDebugInfo("!No PlayerInventorySlot!", 2);
            }

            return itemElement;
        }
    }
}

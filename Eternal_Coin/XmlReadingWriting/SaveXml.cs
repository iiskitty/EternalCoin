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
    public class SaveXml
    {
        public static void SaveLocationXmlFile(Entity player, Node locationNode)
        {
            string fileDir = "Content/GameFiles/" + player.Name + "/" + locationNode.LocatoinFilePath;
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
                XmlElement itemPlayerInventorySlot = itemDoc.CreateElement(string.Empty, GVar.XmlTags.ItemTags.playerinventoryslot, string.Empty);
                XmlText itemPlayerInventorySlotInner = itemDoc.CreateTextNode(item.PlayerInventorySlot.ToString());
                itemPlayerInventorySlot.AppendChild(itemPlayerInventorySlotInner);
                itemElement.AppendChild(itemPlayerInventorySlot);
            }
            catch
            {
                GVar.LogDebugInfo("!No PlayerInventorySlot!", 2);
            }

            return itemElement;
        }
    }
}

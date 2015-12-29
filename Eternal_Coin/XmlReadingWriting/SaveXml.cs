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

            //XmlElement itemClass = itemDoc.CreateElement(string.Empty, "class", string.Empty);
            //XmlText itemClassInner = itemDoc.CreateTextNode(item.ItemClass);
            //itemClass.AppendChild(itemClassInner);
            //itemElement.AppendChild(itemClass);

            XmlElement itemName = itemDoc.CreateElement(string.Empty, "itemname", string.Empty);
            XmlText itemNameInner = itemDoc.CreateTextNode(item.ItemName);
            itemName.AppendChild(itemNameInner);
            itemElement.AppendChild(itemName);

            //XmlElement itemType = itemDoc.CreateElement(string.Empty, "itemtype", string.Empty);
            //XmlText itemTypeInner = itemDoc.CreateTextNode(item.Type.name);
            //itemType.AppendChild(itemTypeInner);
            //itemElement.AppendChild(itemType);

            //XmlElement itemMaterial = itemDoc.CreateElement(string.Empty, "itemmaterial", string.Empty);
            //XmlText itemMaterialInner = itemDoc.CreateTextNode(item.Material.name);
            //itemMaterial.AppendChild(itemMaterialInner);
            //itemElement.AppendChild(itemMaterial);

            //XmlElement itemInventorySlot = itemDoc.CreateElement(string.Empty, "inventoryslot", string.Empty);
            //XmlText itemInventorySlotInner = itemDoc.CreateTextNode(item.InventorySlot);
            //itemInventorySlot.AppendChild(itemInventorySlotInner);
            //itemElement.AppendChild(itemInventorySlot);

            //foreach (string id in item.Attacks)
            //{
            //    XmlElement attackNode = itemDoc.CreateElement(string.Empty, item.Type.name + "attack", string.Empty);
            //    XmlText attackNodeInner = itemDoc.CreateTextNode(id);
            //    attackNode.AppendChild(attackNodeInner);
            //    itemElement.AppendChild(attackNode);
            //}

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

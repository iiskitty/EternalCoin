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
    public class Shop
    {
        public static void LoadShopInventory(XmlDocument shopDoc)
        {
            

            XmlNodeList itemList = shopDoc.SelectNodes("/location/shop/inventory/item");

            foreach (XmlNode item in itemList)
            {
                Item shopItem = ItemBuilder.BuildItem(Dictionaries.items[item[GVar.XmlTags.ItemTags.itemname].InnerText]);
                
                for (int i = 0; i < 40; i++)
                {
                    if (InventoryManager.shopInventory.itemSlots[i].item == null)
                    {
                        InventoryManager.shopInventory.itemSlots[i].item = shopItem;
                        Lists.shopItems.Add(InventoryManager.shopInventory.itemSlots[i].item);
                        break;
                    }
                }
            }
        }

        public static void SaveShopInventory(XmlDocument shopDoc, LocationNode locationNode)
        {
            XmlNode item = shopDoc.SelectSingleNode("/location/shop/inventory");

            item.RemoveAll();

            //XmlNode itemNode = shopDoc.SelectSingleNode("/location/shop/inventory");

            for (int i = 0; i < 40; i++)
            {
                if (InventoryManager.shopInventory.itemSlots[i].item != null)
                {
                    item.AppendChild(SaveXml.CreateItemXmlElement(InventoryManager.shopInventory.itemSlots[i].item, shopDoc));
                }
            }
            string fileDir = "Content/GameFiles/" + GVar.player.Name + "/" + locationNode.LocatoinFilePath;
            shopDoc.Save(fileDir);
        }
    }
}

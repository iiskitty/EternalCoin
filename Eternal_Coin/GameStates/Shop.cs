﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;

namespace Eternal_Coin
{
    public class Shop
    {
        /// <summary>
        /// Loads a shops invemtory from the current locations xml file.
        /// </summary>
        /// <param name="shopDoc">the locations xml file that contains a shop tag.</param>
        public static void LoadShopInventory(XmlDocument shopDoc)
        {
            XmlNodeList itemList = shopDoc.SelectNodes("/location/shop/inventory/item");//select item tags in shops/inventory tags.

            //cycle through the items.
            for (int i = 0; i < itemList.Count; i++)
            {
                Item shopItem = ItemBuilder.BuildItem(Dictionaries.items[itemList[i][GVar.XmlTags.ItemTags.itemname].InnerText]);//create a item from the list.
                int invSlot = 99;//set to 99 to check for no set inventory slot.
                try
                {
                    invSlot = Convert.ToInt32(itemList[i][GVar.XmlTags.ItemTags.inventoryslot].InnerText);//try set inventory slot from xml document.
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo(e.ToString(), 2);//log the error in case there is no set inventory slot.
                }

                AddItemToShopInventory(shopItem, invSlot);
            }
        }

        /// <summary>
        /// If the item has a set inventory slot number(1-40) it will be added accordingly.
        /// If the item does not have a set inventory slot number, an empty slot will be found.
        /// </summary>
        /// <param name="item">the item being added.</param>
        /// <param name="invSlot">the slot for the item to be placed in.</param>
        private static void AddItemToShopInventory(Item item, int invSlot)
        {
            if (invSlot <= 40)
            {
                InventoryManager.shopInventory.ItemSlots[invSlot].item = ItemBuilder.BuildItem(item);
                Lists.shopItems.Add(InventoryManager.shopInventory.ItemSlots[invSlot].item);
            }
            else
            {
                for (int j = 0; j < 40; j++)
                {
                    if (InventoryManager.shopInventory.ItemSlots[j].item == null)
                    {
                        InventoryManager.shopInventory.ItemSlots[j].item = item;
                        Lists.shopItems.Add(InventoryManager.shopInventory.ItemSlots[j].item);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Updates player inventory, mouse inventory and shop inventory.
        /// </summary>
        /// <param name="gameTime">GameTime for stuff.</param>
        /// <param name="playerInventory">Players Inventory.</param>
        /// <param name="mouseInventory">Mouse Inventory.</param>
        /// <param name="shopInventory">Shop Inventory.</param>
        public static void UpdateShopInventories(GameTime gameTime, Inventory playerInventory, MouseInventory mouseInventory, Inventory shopInventory)
        {
            shopInventory.UpdateInventoryBounds(gameTime);//Update bounds of each Item Slot in shops inventory.
            //cycle through Shops Inventory's ItemSlots.
            for (int i = 0; i < 40; i++)
            {
                //if shops inventory item slot's item is not null and mouse bounds intersects item in item slot and left mouse pressed.
                if (shopInventory.ItemSlots[i].item != null && MouseManager.mouseBounds.Intersects(shopInventory.ItemSlots[i].bounds) && InputManager.IsLMPressed())
                {
                    //if mouse inventory item is null and players money is greater than or equal to cost of item in shops inventory.
                    if (mouseInventory.heldItem == null && GVar.silverMoney >= shopInventory.ItemSlots[i].item.Cost)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        mouseInventory.heldItem = shopInventory.ItemSlots[i].item;//set mouse held item to clicked item in shops inventory.
                        GVar.silverMoney -= shopInventory.ItemSlots[i].item.Cost;//take cost of item from players money.
                        Item.FromShop(shopInventory.ItemSlots[i].item, i);//delete the item from shops inventory.
                    }
                    //if mouse held item is not null and players money + half the cost of the held item is greater than or equal to cost of item in shops inventory.
                    else if (mouseInventory.heldItem != null && GVar.silverMoney + (mouseInventory.heldItem.Cost / 2) >= shopInventory.ItemSlots[i].item.Cost)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        Item item = mouseInventory.heldItem;//create temporary item from mouse held item.
                        mouseInventory.heldItem = shopInventory.ItemSlots[i].item;//set held item to clicked item in shops inventory,
                        GVar.silverMoney += mouseInventory.heldItem.Cost / 2;//add half of cost of held item to players money.
                        GVar.silverMoney -= shopInventory.ItemSlots[i].item.Cost;//take cost of clicked item in shops inventory from players money.
                        Item.ToShop(item, i);//add item to shops inventory.
                    }
                }
                //if shops inventory item slot's item is null and mouse bounds intersects item slot bounds and left mouse is pressed.
                else if (shopInventory.ItemSlots[i].item == null && MouseManager.mouseBounds.Intersects(shopInventory.ItemSlots[i].bounds) && InputManager.IsLMPressed())
                {
                    //if mouse held item is not null.
                    if (mouseInventory.heldItem != null)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        Item.ToShop(mouseInventory.heldItem, i);//add held item to shop inventory.
                        GVar.silverMoney += mouseInventory.heldItem.Cost / 2;//add half the cost of the held item to players money.
                        mouseInventory.heldItem = null;//set held item to null;
                    }
                }
            }
            //cycle through list of players items(inventory and equip inventory)
            for (int i = 0; i < Lists.playerItems.Count; i++)
            {
                Lists.playerItems[i].Update(gameTime);//update items sprite.
                Lists.playerItems[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);//update item.
            }
            //cycle through shops items.
            for (int i = 0; i < Lists.shopItems.Count; i++)
            {
                Lists.shopItems[i].Update(gameTime);//update items sprite.
                Lists.shopItems[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);//update item.
            }

            InventoryManager.UpdatePlayerInventory(gameTime);
            InventoryManager.UpdateMouseInventory(gameTime);

            //cycle through InventoryButtons.
            for (int i = 0; i < Lists.inventoryButtons.Count; i++)
            {
                Updates.UpdateInventoryButtons(Lists.inventoryButtons[i], gameTime);//update buttons.
                //if mouse bounds intersects button bounds and left mouse is pressed.
                if (MouseManager.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds) && InputManager.IsLMPressed())
                {
                    //if button name is CloseInventory.
                    if (Lists.inventoryButtons[i].Name == "CloseInventory")
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        Lists.inventoryButtons.Clear();//clear InventoryButtons.
                        SaveShopInventory(GVar.curLocNode, GVar.player.CurrentLocation);//save shops inventory.

                        //cycle through inventory itemslots.
                        for (int j = 0; j < 40; j++)
                        {
                            if (shopInventory.ItemSlots[j].item != null)//if item is not null.
                                shopInventory.ItemSlots[j].item = null;//delete the item.
                        }

                        shopInventory = new Inventory(new Vector2(862, 51));//create new shop inventory(deleting current one)
                        Lists.shopItems.Clear();//clear shops items.
                        GVar.currentGameState = GVar.GameState.game;//set current GameState to game.
                        GVar.previousGameState = GVar.GameState.inventory;
                    }
                }
            }
        }

        /// <summary>
        /// Draws players inventory, mouse inventory and shop inventory.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites.</param>
        /// <param name="gameTime">GameTime for updating sprites.</param>
        /// <param name="playerInventory">players inventory.</param>
        /// <param name="mouseInventory">mouse inventory.</param>
        /// <param name="shopInventory">shops inventory.</param>
        public static void DrawShopInventories(SpriteBatch spriteBatch, GameTime gameTime, Inventory playerInventory, MouseInventory mouseInventory, Inventory shopInventory)
        {
            //cycle through shops inventory itemslots.
            for (int i = 0; i < 40; i++)
            {
                //if mouse bounds intersects shops inventory itemslots bounds and item is not null.
                if (MouseManager.mouseBounds.Intersects(shopInventory.ItemSlots[i].bounds) && shopInventory.ItemSlots[i].item != null)
                {
                    spriteBatch.Draw(Textures.Misc.clearPixel, shopInventory.ItemSlots[i].bounds, null, Color.Gold, 0f, Vector2.Zero, SpriteEffects.None, 0.191f);//draw a mouse over sprite in the item slot.
                    //if mouse held item is null.
                    if (mouseInventory.heldItem == null)
                    {
                        //draw item information of item under mouse.
                        if (shopInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.weapon)
                        {
                            Item.DrawItemInfo(spriteBatch, (Weapon)shopInventory.ItemSlots[i].item);
                        }
                        else if (shopInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.armor)
                        {
                            Item.DrawItemInfo(spriteBatch, (Armor)shopInventory.ItemSlots[i].item);
                        }
                        else if (shopInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.jewellry)
                        {
                            Item.DrawItemInfo(spriteBatch, (Jewellry)shopInventory.ItemSlots[i].item);
                        }
                        else if (shopInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.eternalcoin)
                        {
                            Item.DrawItemInfo(spriteBatch, (EternalCoin)shopInventory.ItemSlots[i].item);
                        }
                    }
                }
            }

            //cycle through and draw players items.
            for (int i = 0; i < Lists.playerItems.Count; i++)
            {
                Lists.playerItems[i].Draw(spriteBatch, Lists.playerItems[i].SpriteID, Lists.playerItems[i].Bounds, 0.19f, 0f, Vector2.Zero);

                //if item is of class jewellry, cast to jewellry and draw eternal coin slot bounds(will be removed when proper art is done)
                if (Lists.playerItems[i].ItemClass == GVar.ItemClassName.jewellry)
                {
                    Jewellry ring = (Jewellry)Lists.playerItems[i];
                    GVar.DrawBoundingBox(ring.eternalCoinSlot.bounds, spriteBatch, Textures.Misc.pixel, 1, 0.2f, Color.Green);
                }
            }

            //cycle through and draw shops items.
            for (int i = 0; i < Lists.shopItems.Count; i++)
            {
                Lists.shopItems[i].Draw(spriteBatch, Lists.shopItems[i].SpriteID, Lists.shopItems[i].Bounds, 0.19f, 0f, Vector2.Zero);
            }
            
            InventoryManager.DrawPlayerInventory(spriteBatch);
            InventoryManager.DrawMouseInventory(spriteBatch, gameTime);

            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, GVar.silverMoney.ToString(), new Vector2(50, 670), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

            //cycle through and update and draw InventoryButtons.
            for (int i = 0; i < Lists.inventoryButtons.Count; i++)
            {
                Lists.inventoryButtons[i].Update(gameTime);
                Lists.inventoryButtons[i].Draw(spriteBatch, Lists.inventoryButtons[i].SpriteID, Lists.inventoryButtons[i].Bounds, 0.19f, 0f, Vector2.Zero);
                if (MouseManager.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds))
                {
                    GVar.DrawBoundingBox(Lists.inventoryButtons[i].Bounds, spriteBatch, Textures.Misc.pixel, 1, 0.19f, Color.Green);
                }
            }
        }
        
        /// <summary>
        /// saves shops inventory to its locations xml file.
        /// </summary>
        /// <param name="shopDoc"></param>
        /// <param name="locationNode"></param>
        public static void SaveShopInventory(XmlDocument shopDoc, LocationNode locationNode)
        {
            XmlNode item = shopDoc.SelectSingleNode("/location/shop/inventory");

            item.RemoveAll();
            
            //cycle through shops inventory item slots, if item slots item is not null, create item xml element and append it to item xml node.
            for (int i = 0; i < 40; i++)
            {
                if (InventoryManager.shopInventory.ItemSlots[i].item != null)
                {
                    item.AppendChild(SaveXml.CreateItemXmlElement(InventoryManager.shopInventory.ItemSlots[i].item, shopDoc));
                }
            }
            string fileDir = GVar.gameFilesLocation + GVar.player.Name + "/" + locationNode.LocatoinFilePath;
            shopDoc.Save(fileDir);
        }
    }
}

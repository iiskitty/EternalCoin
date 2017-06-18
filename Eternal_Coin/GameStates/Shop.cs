using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace Eternal_Coin
{
    public class Shop
    {
        public static void LoadShopInventory(XmlDocument shopDoc)
        {
            XmlNodeList itemList = shopDoc.SelectNodes("/location/shop/inventory/item");

            for (int i = 0; i < itemList.Count; i++)
            {
                Item shopItem = ItemBuilder.BuildItem(Dictionaries.items[itemList[i][GVar.XmlTags.ItemTags.itemname].InnerText]);

                AddItemToShopInventory(shopItem);
            }
        }

        public static void AddItemToShopInventory(Item item)
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

        public static void UpdateShopInventories(GameTime gameTime, Inventory playerInventory, MouseInventory mouseInventory, Inventory shopInventory)
        {
            shopInventory.UpdateInventoryBounds(gameTime);
            for (int i = 0; i < 40; i++)
            {
                if (shopInventory.ItemSlots[i].item != null && MouseManager.mouseBounds.Intersects(shopInventory.ItemSlots[i].bounds) && InputManager.IsLMPressed())
                {
                    if (mouseInventory.heldItem == null && GVar.silverMoney >= shopInventory.ItemSlots[i].item.Cost)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        mouseInventory.heldItem = shopInventory.ItemSlots[i].item;
                        GVar.silverMoney -= shopInventory.ItemSlots[i].item.Cost;
                        Item.FromShop(shopInventory.ItemSlots[i].item, i);
                    }
                    else if (mouseInventory.heldItem != null && GVar.silverMoney + (mouseInventory.heldItem.Cost / 2) >= shopInventory.ItemSlots[i].item.Cost)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        Item item = mouseInventory.heldItem;
                        mouseInventory.heldItem = shopInventory.ItemSlots[i].item;
                        GVar.silverMoney += mouseInventory.heldItem.Cost / 2;
                        GVar.silverMoney -= shopInventory.ItemSlots[i].item.Cost;
                        Item.ToShop(item, i);
                    }
                }
                else if (shopInventory.ItemSlots[i].item == null && MouseManager.mouseBounds.Intersects(shopInventory.ItemSlots[i].bounds) && InputManager.IsLMPressed())
                {
                    if (mouseInventory.heldItem != null)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        Item.ToShop(mouseInventory.heldItem, i);
                        GVar.silverMoney += mouseInventory.heldItem.Cost / 2;
                        mouseInventory.heldItem = null;
                    }
                }
            }

            for (int i = 0; i < Lists.playerItems.Count; i++)
            {
                Lists.playerItems[i].Update(gameTime);
                Lists.playerItems[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            for (int i = 0; i < Lists.shopItems.Count; i++)
            {
                Lists.shopItems[i].Update(gameTime);
                Lists.shopItems[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            InventoryManager.UpdatePlayerInventory(gameTime);
            InventoryManager.UpdateMouseInventory(gameTime);

            for (int i = 0; i < Lists.inventoryButtons.Count; i++)
            {
                Updates.UpdateInventoryButtons(Lists.inventoryButtons[i], gameTime);
                if (MouseManager.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds) && InputManager.IsLMPressed())
                {
                    if (Lists.inventoryButtons[i].Name == "CloseInventory")
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        Lists.inventoryButtons.Clear();
                        SaveShopInventory(GVar.curLocNode, GVar.player.CurrentLocation);

                        for (int j = 0; j < 40; j++)
                        {
                            if (shopInventory.ItemSlots[j].item != null)
                                shopInventory.ItemSlots[j].item = null;
                        }

                        shopInventory = new Inventory(new Vector2(862, 51));
                        GVar.currentGameState = GVar.GameState.game;
                        GVar.previousGameState = GVar.GameState.inventory;
                    }
                }
            }
        }

        public static void DrawShopInventories(SpriteBatch spriteBatch, GameTime gameTime, Inventory playerInventory, MouseInventory mouseInventory, Inventory shopInventory)
        {
            for (int i = 0; i < 40; i++)
            {
                if (MouseManager.mouseBounds.Intersects(shopInventory.ItemSlots[i].bounds) && shopInventory.ItemSlots[i].item != null)
                {
                    spriteBatch.Draw(Textures.Misc.clearPixel, shopInventory.ItemSlots[i].bounds, null, Color.Gold, 0f, Vector2.Zero, SpriteEffects.None, 0.191f);
                    if (mouseInventory.heldItem == null)
                    {
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

            for (int i = 0; i < Lists.playerItems.Count; i++)
            {
                Lists.playerItems[i].Draw(spriteBatch, Lists.playerItems[i].SpriteID, Lists.playerItems[i].Bounds, 0.19f, 0f, Vector2.Zero);

                if (Lists.playerItems[i].InventorySlot.Contains("Ring"))
                {
                    Jewellry ring = (Jewellry)Lists.playerItems[i];
                    GVar.DrawBoundingBox(ring.eternalCoinSlot.bounds, spriteBatch, Textures.Misc.pixel, 1, 0.2f, Color.Green);
                }
            }
            for (int i = 0; i < Lists.shopItems.Count; i++)
            {
                Lists.shopItems[i].Draw(spriteBatch, Lists.shopItems[i].SpriteID, Lists.shopItems[i].Bounds, 0.19f, 0f, Vector2.Zero);
            }
            
            InventoryManager.DrawPlayerInventory(spriteBatch);

            InventoryManager.DrawMouseInventory(spriteBatch, gameTime);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, GVar.silverMoney.ToString(), new Vector2(50, 670), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

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

        public static void SaveShopInventory(XmlDocument shopDoc, LocationNode locationNode)
        {
            XmlNode item = shopDoc.SelectSingleNode("/location/shop/inventory");

            item.RemoveAll();
            

            for (int i = 0; i < 40; i++)
            {
                if (InventoryManager.shopInventory.ItemSlots[i].item != null)
                {
                    item.AppendChild(SaveXml.CreateItemXmlElement(InventoryManager.shopInventory.ItemSlots[i].item, shopDoc));
                }
            }
            string fileDir = "Content/GameFiles/" + GVar.player.Name + "/" + locationNode.LocatoinFilePath;
            shopDoc.Save(fileDir);
        }
    }
}

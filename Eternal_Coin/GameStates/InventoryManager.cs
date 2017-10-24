using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eternal_Coin
{
    public class InventoryManager
    {
        public static MouseInventory mouseInventory;
        public static Inventory playerInventory;
        public static Inventory shopInventory;
        public static EquipInventory characterInventory;
        public static EquipInventory enemyInventory;

        public static void CreateInventories()
        {
            mouseInventory = new MouseInventory();
            playerInventory = new Inventory(new Vector2(437, 51), GVar.InventoryParentNames.inventory);
            enemyInventory = new EquipInventory(GVar.InventoryParentNames.enemy);
            shopInventory = new Inventory(new Vector2(862, 51), GVar.InventoryParentNames.shop);
            characterInventory = new EquipInventory(GVar.InventoryParentNames.character);
        }

        public static void ClearInventories()
        {
            for (int i = 0; i < 40; i++)
            {
                if (playerInventory.ItemSlots[i].item != null)
                {
                    Lists.playerItems.Remove(playerInventory.ItemSlots[i].item);
                    playerInventory.ItemSlots[i].item = null;
                }
                if (shopInventory.ItemSlots[i].item != null)
                {
                    Lists.shopItems.Remove(shopInventory.ItemSlots[i].item);
                    shopInventory.ItemSlots[i].item = null;
                }
            }

            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                if (characterInventory.ItemSlots[Lists.inventorySlots[i]].item != null)
                {
                    GVar.player.TakeItemStats(characterInventory.ItemSlots[Lists.inventorySlots[i]].item);
                    Lists.characterItems.Remove(characterInventory.ItemSlots[Lists.inventorySlots[i]].item);
                    characterInventory.ItemSlots[Lists.inventorySlots[i]].item = null;
                }
            }
        }

        public static void DrawMiniInventory(SpriteBatch spriteBatch, EquipInventory charInv)
        {
            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                if (charInv.ItemSlots[Lists.inventorySlots[i]].item != null)
                    spriteBatch.Draw(charInv.ItemSlots[Lists.inventorySlots[i]].item.SpriteID, new Rectangle((int)charInv.ItemSlots[Lists.inventorySlots[i]].miniPosition.X, (int)charInv.ItemSlots[Lists.inventorySlots[i]].miniPosition.Y, (int)charInv.ItemSlots[Lists.inventorySlots[i]].miniSize.X, (int)charInv.ItemSlots[Lists.inventorySlots[i]].miniSize.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
            }
        }
        public static void DrawEnemyMiniInventory(SpriteBatch spriteBatch, EquipInventory enemyInv)
        {
            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                if (enemyInv.ItemSlots[Lists.inventorySlots[i]].item != null)
                    spriteBatch.Draw(enemyInv.ItemSlots[Lists.inventorySlots[i]].item.SpriteID, new Rectangle((int)enemyInv.ItemSlots[Lists.inventorySlots[i]].miniPosition.X + 851, (int)enemyInv.ItemSlots[Lists.inventorySlots[i]].miniPosition.Y, (int)enemyInv.ItemSlots[Lists.inventorySlots[i]].miniSize.X, (int)enemyInv.ItemSlots[Lists.inventorySlots[i]].miniSize.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
            }
        }

        public static void DrawPlayerInventory(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Lists.playerItems.Count; i++)
            {
                DrawInventoryItem(spriteBatch, Lists.playerItems[i]);
                if (MouseManager.mouse.mouseBounds.Intersects(Lists.playerItems[i].Bounds))
                {
                    spriteBatch.Draw(Textures.Misc.clearPixel, Lists.playerItems[i].Bounds, null, Color.Gold, 0f, Vector2.Zero, SpriteEffects.None, 0.191f);
                    if (mouseInventory.heldItem == null)
                    {
                        if (Lists.playerItems[i].ItemClass == GVar.ItemClassName.weapon)
                        {
                            Item.DrawItemInfo(spriteBatch, (Weapon)Lists.playerItems[i]);
                        }
                        else if (Lists.playerItems[i].ItemClass == GVar.ItemClassName.armor)
                        {
                            Item.DrawItemInfo(spriteBatch, (Armor)Lists.playerItems[i]);
                        }
                        else if (Lists.playerItems[i].ItemClass == GVar.ItemClassName.jewellry)
                        {
                            Item.DrawItemInfo(spriteBatch, (Jewellry)Lists.playerItems[i]);
                        }
                        else if (Lists.playerItems[i].ItemClass == GVar.ItemClassName.eternalcoin)
                        {
                            Item.DrawItemInfo(spriteBatch, (EternalCoin)Lists.playerItems[i]);
                        }
                    }
                }
            }
        }

        public static void DrawCharacterInventory(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Lists.characterItems.Count; i++)
            {
                DrawInventoryItem(spriteBatch, Lists.characterItems[i]);
                if (MouseManager.mouse.mouseBounds.Intersects(Lists.characterItems[i].Bounds))
                {
                    spriteBatch.Draw(Textures.Misc.clearPixel, Lists.characterItems[i].Bounds, null, Color.Gold, 0f, Vector2.Zero, SpriteEffects.None, 0.191f);

                    if (mouseInventory.heldItem == null)
                    {
                        if (Lists.characterItems[i].ItemClass == GVar.ItemClassName.weapon)
                        {
                            Item.DrawItemInfo(spriteBatch, (Weapon)Lists.characterItems[i]);
                        }
                        else if (Lists.characterItems[i].ItemClass == GVar.ItemClassName.armor)
                        {
                            Item.DrawItemInfo(spriteBatch, (Armor)Lists.characterItems[i]);
                        }
                        else if (Lists.characterItems[i].ItemClass == GVar.ItemClassName.jewellry)
                        {
                            Item.DrawItemInfo(spriteBatch, (Jewellry)Lists.characterItems[i]);
                        }

                    }
                }
            }
        }

        public static void OnMouseClicked(ItemSlot itemSlot)
        {
            if (GVar.currentGameState == GVar.GameState.inventory || GVar.currentGameState == GVar.GameState.shop)
            {
                if (itemSlot.item == null && mouseInventory.heldItem != null && MouseManager.mouse.mouseBounds.Intersects(itemSlot.bounds))
                {
                    if (Item.ToInventory(itemSlot, mouseInventory.heldItem))
                        mouseInventory.heldItem = null;
                }
                else if (itemSlot.item != null && mouseInventory.heldItem != null && MouseManager.mouse.mouseBounds.Intersects(itemSlot.bounds))
                {
                    Item tItem = mouseInventory.heldItem;
                    mouseInventory.heldItem = itemSlot.item;
                    Item.FromInventory(itemSlot, itemSlot.item);
                    Item.ToInventory(itemSlot, tItem);
                }
                else if (itemSlot.item != null && mouseInventory.heldItem == null && MouseManager.mouse.mouseBounds.Intersects(itemSlot.item.Bounds))
                {
                    mouseInventory.heldItem = itemSlot.item;
                    Item.FromInventory(itemSlot, itemSlot.item);
                }
            }
        }

        public static void UpdateCharacterInventory(GameTime gameTime)
        {
            for (int i = 0; i < Lists.characterItems.Count; i++)
            {
                Lists.characterItems[i].Update(gameTime);
                Lists.characterItems[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (Lists.characterItems[i].ItemClass == GVar.ItemClassName.jewellry)
                {
                    Jewellry jewl = (Jewellry)Lists.characterItems[i];
                    jewl.eternalCoinSlot.Update(gameTime);
                }
            }

            characterInventory.UpdateInventoryBounds(gameTime);
            
            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                if (MouseManager.mouse.mouseBounds.Intersects(characterInventory.ItemSlots[Lists.inventorySlots[i]].bounds))
                {
                    GVar.mouseHoveredItem = characterInventory.ItemSlots[Lists.inventorySlots[i]];
                }
            }
        }

        public static void UpdatePlayerInventory(GameTime gameTime)
        {
            for (int i = 0; i < Lists.playerItems.Count; i++)
            {
                Lists.playerItems[i].Update(gameTime);
                Lists.playerItems[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (Lists.playerItems[i].ItemClass == GVar.ItemClassName.jewellry)
                {
                    Jewellry jewl = (Jewellry)Lists.playerItems[i];
                    jewl.eternalCoinSlot.Update(gameTime);
                }
            }

            playerInventory.UpdateInventoryBounds(gameTime);

            for (int i = 0; i < 40; i++)
            {
                if (MouseManager.mouse.mouseBounds.Intersects(playerInventory.ItemSlots[i].bounds))
                {
                    GVar.mouseHoveredItem = playerInventory.ItemSlots[i];
                }
            }
        }

        public static void DrawMouseInventory(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (mouseInventory.heldItem != null)
            {
                mouseInventory.heldItem.Draw(spriteBatch, mouseInventory.heldItem.SpriteID, mouseInventory.heldItem.Bounds, 0.192f, 0f, Vector2.Zero);
                mouseInventory.heldItem.Update(gameTime);

                if (mouseInventory.heldItem.ItemClass == GVar.ItemClassName.jewellry)
                {
                    Jewellry jewl = (Jewellry)mouseInventory.heldItem;
                    if (jewl.eternalCoinSlot.item != null)
                    {
                        jewl.eternalCoinSlot.item.Draw(spriteBatch, jewl.eternalCoinSlot.item.SpriteID, jewl.eternalCoinSlot.item.Bounds, 0.2f, 0f, Vector2.Zero);
                    }
                    GVar.DrawBoundingBox(jewl.eternalCoinSlot.bounds, spriteBatch, Textures.Misc.pixel, 1, 0.2f, Color.Green);
                }
                
                if (GVar.currentGameState == GVar.GameState.inventory) //TODO This function draws a box on the inventory slot that the held item can go into. Try make this a much shorter function.
                {
                    if (mouseInventory.heldItem != null && mouseInventory.heldItem.ItemClass != GVar.ItemClassName.jewellry && mouseInventory.heldItem.ItemClass != GVar.ItemClassName.weapon && mouseInventory.heldItem.ItemClass != GVar.ItemClassName.eternalcoin)
                    {
                        Vector2 pos = characterInventory.ItemSlots[mouseInventory.heldItem.InventorySlot].position;
                        Vector2 size = characterInventory.ItemSlots[mouseInventory.heldItem.InventorySlot].size;
                        spriteBatch.Draw(Textures.Misc.clearPixel, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), null, Color.Green, 0f, Vector2.Zero, SpriteEffects.None, 0.192f);
                    }
                    else if (mouseInventory.heldItem != null && mouseInventory.heldItem.ItemClass == GVar.ItemClassName.jewellry)
                    {
                        for (int k = 0; k < Lists.inventorySlots.Count; k++)
                        {
                            if (characterInventory.ItemSlots[Lists.inventorySlots[k]].inventorySlot.Contains(mouseInventory.heldItem.InventorySlot))
                            {
                                Vector2 pos = characterInventory.ItemSlots[Lists.inventorySlots[k]].position;
                                Vector2 size = characterInventory.ItemSlots[Lists.inventorySlots[k]].size;
                                spriteBatch.Draw(Textures.Misc.clearPixel, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), null, Color.Green, 0f, Vector2.Zero, SpriteEffects.None, 0.192f);
                            }
                        }
                    }
                    else if (mouseInventory.heldItem != null && mouseInventory.heldItem.ItemClass == GVar.ItemClassName.weapon)
                    {
                        for (int k = 0; k < Lists.inventorySlots.Count; k++)
                        {
                            if (characterInventory.ItemSlots[Lists.inventorySlots[k]].inventorySlot.Contains(mouseInventory.heldItem.InventorySlot))
                            {
                                Vector2 pos = characterInventory.ItemSlots[Lists.inventorySlots[k]].position;
                                Vector2 size = characterInventory.ItemSlots[Lists.inventorySlots[k]].size;
                                spriteBatch.Draw(Textures.Misc.clearPixel, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), null, Color.Green, 0f, Vector2.Zero, SpriteEffects.None, 0.192f);
                            }
                        }
                    }
                    else if (mouseInventory.heldItem != null && mouseInventory.heldItem.ItemClass == GVar.ItemClassName.eternalcoin)
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            if (playerInventory.ItemSlots[i].item != null && playerInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.jewellry)
                            {
                                Jewellry jewl = (Jewellry)playerInventory.ItemSlots[i].item;
                                Vector2 pos = jewl.eternalCoinSlot.position;
                                Vector2 size = jewl.eternalCoinSlot.size;
                                spriteBatch.Draw(Textures.Misc.clearPixel, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), null, Color.Green, 0f, Vector2.Zero, SpriteEffects.None, 0.194f);
                            }
                        }
                    }
                }

                if (mouseInventory.heldItem.ItemClass == GVar.ItemClassName.weapon)
                {
                    Item.DrawItemInfo(spriteBatch, (Weapon)mouseInventory.heldItem);
                }
                else if (mouseInventory.heldItem.ItemClass == GVar.ItemClassName.armor)
                {
                    Item.DrawItemInfo(spriteBatch, (Armor)mouseInventory.heldItem);
                }
                else if (mouseInventory.heldItem.ItemClass == GVar.ItemClassName.jewellry)
                {
                    Item.DrawItemInfo(spriteBatch, (Jewellry)mouseInventory.heldItem);
                }
                else if (mouseInventory.heldItem.ItemClass == GVar.ItemClassName.eternalcoin)
                {
                    Item.DrawItemInfo(spriteBatch, (EternalCoin)mouseInventory.heldItem);
                }
            }
        }

        public static void UpdateMouseInventory(GameTime gameTime)
        {
            if (mouseInventory.heldItem != null)
            {
                mouseInventory.heldItem.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (mouseInventory.heldItem.ItemClass == GVar.ItemClassName.jewellry)
                {
                    Jewellry jewl = (Jewellry)mouseInventory.heldItem;
                    if (jewl.eternalCoinSlot.item != null)
                    {
                        jewl.eternalCoinSlot.Update(gameTime);
                    }
                }
                if (mouseInventory.heldItem.Size.X < Vector.itemNormalSize.X && mouseInventory.heldItem.Size.Y < Vector.itemNormalSize.Y)
                {
                    mouseInventory.heldItem.Size = Vector.itemNormalSize;
                }
                mouseInventory.heldItem.Position = new Vector2(MouseManager.mouse.GetMousePosition().X - mouseInventory.heldItem.Size.X, MouseManager.mouse.GetMousePosition().Y - mouseInventory.heldItem.Size.Y);
            }
        }

        public static void DrawInventoryItem(SpriteBatch spriteBatch, Item item)
        {
            item.Draw(spriteBatch, item.SpriteID, item.Bounds, 0.19f, 0f, Vector2.Zero);

            if (item.ItemClass == GVar.ItemClassName.jewellry)
            {
                Jewellry jewl = (Jewellry)item;
                if (jewl.eternalCoinSlot.item != null)
                {
                    jewl.eternalCoinSlot.item.Draw(spriteBatch, jewl.eternalCoinSlot.item.SpriteID, jewl.eternalCoinSlot.item.Bounds, 0.2f, 0f, Vector2.Zero);
                }
                GVar.DrawBoundingBox(jewl.eternalCoinSlot.bounds, spriteBatch, Textures.Misc.pixel, 1, 0.2f, Color.Green);
            }
        }

        public static void DrawPlayerInventories(SpriteBatch spriteBatch, GameTime gameTime, Inventory playerInventory, MouseInventory mouseInventory, EquipInventory characterInventory)
        {
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Damage: " + GVar.player.Damage.ToString(), new Vector2(879, 572), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Armor: " + GVar.player.Armour.ToString(), new Vector2(879, 590), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

            for (int i = 0; i < Lists.inventoryButtons.Count; i++)
            {
                Lists.inventoryButtons[i].Update(gameTime);
                Lists.inventoryButtons[i].Draw(spriteBatch, Lists.inventoryButtons[i].SpriteID, Lists.inventoryButtons[i].Bounds, 0.19f, 0f, Vector2.Zero);
                if (MouseManager.mouse.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds))
                {
                    GVar.DrawBoundingBox(Lists.inventoryButtons[i].Bounds, spriteBatch, Textures.Misc.pixel, 1, 0.19f, Color.Green);
                }
            }

            DrawMouseInventory(spriteBatch, gameTime);
            DrawPlayerInventory(spriteBatch);
            DrawCharacterInventory(spriteBatch);
            
        }

        public static void ManagePlayerInventories(GameTime gameTime, Inventory playerInventory, MouseInventory mouseInventory, EquipInventory characterInventory)
        {
            for (int i = 0; i < Lists.inventoryButtons.Count; i++)
            {
                Updates.UpdateInventoryButtons(Lists.inventoryButtons[i], gameTime);
                //if (MouseManager.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds) && InputManager.IsLMPressed())
                //{
                //    if (Lists.inventoryButtons[i].Name == "CloseInventory")
                //    {
                //        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                //        Lists.inventoryButtons.Clear();
                //        GVar.currentGameState = GVar.GameState.game;
                //        GVar.previousGameState = GVar.GameState.inventory;
                //    }
                //}
            }
            UpdatePlayerInventory(gameTime);
            UpdateCharacterInventory(gameTime);
            UpdateMouseInventory(gameTime);
            
            if (InputManager.IsKeyPressed(Keys.I))
            {
                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                GVar.currentGameState = GVar.GameState.game;
                GVar.previousGameState = GVar.GameState.inventory;
            }
        }
    }
}

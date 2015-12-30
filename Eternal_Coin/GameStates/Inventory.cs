﻿using Microsoft.Xna.Framework;
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
    public class InventoryManager
    {
        public static MouseInventory mouseInventory;
        public static PlayerInventory playerInventory;
        public static ShopInventory shopInventory;
        public static CharacterInventory characterInventory;
        public static EnemyInventory enemyInventory;

        public static void CreateInventories()
        {
            mouseInventory = new MouseInventory();
            playerInventory = new PlayerInventory();
            enemyInventory = new EnemyInventory();
            shopInventory = new ShopInventory();
            characterInventory = new CharacterInventory();
        }

        public static void ClearInventories()
        {
            for (int i = 0; i < 40; i++)
            {
                if (playerInventory.itemSlots[i].item != null)
                    playerInventory.itemSlots[i].item = null;
                if (shopInventory.itemSlots[i].item != null)
                    shopInventory.itemSlots[i].item = null;
            }

            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                if (characterInventory.itemSlots[Lists.inventorySlots[i]].item != null)
                {
                    foreach (Player P in Lists.entity)
                    {
                        P.TakeItemStats(characterInventory.itemSlots[Lists.inventorySlots[i]].item);
                    }
                    characterInventory.itemSlots[Lists.inventorySlots[i]].item = null;
                }
            }
        }

        public static void DrawMiniInventory(SpriteBatch spriteBatch, CharacterInventory charInv)
        {
            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                if (charInv.itemSlots[Lists.inventorySlots[i]].item != null)
                    spriteBatch.Draw(charInv.itemSlots[Lists.inventorySlots[i]].item.SpriteID, new Rectangle((int)charInv.itemSlots[Lists.inventorySlots[i]].miniPosition.X, (int)charInv.itemSlots[Lists.inventorySlots[i]].miniPosition.Y, (int)charInv.itemSlots[Lists.inventorySlots[i]].miniSize.X, (int)charInv.itemSlots[Lists.inventorySlots[i]].miniSize.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
            }
        }
        public static void DrawMiniInventory(SpriteBatch spriteBatch, EnemyInventory enemyInv)
        {
            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                if (enemyInv.itemSlots[Lists.inventorySlots[i]].item != null)
                    spriteBatch.Draw(enemyInv.itemSlots[Lists.inventorySlots[i]].item.SpriteID, new Rectangle((int)enemyInv.itemSlots[Lists.inventorySlots[i]].miniPosition.X + 851, (int)enemyInv.itemSlots[Lists.inventorySlots[i]].miniPosition.Y, (int)enemyInv.itemSlots[Lists.inventorySlots[i]].miniSize.X, (int)enemyInv.itemSlots[Lists.inventorySlots[i]].miniSize.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
            }
        }

        public static void DrawPlayerInventory(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 40; i++)
            {
                if (playerInventory.itemSlots[i].item != null)
                    playerInventory.itemSlots[i].item.Draw(spriteBatch, playerInventory.itemSlots[i].item.SpriteID, playerInventory.itemSlots[i].item.Bounds, 0.19f, 0f, Vector2.Zero);

                if (MouseManager.mouseBounds.Intersects(playerInventory.itemSlots[i].bounds) && playerInventory.itemSlots[i].item != null)
                {
                    spriteBatch.Draw(Textures.clearPixel, playerInventory.itemSlots[i].bounds, null, Color.Gold, 0f, Vector2.Zero, SpriteEffects.None, 0.191f);
                    spriteBatch.Draw(playerInventory.itemSlots[i].item.SpriteID, new Rectangle(114, 92, 200, 200), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
                    if (mouseInventory.heldItem.Count == 0)
                    {
                        if (playerInventory.itemSlots[i].item.ItemClass == GVar.ItemClassName.weapon)
                        {
                            Item.DrawItemInfo(spriteBatch, (Weapon)playerInventory.itemSlots[i].item);
                        }
                        else if (playerInventory.itemSlots[i].item.ItemClass == GVar.ItemClassName.armor)
                        {
                            Item.DrawItemInfo(spriteBatch, (Armor)playerInventory.itemSlots[i].item);
                        }
                        else if (playerInventory.itemSlots[i].item.ItemClass == GVar.ItemClassName.jewellry)
                        {
                            Item.DrawItemInfo(spriteBatch, (Jewellry)playerInventory.itemSlots[i].item);
                        }
                    }
                }
            }
        }

        public static void UpdateShopInventory(GameTime gameTime)
        {
            shopInventory.UpdateInventoryBounds(gameTime);
            for (int i = 0; i < 40; i++)
            {
                if (shopInventory.itemSlots[i].item != null && MouseManager.mouseBounds.Intersects(shopInventory.itemSlots[i].bounds) && InputManager.IsLMPressed())
                {
                    if (mouseInventory.heldItem.Count == 0 && GVar.silverMoney >= shopInventory.itemSlots[i].item.Cost)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        mouseInventory.heldItem.Add(shopInventory.itemSlots[i].item);
                        GVar.silverMoney -= shopInventory.itemSlots[i].item.Cost;
                        shopInventory.itemSlots[i].item = null;
                    }
                    else if (mouseInventory.heldItem.Count == 1 && GVar.silverMoney + (mouseInventory.heldItem[0].Cost / 2) >= shopInventory.itemSlots[i].item.Cost)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        Item item = mouseInventory.heldItem[0];
                        mouseInventory.heldItem[0] = shopInventory.itemSlots[i].item;
                        GVar.silverMoney += mouseInventory.heldItem[0].Cost / 2;
                        GVar.silverMoney -= shopInventory.itemSlots[i].item.Cost;
                        shopInventory.itemSlots[i].item = item;
                    }
                }
                else if (shopInventory.itemSlots[i].item == null && MouseManager.mouseBounds.Intersects(shopInventory.itemSlots[i].bounds) && InputManager.IsLMPressed())
                {
                    if (mouseInventory.heldItem.Count == 1)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        shopInventory.itemSlots[i].item = mouseInventory.heldItem[0];
                        GVar.silverMoney += mouseInventory.heldItem[0].Cost / 2;
                        mouseInventory.heldItem.RemoveAt(0);
                    }
                }
            }
        }

        public static void UpdatePlayerInventory(GameTime gameTime)
        {
            playerInventory.UpdateInventoryBounds(gameTime);
            for (int i = 0; i < 40; i++)
            {
                if (playerInventory.itemSlots[i].item != null && MouseManager.mouseBounds.Intersects(playerInventory.itemSlots[i].bounds) && InputManager.IsLMPressed())
                {
                    if (mouseInventory.heldItem.Count == 0)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        mouseInventory.heldItem.Add(playerInventory.itemSlots[i].item);
                        playerInventory.itemSlots[i].item = null;
                    }
                    else if (mouseInventory.heldItem.Count == 1)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        Item item = mouseInventory.heldItem[0];
                        mouseInventory.heldItem[0] = playerInventory.itemSlots[i].item;
                        playerInventory.itemSlots[i].item = item;
                    }
                }
                else if (playerInventory.itemSlots[i].item == null && MouseManager.mouseBounds.Intersects(playerInventory.itemSlots[i].bounds) && InputManager.IsLMPressed())
                {
                    if (mouseInventory.heldItem.Count == 1)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        playerInventory.itemSlots[i].item = mouseInventory.heldItem[0];
                        mouseInventory.heldItem.RemoveAt(0);
                    }
                }
            }
        }

        public static void DrawShopInventory(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 40; i++)
            {
                if (shopInventory.itemSlots[i].item != null)
                    shopInventory.itemSlots[i].item.Draw(spriteBatch, shopInventory.itemSlots[i].item.SpriteID, shopInventory.itemSlots[i].item.Bounds, 0.19f, 0f, Vector2.Zero);

                if (MouseManager.mouseBounds.Intersects(shopInventory.itemSlots[i].bounds) && shopInventory.itemSlots[i].item != null)
                {
                    spriteBatch.Draw(Textures.clearPixel, shopInventory.itemSlots[i].bounds, null, Color.Gold, 0f, Vector2.Zero, SpriteEffects.None, 0.191f);
                    spriteBatch.Draw(shopInventory.itemSlots[i].item.SpriteID, new Rectangle(114, 92, 200, 200), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
                    if (mouseInventory.heldItem.Count == 0)
                    {
                        if (shopInventory.itemSlots[i].item.ItemClass == GVar.ItemClassName.weapon)
                        {
                            Item.DrawItemInfo(spriteBatch, (Weapon)shopInventory.itemSlots[i].item);
                        }
                        else if (shopInventory.itemSlots[i].item.ItemClass == GVar.ItemClassName.armor)
                        {
                            Item.DrawItemInfo(spriteBatch, (Armor)shopInventory.itemSlots[i].item);
                        }
                    }
                }
            }
        }

        public static void DrawMouseInventory(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < mouseInventory.heldItem.Count; i++)
            {
                mouseInventory.heldItem[i].Update(gameTime);
                mouseInventory.heldItem[i].Draw(spriteBatch, mouseInventory.heldItem[i].SpriteID, mouseInventory.heldItem[i].Bounds, 0.192f, 0f, Vector2.Zero);

                spriteBatch.Draw(mouseInventory.heldItem[i].SpriteID, new Rectangle(114, 92, 200, 200), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.192f);

                if (GVar.currentGameState == GVar.GameState.game)
                {
                    if (mouseInventory.heldItem[i] != null && mouseInventory.heldItem[i].ItemClass != GVar.ItemClassName.jewellry && mouseInventory.heldItem[i].ItemClass != GVar.ItemClassName.weapon)
                    {
                        Vector2 pos = characterInventory.itemSlots[mouseInventory.heldItem[i].InventorySlot].position;
                        Vector2 size = characterInventory.itemSlots[mouseInventory.heldItem[i].InventorySlot].size;
                        spriteBatch.Draw(Textures.clearPixel, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), null, Color.Green, 0f, Vector2.Zero, SpriteEffects.None, 0.192f);
                    }
                    else if (mouseInventory.heldItem[i] != null && mouseInventory.heldItem[i].ItemClass == GVar.ItemClassName.jewellry)
                    {
                        for (int k = 0; k < Lists.inventorySlots.Count; k++)
                        {
                            if (characterInventory.itemSlots[Lists.inventorySlots[k]].inventorySlot.Contains(mouseInventory.heldItem[i].InventorySlot))
                            {
                                Vector2 pos = characterInventory.itemSlots[Lists.inventorySlots[k]].position;
                                Vector2 size = characterInventory.itemSlots[Lists.inventorySlots[k]].size;
                                spriteBatch.Draw(Textures.clearPixel, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), null, Color.Green, 0f, Vector2.Zero, SpriteEffects.None, 0.192f);
                            }
                        }
                    }
                    else if (mouseInventory.heldItem[i] != null && mouseInventory.heldItem[i].ItemClass == GVar.ItemClassName.weapon)
                    {
                        for (int k = 0; k < Lists.inventorySlots.Count; k++)
                        {
                            if (characterInventory.itemSlots[Lists.inventorySlots[k]].inventorySlot.Contains(mouseInventory.heldItem[i].InventorySlot))
                            {
                                Vector2 pos = characterInventory.itemSlots[Lists.inventorySlots[k]].position;
                                Vector2 size = characterInventory.itemSlots[Lists.inventorySlots[k]].size;
                                spriteBatch.Draw(Textures.clearPixel, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), null, Color.Green, 0f, Vector2.Zero, SpriteEffects.None, 0.192f);
                            }
                        }
                    }
                }

                if (mouseInventory.heldItem[i].ItemClass == GVar.ItemClassName.weapon)
                {
                    Item.DrawItemInfo(spriteBatch, (Weapon)mouseInventory.heldItem[i]);

                }
                else if (mouseInventory.heldItem[i].ItemClass == GVar.ItemClassName.armor)
                {
                    Item.DrawItemInfo(spriteBatch, (Armor)mouseInventory.heldItem[i]);

                }
                else if (mouseInventory.heldItem[i].ItemClass == GVar.ItemClassName.jewellry)
                {
                    Item.DrawItemInfo(spriteBatch, (Jewellry)mouseInventory.heldItem[i]);
                }
            }
        }

        public static void UpdateMouseInventory(GameTime gameTime)
        {
            for (int i = 0; i < mouseInventory.heldItem.Count; i++)
            {
                if (mouseInventory.heldItem[i].Size.X < Vector.itemNormalSize.X && mouseInventory.heldItem[i].Size.Y < Vector.itemNormalSize.Y)
                {
                    mouseInventory.heldItem[i].Size = Vector.itemNormalSize;
                }
                mouseInventory.heldItem[i].Position = new Vector2(MouseManager.GetMousePosition().X - mouseInventory.heldItem[i].Size.X, MouseManager.GetMousePosition().Y - mouseInventory.heldItem[i].Size.Y);
                mouseInventory.heldItem[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public static void DrawShopInventories(SpriteBatch spriteBatch, GameTime gameTime, PlayerInventory playerInventory, MouseInventory mouseInventory, ShopInventory shopInventory)
        {
            DrawShopInventory(spriteBatch);
            DrawPlayerInventory(spriteBatch);
            
            DrawMouseInventory(spriteBatch, gameTime);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, GVar.silverMoney.ToString(), new Vector2(50, 670), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

            for (int i = 0; i < Lists.inventoryButtons.Count; i++)
            {
                Lists.inventoryButtons[i].Update(gameTime);
                Lists.inventoryButtons[i].Draw(spriteBatch, Lists.inventoryButtons[i].SpriteID, Lists.inventoryButtons[i].Bounds, 0.19f, 0f, Vector2.Zero);
                if (MouseManager.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds))
                {
                    GVar.DrawBoundingBox(Lists.inventoryButtons[i].Bounds, spriteBatch, Textures.pixel, 1, 0.19f, Color.Green);
                }
            }
            
        }

        public static void ManageShopInventories(GameTime gameTime, PlayerInventory playerInventory, MouseInventory mouseInventory, ShopInventory shopInventory)
        {
            if (InputManager.IsKeyPressed(Keys.M))
                GVar.silverMoney += 100;
            
            UpdatePlayerInventory(gameTime);
            UpdateShopInventory(gameTime);
            UpdateMouseInventory(gameTime);

            for (int i = 0; i < Lists.inventoryButtons.Count; i++)
            {
                Updates.UpdateInventoryButtons(Lists.inventoryButtons[i], gameTime);
                if (MouseManager.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds) && InputManager.IsLMPressed())
                {
                    if (Lists.inventoryButtons[i].Name == "CloseInventory")
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        Lists.inventoryButtons.Clear();
                        Shop.SaveShopInventory(GVar.curLocNode, Lists.entity[0].CurrentLocation[0]);

                        for (int j = 0; j < 40; j++)
                        {
                            if (shopInventory.itemSlots[j].item != null)
                                shopInventory.itemSlots[j].item = null;
                        }

                        GVar.currentGameState = GVar.GameState.game;
                        GVar.previousGameState = GVar.GameState.inventory;
                    }
                }
            }
        }

        public static void DrawPlayerInventories(SpriteBatch spriteBatch, GameTime gameTime, PlayerInventory playerInventory, MouseInventory mouseInventory, CharacterInventory characterInventory)
        {
            characterInventory.Draw(spriteBatch);

            foreach (Player P in Lists.entity)
            {
                spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Damage: " + P.Damage.ToString(), new Vector2(879, 572), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Armor: " + P.Armour.ToString(), new Vector2(879, 590), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            }

            for (int i = 0; i < Lists.inventoryButtons.Count; i++)
            {
                Lists.inventoryButtons[i].Update(gameTime);
                Lists.inventoryButtons[i].Draw(spriteBatch, Lists.inventoryButtons[i].SpriteID, Lists.inventoryButtons[i].Bounds, 0.19f, 0f, Vector2.Zero);
                if (MouseManager.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds))
                {
                    GVar.DrawBoundingBox(Lists.inventoryButtons[i].Bounds, spriteBatch, Textures.pixel, 1, 0.19f, Color.Green);
                }
            }

            DrawMouseInventory(spriteBatch, gameTime);
            DrawPlayerInventory(spriteBatch);

            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                if (characterInventory.itemSlots[Lists.inventorySlots[i]].item != null && MouseManager.mouseBounds.Intersects(characterInventory.itemSlots[Lists.inventorySlots[i]].item.Bounds))
                {
                    spriteBatch.Draw(Textures.clearPixel, characterInventory.itemSlots[Lists.inventorySlots[i]].item.Bounds, null, Color.Gold, 0f, Vector2.Zero, SpriteEffects.None, 0.191f);
                    spriteBatch.Draw(characterInventory.itemSlots[Lists.inventorySlots[i]].item.SpriteID, new Rectangle(114, 92, 200, 200), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);

                    if (mouseInventory.heldItem.Count == 0)
                    {
                        if (characterInventory.itemSlots[Lists.inventorySlots[i]].item.ItemClass == GVar.ItemClassName.weapon)
                        {
                            Item.DrawItemInfo(spriteBatch, (Weapon)characterInventory.itemSlots[Lists.inventorySlots[i]].item);
                        }
                        else if (characterInventory.itemSlots[Lists.inventorySlots[i]].item.ItemClass == GVar.ItemClassName.armor)
                        {
                            Item.DrawItemInfo(spriteBatch, (Armor)characterInventory.itemSlots[Lists.inventorySlots[i]].item);
                        }
                        else if (characterInventory.itemSlots[Lists.inventorySlots[i]].item.ItemClass == GVar.ItemClassName.jewellry)
                        {
                            Item.DrawItemInfo(spriteBatch, (Jewellry)characterInventory.itemSlots[Lists.inventorySlots[i]].item);
                        }
                    }
                }
            }
        }

        public static void ManagePlayerInventories(GameTime gameTime, PlayerInventory playerInventory, MouseInventory mouseInventory, CharacterInventory characterInventory)
        {
            for (int i = 0; i < Lists.inventoryButtons.Count; i++)
            {
                Updates.UpdateInventoryButtons(Lists.inventoryButtons[i], gameTime);
                if (MouseManager.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds) && InputManager.IsLMPressed())
                {
                    if (Lists.inventoryButtons[i].Name == "CloseInventory")
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        Lists.inventoryButtons.Clear();
                        GVar.currentGameState = GVar.GameState.game;
                        GVar.previousGameState = GVar.GameState.inventory;
                    }
                }
            }

            characterInventory.UpdateInventoryBounds(gameTime);
            if (InputManager.IsKeyPressed(Keys.I))
            {
                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                GVar.currentGameState = GVar.GameState.game;
                GVar.previousGameState = GVar.GameState.inventory;
            }

            for (int i = 0; i < mouseInventory.heldItem.Count; i++)
            {
                foreach (Player P in Lists.entity)
                {
                    for (int j = 0; j < Lists.inventorySlots.Count; j++)
                    {
                        if (MouseManager.mouseBounds.Intersects(characterInventory.itemSlots[Lists.inventorySlots[j]].bounds) && InputManager.IsLMPressed() && mouseInventory.heldItem.Count == 1)
                        {
                            if (characterInventory.itemSlots[Lists.inventorySlots[j]].item == null && characterInventory.itemSlots[Lists.inventorySlots[j]].inventorySlot.Contains(mouseInventory.heldItem[i].InventorySlot))
                            {
                                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                                characterInventory.itemSlots[Lists.inventorySlots[j]].item = mouseInventory.heldItem[i];
                                if (mouseInventory.heldItem[i].Attacks != null)
                                {
                                    for (int k = 0; k < mouseInventory.heldItem[i].Attacks.Count; k++)
                                    {
                                        try
                                        {
                                            Attack.AddAvailableAttacks(mouseInventory.heldItem[i].Attacks);
                                        }
                                        catch
                                        {
                                            GVar.LogDebugInfo("!No attacks to add..MouseInv->CharInv..!", 2);
                                        }
                                    }
                                }
                                P.AddItemStats(mouseInventory.heldItem[i]);
                                mouseInventory.heldItem.RemoveAt(i);
                            }
                            else if (characterInventory.itemSlots[Lists.inventorySlots[j]].item != null && characterInventory.itemSlots[Lists.inventorySlots[j]].inventorySlot.Contains(mouseInventory.heldItem[i].InventorySlot))
                            {
                                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                                Item tempItem = mouseInventory.heldItem[i];
                                mouseInventory.heldItem[i] = characterInventory.itemSlots[Lists.inventorySlots[j]].item;
                                if (characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks != null)
                                {
                                    for (int k = 0; k < characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks.Count; k++)
                                    {
                                        try
                                        {
                                            Attack.TakeAvailableAttacks(characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks);
                                        }
                                        catch
                                        {
                                            GVar.LogDebugInfo("!No attacks to remove..MouseInv->CharInv|CharInv->MouseInv..!", 2);
                                        }
                                    }
                                }
                                characterInventory.itemSlots[Lists.inventorySlots[j]].item = tempItem;
                                if (characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks != null)
                                {
                                    for (int k = 0; k < characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks.Count; k++)
                                    {
                                        try
                                        {
                                            Attack.AddAvailableAttacks(characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks);
                                        }
                                        catch
                                        {
                                            GVar.LogDebugInfo("!No attacks to add..MouseInv->CharInv|CharInv->MouseInv..!", 2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            int preventclick = 0;
            foreach (Player P in Lists.entity)
            {
                for (int i = 0; i < 40; i++)
                {
                    for (int j = 0; j < Lists.inventorySlots.Count; j++)
                    {
                        if (mouseInventory.heldItem.Count == 0 && characterInventory.itemSlots[Lists.inventorySlots[j]].item != null && MouseManager.mouseBounds.Intersects(characterInventory.itemSlots[Lists.inventorySlots[j]].item.Bounds) && InputManager.IsLMPressed())
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                            mouseInventory.heldItem.Add(characterInventory.itemSlots[Lists.inventorySlots[j]].item);
                            P.TakeItemStats(characterInventory.itemSlots[Lists.inventorySlots[j]].item);
                            if (characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks != null)
                            {
                                for (int k = 0; k < characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks.Count; k++)
                                {
                                    try
                                    {
                                        Attack.TakeAvailableAttacks(characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks);
                                    }
                                    catch
                                    {
                                        GVar.LogDebugInfo("!No attacks to remove..CharInv->MouseInv..!", 2);
                                    }
                                }
                            }
                            characterInventory.itemSlots[Lists.inventorySlots[j]].item = null;
                        }
                        else if (characterInventory.itemSlots[Lists.inventorySlots[j]].item != null && MouseManager.mouseBounds.Intersects(characterInventory.itemSlots[Lists.inventorySlots[j]].item.Bounds) && InputManager.IsRMPressed() && preventclick == 0)
                        {

                            if (playerInventory.itemSlots[i].item == null)
                            {
                                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                                playerInventory.itemSlots[i].item = characterInventory.itemSlots[Lists.inventorySlots[j]].item;
                                P.TakeItemStats(characterInventory.itemSlots[Lists.inventorySlots[j]].item);
                                if (characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks != null)
                                {
                                    for (int k = 0; k < characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks.Count; k++)
                                    {
                                        try
                                        {
                                            Attack.TakeAvailableAttacks(characterInventory.itemSlots[Lists.inventorySlots[j]].item.Attacks);
                                        }
                                        catch
                                        {
                                            GVar.LogDebugInfo("!No attacks to remove..CharInv->PlayerInv..", 2);
                                        }
                                    }
                                }
                                characterInventory.itemSlots[Lists.inventorySlots[j]].item = null;
                                preventclick = 1;
                                break;
                            }
                        }
                    }
                    if (playerInventory.itemSlots[i].item != null && MouseManager.mouseBounds.Intersects(playerInventory.itemSlots[i].item.Bounds) && InputManager.IsRMPressed() && preventclick == 0)
                    {
                        if (playerInventory.itemSlots[i].item.ItemClass == GVar.ItemClassName.armor)
                        {
                            if (characterInventory.itemSlots[playerInventory.itemSlots[i].item.InventorySlot].item == null)
                            {
                                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                                characterInventory.itemSlots[playerInventory.itemSlots[i].item.InventorySlot].item = playerInventory.itemSlots[i].item;
                                P.AddItemStats((Armor)playerInventory.itemSlots[i].item);
                                for (int k = 0; k < playerInventory.itemSlots[i].item.Attacks.Count; k++)
                                {
                                    try
                                    {
                                        Attack.AddAvailableAttacks(playerInventory.itemSlots[i].item.Attacks);
                                    }
                                    catch
                                    {
                                        GVar.LogDebugInfo("!No attacks to add..PlayerInv->CharInv..", 2);
                                    }
                                }
                                playerInventory.itemSlots[i].item = null;
                                preventclick = 1;
                                break;
                            }
                        }
                        else if (playerInventory.itemSlots[i].item.ItemClass == GVar.ItemClassName.weapon)
                        {
                            if (characterInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item == null)
                            {
                                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                                characterInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item = playerInventory.itemSlots[i].item;
                                P.AddItemStats((Weapon)playerInventory.itemSlots[i].item);
                                for (int k = 0; k < playerInventory.itemSlots[i].item.Attacks.Count; k++)
                                {
                                    try
                                    {
                                        Attack.AddAvailableAttacks(playerInventory.itemSlots[i].item.Attacks);
                                    }
                                    catch
                                    {
                                        GVar.LogDebugInfo("!No attacks to add..PlayerInv->CharInv..", 2);
                                    }
                                }
                                playerInventory.itemSlots[i].item = null;
                                preventclick = 1;
                                break;
                            }
                            else if (characterInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item != null && characterInventory.itemSlots[GVar.InventorySlot.rightHandWeapon].item == null)
                            {
                                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                                characterInventory.itemSlots[GVar.InventorySlot.rightHandWeapon].item = playerInventory.itemSlots[i].item;
                                P.AddItemStats((Weapon)playerInventory.itemSlots[i].item);
                                for (int k = 0; k < playerInventory.itemSlots[i].item.Attacks.Count; k++)
                                {
                                    try
                                    {
                                        Attack.AddAvailableAttacks(playerInventory.itemSlots[i].item.Attacks);
                                    }
                                    catch
                                    {
                                        GVar.LogDebugInfo("!No attacks to add..PlayerInv->CharInv..", 2);
                                    }
                                }
                                playerInventory.itemSlots[i].item = null;
                                preventclick = 1;
                                break;
                            }
                        }
                        else if (playerInventory.itemSlots[i].item.ItemClass == GVar.ItemClassName.jewellry)
                        {
                            if (playerInventory.itemSlots[i].item.InventorySlot.Contains("Ring"))
                            {
                                for (int k = 0; k < 8; k++)
                                {
                                    int num = k + 1;
                                    string invslot = "Ring" + num.ToString();
                                    if (characterInventory.itemSlots[invslot].item == null)
                                    {
                                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                                        characterInventory.itemSlots[invslot].item = playerInventory.itemSlots[i].item;
                                        P.AddItemStats((Jewellry)playerInventory.itemSlots[i].item);
                                        if (playerInventory.itemSlots[i].item.Attacks != null)
                                        {
                                            Attack.AddAvailableAttacks(playerInventory.itemSlots[i].item.Attacks);
                                        }
                                        playerInventory.itemSlots[i].item = null;
                                        preventclick = 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            UpdatePlayerInventory(gameTime);
            UpdateMouseInventory(gameTime);
        }
    }

    public class ShopInventory
    {
        public Dictionary<int, ItemSlot> itemSlots;
        
        public ShopInventory()
        {
            itemSlots = new Dictionary<int, ItemSlot>();
            int itemCount = 0;
            Vector2 size = new Vector2(71, 71);
            Vector2 position = new Vector2(862, 51);
            for (int i = 0; i < 40; i ++)
            {
                itemCount++;

                itemSlots.Add(i, new ItemSlot(position, Vector2.Zero, size, Vector2.Zero, i.ToString()));

                if (itemCount == 5)
                {
                    itemCount = 0;
                    position.X = 862;
                    position.Y += 84;
                }
                else
                {
                    position.X += 84;
                }
            }
        }

        public void UpdateInventoryBounds(GameTime gameTime)
        {
            for (int i = 0; i < 40; i++)
            {
                itemSlots[i].Update(gameTime);
                if (itemSlots[i].item != null)
                    itemSlots[i].item.PlayerInventorySlot = i;
            }
        }
    }

    public class PlayerInventory
    {
        public Dictionary<int, ItemSlot> itemSlots;
        public bool fullInventory;

        public PlayerInventory()
        {
            itemSlots = new Dictionary<int, ItemSlot>();
            int itemCount = 0;
            Vector2 size = new Vector2(71, 71);
            Vector2 pos = new Vector2(437, 51);
            for (int i = 0; i < 40; i++)
            {
                itemCount++;

                itemSlots.Add(i, new ItemSlot(pos, Vector2.Zero, size, Vector2.Zero, i.ToString()));

                if (itemCount == 5)
                {
                    itemCount = 0;
                    pos.X = 437;
                    pos.Y += 84;
                }
                else
                {
                    pos.X += 84;
                }
            }

            fullInventory = false;
        }

        public void UpdateInventoryBounds(GameTime gameTime)
        {
            for (int i = 0; i < 40; i++)
            {
                itemSlots[i].Update(gameTime);
                if (itemSlots[i].item != null)
                {
                    itemSlots[i].item.PlayerInventorySlot = i;
                }
            }
        }
    }

    public class EnemyInventory
    {
        public Dictionary<string, ItemSlot> itemSlots;

        public EnemyInventory()
        {
            itemSlots = new Dictionary<string, ItemSlot>();
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.battleUI)
                {
                    itemSlots.Add(GVar.InventorySlot.helmet, new ItemSlot(new Vector2(1032, 55), new Vector2(ui.Position.X + 202, ui.Position.Y + 191), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.helmet));
                    itemSlots.Add(GVar.InventorySlot.chestplate, new ItemSlot(new Vector2(1032, 245), new Vector2(ui.Position.X + 202, ui.Position.Y + 261), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.chestplate));
                    itemSlots.Add(GVar.InventorySlot.leggings, new ItemSlot(new Vector2(1032, 340), new Vector2(ui.Position.X + 202, ui.Position.Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leggings));
                    itemSlots.Add(GVar.InventorySlot.leftBoot, new ItemSlot(new Vector2(969, 435), new Vector2(ui.Position.X + 179, ui.Position.Y + 331), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftBoot));
                    itemSlots.Add(GVar.InventorySlot.rightBoot, new ItemSlot(new Vector2(1095, 435), new Vector2(ui.Position.X + 225, ui.Position.Y + 331), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightBoot));
                    itemSlots.Add(GVar.InventorySlot.leftGauntlet, new ItemSlot(new Vector2(909, 340), new Vector2(ui.Position.X + 156, ui.Position.Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftGauntlet));
                    itemSlots.Add(GVar.InventorySlot.rightGauntlet, new ItemSlot(new Vector2(1155, 340), new Vector2(ui.Position.X + 246, ui.Position.Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightGauntlet));
                    itemSlots.Add(GVar.InventorySlot.leftHandWeapon, new ItemSlot(new Vector2(927, 140), new Vector2(ui.Position.X + 163, ui.Position.Y + 222), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftHandWeapon));
                    itemSlots.Add(GVar.InventorySlot.rightHandWeapon, new ItemSlot(new Vector2(1137, 140), new Vector2(ui.Position.X + 240, ui.Position.Y + 222), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightHandWeapon));
                    itemSlots.Add(GVar.InventorySlot.RingOne, new ItemSlot(new Vector2(865, 343), new Vector2(ui.Position.X + 140, ui.Position.Y + 297), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingOne));
                    itemSlots.Add(GVar.InventorySlot.RingTwo, new ItemSlot(new Vector2(871, 384), new Vector2(ui.Position.X + 142, ui.Position.Y + 312), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingTwo));
                    itemSlots.Add(GVar.InventorySlot.RingThree, new ItemSlot(new Vector2(887, 421), new Vector2(ui.Position.X + 148, ui.Position.Y + 326), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingThree));
                    itemSlots.Add(GVar.InventorySlot.RingFour, new ItemSlot(new Vector2(928, 427), new Vector2(ui.Position.X + 163, ui.Position.Y + 328), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingFour));
                    itemSlots.Add(GVar.InventorySlot.RingFive, new ItemSlot(new Vector2(1244, 343), new Vector2(ui.Position.X + 279, ui.Position.Y + 297), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingFive));
                    itemSlots.Add(GVar.InventorySlot.RingSix, new ItemSlot(new Vector2(1238, 384), new Vector2(ui.Position.X + 277, ui.Position.Y + 312), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingSix));
                    itemSlots.Add(GVar.InventorySlot.RingSeven, new ItemSlot(new Vector2(1222, 421), new Vector2(ui.Position.X + 271, ui.Position.Y + 326), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingSeven));
                    itemSlots.Add(GVar.InventorySlot.RingEight, new ItemSlot(new Vector2(1181, 427), new Vector2(ui.Position.X + 256, ui.Position.Y + 328), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingEight));
                }
            }
        }

        public void UpdateInventoryBounds(GameTime gameTime)
        {
            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                itemSlots[Lists.inventorySlots[i]].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                string name = Lists.inventorySlots[i];
                if (itemSlots[name].item != null)
                {
                    itemSlots[name].item.Draw(spriteBatch, itemSlots[name].item.SpriteID, itemSlots[name].item.Bounds, 0.19f, 0f, Vector2.Zero);
                }
            }
        }
    }

    public class CharacterInventory
    {
        public Dictionary<string, ItemSlot> itemSlots;

        public CharacterInventory()
        {
            itemSlots = new Dictionary<string, ItemSlot>();
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.battleUI)
                {
                    itemSlots.Add(GVar.InventorySlot.helmet, new ItemSlot(new Vector2(1032, 55), new Vector2(ui.Position.X + 202, ui.Position.Y + 191), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.helmet));
                    itemSlots.Add(GVar.InventorySlot.chestplate, new ItemSlot(new Vector2(1032, 245), new Vector2(ui.Position.X + 202, ui.Position.Y + 261), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.chestplate));
                    itemSlots.Add(GVar.InventorySlot.leggings, new ItemSlot(new Vector2(1032, 340), new Vector2(ui.Position.X + 202, ui.Position.Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leggings));
                    itemSlots.Add(GVar.InventorySlot.leftBoot, new ItemSlot(new Vector2(969, 435), new Vector2(ui.Position.X + 179, ui.Position.Y + 331), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftBoot));
                    itemSlots.Add(GVar.InventorySlot.rightBoot, new ItemSlot(new Vector2(1095, 435), new Vector2(ui.Position.X + 225, ui.Position.Y + 331), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightBoot));
                    itemSlots.Add(GVar.InventorySlot.leftGauntlet, new ItemSlot(new Vector2(909, 340), new Vector2(ui.Position.X + 156, ui.Position.Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftGauntlet));
                    itemSlots.Add(GVar.InventorySlot.rightGauntlet, new ItemSlot(new Vector2(1155, 340), new Vector2(ui.Position.X + 246, ui.Position.Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightGauntlet));
                    itemSlots.Add(GVar.InventorySlot.leftHandWeapon, new ItemSlot(new Vector2(927, 140), new Vector2(ui.Position.X + 163, ui.Position.Y + 222), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftHandWeapon));
                    itemSlots.Add(GVar.InventorySlot.rightHandWeapon, new ItemSlot(new Vector2(1137, 140), new Vector2(ui.Position.X + 240, ui.Position.Y + 222), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightHandWeapon));
                    itemSlots.Add(GVar.InventorySlot.RingOne, new ItemSlot(new Vector2(865, 343), new Vector2(ui.Position.X + 140, ui.Position.Y + 297), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingOne));
                    itemSlots.Add(GVar.InventorySlot.RingTwo, new ItemSlot(new Vector2(871, 384), new Vector2(ui.Position.X + 142, ui.Position.Y + 312), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingTwo));
                    itemSlots.Add(GVar.InventorySlot.RingThree, new ItemSlot(new Vector2(887, 421), new Vector2(ui.Position.X + 148, ui.Position.Y + 326), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingThree));
                    itemSlots.Add(GVar.InventorySlot.RingFour, new ItemSlot(new Vector2(928, 427), new Vector2(ui.Position.X + 163, ui.Position.Y + 328), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingFour));
                    itemSlots.Add(GVar.InventorySlot.RingFive, new ItemSlot(new Vector2(1244, 343), new Vector2(ui.Position.X + 279, ui.Position.Y + 297), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingFive));
                    itemSlots.Add(GVar.InventorySlot.RingSix, new ItemSlot(new Vector2(1238, 384), new Vector2(ui.Position.X + 277, ui.Position.Y + 312), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingSix));
                    itemSlots.Add(GVar.InventorySlot.RingSeven, new ItemSlot(new Vector2(1222, 421), new Vector2(ui.Position.X + 271, ui.Position.Y + 326), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingSeven));
                    itemSlots.Add(GVar.InventorySlot.RingEight, new ItemSlot(new Vector2(1181, 427), new Vector2(ui.Position.X + 256, ui.Position.Y + 328), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingEight));
                }
            }
        }

        public void UpdateInventoryBounds(GameTime gameTime)
        {
            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                itemSlots[Lists.inventorySlots[i]].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                string name = Lists.inventorySlots[i];
                if (itemSlots[name].item != null)
                {
                    itemSlots[name].item.Draw(spriteBatch, itemSlots[name].item.SpriteID, itemSlots[name].item.Bounds, 0.19f, 0f, Vector2.Zero);
                }
            }
        }
    }

    public class ItemSlot
    {
        public Rectangle bounds;
        public Vector2 position;
        public Vector2 size;
        public Vector2 miniPosition;
        public Vector2 miniSize;
        public Item item;
        public string inventorySlot;

        public ItemSlot(Vector2 position, Vector2 miniPosition, Vector2 size, Vector2 miniSize, string inventorySlot)
        {
            this.position = position;
            this.miniPosition = miniPosition;
            this.size = size;
            this.miniSize = miniSize;
            this.inventorySlot = inventorySlot;
        }

        public void Update(GameTime gameTime)
        {
            if (item != null)
            {
                item.Position = position;
                item.Size = size;
                item.Bounds = bounds;
            }
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

    }

    public class MouseInventory
    {
        public List<Item> heldItem;

        public MouseInventory()
        {
            heldItem = new List<Item>();
        }
    }
}

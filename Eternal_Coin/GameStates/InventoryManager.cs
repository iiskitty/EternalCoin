using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
            playerInventory = new Inventory(new Vector2(437, 51));
            enemyInventory = new EquipInventory();
            shopInventory = new Inventory(new Vector2(862, 51));
            characterInventory = new EquipInventory();
        }

        public static void ClearInventories()
        {
            for (int i = 0; i < 40; i++)
            {
                if (playerInventory.ItemSlots[i].item != null)
                    Item.FromPlayer(playerInventory.ItemSlots[i].item, i);
                if (shopInventory.ItemSlots[i].item != null)
                    Item.FromShop(shopInventory.ItemSlots[i].item, i);
            }

            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                if (characterInventory.ItemSlots[Lists.inventorySlots[i]].item != null)
                {
                    GVar.player.TakeItemStats(characterInventory.ItemSlots[Lists.inventorySlots[i]].item);
                    Item.FromCharacter(characterInventory.ItemSlots[Lists.inventorySlots[i]].item, Lists.inventorySlots[i]);
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
            for (int i = 0; i < 40; i++)
            {
                if (MouseManager.mouseBounds.Intersects(playerInventory.ItemSlots[i].bounds) && playerInventory.ItemSlots[i].item != null)
                {
                    spriteBatch.Draw(Textures.Misc.clearPixel, playerInventory.ItemSlots[i].bounds, null, Color.Gold, 0f, Vector2.Zero, SpriteEffects.None, 0.191f);
                    if (mouseInventory.heldItem == null)
                    {
                        if (playerInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.weapon)
                        {
                            Item.DrawItemInfo(spriteBatch, (Weapon)playerInventory.ItemSlots[i].item);
                        }
                        else if (playerInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.armor)
                        {
                            Item.DrawItemInfo(spriteBatch, (Armor)playerInventory.ItemSlots[i].item);
                        }
                        else if (playerInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.jewellry)
                        {
                            Item.DrawItemInfo(spriteBatch, (Jewellry)playerInventory.ItemSlots[i].item);
                        }
                        else if (playerInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.eternalcoin)
                        {
                            Item.DrawItemInfo(spriteBatch, (EternalCoin)playerInventory.ItemSlots[i].item);
                        }
                    }
                }
            }
        }

        public static void UpdatePlayerInventory(GameTime gameTime)
        {
            playerInventory.UpdateInventoryBounds(gameTime);
            for (int i = 0; i < 40; i++)
            {
                if (playerInventory.ItemSlots[i].item != null && MouseManager.mouseBounds.Intersects(playerInventory.ItemSlots[i].bounds) && InputManager.IsLMPressed())
                {
                    if (mouseInventory.heldItem == null)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        mouseInventory.heldItem = playerInventory.ItemSlots[i].item;
                        Item.FromPlayer(playerInventory.ItemSlots[i].item, i);
                    }
                    else if (mouseInventory.heldItem != null)
                    {
                        if (mouseInventory.heldItem.ItemClass == GVar.ItemClassName.eternalcoin && playerInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.jewellry)
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            Jewellry jewl = (Jewellry)playerInventory.ItemSlots[i].item;
                            playerInventory.ItemSlots[i].item = null;
                            jewl.eternalCoinSlot.item = ItemBuilder.BuildItem(mouseInventory.heldItem);
                            jewl.Attacks = jewl.eternalCoinSlot.item.Attacks;
                            playerInventory.ItemSlots[i].item = jewl;
                            mouseInventory.heldItem = null;
                        }
                        else
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            Item item = mouseInventory.heldItem;
                            mouseInventory.heldItem = playerInventory.ItemSlots[i].item;
                            Item.FromPlayer(playerInventory.ItemSlots[i].item, i);
                            Item.ToPlayer(item, i);
                        }
                    }
                }
                else if (playerInventory.ItemSlots[i].item == null && MouseManager.mouseBounds.Intersects(playerInventory.ItemSlots[i].bounds) && InputManager.IsLMPressed())
                {
                    if (mouseInventory.heldItem != null)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        Item.ToPlayer(mouseInventory.heldItem, i);
                        mouseInventory.heldItem = null;
                    }
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
                

                if (GVar.currentGameState == GVar.GameState.inventory)
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
                mouseInventory.heldItem.Position = new Vector2(MouseManager.GetMousePosition().X - mouseInventory.heldItem.Size.X, MouseManager.GetMousePosition().Y - mouseInventory.heldItem.Size.Y);
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
            if (GVar.currentGameState == GVar.GameState.inventory)
            {
                for (int i = 0; i < Lists.playerItems.Count; i++)
                {
                    DrawInventoryItem(spriteBatch, Lists.playerItems[i]);
                }
                for (int i = 0; i < Lists.characterItems.Count; i++)
                {
                    DrawInventoryItem(spriteBatch, Lists.characterItems[i]);
                }
            }
            else if (GVar.currentGameState == GVar.GameState.shop)
            {
                for (int i = 0; i < Lists.playerItems.Count; i++)
                {
                    DrawInventoryItem(spriteBatch, Lists.playerItems[i]);
                }
            }

            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Damage: " + GVar.player.Damage.ToString(), new Vector2(879, 572), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Armor: " + GVar.player.Armour.ToString(), new Vector2(879, 590), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

            for (int i = 0; i < Lists.inventoryButtons.Count; i++)
            {
                Lists.inventoryButtons[i].Update(gameTime);
                Lists.inventoryButtons[i].Draw(spriteBatch, Lists.inventoryButtons[i].SpriteID, Lists.inventoryButtons[i].Bounds, 0.19f, 0f, Vector2.Zero);
                if (MouseManager.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds))
                {
                    GVar.DrawBoundingBox(Lists.inventoryButtons[i].Bounds, spriteBatch, Textures.Misc.pixel, 1, 0.19f, Color.Green);
                }
            }

            DrawMouseInventory(spriteBatch, gameTime);
            DrawPlayerInventory(spriteBatch);

            for (int i = 0; i < Lists.inventorySlots.Count; i++)
            {
                if (characterInventory.ItemSlots[Lists.inventorySlots[i]].item != null && MouseManager.mouseBounds.Intersects(characterInventory.ItemSlots[Lists.inventorySlots[i]].item.Bounds))
                {
                    spriteBatch.Draw(Textures.Misc.clearPixel, characterInventory.ItemSlots[Lists.inventorySlots[i]].item.Bounds, null, Color.Gold, 0f, Vector2.Zero, SpriteEffects.None, 0.191f);

                    if (mouseInventory.heldItem == null)
                    {
                        if (characterInventory.ItemSlots[Lists.inventorySlots[i]].item.ItemClass == GVar.ItemClassName.weapon)
                        {
                            Item.DrawItemInfo(spriteBatch, (Weapon)characterInventory.ItemSlots[Lists.inventorySlots[i]].item);
                        }
                        else if (characterInventory.ItemSlots[Lists.inventorySlots[i]].item.ItemClass == GVar.ItemClassName.armor)
                        {
                            Item.DrawItemInfo(spriteBatch, (Armor)characterInventory.ItemSlots[Lists.inventorySlots[i]].item);
                        }
                        else if (characterInventory.ItemSlots[Lists.inventorySlots[i]].item.ItemClass == GVar.ItemClassName.jewellry)
                        {
                            Item.DrawItemInfo(spriteBatch, (Jewellry)characterInventory.ItemSlots[Lists.inventorySlots[i]].item);
                        }

                    }
                }
            }
        }

        public static void ManagePlayerInventories(GameTime gameTime, Inventory playerInventory, MouseInventory mouseInventory, EquipInventory characterInventory)
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

            for (int i = 0; i < Lists.inventoryButtons.Count; i++)
            {
                Updates.UpdateInventoryButtons(Lists.inventoryButtons[i], gameTime);
                if (MouseManager.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds) && InputManager.IsLMPressed())
                {
                    if (Lists.inventoryButtons[i].Name == "CloseInventory")
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        Lists.inventoryButtons.Clear();
                        GVar.currentGameState = GVar.GameState.game;
                        GVar.previousGameState = GVar.GameState.inventory;
                    }
                }
            }
            UpdatePlayerInventory(gameTime);
            UpdateMouseInventory(gameTime);
            characterInventory.UpdateInventoryBounds(gameTime);
            if (InputManager.IsKeyPressed(Keys.I))
            {
                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                GVar.currentGameState = GVar.GameState.game;
                GVar.previousGameState = GVar.GameState.inventory;
            }

            if (mouseInventory.heldItem != null)
            {
                for (int j = 0; j < Lists.inventorySlots.Count; j++)
                {
                    if (MouseManager.mouseBounds.Intersects(characterInventory.ItemSlots[Lists.inventorySlots[j]].bounds) && InputManager.IsLMPressed())
                    {
                        if (characterInventory.ItemSlots[Lists.inventorySlots[j]].item == null && characterInventory.ItemSlots[Lists.inventorySlots[j]].inventorySlot.Contains(mouseInventory.heldItem.InventorySlot))
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            Item.ToCharacter(mouseInventory.heldItem, Lists.inventorySlots[j]);
                            if (mouseInventory.heldItem.Attacks != null)
                            {
                                try
                                {
                                    Attack.AddAvailableAttacks(mouseInventory.heldItem.Attacks);
                                    for (int k = 0; k < mouseInventory.heldItem.Attacks.Count; k++)
                                    {
                                        GVar.LogDebugInfo("Attack added: " + mouseInventory.heldItem.Attacks[k], 1);
                                    }
                                }
                                catch
                                {
                                    GVar.LogDebugInfo("!No attacks to add..MouseInv->CharInv..!", 2);
                                }
                            }
                            GVar.player.AddItemStats(mouseInventory.heldItem);
                            mouseInventory.heldItem = null;
                        }
                        else if (characterInventory.ItemSlots[Lists.inventorySlots[j]].item != null && characterInventory.ItemSlots[Lists.inventorySlots[j]].inventorySlot.Contains(mouseInventory.heldItem.InventorySlot))
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            Item tempItem = mouseInventory.heldItem;
                            mouseInventory.heldItem = characterInventory.ItemSlots[Lists.inventorySlots[j]].item;
                            if (characterInventory.ItemSlots[Lists.inventorySlots[j]].item.Attacks != null)
                            {
                                try
                                {
                                    Attack.TakeAvailableAttacks(characterInventory.ItemSlots[Lists.inventorySlots[j]].item.Attacks);
                                }
                                catch
                                {
                                    GVar.LogDebugInfo("!No attacks to remove..MouseInv->CharInv|CharInv->MouseInv..!", 2);
                                }
                            }
                            Item.ToCharacter(tempItem, Lists.inventorySlots[j]);
                            if (characterInventory.ItemSlots[Lists.inventorySlots[j]].item.Attacks != null)
                            {
                                try
                                {
                                    Attack.AddAvailableAttacks(characterInventory.ItemSlots[Lists.inventorySlots[j]].item.Attacks);
                                    for (int k = 0; k < mouseInventory.heldItem.Attacks.Count; k++)
                                    {
                                        GVar.LogDebugInfo("Attack added: " + mouseInventory.heldItem.Attacks[k], 1);
                                    }
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
            int preventclick = 0;
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < Lists.inventorySlots.Count; j++)
                {
                    if (mouseInventory.heldItem == null && characterInventory.ItemSlots[Lists.inventorySlots[j]].item != null && MouseManager.mouseBounds.Intersects(characterInventory.ItemSlots[Lists.inventorySlots[j]].item.Bounds) && InputManager.IsLMPressed())
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);

                        mouseInventory.heldItem = characterInventory.ItemSlots[Lists.inventorySlots[j]].item;
                        GVar.player.TakeItemStats(characterInventory.ItemSlots[Lists.inventorySlots[j]].item);
                        if (characterInventory.ItemSlots[Lists.inventorySlots[j]].item.Attacks != null)
                        {
                            try
                            {
                                Attack.TakeAvailableAttacks(characterInventory.ItemSlots[Lists.inventorySlots[j]].item.Attacks);
                            }
                            catch
                            {
                                GVar.LogDebugInfo("!No attacks to remove..CharInv->MouseInv..!", 2);
                            }
                        }
                        Item.FromCharacter(characterInventory.ItemSlots[Lists.inventorySlots[j]].item, Lists.inventorySlots[j]);
                    }
                    else if (characterInventory.ItemSlots[Lists.inventorySlots[j]].item != null && MouseManager.mouseBounds.Intersects(characterInventory.ItemSlots[Lists.inventorySlots[j]].item.Bounds) && InputManager.IsRMPressed() && preventclick == 0)
                    {

                        if (playerInventory.ItemSlots[i].item == null)
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            Item.ToPlayer(characterInventory.ItemSlots[Lists.inventorySlots[j]].item, i);
                            GVar.player.TakeItemStats(characterInventory.ItemSlots[Lists.inventorySlots[j]].item);
                            if (characterInventory.ItemSlots[Lists.inventorySlots[j]].item.Attacks != null)
                            {
                                try
                                {
                                    Attack.TakeAvailableAttacks(characterInventory.ItemSlots[Lists.inventorySlots[j]].item.Attacks);
                                }
                                catch
                                {
                                    GVar.LogDebugInfo("!No attacks to remove..CharInv->PlayerInv..", 2);
                                }
                            }
                            Item.FromCharacter(characterInventory.ItemSlots[Lists.inventorySlots[j]].item, Lists.inventorySlots[j]);
                            preventclick = 1;
                            break;
                        }
                    }
                }
                if (playerInventory.ItemSlots[i].item != null && MouseManager.mouseBounds.Intersects(playerInventory.ItemSlots[i].item.Bounds) && InputManager.IsRMPressed() && preventclick == 0)
                {
                    if (playerInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.armor)
                    {
                        if (characterInventory.ItemSlots[playerInventory.ItemSlots[i].item.InventorySlot].item == null)
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            Item.ToCharacter(playerInventory.ItemSlots[i].item, playerInventory.ItemSlots[i].item.InventorySlot);
                            GVar.player.AddItemStats((Armor)playerInventory.ItemSlots[i].item);
                            try
                            {
                                Attack.AddAvailableAttacks(playerInventory.ItemSlots[i].item.Attacks);
                                for (int j = 0; j < playerInventory.ItemSlots[i].item.Attacks.Count; j++)
                                {
                                    GVar.LogDebugInfo("Attack added: " + playerInventory.ItemSlots[i].item.Attacks[j], 1);
                                }
                            }
                            catch (Exception e)
                            {
                                GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                            }
                            Item.FromPlayer(playerInventory.ItemSlots[i].item, i);
                            preventclick = 1;
                            break;
                        }
                    }
                    else if (playerInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.weapon)
                    {
                        if (characterInventory.ItemSlots[GVar.InventorySlot.leftHandWeapon].item == null)
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            Item.ToCharacter(playerInventory.ItemSlots[i].item, GVar.InventorySlot.leftHandWeapon);
                            GVar.player.AddItemStats((Weapon)playerInventory.ItemSlots[i].item);
                            try
                            {
                                Attack.AddAvailableAttacks(playerInventory.ItemSlots[i].item.Attacks);
                                for (int j = 0; j < playerInventory.ItemSlots[i].item.Attacks.Count; j++)
                                {
                                    GVar.LogDebugInfo("Attack added: " + playerInventory.ItemSlots[i].item.Attacks[j], 1);
                                }
                            }
                            catch (Exception e)
                            {
                                GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                            }
                            Item.FromPlayer(playerInventory.ItemSlots[i].item, i);
                            preventclick = 1;
                            break;
                        }
                        else if (characterInventory.ItemSlots[GVar.InventorySlot.leftHandWeapon].item != null && characterInventory.ItemSlots[GVar.InventorySlot.rightHandWeapon].item == null)
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            Item.ToCharacter(playerInventory.ItemSlots[i].item, GVar.InventorySlot.rightHandWeapon);
                            GVar.player.AddItemStats((Weapon)playerInventory.ItemSlots[i].item);
                            try
                            {
                                Attack.AddAvailableAttacks(playerInventory.ItemSlots[i].item.Attacks);
                                for (int j = 0; j < playerInventory.ItemSlots[i].item.Attacks.Count; j++)
                                {
                                    GVar.LogDebugInfo("Attack added: " + playerInventory.ItemSlots[i].item.Attacks[j], 1);
                                }
                            }
                            catch (Exception e)
                            {
                                GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                            }
                            Item.FromPlayer(playerInventory.ItemSlots[i].item, i);
                            preventclick = 1;
                            break;
                        }
                    }
                    else if (playerInventory.ItemSlots[i].item.ItemClass == GVar.ItemClassName.jewellry)
                    {
                        if (playerInventory.ItemSlots[i].item.InventorySlot.Contains("Ring"))
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                int num = k + 1;
                                string invslot = "Ring" + num.ToString();
                                if (characterInventory.ItemSlots[invslot].item == null)
                                {
                                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                                    Item.ToCharacter(playerInventory.ItemSlots[i].item, invslot);
                                    GVar.player.AddItemStats((Jewellry)playerInventory.ItemSlots[i].item);
                                    if (playerInventory.ItemSlots[i].item.Attacks.Count > 0)
                                    {
                                        Attack.AddAvailableAttacks(playerInventory.ItemSlots[i].item.Attacks);
                                        for (int j = 0; j < playerInventory.ItemSlots[i].item.Attacks.Count; j++)
                                        {
                                            GVar.LogDebugInfo("Attack added: " + playerInventory.ItemSlots[i].item.Attacks[j], 1);
                                        }
                                    }
                                    Item.FromPlayer(playerInventory.ItemSlots[i].item, i);
                                    preventclick = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

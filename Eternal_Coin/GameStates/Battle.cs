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
    public class Battle
    {
        public static Rectangle playerAttackRec;
        public static Rectangle playerPort;
        public static Rectangle enemyPort;
        public static Rectangle enemyAttackRec;
        public static BattleEnemy battleEnemy;
        public static BattlePlayer battlePlayer;

        public static string currentAttackType = "";
        static string enemyNextAttack = string.Empty;
        static float enemyAttackTimer = 100;

        public static void LoadBattle(XmlDocument battleDoc)
        {
            try
            {
                battlePlayer = new BattlePlayer(Dictionaries.availableAttacks[Lists.availableAttacksIDs[0]], new Vector2(100, 20), new Vector2(128, 128), Lists.entity[0].Name, "Alive", new Vector2(), Color.White, Lists.entity[0].Health, Lists.entity[0].Armour, Lists.entity[0].Damage);
                

                XmlNodeList enemyItems = battleDoc.SelectNodes("/location/enemy/inventory/item");

                XmlNode eNode = battleDoc.SelectSingleNode("/location/enemy");
                GVar.eDisplayPicID = eNode["dpid"].InnerText;


                foreach (XmlNode enemyItem in enemyItems)
                {
                    Item item = ItemBuilder.BuildItem(Dictionaries.items[enemyItem[GVar.XmlTags.ItemTags.itemname].InnerText]);
                    if (item.ItemClass == GVar.ItemClassName.weapon)
                    {
                        if (InventoryManager.enemyInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item == null)
                        {
                            Attack.AddEnemyAttack(item.Attacks);
                            if (battleEnemy == null)
                            {
                                battleEnemy = new BattleEnemy(Dictionaries.enemyAttacks[Lists.enemyAttackIDs[0]], new Vector2(1000, 20), new Vector2(128, 128), eNode["name"].InnerText, "Alive", Vector2.Zero, Color.White, 100f, 0f, 0f, 0);
                                enemyNextAttack = Lists.enemyAttackIDs[0];
                            }
                            battleEnemy.AddItemStats(item);
                            InventoryManager.enemyInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item = item;
                            
                        }
                        else if (InventoryManager.enemyInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item != null && InventoryManager.enemyInventory.itemSlots[GVar.InventorySlot.rightHandWeapon].item == null)
                        {
                            Attack.AddEnemyAttack(item.Attacks);
                            if (battleEnemy == null)
                            {
                                battleEnemy = new BattleEnemy(Dictionaries.enemyAttacks[Lists.enemyAttackIDs[0]], new Vector2(1000, 20), new Vector2(128, 128), eNode["name"].InnerText, "Alive", Vector2.Zero, Color.White, 100f, 0f, 0f, 0);
                                enemyNextAttack = Lists.enemyAttackIDs[0];
                            }
                            battleEnemy.AddItemStats(item);
                            InventoryManager.enemyInventory.itemSlots[GVar.InventorySlot.rightHandWeapon].item = item;
                        }
                    }
                    else if (item.ItemClass == GVar.ItemClassName.armor)
                    {
                        if (InventoryManager.enemyInventory.itemSlots[item.InventorySlot].item == null)
                        {
                            if (battleEnemy == null)
                            {
                                battleEnemy = new BattleEnemy(Dictionaries.enemyAttacks[Lists.enemyAttackIDs[0]], new Vector2(1000, 20), new Vector2(128, 128), eNode["name"].InnerText, "Alive", Vector2.Zero, Color.White, 100f, 0f, 0f, 0);
                                enemyNextAttack = Lists.enemyAttackIDs[0];
                            }
                            battleEnemy.AddItemStats(item);
                            InventoryManager.enemyInventory.itemSlots[item.InventorySlot].item = item;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                GVar.currentGameState = GVar.GameState.game;
                GVar.previousGameState = GVar.GameState.battle;
            }

            foreach (string eAtkID in Lists.enemyAttackIDs)
            {
                foreach (AttackAnim eAtkAnim in Dictionaries.enemyAttacks[eAtkID].AttackAnims)
                {
                    try
                    {
                        battleEnemy.AddAnimation(eAtkAnim.Frames, eAtkAnim.YPos, eAtkAnim.XPos, eAtkAnim.ID, eAtkAnim.Width, eAtkAnim.Height, Vector2.Zero);
                    }
                    catch
                    {
                        
                    }
                }
            }

            foreach (string atkID in Lists.availableAttacksIDs)
            {
                foreach (AttackAnim atkAnim in Dictionaries.availableAttacks[atkID].AttackAnims)
                {
                    try
                    {
                        battlePlayer.AddAnimation(atkAnim.Frames, atkAnim.YPos, atkAnim.XPos, atkAnim.ID, atkAnim.Width, atkAnim.Height, Vector2.Zero);
                    }
                    catch
                    {

                    }
                }
            }

            battlePlayer.PlayAnimation(GVar.AttackAnimStates.idle);
            battleEnemy.PlayAnimation(GVar.AttackAnimStates.idle);
            battlePlayer.FPS = 5;
            battleEnemy.FPS = 5;

            for (int i = 0; i < Lists.availableAttacksIDs.Count; i++)
            {
                if (Lists.availableAttacksIDs[i] == "DefaultPunch" && InventoryManager.characterInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item != null && InventoryManager.characterInventory.itemSlots[GVar.InventorySlot.rightHandWeapon].item != null)
                {
                    continue;
                }
                Button atkButton = new Button(Textures.pixel, new Vector2(), new Vector2(25, 25), Color.Blue, "Attack", Lists.availableAttacksIDs[i], 0f);
                Lists.attackButtons.Add(atkButton);
            }

            Vector2 pos = new Vector2();

            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.battleUI)
                {
                    pos.X = ui.Position.X + 473;
                    pos.Y = ui.Position.Y + 35;
                }
            }

            foreach (Object B in Lists.attackButtons)
            {
                B.Position = pos;
                pos.X += 30;
            }
        }

        public static void UpdateBattle(GameTime gameTime)
        {
            battlePlayer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            battleEnemy.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            if (battlePlayer.Bounds.Intersects(playerAttackRec) && battlePlayer.CurrentAnimation == GVar.AttackAnimStates.buildUp)
            {
                battlePlayer.PlayAnimation(GVar.AttackAnimStates.attack);
            }
            else if (battlePlayer.Bounds.Intersects(playerPort) && battlePlayer.CurrentAnimation == GVar.AttackAnimStates.retreat)
            {
                battlePlayer.PlayAnimation(GVar.AttackAnimStates.idle);
            }

            if (battleEnemy.Bounds.Intersects(enemyAttackRec) && battleEnemy.CurrentAnimation == GVar.AttackAnimStates.buildUp)
            {
                battleEnemy.PlayAnimation(GVar.AttackAnimStates.attack);
            }
            else if (battleEnemy.Bounds.Intersects(enemyPort) && battleEnemy.CurrentAnimation == GVar.AttackAnimStates.retreat)
            {
                enemyAttackTimer = 100;
                Random rand = new Random();
                int num = rand.Next(0, Lists.enemyAttackIDs.Count);
                enemyNextAttack = Lists.enemyAttackIDs[num];
                battleEnemy.PlayAnimation(GVar.AttackAnimStates.idle);
            }

            playerAttackRec = new Rectangle((int)battleEnemy.Position.X - 7, (int)battleEnemy.Position.Y, 10, (int)battleEnemy.Size.Y);
            enemyAttackRec = new Rectangle((int)battlePlayer.Position.X + (int)battlePlayer.Size.X - 3, (int)battlePlayer.Position.Y, 10, (int)battlePlayer.Size.Y);

            playerPort = new Rectangle(95, 0, 10, 300);
            enemyPort = new Rectangle(1123, 0, 10, 300);
            
            if (enemyAttackTimer > 0 && battleEnemy.Bounds.Intersects(enemyPort) && battleEnemy.CurrentAnimation == GVar.AttackAnimStates.idle)
            {
                enemyAttackTimer -= 20f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (enemyAttackTimer < 0)
            {
                battleEnemy.SetAttack(Dictionaries.enemyAttacks[enemyNextAttack]);
                battleEnemy.PlayAnimation(GVar.AttackAnimStates.buildUp);
                enemyAttackTimer = 0;
            }
                

            foreach (Object B in Lists.attackButtons)
            {
                B.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                
                if (MouseManager.mouseBounds.Intersects(B.Bounds) && InputManager.IsLMPressed() && battlePlayer.CurrentAnimation == GVar.AttackAnimStates.idle)
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                    try
                    {
                        currentAttackType = Dictionaries.availableAttacks[B.State].Type;
                        battlePlayer.SetAttack(Dictionaries.availableAttacks[B.State]);
                        battlePlayer.PlayAnimation(GVar.AttackAnimStates.buildUp);
                    }
                    catch (Exception e)
                    {
                        GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                    }
                }
            }
        }

        public static void DrawPlayerInfo(SpriteBatch spriteBatch)
        {
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.battleUI)
                {
                    spriteBatch.Draw(Dictionaries.displayPictures[GVar.displayPicID].displayPic, new Rectangle((int)ui.Position.X + 10, (int)ui.Position.Y + 10, 50, 50), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, battlePlayer.Name, new Vector2(ui.Position.X + 62, ui.Position.Y + 32), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Damage: " + battlePlayer.Damage.ToString(), new Vector2(ui.Position.X + 33, ui.Position.Y + 227), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Armor: " + battlePlayer.Armour.ToString(), new Vector2(ui.Position.X + 316, ui.Position.Y + 227), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Health: " + battlePlayer.Health.ToString(), new Vector2(ui.Position.X + 176, ui.Position.Y + 114), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                }
            }
        }

        public static void DrawEnemyInfo(SpriteBatch spriteBatch)
        {
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.battleUI)
                {
                    spriteBatch.Draw(Dictionaries.eDisplayPictures[GVar.eDisplayPicID].displayPic, new Rectangle((int)ui.Position.X + 861, (int)ui.Position.Y + 10, 50, 50), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, battleEnemy.Name, new Vector2(ui.Position.X + 912, ui.Position.Y + 32), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Damage: " + battleEnemy.Damage.ToString(), new Vector2(ui.Position.X + 883, ui.Position.Y + 227), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Armor: " + battleEnemy.Armor.ToString(), new Vector2(ui.Position.X + 1136, ui.Position.Y + 227), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Health: " + battleEnemy.Health.ToString(), new Vector2(ui.Position.X + 1026, ui.Position.Y + 114), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                }
            }
        }

        public static void DrawBattle(SpriteBatch spriteBatch, GameTime gameTime)
        {
            battlePlayer.Update(gameTime);
            battlePlayer.Draw(spriteBatch, battlePlayer.SpriteID, battlePlayer.Bounds, 0.2f, 0f, Vector2.Zero);

            battleEnemy.Update(gameTime);
            battleEnemy.Draw(spriteBatch, battleEnemy.SpriteID, battleEnemy.Bounds, 0.2f, 0f, Vector2.Zero);

            GVar.DrawBoundingBox(playerAttackRec, spriteBatch, Textures.pixel, 1, 0.2f, Color.Green);
            GVar.DrawBoundingBox(playerPort, spriteBatch, Textures.pixel, 1, 0.2f, Color.Green);
            GVar.DrawBoundingBox(enemyAttackRec, spriteBatch, Textures.pixel, 1, 0.2f, Color.Green);
            GVar.DrawBoundingBox(enemyPort, spriteBatch, Textures.pixel, 1, 0.2f, Color.Green);

            DrawPlayerInfo(spriteBatch);
            DrawEnemyInfo(spriteBatch);

            int timer = (int)enemyAttackTimer;
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, timer.ToString(), new Vector2(1200, 20), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);

            foreach (Object B in Lists.attackButtons)
            {
                B.Update(gameTime);
                B.Draw(spriteBatch, B.SpriteID, B.Bounds, 0.19f, 0f, Vector2.Zero);
            }

            InventoryManager.DrawMiniInventory(spriteBatch, InventoryManager.characterInventory);
            InventoryManager.DrawMiniInventory(spriteBatch, InventoryManager.enemyInventory);
        }
    }
}

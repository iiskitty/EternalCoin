using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;

namespace Eternal_Coin
{
    public class Battle
    {
        public static Rectangle playerAttackRec;            //rectangle for the player to start attacking
        public static Rectangle playerPort;                 //rectangle for the player to sit idle
        public static Rectangle enemyPort;                  //rectangle for the enemy to sit idle
        public static Rectangle enemyAttackRec;             //rectangle for the enemy to start attacking
        public static BattleEnemy battleEnemy;              //new enemy is created when entering a battle
        public static BattlePlayer battlePlayer;            //new battleplayer is created when entering battle

        public static bool battleWon = false;               //sets to true if a battle is won

        public static List<Item> loot = new List<Item>();   //List to hold loot for when a battle is won
        public static int silverReward;                     //amount of money looted for when a battle is won

        public static string currentAttackType = "";        //type of current attack
        public static string currentAttackProj = "";        //key of projectile (if any)
        static string enemyNextAttack = string.Empty;       //key of enemy next attack
        static float enemyAttackTimer = 100;                //timer for next enemy attack

        /// <summary>
        /// Loads the battle when player runs into an enemy
        /// </summary>
        /// <param name="battleDoc">current location</param>
        public static void LoadBattle(XmlDocument battleDoc)
        {
            enemyAttackTimer = 100;
            Attack.LoadDefaultAttack();
            try
            {
                battlePlayer = new BattlePlayer(Dictionaries.availableAttacks[Lists.availableAttacksIDs[0]], new Vector2(100, 50), new Vector2(128, 128), GVar.player.Name, "Alive", new Vector2(), Color.White, GVar.player.Health, GVar.player.Armour, GVar.player.Damage);
                try
                {
                    for (int i = 0; i < Lists.availableAttacksIDs.Count; i++)
                    {
                        GVar.LogDebugInfo("Attack ID: " + Lists.availableAttacksIDs[i], 1);
                    }
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }

                XmlNodeList enemyItems = battleDoc.SelectNodes("/location/enemy/inventory/item");

                XmlNode eNode = battleDoc.SelectSingleNode("/location/enemy");
                GVar.eDisplayPicID = eNode["dpid"].InnerText;

                //adds enemy attacks and creates a new BattleEnemy
                foreach (XmlNode enemyItem in enemyItems)
                {
                    Item item = ItemBuilder.BuildItem(Dictionaries.items[enemyItem[GVar.XmlTags.ItemTags.itemname].InnerText]);
                    if (item.ItemClass == GVar.ItemClassName.weapon)
                    {
                        if (InventoryManager.enemyInventory.ItemSlots[GVar.InventorySlot.leftHandWeapon].item == null)
                        {
                            Attack.AddEnemyAttack(item.Attacks);
                            if (battleEnemy == null)
                            {
                                battleEnemy = new BattleEnemy(Dictionaries.enemyAttacks[Lists.enemyAttackIDs[0]], new Vector2(1000, 50), new Vector2(128, 128), eNode["name"].InnerText, "Alive", Vector2.Zero, Color.White, 40f, 0f, 0f, 0);
                                enemyNextAttack = Lists.enemyAttackIDs[0];
                            }
                            battleEnemy.AddItemStats(item);
                            InventoryManager.enemyInventory.ItemSlots[GVar.InventorySlot.leftHandWeapon].item = item;
                            
                        }
                        else if (InventoryManager.enemyInventory.ItemSlots[GVar.InventorySlot.leftHandWeapon].item != null && InventoryManager.enemyInventory.ItemSlots[GVar.InventorySlot.rightHandWeapon].item == null)
                        {
                            Attack.AddEnemyAttack(item.Attacks);
                            if (battleEnemy == null)
                            {
                                battleEnemy = new BattleEnemy(Dictionaries.enemyAttacks[Lists.enemyAttackIDs[0]], new Vector2(1000, 50), new Vector2(128, 128), eNode["name"].InnerText, "Alive", Vector2.Zero, Color.White, 40f, 0f, 0f, 0);
                                enemyNextAttack = Lists.enemyAttackIDs[0];
                            }
                            battleEnemy.AddItemStats(item);
                            InventoryManager.enemyInventory.ItemSlots[GVar.InventorySlot.rightHandWeapon].item = item;
                        }
                    }
                    else if (item.ItemClass == GVar.ItemClassName.armor)
                    {
                        if (InventoryManager.enemyInventory.ItemSlots[item.InventorySlot].item == null)
                        {
                            if (battleEnemy == null)
                            {
                                battleEnemy = new BattleEnemy(Dictionaries.enemyAttacks[Lists.enemyAttackIDs[0]], new Vector2(1000, 50), new Vector2(128, 128), eNode["name"].InnerText, "Alive", Vector2.Zero, Color.White, 40f, 0f, 0f, 0);
                                enemyNextAttack = Lists.enemyAttackIDs[0];
                            }
                            battleEnemy.AddItemStats(item);
                            InventoryManager.enemyInventory.ItemSlots[item.InventorySlot].item = item;
                        }
                    }
                }
                try
                {
                    XmlNode silver = battleDoc.SelectSingleNode("/location/enemy/loot/silver");
                    silverReward = Convert.ToInt32(silver["amount"].InnerText); //sets silverReward to the xml tag silver value
                }
                catch(Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
                try
                {
                    XmlNodeList lootItems = battleDoc.SelectNodes("/location/enemy/loot/item");
                    
                    foreach (XmlNode lootItem in lootItems)
                    {
                        loot.Add(Dictionaries.items[lootItem[GVar.XmlTags.ItemTags.itemname].InnerText]); //adds items to the loot List
                    }
                    Vector2 itemPos = new Vector2();
                    for (int i = 0; i < Lists.uiElements.Count; i++)
                    {
                        if (Lists.uiElements[i].SpriteID == Textures.UI.endBattleUI)
                        {
                            itemPos = new Vector2(Lists.uiElements[i].Position.X + 14, Lists.uiElements[i].Position.Y + 56); 
                        }
                    }
                    for (int i = 0; i < loot.Count; i++)
                    {
                        loot[i].Position = itemPos; //sets items position to the UI's position
                        itemPos.X += 82;
                    }
                }
                catch(Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e +"]", 1);
                }
            }
            catch (Exception e)
            {
                GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                GVar.currentGameState = GVar.GameState.game;
                GVar.previousGameState = GVar.GameState.battle;
            }

            //adds attack animations to created battlEnemy
            for (int i = 0; i < Lists.enemyAttackIDs.Count; i++)
            {
                for (int j = 0; j < Dictionaries.enemyAttacks[Lists.enemyAttackIDs[i]].AttackAnims.Count; j++)
                {
                    try
                    {
                        battleEnemy.AddAnimation(Dictionaries.enemyAttacks[Lists.enemyAttackIDs[i]].AttackAnims[j].Frames, Dictionaries.enemyAttacks[Lists.enemyAttackIDs[i]].AttackAnims[j].YPos, Dictionaries.enemyAttacks[Lists.enemyAttackIDs[i]].AttackAnims[j].XPos, Dictionaries.enemyAttacks[Lists.enemyAttackIDs[i]].AttackAnims[j].ID, Dictionaries.enemyAttacks[Lists.enemyAttackIDs[i]].AttackAnims[j].Width, Dictionaries.enemyAttacks[Lists.enemyAttackIDs[i]].AttackAnims[j].Height, Vector2.Zero);
                    }
                    catch
                    {
                        
                    }
                }
            }

            //adds attack animations to created battlePlayer
            for (int i = 0; i < Lists.availableAttacksIDs.Count; i++)
            {
                for (int j = 0; j < Dictionaries.availableAttacks[Lists.availableAttacksIDs[i]].AttackAnims.Count; j++)
                {
                    try
                    {
                        battlePlayer.AddAnimation(Dictionaries.availableAttacks[Lists.availableAttacksIDs[i]].AttackAnims[j].Frames, Dictionaries.availableAttacks[Lists.availableAttacksIDs[i]].AttackAnims[j].YPos, Dictionaries.availableAttacks[Lists.availableAttacksIDs[i]].AttackAnims[j].XPos, Dictionaries.availableAttacks[Lists.availableAttacksIDs[i]].AttackAnims[j].ID, Dictionaries.availableAttacks[Lists.availableAttacksIDs[i]].AttackAnims[j].Width, Dictionaries.availableAttacks[Lists.availableAttacksIDs[i]].AttackAnims[j].Height, Vector2.Zero);
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
                if (Lists.availableAttacksIDs[i] == "DefaultPunch" && InventoryManager.characterInventory.ItemSlots[GVar.InventorySlot.leftHandWeapon].item != null && InventoryManager.characterInventory.ItemSlots[GVar.InventorySlot.rightHandWeapon].item != null)
                {
                    continue; //if left hand weapon and right hand weapon are not null (both hands have a weapon) dont create a punch button
                }
                Button atkButton = new Button(Dictionaries.textures[Lists.availableAttacksIDs[i]], new Vector2(), new Vector2(25, 25), Color.White, "Attack", Lists.availableAttacksIDs[i], 0f);
                Lists.attackButtons.Add(atkButton); //if one hand does not have a weapon, create the punch button
            }

            Vector2 pos = new Vector2();

            for (int i = 0; i < Lists.uiElements.Count; i++)
            {
                if (Lists.uiElements[i].SpriteID == Textures.UI.battleUI)
                {
                    pos.X = Lists.uiElements[i].Position.X + 473;
                    pos.Y = Lists.uiElements[i].Position.Y + 35;
                }
            }

            for (int i = 0; i < Lists.attackButtons.Count; i++)
            {
                Lists.attackButtons[i].Position = pos;
                pos.X += 30;
            }
        }

        /// <summary>
        /// Updates battle scene
        /// </summary>
        /// <param name="gameTime">game time to keep things smooth (delta time)</param>
        public static void UpdateBattle(GameTime gameTime)
        {
            battlePlayer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            if (!battleWon)
                battleEnemy.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            for (int i = 0; i < Lists.activeProjectiles.Count; i++)
            {
                Lists.activeProjectiles[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (Lists.activeProjectiles[i].Bounds.Intersects(playerAttackRec))
                {
                    Projectile proj = (Projectile)Lists.activeProjectiles[i];
                    battleEnemy.Health -= proj.Damage;
                    Lists.activeProjectiles.RemoveAt(i);
                }
            }

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
            
            if (enemyAttackTimer > 0 && battleEnemy.Bounds.Intersects(enemyPort) && battleEnemy.CurrentAnimation == GVar.AttackAnimStates.idle && battleEnemy.Health > 0)
            {
                enemyAttackTimer -= 20f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (enemyAttackTimer < 0)
            {
                battleEnemy.SetAttack(Dictionaries.enemyAttacks[enemyNextAttack]);
                battleEnemy.PlayAnimation(GVar.AttackAnimStates.buildUp);
                enemyAttackTimer = 0;
            }
                

            for (int i = 0; i < Lists.attackButtons.Count; i++)
            {
                Lists.attackButtons[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                
                if (MouseManager.mouse.mouseBounds.Intersects(Lists.attackButtons[i].Bounds) && InputManager.IsLMPressed() && battlePlayer.CurrentAnimation == GVar.AttackAnimStates.idle && !battleWon)
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                    try
                    {
                        currentAttackType = Dictionaries.availableAttacks[Lists.attackButtons[i].State].Type;
                        try
                        {
                            currentAttackProj = Dictionaries.availableAttacks[Lists.attackButtons[i].State].MagicProjectile;
                        }
                        catch (Exception e)
                        {
                            GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                        }
                        battlePlayer.SetAttack(Dictionaries.availableAttacks[Lists.attackButtons[i].State]);
                        battlePlayer.PlayAnimation(GVar.AttackAnimStates.buildUp);
                    }
                    catch (Exception e)
                    {
                        GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                    }
                }
            }

            if (battleWon)
            {
                for (int i = 0; i < Lists.battleSceneButtons.Count; i++)
                {
                    Lists.battleSceneButtons[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                    if (MouseManager.mouse.mouseBounds.Intersects(Lists.battleSceneButtons[i].Bounds))
                    {
                        Lists.battleSceneButtons[i].PlayAnimation(GVar.AnimStates.Button.mouseover);

                        if (InputManager.IsLMPressed() && battleWon)
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            if (Lists.battleSceneButtons[i].Name == "Continue")
                            {
                                XmlDocument battleDoc = new XmlDocument();
                                battleDoc.Load(GVar.gameFilesLocation + GVar.player.Name + "/" + GVar.player.CurrentLocation.LocatoinFilePath);
                                XmlNode hasEnemy = battleDoc.SelectSingleNode("/location");
                                hasEnemy["hasenemy"].InnerText = "False";
                                battleDoc.Save(GVar.gameFilesLocation + GVar.player.Name + "/" + GVar.player.CurrentLocation.LocatoinFilePath);
                                GVar.location.HasEnemy = false;
                                battleWon = false;
                                for (int j = 0; j < Lists.uiElements.Count; j++)
                                {
                                    if (Lists.uiElements[i].SpriteID == Textures.UI.endBattleUI)
                                    {
                                        Lists.uiElements[i].Draw = false;
                                    }
                                }
                                GVar.changeBackToGame = true;
                                Colours.drawBlackFade = true;
                                Colours.fadeIn = true;
                                GVar.silverMoney += silverReward;
                                for (int j = 0; j < loot.Count; j++)
                                {
                                    for (int k = 0; k < 40; k++)
                                    {
                                        if (InventoryManager.playerInventory.ItemSlots[k].item == null)
                                        {
                                            Item item = ItemBuilder.BuildItem(loot[j]);
                                            Lists.playerItems.Add(item);
                                            InventoryManager.playerInventory.ItemSlots[k].item = item;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (!MouseManager.mouse.mouseBounds.Intersects(Lists.battleSceneButtons[i].Bounds) && Lists.battleSceneButtons[i].CurrentAnimation != GVar.AnimStates.Button.def)
                    {
                        Lists.battleSceneButtons[i].PlayAnimation(GVar.AnimStates.Button.def);
                    }
                }
                for (int i = 0; i < loot.Count; i++)
                {
                    loot[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            }
        }

        public static void DrawPlayerInfo(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Lists.uiElements.Count; i++)
            {
                if (Lists.uiElements[i].SpriteID == Textures.UI.battleUI)
                {
                    spriteBatch.Draw(Dictionaries.displayPictures[GVar.displayPicID].displayPic, new Rectangle((int)Lists.uiElements[i].Position.X + 10, (int)Lists.uiElements[i].Position.Y + 10, 50, 50), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, battlePlayer.Name, new Vector2(Lists.uiElements[i].Position.X + 62, Lists.uiElements[i].Position.Y + 32), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Damage: " + battlePlayer.Damage.ToString(), new Vector2(Lists.uiElements[i].Position.X + 33, Lists.uiElements[i].Position.Y + 227), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Armor: " + battlePlayer.Armour.ToString(), new Vector2(Lists.uiElements[i].Position.X + 316, Lists.uiElements[i].Position.Y + 227), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Health: " + battlePlayer.Health.ToString(), new Vector2(Lists.uiElements[i].Position.X + 176, Lists.uiElements[i].Position.Y + 114), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                }
            }
        }

        public static void DrawEnemyInfo(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Lists.uiElements.Count; i++)
            {
                if (Lists.uiElements[i].SpriteID == Textures.UI.battleUI)
                {
                    spriteBatch.Draw(Dictionaries.eDisplayPictures[GVar.eDisplayPicID].displayPic, new Rectangle((int)Lists.uiElements[i].Position.X + 861, (int)Lists.uiElements[i].Position.Y + 10, 50, 50), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, battleEnemy.Name, new Vector2(Lists.uiElements[i].Position.X + 912, Lists.uiElements[i].Position.Y + 32), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Damage: " + battleEnemy.Damage.ToString(), new Vector2(Lists.uiElements[i].Position.X + 883, Lists.uiElements[i].Position.Y + 227), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Armor: " + battleEnemy.Armor.ToString(), new Vector2(Lists.uiElements[i].Position.X + 1136, Lists.uiElements[i].Position.Y + 227), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, "Health: " + battleEnemy.Health.ToString(), new Vector2(Lists.uiElements[i].Position.X + 1026, Lists.uiElements[i].Position.Y + 114), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                }
            }
        }

        public static void DrawBattle(SpriteBatch spriteBatch, GameTime gameTime)
        {
            battlePlayer.Update(gameTime);
            battlePlayer.Draw(spriteBatch, battlePlayer.SpriteID, battlePlayer.Bounds, 0.2f, 0f, Vector2.Zero);

            for (int i = 0; i < Lists.activeProjectiles.Count; i++)
            {
                Lists.activeProjectiles[i].Update(gameTime);
                Lists.activeProjectiles[i].Draw(spriteBatch, Lists.activeProjectiles[i].SpriteID, Lists.activeProjectiles[i].Bounds, 0.2f, 0f, Vector2.Zero);
            }

            if (battleWon)
            {
                for (int i = 0; i < Lists.uiElements.Count; i++)
                {
                    if (Lists.uiElements[i].SpriteID == Textures.UI.endBattleUI)
                    {
                        spriteBatch.DrawString(Fonts.lucidaConsole16Regular, "You defeated " + battleEnemy.Name + "!", new Vector2(Lists.uiElements[i].Position.X + 123, Lists.uiElements[i].Position.Y + 5), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.182f);
                        spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Silver: " + silverReward, new Vector2(Lists.uiElements[i].Position.X + 160, Lists.uiElements[i].Position.Y + 135), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.182f);
                    }
                }
                for (int i = 0; i < Lists.battleSceneButtons.Count; i++)
                {
                    Lists.battleSceneButtons[i].Update(gameTime);
                    Lists.battleSceneButtons[i].Draw(spriteBatch, Lists.battleSceneButtons[i].SpriteID, Lists.battleSceneButtons[i].Bounds, 0.182f, 0f, Vector2.Zero);
                }
                for (int i = 0; i < loot.Count; i++)
                {
                    loot[i].Update(gameTime);
                    loot[i].Draw(spriteBatch, loot[i].SpriteID, loot[i].Bounds, 0.183f, 0f, Vector2.Zero);

                    if (MouseManager.mouse.mouseBounds.Intersects(loot[i].Bounds))
                    {
                        Vector2 position = new Vector2(MouseManager.mouse.GetMousePosition().X, MouseManager.mouse.GetMousePosition().Y - Textures.UI.itemInfoUI.Height);
                        spriteBatch.Draw(Textures.UI.itemInfoUI, new Rectangle((int)position.X, (int)position.Y, Textures.UI.itemInfoUI.Width, Textures.UI.itemInfoUI.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.184f);
                        spriteBatch.Draw(loot[i].SpriteID, new Rectangle((int)position.X + 3, (int)position.Y + 3, 87, 87), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.185f);
                        spriteBatch.DrawString(Fonts.lucidaConsole14Regular, loot[i].ItemName, new Vector2(position.X + 99, position.Y + 7), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.185f);

                        if (loot[i].ItemClass.Contains(GVar.ItemClassName.armor))
                        {
                            Armor armor = (Armor)loot[i];
                            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Armor: " + armor.ArmorValue.ToString(), new Vector2(position.X + 99, position.Y + 27), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.185f);
                        }
                        else if (loot[i].ItemClass.Contains(GVar.ItemClassName.weapon))
                        {
                            Weapon weapon = (Weapon)loot[i];
                            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Damage: " + weapon.Damage.ToString(), new Vector2(position.X + 99, position.Y + 27), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.185f);
                        }
                        else if (loot[i].ItemClass.Contains(GVar.ItemClassName.jewellry))
                        {
                            Jewellry jewl = (Jewellry)loot[i];
                            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, jewl.ItemName, new Vector2(position.X + 99, position.Y + 27), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.185f);
                            if (jewl.eternalCoinSlot.item != null)
                            {
                                EternalCoin EC = (EternalCoin)jewl.eternalCoinSlot.item;
                                spriteBatch.DrawString(Fonts.lucidaConsole14Regular, EC.ItemName, new Vector2(position.X + 99, position.Y + 47), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.185f);
                            }
                        }
                    }
                }
            }

            if (battleEnemy.Health > 0)
            {
                battleEnemy.Update(gameTime);
                battleEnemy.Draw(spriteBatch, battleEnemy.SpriteID, battleEnemy.Bounds, 0.2f, 0f, Vector2.Zero);
            }

            GVar.DrawBoundingBox(playerAttackRec, spriteBatch, Textures.Misc.pixel, 1, 0.2f, Color.Green);
            GVar.DrawBoundingBox(playerPort, spriteBatch, Textures.Misc.pixel, 1, 0.2f, Color.Green);
            GVar.DrawBoundingBox(enemyAttackRec, spriteBatch, Textures.Misc.pixel, 1, 0.2f, Color.Green);
            GVar.DrawBoundingBox(enemyPort, spriteBatch, Textures.Misc.pixel, 1, 0.2f, Color.Green);

            DrawPlayerInfo(spriteBatch);
            DrawEnemyInfo(spriteBatch);

            if (battleEnemy.Health > 0)
            {
                int timer = (int)enemyAttackTimer;
                spriteBatch.DrawString(Fonts.lucidaConsole14Regular, timer.ToString(), new Vector2(1200, 20), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
            }

            for (int i = 0; i < Lists.attackButtons.Count; i++)
            {
                Lists.attackButtons[i].Update(gameTime);
                Lists.attackButtons[i].Draw(spriteBatch, Lists.attackButtons[i].SpriteID, Lists.attackButtons[i].Bounds, 0.1801f, 0f, Vector2.Zero);
            }

            InventoryManager.DrawMiniInventory(spriteBatch, GVar.InventoryParentNames.character);
            InventoryManager.DrawMiniInventory(spriteBatch, GVar.InventoryParentNames.enemy);
        }
    }
}

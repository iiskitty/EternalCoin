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
                battlePlayer = new BattlePlayer(Dictionaries.availableAttacks[Lists.availableAttacksIDs[0]], new Vector2(100, 50), new Vector2(128, 128), Lists.entity[0].Name, "Alive", new Vector2(), Color.White, Lists.entity[0].Health, Lists.entity[0].Armour, Lists.entity[0].Damage);
                try
                {
                    foreach (string atkID in Lists.availableAttacksIDs)
                    {
                        GVar.LogDebugInfo("Attack ID: " + atkID, 1);
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
                        if (InventoryManager.enemyInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item == null)
                        {
                            Attack.AddEnemyAttack(item.Attacks);
                            if (battleEnemy == null)
                            {
                                battleEnemy = new BattleEnemy(Dictionaries.enemyAttacks[Lists.enemyAttackIDs[0]], new Vector2(1000, 50), new Vector2(128, 128), eNode["name"].InnerText, "Alive", Vector2.Zero, Color.White, 40f, 0f, 0f, 0);
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
                                battleEnemy = new BattleEnemy(Dictionaries.enemyAttacks[Lists.enemyAttackIDs[0]], new Vector2(1000, 50), new Vector2(128, 128), eNode["name"].InnerText, "Alive", Vector2.Zero, Color.White, 40f, 0f, 0f, 0);
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
                                battleEnemy = new BattleEnemy(Dictionaries.enemyAttacks[Lists.enemyAttackIDs[0]], new Vector2(1000, 50), new Vector2(128, 128), eNode["name"].InnerText, "Alive", Vector2.Zero, Color.White, 40f, 0f, 0f, 0);
                                enemyNextAttack = Lists.enemyAttackIDs[0];
                            }
                            battleEnemy.AddItemStats(item);
                            InventoryManager.enemyInventory.itemSlots[item.InventorySlot].item = item;
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
                    foreach (UIElement ui in Lists.uiElements)
                    {
                        if (ui.SpriteID == Textures.UI.endBattleUI)
                        {
                            itemPos = new Vector2(ui.Position.X + 14, ui.Position.Y + 56); 
                        }
                    }
                    foreach (Item item in loot)
                    {
                        item.Position = itemPos; //sets items position to the UI's position
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

            //adds attack animations to created battlePlayer
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
                    continue; //if left hand weapon and right hand weapon are not null (both hands have a weapon) dont create a punch button
                }
                Button atkButton = new Button(Dictionaries.textures[Lists.availableAttacksIDs[i]], new Vector2(), new Vector2(25, 25), Color.White, "Attack", Lists.availableAttacksIDs[i], 0f);
                Lists.attackButtons.Add(atkButton); //if one hand does not have a weapon, create the punch button
            }

            Vector2 pos = new Vector2();

            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.UI.battleUI)
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
                

            foreach (Object B in Lists.attackButtons)
            {
                B.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                
                if (MouseManager.mouseBounds.Intersects(B.Bounds) && InputManager.IsLMPressed() && battlePlayer.CurrentAnimation == GVar.AttackAnimStates.idle && !battleWon)
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                    try
                    {
                        currentAttackType = Dictionaries.availableAttacks[B.State].Type;
                        try
                        {
                            currentAttackProj = Dictionaries.availableAttacks[B.State].MagicProjectile;
                        }
                        catch (Exception e)
                        {
                            GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                        }
                        battlePlayer.SetAttack(Dictionaries.availableAttacks[B.State]);
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
                foreach (Object B in Lists.battleSceneButtons)
                {
                    B.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                    if (MouseManager.mouseBounds.Intersects(B.Bounds))
                    {
                        B.PlayAnimation(GVar.AnimStates.Button.mouseover);

                        if (InputManager.IsLMPressed() && battleWon)
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            if (B.Name == "Continue")
                            {
                                XmlDocument battleDoc = new XmlDocument();
                                foreach (Entity E in Lists.entity)
                                {
                                    battleDoc.Load("Content/GameFiles/" + E.Name + "/" + E.CurrentLocation[0].LocatoinFilePath);
                                    XmlNode hasEnemy = battleDoc.SelectSingleNode("/location");
                                    hasEnemy["hasenemy"].InnerText = "False";
                                    battleDoc.Save("Content/GameFiles/" + E.Name + "/" + E.CurrentLocation[0].LocatoinFilePath);
                                    GVar.location.HasEnemy = false;
                                }
                                battleWon = false;
                                foreach(UIElement ui in Lists.uiElements)
                                {
                                    if (ui.SpriteID == Textures.UI.endBattleUI)
                                    {
                                        ui.Draw = false;
                                    }
                                }
                                GVar.changeBackToGame = true;
                                Colours.drawBlackFade = true;
                                Colours.fadeIn = true;
                                GVar.silverMoney += silverReward;
                                for (int i = 0; i < loot.Count; i++)
                                {
                                    for (int j = 0; j < 40; j++)
                                    {
                                        if (InventoryManager.playerInventory.itemSlots[j].item == null)
                                        {
                                            Item item = ItemBuilder.BuildItem(loot[i]);
                                            Lists.playerItems.Add(item);
                                            InventoryManager.playerInventory.itemSlots[j].item = item;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (!MouseManager.mouseBounds.Intersects(B.Bounds) && B.CurrentAnimation != GVar.AnimStates.Button.def)
                    {
                        B.PlayAnimation(GVar.AnimStates.Button.def);
                    }
                }
                foreach (Item item in loot)
                {
                    item.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            }
        }

        public static void DrawPlayerInfo(SpriteBatch spriteBatch)
        {
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.UI.battleUI)
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
                if (ui.SpriteID == Textures.UI.battleUI)
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

            for (int i = 0; i < Lists.activeProjectiles.Count; i++)
            {
                Lists.activeProjectiles[i].Update(gameTime);
                Lists.activeProjectiles[i].Draw(spriteBatch, Lists.activeProjectiles[i].SpriteID, Lists.activeProjectiles[i].Bounds, 0.2f, 0f, Vector2.Zero);
            }

            if (battleWon)
            {
                foreach (UIElement ui in Lists.uiElements)
                {
                    if (ui.SpriteID == Textures.UI.endBattleUI)
                    {
                        spriteBatch.DrawString(Fonts.lucidaConsole16Regular, "You defeated " + battleEnemy.Name + "!", new Vector2(ui.Position.X + 123, ui.Position.Y + 5), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.182f);
                        spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Silver: " + silverReward, new Vector2(ui.Position.X + 160, ui.Position.Y + 135), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.182f);
                    }
                }
                foreach (Object B in Lists.battleSceneButtons)
                {
                    B.Update(gameTime);
                    B.Draw(spriteBatch, B.SpriteID, B.Bounds, 0.182f, 0f, Vector2.Zero);
                }
                foreach (Item item in loot)
                {
                    item.Update(gameTime);
                    item.Draw(spriteBatch, item.SpriteID, item.Bounds, 0.183f, 0f, Vector2.Zero);

                    if (MouseManager.mouseBounds.Intersects(item.Bounds))
                    {
                        Vector2 position = new Vector2(MouseManager.GetMousePosition().X, MouseManager.GetMousePosition().Y - Textures.UI.itemInfoUI.Height);
                        spriteBatch.Draw(Textures.UI.itemInfoUI, new Rectangle((int)position.X, (int)position.Y, Textures.UI.itemInfoUI.Width, Textures.UI.itemInfoUI.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.184f);
                        spriteBatch.Draw(item.SpriteID, new Rectangle((int)position.X + 3, (int)position.Y + 3, 87, 87), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.185f);
                        spriteBatch.DrawString(Fonts.lucidaConsole14Regular, item.ItemName, new Vector2(position.X + 99, position.Y + 7), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.185f);
                        
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

            foreach (Object B in Lists.attackButtons)
            {
                B.Update(gameTime);
                B.Draw(spriteBatch, B.SpriteID, B.Bounds, 0.1801f, 0f, Vector2.Zero);
            }

            InventoryManager.DrawMiniInventory(spriteBatch, InventoryManager.characterInventory);
            InventoryManager.DrawMiniInventory(spriteBatch, InventoryManager.enemyInventory);
        }
    }
}

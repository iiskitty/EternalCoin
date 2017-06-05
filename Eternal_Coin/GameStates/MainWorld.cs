using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Xml;

namespace Eternal_Coin
{
    public class MainWorld
    {
        /// <summary>
        /// Draw Game World.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        /// <param name="gameTime">GameTime for smooth movement</param>
        public static void DrawMainWorld(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //Draw location names.
            if (GVar.location != null)
                Location.DrawLocationInfo(spriteBatch, GVar.location);

            //draw npc info.
            NPC.DrawNPCInfo(spriteBatch, GVar.npc.Name, GVar.npc.Greeting);

            //Draw Quest UI's if active.
            for (int i = 0; i < Lists.uiElements.Count; i++)
            {
                //Draw quest info when clicked on a quest.
                if (Lists.uiElements[i].SpriteID == Textures.UI.questInfoUI && Lists.uiElements[i].Draw)
                {
                    Vector2 questInfoPosition = new Vector2(Lists.uiElements[i].Position.X + Lists.uiElements[i].SpriteID.Width - 195, Lists.uiElements[i].Position.Y + Lists.uiElements[i].SpriteID.Height - 120);//position for full quest description.
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, GVar.questInfo, questInfoPosition, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);//draw full quest description.
                }

                //Draw list of quests.
                if (Lists.uiElements[i].SpriteID == Textures.UI.questListUI && Lists.uiElements[i].Draw)
                {
                    Vector2 questInfoPosition = new Vector2(Lists.uiElements[i].SpriteID.Width - 275, Lists.uiElements[i].SpriteID.Height - 364);//position of the quest ShortDescription text.
                    Vector2 questCompletedPosition = new Vector2(Lists.uiElements[i].SpriteID.Width - 24, Lists.uiElements[i].SpriteID.Height - 366);//position of the cross or tick.

                    //cycle through active quests.
                    for (int j = 0; j < Lists.quests.Count; j++)
                    {
                        if (Lists.quests[j].Completed)//if quest is complete draw a tick.
                            spriteBatch.Draw(Textures.Misc.tick, new Rectangle((int)questCompletedPosition.X, (int)questCompletedPosition.Y, Textures.Misc.tick.Width, Textures.Misc.tick.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);//draw tick.
                        else if (!Lists.quests[j].Completed)//if quest not complete draw a cross.
                            spriteBatch.Draw(Textures.Misc.cross, new Rectangle((int)questCompletedPosition.X, (int)questCompletedPosition.Y, Textures.Misc.cross.Width, Textures.Misc.cross.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);//draw cross.
                        spriteBatch.DrawString(Fonts.lucidaConsole10Regular, Lists.quests[j].ShortDescription, questInfoPosition, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);//draw quest short description.
                        questInfoPosition.Y += 18;//move position down for next quest info to be drawn at.
                        questCompletedPosition.Y += 18;//move position down for next quests cross or tick to be drawn at.
                    }
                }
            }

            //draw quest info UI buttons.
            for (int i = 0; i < Lists.viewQuestInfoButtons.Count; i++)
            {
                //Update and Draw Quests UI Buttons.
                Lists.viewQuestInfoButtons[i].Update(gameTime);
                Lists.viewQuestInfoButtons[i].Draw(spriteBatch, Lists.viewQuestInfoButtons[i].SpriteID, Lists.viewQuestInfoButtons[i].Bounds, 0.2f, 0f, Vector2.Zero);

                if (MouseManager.mouseBounds.Intersects(Lists.viewQuestInfoButtons[i].Bounds))//check if mouse hovers over Quests UI Buttons.
                {
                    GVar.DrawBoundingBox(Lists.viewQuestInfoButtons[i].Bounds, spriteBatch, Textures.Misc.pixel, 1, 0.2f, Color.Green);//draw a box around quest info when moused over.
                }
            }

            //Draw main world buttons.
            for (int i = 0; i < Lists.mainWorldButtons.Count; i++)
            {
                //Update and Draw MainWorldButtons.
                Lists.mainWorldButtons[i].Update(gameTime);
                Lists.mainWorldButtons[i].Draw(spriteBatch, Lists.mainWorldButtons[i].SpriteID, Lists.mainWorldButtons[i].Bounds, 0.2f, 0f, Vector2.Zero);

                if (MouseManager.mouseBounds.Intersects(Lists.mainWorldButtons[i].Bounds))//check if mouse hovers over MainWorldButtons.
                {
                    Lists.mainWorldButtons[i].PlayAnimation(GVar.AnimStates.Button.mouseover);//change animation state to mouseover when mouse in hovering over button.
                }
                if (Lists.mainWorldButtons[i].CurrentAnimation == GVar.AnimStates.Button.mouseover && !MouseManager.mouseBounds.Intersects(Lists.mainWorldButtons[i].Bounds))//check if mouse is not hovering over MainWorldButtons
                {
                    Lists.mainWorldButtons[i].PlayAnimation(GVar.AnimStates.Button.def);//change animation state to default when mouse is not hovering over.
                }
                if (Lists.mainWorldButtons[i].Name == "MainMenu")///*TEMPORARY
                {
                    spriteBatch.DrawString(Fonts.lucidaConsole18Bold, "Main Menu", new Vector2(Lists.mainWorldButtons[i].Position.X + Lists.mainWorldButtons[i].Size.X / 4, Lists.mainWorldButtons[i].Position.Y + Lists.mainWorldButtons[i].Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.21f);//draw Main Menu on main menu button.
                }
                if (Lists.mainWorldButtons[i].Name == "Options")///*TEMPORARY
                {
                    spriteBatch.DrawString(Fonts.lucidaConsole18Bold, "Options", new Vector2(Lists.mainWorldButtons[i].Position.X + Lists.mainWorldButtons[i].Size.X / 4, Lists.mainWorldButtons[i].Position.Y + Lists.mainWorldButtons[i].Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.21f);//draw Options on options button.
                }
            }

            //update and draw world map.
            GVar.worldMap.Update(gameTime);
            GVar.worldMap.Draw(spriteBatch, GVar.worldMap.SpriteID, GVar.worldMap.Bounds, 0.1f, 0f, Vector2.Zero);

            //update and draw player, as well as current.
            for (int i = 0; i < Lists.entity.Count; i++)
            {
                //Update and Draw the Player.
                Lists.entity[i].Update(gameTime);
                Lists.entity[i].Draw(spriteBatch, Lists.entity[i].SpriteID, Lists.entity[i].Bounds, 0.172f, 0f, Vector2.Zero);

                //if the player is inside the current locations 'Port' or 'Dock' a small bounding box on top of each location node, 
                //only active for current location, to detect when the player arrives at the location.
                if (Lists.entity[i].Bounds.Intersects(Lists.entity[i].CurrentLocation.PlayerPort))
                {
                    //Update and Draw the location buttons(The eye, NPC and shop button, exit and enter button)
                    foreach (Object LB in Lists.locationButtons)
                    {
                        LB.Update(gameTime);
                        LB.Draw(spriteBatch, LB.SpriteID, LB.Bounds, 0.17f, 0f, Vector2.Zero);

                        if (MouseManager.mouseBounds.Intersects(LB.Bounds) && !GVar.gamePaused)//check if mouse hovers over any buttons.
                        {
                            LB.PlayAnimation(GVar.AnimStates.Button.mouseover);//change animation state to mouseover if the mouse is hovering over a button.
                        }
                        if (LB.CurrentAnimation == GVar.AnimStates.Button.mouseover && !MouseManager.mouseBounds.Intersects(LB.Bounds))//check if mouse doesn't hover over any buttons, and if buttons animation state is in mouseover.
                        {
                            LB.PlayAnimation(GVar.AnimStates.Button.def);//change the animation state to default if the mouse is not hovering over a button.
                        }
                    }
                }
                
                //update and draw the current locatio node.
                Lists.entity[i].CurrentLocation.Update(gameTime);
                Lists.entity[i].CurrentLocation.Draw(spriteBatch, Lists.entity[i].CurrentLocation.SpriteID, Lists.entity[i].CurrentLocation.Bounds, 0.17f, 0f, Vector2.Zero);

                //update and draw connecting location nodes of the current location node.
                foreach (Node conLocNode in Lists.entity[i].CurrentLocation.LocNodeConnections)
                {
                    conLocNode.Update(gameTime);
                    conLocNode.Draw(spriteBatch, conLocNode.SpriteID, conLocNode.Bounds, 0.17f, 0f, Vector2.Zero);
                }
            }
        }

        /// <summary>
        /// Update game world.
        /// </summary>
        /// <param name="gameTime">GameTime for smooth movement pretty sure.</param>
        public static void UpdateMainWorld(GameTime gameTime)
        {
            //check if MainWorldButtons for deletion.
            Button.CheckButtonsForDelete();

            //check if Q key has been pressed to open or close quests UI.
            if (InputManager.IsKeyPressed(Keys.Q))
            {
                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);//play button click sound.

                //cycle through UIElements to find the Quests UI.
                for (int i = 0; i < Lists.uiElements.Count; i++)
                {
                    if (Lists.uiElements[i].SpriteID == Textures.UI.questListUI)//if the Sprite is the quests UI.
                    {
                        if (!Lists.uiElements[i].Draw)//if the Quests UI is not active.
                        {
                            UI.DisplayQuests();//activate Quests UI.
                        }
                        else if (Lists.uiElements[i].Draw)//if the Quests UI is active.
                        {
                            UI.CloseQuestListUI();//deactivate Quests UI.

                            //cycle through MainWorldButtons.
                            for (int j = 0; j < Lists.mainWorldButtons.Count; j++)
                            {
                                if (Lists.mainWorldButtons[j].Name == "CloseQuestListUI")//if button is Close Quests UI.
                                {
                                    Lists.mainWorldButtons.RemoveAt(j);//delete the button.
                                    Lists.viewQuestInfoButtons.Clear();//clear Quests UI buttons.
                                }
                            }
                        }
                    }
                }
            }

            //check if I key has been pressed to open or close Inventory.
            if (InputManager.IsKeyPressed(Keys.I))
            {
                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);//play button click sound.
                Button closeInv = new Button(Textures.Misc.pixel, new Vector2(), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);//create Close Inventory Button.
                Lists.inventoryButtons.Add(closeInv);//Add button to InventoryButtons.
                GVar.currentGameState = GVar.GameState.inventory;//Set current GameState to inventory.
                GVar.previousGameState = GVar.GameState.game;//set previouse GameState to game(even though I don't use this)
            }

            //cycle through UIElements.
            for (int i = 0; i < Lists.uiElements.Count; i++)
            {
                if (InputManager.IsKeyPressed(Keys.Escape) && Lists.uiElements[i].SpriteID == Textures.UI.pauseUI && !Lists.uiElements[i].Draw)//if escape key is pressed and Sprite is Pause UI and Pause UI is not active.
                {
                    GVar.gamePaused = true;//set Pause Game Bool to true.
                    Lists.uiElements[i].Draw = true;//activate Pause UI
                    Button mainMenu = new Button(Textures.Misc.pixel, new Vector2(Lists.uiElements[i].Position.X + 93, Lists.uiElements[i].Position.Y + 93), new Vector2(322, 40), Color.Yellow, "MainMenu", "Alive", 0f);//create Main Menu Button.
                    Button options = new Button(Textures.Misc.pixel, new Vector2(Lists.uiElements[i].Position.X + 93, Lists.uiElements[i].Position.Y + 158), new Vector2(322, 40), Color.Yellow, "Options", "Alive", 0f);//create Options Button.
                    Lists.mainWorldButtons.Add(options);//add options button to MainWorldButtons.
                    Lists.mainWorldButtons.Add(mainMenu);//add menu button to MainWorldButtons.
                }
                else if (InputManager.IsKeyPressed(Keys.Escape) && Lists.uiElements[i].SpriteID == Textures.UI.pauseUI && Lists.uiElements[i].Draw)//if escape key is pressed and Sprite is Pause UI and Pause UI is active.
                {
                    GVar.gamePaused = false;//set Pause Game Bool to false.
                    Lists.uiElements[i].Draw = false;//deactivate Pause UI.
                    //cycle through MainWorldButtons.
                    for (int j = 0; j < Lists.mainWorldButtons.Count; j++)
                    {
                        if (Lists.mainWorldButtons[j].Name == "MainMenu" || Lists.mainWorldButtons[j].Name == "Options")//if button is Main Menu Button or Options Button.
                        {
                            Lists.mainWorldButtons[j].State = "delete";//delete the button.
                        }
                    }
                }
            }

            //update world map
            GVar.worldMap.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            //cycle through entities.
            for (int i = 0; i < Lists.entity.Count; i++)
            {
                ///*TEMPORARY GAME SAVE
                if (InputManager.IsKeyDown(Keys.LeftShift) && InputManager.IsKeyPressed(Keys.S))//left shift + s keys to save
                {
                    Save.SaveGame(GVar.savedGameLocation, Lists.entity[i], Lists.quests);//save the game
                }


                Vector2 viewQuestInfoButtonPosition = new Vector2();


                for (int j = 0; j < Lists.uiElements.Count; j++)
                {
                    if (Lists.uiElements[j].SpriteID == Textures.UI.questListUI)
                    {
                        viewQuestInfoButtonPosition = new Vector2(Lists.uiElements[j].SpriteID.Width - 277, Lists.uiElements[j].SpriteID.Height - 366);
                    }
                }


                for (int j = 0; j < Lists.viewQuestInfoButtons.Count; j++)
                {
                    Lists.viewQuestInfoButtons[j].Position = viewQuestInfoButtonPosition;
                    Lists.viewQuestInfoButtons[j].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    viewQuestInfoButtonPosition.Y += 18;
                    if (MouseManager.mouseBounds.Intersects(Lists.viewQuestInfoButtons[j].Bounds) && InputManager.IsLMPressed())
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        GVar.LogDebugInfo("ButtonClicked: " + Lists.viewQuestInfoButtons[j].Name, 2);
                        for (int ui = 0; ui < Lists.uiElements.Count; ui++)
                        {
                            if (Lists.uiElements[ui].SpriteID == Textures.UI.questInfoUI && !Lists.uiElements[ui].Draw)
                            {
                                Lists.mainWorldButtons.Add(new Button(Textures.Misc.pixel, new Vector2(), new Vector2(20, 20), Color.Red, "CloseQuestInfoUI", "Alive", 0f));
                                Lists.uiElements[ui].Draw = true;
                                GVar.questInfo = Lists.quests[j].Description;
                                GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);
                            }
                            else if (Lists.uiElements[ui].SpriteID == Textures.UI.questInfoUI && Lists.uiElements[ui].Draw)
                            {
                                GVar.questInfo = Lists.quests[j].Description;
                                GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);
                            }
                        }
                    }
                }
                for (int j = 0; j < Lists.mainWorldButtons.Count; j++)
                {
                    Updates.UpdateGameButtons(Lists.mainWorldButtons[j], Lists.entity[i], gameTime);
                    if (MouseManager.mouseBounds.Intersects(Lists.mainWorldButtons[j].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        GVar.LogDebugInfo("ButtonClicked: " + Lists.mainWorldButtons[j].Name, 2);
                        if (Lists.mainWorldButtons.Count > 0 && Lists.mainWorldButtons[j].Name == "QuestAcceptButton")
                        {
                            Quest.AcceptQuest(Lists.entity[i]);
                            Lists.mainWorldButtons.RemoveAt(j);
                            break;
                        }
                        else if (Lists.mainWorldButtons.Count > 0 && Lists.mainWorldButtons[j].Name == "HandInQuestButton")
                        {
                            Quest.HandInQuest(Lists.entity[i]);
                            Lists.mainWorldButtons.RemoveAt(j);
                        }
                        else if (Lists.mainWorldButtons[j].Name == "OpenShop")
                        {
                            Shop.LoadShopInventory(GVar.curLocNode);

                            Button closeInv = new Button(Textures.Misc.pixel, new Vector2(), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);
                            Lists.inventoryButtons.Add(closeInv);

                            Lists.mainWorldButtons.RemoveAt(j);

                            GVar.currentGameState = GVar.GameState.shop;
                            GVar.previousGameState = GVar.GameState.game;

                            UI.CloseNPCUI();
                        }
                        else if (Lists.mainWorldButtons[j].Name == "DisplayQuests")
                        {
                            UI.DisplayQuests();
                        }
                        else if (Lists.mainWorldButtons[j].Name == "DisplayInventory")
                        {
                            Button closeInv = new Button(Textures.Misc.pixel, new Vector2(), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);
                            Lists.inventoryButtons.Add(closeInv);
                            GVar.currentGameState = GVar.GameState.inventory;
                            GVar.previousGameState = GVar.GameState.game;
                        }
                        else if (Lists.mainWorldButtons[j].Name == "CloseQuestListUI")
                        {
                            UI.CloseQuestListUI();
                            Lists.mainWorldButtons.RemoveAt(j);
                            Lists.viewQuestInfoButtons.Clear();
                        }
                        else if (Lists.mainWorldButtons[j].Name == "CloseQuestInfoUI")
                        {
                            UI.CloseQuestInfoUI();
                            Lists.mainWorldButtons.RemoveAt(j);
                        }
                        else if (Lists.mainWorldButtons[j].Name == "CloseNPCUIButton")
                        {
                            Lists.mainWorldButtons.RemoveAt(j);
                            for (int k = 0; k < Lists.mainWorldButtons.Count; k++)
                            {
                                if (Lists.mainWorldButtons[k].Name == "OpenShop")
                                    Lists.mainWorldButtons.RemoveAt(k);
                            }
                            UI.CloseNPCUI();
                            break;
                        }
                    }
                    else if (MouseManager.mouseBounds.Intersects(Lists.mainWorldButtons[j].Bounds) && InputManager.IsLMPressed() && GVar.gamePaused)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        if (Lists.mainWorldButtons[j].Name == "MainMenu")
                        {
                            Save.SaveGame(GVar.savedGameLocation, Lists.entity[i], Lists.quests);
                            GVar.changeToMainMenu = true;
                            Colours.drawBlackFade = true;
                            Colours.fadeIn = true;
                            GVar.playerName = string.Empty;
                        }
                        else if (Lists.mainWorldButtons[j].Name == "Options")
                        {
                            Button backToGame = new Button(Textures.Misc.pixel, new Vector2(1130, 50), new Vector2(100, 50), Color.Yellow, "BackToGame", "Alive", 0f);
                            Lists.optionsButtons.Add(backToGame);
                            GVar.changeToOptions = true;
                            Colours.drawBlackFade = true;
                            Colours.fadeIn = true;
                        }
                    }
                }

                if (InputManager.IsKeyDown(Keys.LeftShift) && InputManager.IsKeyPressed(Keys.S))
                    Save.SaveGame(GVar.savedGameLocation, Lists.entity[i], Lists.quests);

                Lists.entity[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);


                if (Lists.entity[i].Bounds.Intersects(Lists.entity[i].CurrentLocation.PlayerPort))
                {
                    if (GVar.location.HasEnemy && !Colours.fadeIn && !GVar.changeToBattle && GVar.location.Searched)
                    {
                        Colours.fadeIn = true;
                        Colours.drawBlackFade = true;
                        GVar.changeToBattle = true;
                    }

                    Colours.UpdateMainAlphas(Lists.entity[i].CurrentLocation);

                    for (int j = 0; j < Lists.locationButtons.Count; j++)
                    {
                        Updates.UpdateGameButtons(Lists.locationButtons[j], Lists.entity[i], gameTime);

                        if (MouseManager.mouseBounds.Intersects(Lists.locationButtons[j].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            GVar.LogDebugInfo("ButtonClicked: " + Lists.locationButtons[j].Name, 2);
                            if (GVar.location != null && GVar.location.Searched)
                            {
                                if (Lists.locationButtons[j].Name == "NPCButton")
                                {
                                    for (int k = 0; k < Lists.mainWorldButtons.Count; k++)
                                    {
                                        if (Lists.mainWorldButtons[k].Name == "OpenShop")
                                        {
                                            Lists.mainWorldButtons[k].State = "delete";
                                        }
                                    }
                                    try
                                    {
                                        XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");
                                        GVar.npc = new NPC(locNPC[GVar.XmlTags.NPCTags.name].InnerText, "", Convert.ToBoolean(locNPC[GVar.XmlTags.NPCTags.hasquest].InnerText), Convert.ToBoolean(locNPC[GVar.XmlTags.QuestTags.questaccepted].InnerText), Convert.ToBoolean(locNPC[GVar.XmlTags.QuestTags.questfinished].InnerText), Convert.ToBoolean(locNPC[GVar.XmlTags.QuestTags.questcompleted].InnerText));
                                        locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting");

                                        if (GVar.npc.HasQuest && !GVar.npc.QuestAccepted)
                                        {
                                            GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.questunaccepted].InnerText;
                                            foreach (UIElement ui in Lists.uiElements)
                                            {
                                                if (ui.SpriteID == Textures.UI.NPCInfoUI && !ui.Draw)
                                                {
                                                    Button acceptQuest = new Button(Textures.Misc.pixel, new Vector2(ui.Position.X, ui.Position.Y + ui.Size.Y - Textures.Misc.pixel.Width), new Vector2(25, 15), Color.Green, "QuestAcceptButton", "Alive", 0f);
                                                    acceptQuest.PlayAnimation(GVar.AnimStates.Button.def);
                                                    Lists.mainWorldButtons.Add(acceptQuest);
                                                }
                                            }
                                        }
                                        else if (!GVar.npc.HasQuest && GVar.npc.QuestAccepted)
                                        {
                                            GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.questaccepted].InnerText;
                                        }
                                        else if (!GVar.npc.HasQuest && GVar.npc.QuestFinished)
                                        {
                                            GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.questfinished].InnerText;
                                            foreach (UIElement ui in Lists.uiElements)
                                            {
                                                if (ui.SpriteID == Textures.UI.NPCInfoUI && !ui.Draw)
                                                {
                                                    Button handInQuest = new Button(Textures.Misc.pixel, new Vector2(ui.Position.X, ui.Position.Y + ui.Size.Y - Textures.Misc.pixel.Width), new Vector2(25, 15), Color.Blue, "HandInQuestButton", "Alive", 0f);
                                                    handInQuest.PlayAnimation(GVar.AnimStates.Button.def);
                                                    Lists.mainWorldButtons.Add(handInQuest);
                                                }
                                            }
                                        }
                                        else if (!GVar.npc.HasQuest && GVar.npc.QuestCompleted)
                                        {
                                            GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.questcompleted].InnerText;
                                        }
                                        GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, GVar.npcTextWrapLength);

                                        locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");
                                        Quest.CheckAction(locNPC[GVar.XmlTags.Actions.talknpc].InnerText, Lists.entity[i].CurrentLocation);

                                        foreach (UIElement ui in Lists.uiElements)
                                        {
                                            if (ui.SpriteID == Textures.UI.NPCInfoUI && !ui.Draw)
                                            {
                                                Button closeNPCUI = new Button(Textures.Button.closeButton, new Vector2(ui.Position.X + ui.Size.X - Textures.Button.closeButton.Width, ui.Position.Y), new Vector2(35, 35), Color.White, "CloseNPCUIButton", "Alive", 0f);
                                                closeNPCUI.PlayAnimation(GVar.AnimStates.Button.def);
                                                Lists.mainWorldButtons.Add(closeNPCUI);
                                                ui.Draw = true;
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        GVar.npc = new NPC();
                                    }
                                }
                                else if (Lists.locationButtons[j].Name == "ShopButton")
                                {
                                    XmlNode shopKeep = GVar.curLocNode.SelectSingleNode("/location/shop");

                                    string greeting = Text.WrapText(Fonts.lucidaConsole14Regular, shopKeep["greeting"].InnerText, GVar.npcTextWrapLength);

                                    GVar.npc = new NPC(shopKeep["name"].InnerText, greeting, false, false, false, false);

                                    Button openShop = new Button(Textures.Misc.pixel, Vector2.Zero, new Vector2(25, 15), Color.Yellow, "OpenShop", "Alive", 0f);
                                    Lists.mainWorldButtons.Add(openShop);

                                    foreach (UIElement ui in Lists.uiElements)
                                    {
                                        if (ui.SpriteID == Textures.UI.NPCInfoUI && !ui.Draw)
                                        {
                                            Button closeNPCUI = new Button(Textures.Button.closeButton, new Vector2(ui.Position.X + ui.Size.X - Textures.Button.closeButton.Width, ui.Position.Y), new Vector2(35, 35), Color.White, "CloseNPCUIButton", "Alive", 0f);
                                            closeNPCUI.PlayAnimation(GVar.AnimStates.Button.def);
                                            Lists.mainWorldButtons.Add(closeNPCUI);
                                            ui.Draw = true;
                                        }
                                    }
                                }
                            }
                            if (GVar.location != null && !GVar.location.Searched)
                            {
                                if (Lists.locationButtons[j].Name == "LookEyeButton")
                                {
                                    if (Lists.entity[i].CurrentLocation.State.Contains("Sub"))
                                    {
                                        for (int k = 0; k < Lists.entity[i].CurrentLocation.MainLocNode.Count; k++)
                                        {
                                            Button exitLocationButton = new Button(Textures.Button.exitLocationButton, Lists.entity[i].CurrentLocation.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", "Alive", 0f);
                                            exitLocationButton.PlayAnimation(GVar.AnimStates.Button.def);
                                            Lists.locationButtons.Add(exitLocationButton);
                                            ReadXml.ReadLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation.MainLocNode[k]);
                                            GVar.location.Searched = true;
                                            Lists.entity[i].CurrentLocation.MainLocNode[k].Searched = true;
                                            XmlNode mainLocNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");
                                            mainLocNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.location.Searched.ToString();
                                            SaveXml.SaveLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation.MainLocNode[k]);
                                            GVar.location = null;
                                        }
                                    }
                                    ReadXml.ReadLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation);
                                    GVar.location.Searched = true;
                                    Lists.entity[i].CurrentLocation.Searched = true;
                                    XmlNode locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");
                                    locNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.location.Searched.ToString();

                                    locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");
                                    Quest.CheckAction(locNode[GVar.XmlTags.Actions.explore].InnerText, Lists.entity[i].CurrentLocation);

                                    SaveXml.SaveLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation);

                                    if (GVar.location.HasNPC)
                                    {
                                        Button npcButton = new Button(Textures.Button.npcButton, Lists.entity[i].CurrentLocation.Position, Vector.locationButtonSize, Color.White, "NPCButton", "Alive", 0f);
                                        npcButton.PlayAnimation(GVar.AnimStates.Button.def);
                                        Lists.locationButtons.Add(npcButton);
                                    }

                                    if (GVar.location.HasShop)
                                    {
                                        Button shopButton = new Button(Textures.Misc.pixel, Lists.entity[i].CurrentLocation.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", "Alive", 0f);
                                        shopButton.PlayAnimation(GVar.AnimStates.Button.def);
                                        Lists.locationButtons.Add(shopButton);
                                    }

                                    Lists.locationButtons.RemoveAt(j);
                                }
                            }
                        }
                    }
                }
                if (!Lists.entity[i].Bounds.Intersects(Lists.entity[i].CurrentLocation.PlayerPort))
                {
                    GVar.worldMap.SetMapSpeed(Lists.entity[i], Lists.entity[i].CurrentLocation);
                    GVar.worldMap.MapMovement((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                else
                {
                    GVar.worldMap.SetMapSpeed(Lists.entity[i], Lists.entity[i].CurrentLocation);
                    GVar.worldMap.MapMovement((float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                Lists.entity[i].CurrentLocation.HandleMovement(GVar.worldMap.Position);
                Lists.entity[i].CurrentLocation.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                int count = 0;

                if (Lists.entity[i].CurrentLocation.LocNodeConnections.Count > 0)
                    count = Lists.entity[i].CurrentLocation.LocNodeConnections.Count;
                else
                    count = 1;

                for (int j = 0; j < count; j++)
                {
                    if (Lists.entity[i].CurrentLocation.LocNodeConnections.Count > 0)
                        Lists.entity[i].CurrentLocation.LocNodeConnections[j].HandleMovement(GVar.worldMap.Position);

                    for (int k = 0; k < Lists.locationButtons.Count; k++)
                    {
                        if (MouseManager.mouseBounds.Intersects(Lists.locationButtons[k].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
                        {
                            //SoundManager.PlaySound(Dictionaries.sounds[GVar.PlaySound.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickLocButton", false);
                            GVar.LogDebugInfo("ButtonClicked: " + Lists.locationButtons[k].Name, 2);
                            if (Lists.locationButtons[k].Name == "EnterLocation")
                            {
                                UI.CloseNPCUI();

                                Colours.EnableFadeOutMap();

                                for (int l = 0; l < Lists.entity[i].CurrentLocation.SubLocNode.Count; l++)
                                {
                                    for (int slnc = 0; slnc < Lists.entity[i].CurrentLocation.SubLocNode[l].LocNodeConnections.Count; slnc++)
                                    {
                                        ReadXml.ReadLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation.SubLocNode[l].LocNodeConnections[slnc]);
                                        Lists.entity[i].CurrentLocation.SubLocNode[l].LocNodeConnections[slnc].ColourA = 5;
                                    }
                                    ReadXml.ReadLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation.SubLocNode[l]);
                                    XmlNode tempNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");
                                    Quest.CheckAction(tempNode[GVar.XmlTags.Actions.enter].InnerText, Lists.entity[i].CurrentLocation);

                                    WorldMap.SelectNewMap(Lists.entity[i].CurrentLocation.SubLocNode[l]);
                                    Lists.entity[i].CurrentLocation.SubLocNode[l].ColourA = 5;

                                    GVar.LogDebugInfo("LocationChange: " + Lists.entity[i].CurrentLocation.SubLocNode[l].Name, 2);
                                    GVar.npc = new NPC();
                                    Lists.locationButtons.Clear();
                                    Lists.entity[i].CurrentLocation = Lists.entity[i].CurrentLocation.SubLocNode[l];
                                    Button.CreateLocationButtons(Lists.entity[i].CurrentLocation);
                                }
                            }
                            else if (Lists.locationButtons[k].Name == "ExitLocation")
                            {
                                UI.CloseNPCUI();

                                Colours.EnableFadeOutMap();

                                for (int l = 0; l < Lists.entity[i].CurrentLocation.MainLocNode.Count; l++)
                                {
                                    for (int slnc = 0; slnc < Lists.entity[i].CurrentLocation.MainLocNode[l].LocNodeConnections.Count; slnc++)
                                    {
                                        ReadXml.ReadLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation.MainLocNode[l].LocNodeConnections[slnc]);
                                        Lists.entity[i].CurrentLocation.MainLocNode[l].LocNodeConnections[slnc].ColourA = 5;
                                    }
                                    ReadXml.ReadLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation.MainLocNode[l]);

                                    GVar.worldMap.SpriteID = Textures.Misc.worldMap;
                                    Lists.entity[i].CurrentLocation.MainLocNode[l].ColourA = 5;

                                    GVar.LogDebugInfo("LocationChange: " + Lists.entity[i].CurrentLocation.MainLocNode[l].Name, 2);
                                    GVar.npc = new NPC();
                                    Lists.locationButtons.Clear();
                                    if (Lists.entity[i].CurrentLocation.MainLocNode[l].State.Contains("Main"))
                                    {
                                        Button enterLocationButton = new Button(Textures.Button.enterLocationButton, Lists.entity[i].CurrentLocation.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", "Alive", 0f);
                                        enterLocationButton.PlayAnimation(GVar.AnimStates.Button.def);
                                        Lists.locationButtons.Add(enterLocationButton);
                                    }
                                    Lists.entity[i].CurrentLocation = Lists.entity[i].CurrentLocation.MainLocNode[l];
                                }
                            }
                        }
                    }

                    if (Lists.entity[i].CurrentLocation.LocNodeConnections.Count > 0)
                    {
                        if (MouseManager.mouseBounds.Intersects(Lists.entity[i].CurrentLocation.LocNodeConnections[j].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
                        {
                            UI.CloseNPCUI();
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clicklocnode]);
                            ReadXml.ReadLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation.LocNodeConnections[j]);
                            GVar.worldMap.SetMapSpeed(Lists.entity[i], Lists.entity[i].CurrentLocation.LocNodeConnections[j]);
                            foreach (LocationNode LN in Lists.entity[i].CurrentLocation.LocNodeConnections[j].LocNodeConnections)
                            {
                                if (LN.Name != Lists.entity[i].CurrentLocation.Name)
                                {
                                    LN.ColourA = 5;
                                }
                            }
                            GVar.LogDebugInfo("LocationChange: " + Lists.entity[i].CurrentLocation.LocNodeConnections[j].Name, 2);


                            GVar.npc = new NPC();
                            Lists.locationButtons.Clear();
                            Button.CreateLocationButtons(Lists.entity[i].CurrentLocation.LocNodeConnections[j]);

                            XmlNode tempNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");
                            Quest.CheckAction(tempNode[GVar.XmlTags.Actions.enter].InnerText, Lists.entity[i].CurrentLocation.LocNodeConnections[j]);

                            Lists.entity[i].CurrentLocation = Lists.entity[i].CurrentLocation.LocNodeConnections[j];
                            break;
                        }
                        Lists.entity[i].CurrentLocation.LocNodeConnections[j].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }

                }
                //}
            }
        }

        public static void LoadMainWorld()
        {
            string folderDir = "Content/GameFiles/" + GVar.playerName;
            string locationTemplate = "Content/LocationTemplates/" + GVar.storyName;

            if (!Directory.Exists(folderDir))
            {
                Directory.CreateDirectory(folderDir);

                DirectoryCopy(locationTemplate, folderDir, true);

                GVar.loadData = true;
            }

            Button quests = new Button(Textures.Misc.pixel, new Vector2(), new Vector2(50, 50), Color.Violet, "DisplayQuests", "Alive", 0f);
            Button inventory = new Button(Textures.Button.inventoryButton, new Vector2(), new Vector2(50, 50), Color.White, "DisplayInventory", "Alive", 0f);
            Lists.mainWorldButtons.Add(inventory);
            Lists.mainWorldButtons.Add(quests);

            for (int i = 0; i < Lists.entity.Count; i++)
            {
                //for (int j = 0; j < Lists.entity[i].CurrentLocation.Count; j++)
                //{
                for (int k = 0; k < Lists.entity[i].CurrentLocation.LocNodeConnections.Count; k++)
                {
                    ReadXml.ReadLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation.LocNodeConnections[k]);
                }
                ReadXml.ReadLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation);
                GameTime gameTime = new GameTime();
                Lists.entity[i].CurrentLocation.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                GVar.worldMap.SetMapPosition(Lists.entity[i].CurrentLocation);

                Button.CreateLocationButtons(Lists.entity[i].CurrentLocation);
                //}

            }
            GVar.npc = new NPC();
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}

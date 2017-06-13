using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            //for (int i = 0; i < Lists.entity.Count; i++)
            //{
            //Update and Draw the Player.
            GVar.player.Update(gameTime);
            GVar.player.Draw(spriteBatch, GVar.player.SpriteID, GVar.player.Bounds, 0.172f, 0f, Vector2.Zero);

            //if the player is inside the current locations 'Port' or 'Dock' a small bounding box on top of each location node, 
            //only active for current location, to detect when the player arrives at the location.
            if (GVar.player.Bounds.Intersects(GVar.player.CurrentLocation.PlayerPort))
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
            GVar.player.CurrentLocation.Update(gameTime);
            GVar.player.CurrentLocation.Draw(spriteBatch, GVar.player.CurrentLocation.SpriteID, GVar.player.CurrentLocation.Bounds, 0.17f, 0f, Vector2.Zero);

            //update and draw connecting location nodes of the current location node.
            foreach (Node conLocNode in GVar.player.CurrentLocation.LocNodeConnections)
            {
                conLocNode.Update(gameTime);
                conLocNode.Draw(spriteBatch, conLocNode.SpriteID, conLocNode.Bounds, 0.17f, 0f, Vector2.Zero);
            }
            //}
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

            ///*TEMPORARY GAME SAVE
            if (InputManager.IsKeyDown(Keys.LeftShift) && InputManager.IsKeyPressed(Keys.S))//left shift + s keys to save
            {
                Save.SaveGame(GVar.savedGameLocation, GVar.player, Lists.quests);//save the game
            }

            //Update ViewQuestButtons.
            Button.UpdateViewQuestButtons(gameTime);

            //Update MainWorldButtons.
            Button.UpdateMainWorldButtons(gameTime);

            //another temp save key combo, cant remember where the other one is.
            if (InputManager.IsKeyDown(Keys.LeftShift) && InputManager.IsKeyPressed(Keys.S))
                Save.SaveGame(GVar.savedGameLocation, GVar.player, Lists.quests);


            GVar.player.Update((float)gameTime.ElapsedGameTime.TotalSeconds);//update the player.

            //if the player intersects the "dock" or "port" of the current location.
            if (GVar.player.Bounds.Intersects(GVar.player.CurrentLocation.PlayerPort))
            {
                //if the location has an enemy and black fade in is disabled and change to battle is disabled and location has been searched...the fuck.
                if (GVar.location.HasEnemy && !Colours.fadeIn && !GVar.changeToBattle && GVar.location.Searched)
                {
                    Colours.fadeIn = true;
                    Colours.drawBlackFade = true;
                    GVar.changeToBattle = true;
                }

                Colours.UpdateMainAlphas(GVar.player.CurrentLocation);//update alpha colours of locations buttons and location nodes.

                //cycle through LocationButtons.
                for (int j = 0; j < Lists.locationButtons.Count; j++)
                {
                    Updates.UpdateGameButtons(Lists.locationButtons[j], GVar.player, gameTime);//upate the LocationButtons.

                    Button.CheckLocationButtonClick(j);//Check for click on LocationButtons.
                }
            }
            //if the player does not intersect the "dock" or "port" of the current location.
            if (!GVar.player.Bounds.Intersects(GVar.player.CurrentLocation.PlayerPort))
            {
                GVar.worldMap.SetMapSpeed(GVar.player, GVar.player.CurrentLocation);//set the maps movement speed(the whole map moves around the player, the player does not move at all)
                GVar.worldMap.MapMovement((float)gameTime.ElapsedGameTime.TotalSeconds);//move the map.
            }
            else//for what ever reason the above does not work.
            {
                GVar.worldMap.SetMapSpeed(GVar.player, GVar.player.CurrentLocation);//make sure the maps movement speed is set.
                GVar.worldMap.MapMovement((float)gameTime.ElapsedGameTime.TotalSeconds);//move the map.
            }

            //update the player.
            GVar.player.CurrentLocation.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            //keeps the current locations node in the right position when the map is moving.
            GVar.player.CurrentLocation.HandleMovement(GVar.worldMap.Position);
            int count = 0;//int counter.

            //if the current locations connecting location nodes count is greater than zero.
            if (GVar.player.CurrentLocation.LocNodeConnections.Count > 0)
                count = GVar.player.CurrentLocation.LocNodeConnections.Count;//set counter to ammount of connecting location nodes of the current location.
            else
                count = 1;//if there are no connecting locations set counter to 1???

            //for loop with counter as max.
            for (int j = 0; j < count; j++)
            {
                //again check for connecting locations.
                if (GVar.player.CurrentLocation.LocNodeConnections.Count > 0)
                    GVar.player.CurrentLocation.LocNodeConnections[j].HandleMovement(GVar.worldMap.Position);//keeps the current locations connecting locations nodes in the right position when the map is moving.
                
                //why am i doing this.
                if (GVar.player.CurrentLocation.LocNodeConnections.Count > 0)
                {
                    //if the mouse intersects and connting location nodes and left mouse is pressed and the game is not paused.
                    if (MouseManager.mouseBounds.Intersects(GVar.player.CurrentLocation.LocNodeConnections[j].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
                    {
                        UI.CloseNPCUI();//deactivate NPC UI.
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clicklocnode]);
                        ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.LocNodeConnections[j]);//read the current locations connceting locations xml file.
                        GVar.worldMap.SetMapSpeed(GVar.player, GVar.player.CurrentLocation.LocNodeConnections[j]);//set the movement speed of the map.

                        //cycle through the current location nodes connected locations connected location nodes.
                        for (int k = 0; k < GVar.player.CurrentLocation.LocNodeConnections[j].LocNodeConnections.Count; k++)
                        {
                            //if the current locations connected location connected locations name is not the current locations name.
                            if (GVar.player.CurrentLocation.LocNodeConnections[j].LocNodeConnections[k].Name != GVar.player.CurrentLocation.Name)
                            {
                                GVar.player.CurrentLocation.LocNodeConnections[j].LocNodeConnections[k].ColourA = 5;//set the alpha colour value of the node ready for fade in.
                            }
                        }
                        GVar.LogDebugInfo("LocationChange: " + GVar.player.CurrentLocation.LocNodeConnections[j].Name, 2);


                        GVar.npc = new NPC();//set NPC to nothing.
                        Lists.locationButtons.Clear();//clear the location buttons ready for next location.
                        Button.CreateLocationButtons(GVar.player.CurrentLocation.LocNodeConnections[j]);//create location buttons for new location.

                        XmlNode tempNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");//grab the actions tag of the next locations xml file.
                        Quest.CheckAction(tempNode[GVar.XmlTags.Actions.enter].InnerText, GVar.player.CurrentLocation.LocNodeConnections[j]);//check the action against any active quests.

                        GVar.player.CurrentLocation = GVar.player.CurrentLocation.LocNodeConnections[j];//set the current location to the current locations connected location.
                        break;
                    }
                    GVar.player.CurrentLocation.LocNodeConnections[j].Update((float)gameTime.ElapsedGameTime.TotalSeconds);//update connecting location nodes of the current location.
                }

            }
        }

        /// <summary>
        /// Loads the main world.
        /// </summary>
        public static void LoadMainWorld()
        {
            string folderDir = "Content/GameFiles/" + GVar.playerName;//set string for directory of current players game files.
            string locationTemplate = "Content/LocationTemplates/" + GVar.storyName;//set string for directory of current story being played.

            //check if player game files does not exist.
            if (!Directory.Exists(folderDir))
            {
                Directory.CreateDirectory(folderDir);//create directory for game files.

                DirectoryCopy(locationTemplate, folderDir, true);//copy the files for the story being played.

                GVar.loadData = true;//data loaded.
            }

            Button quests = new Button(Textures.Misc.pixel, new Vector2(), new Vector2(50, 50), Color.Violet, "DisplayQuests", "Alive", 0f);//create button for quests.
            Button inventory = new Button(Textures.Button.inventoryButton, new Vector2(), new Vector2(50, 50), Color.White, "DisplayInventory", "Alive", 0f);//create button for inventory.
            Lists.mainWorldButtons.Add(inventory);//add inventory button to MainWorldButtons.
            Lists.mainWorldButtons.Add(quests);//add quests button to MainWorldButtons.

            //cycle through the current locations connecting locations.
            for (int k = 0; k < GVar.player.CurrentLocation.LocNodeConnections.Count; k++)
            {
                ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.LocNodeConnections[k]);//read the current locations connecting locations xml file.
            }
            ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation);//read the current locations xml file.
            GameTime gameTime = new GameTime();//gameTime???
            GVar.player.CurrentLocation.Update((float)gameTime.ElapsedGameTime.TotalSeconds);//Update the current location once, sets positions and such.
            GVar.worldMap.SetMapPosition(GVar.player.CurrentLocation);//set the map position on the current locations position.

            Button.CreateLocationButtons(GVar.player.CurrentLocation);//create location buttons for the current locations.

            GVar.npc = new NPC();//set NPC to nothing.
        }

        /// <summary>
        /// Copies an entire directory from one place to another.
        /// I did not wright this code, but I don't remember where I got it from.
        /// </summary>
        /// <param name="sourceDirName">Directory the files are coming from.</param>
        /// <param name="destDirName">Directory the files are going to.</param>
        /// <param name="copySubDirs">copy sub directories or not.</param>
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

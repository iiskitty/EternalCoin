using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace Eternal_Coin
{
    public class GeneratedButton
    {
        public bool mouseOver = false;

        Rectangle bounds;

        Color colour;

        string name;
        string state;

        Vector2 position;
        Vector2 size;

        public GeneratedButton(Vector2 position, Color colour, string name, string state)
        {
            this.position = position;
            this.colour = colour;
            this.name = name;
            this.state = state;

            size = new Vector2(state.Length * 19, Textures.Button.middleLight.Height);
        }

        public void Update(float gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X + Textures.Button.leftLightSide.Width * 2, (int)size.Y);
        }

        public void DrawLightButton(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.Button.leftLightSide, new Rectangle((int)position.X, (int)position.Y, Textures.Button.leftLightSide.Width, Textures.Button.leftLightSide.Height), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.Draw(Textures.Button.middleLight, new Rectangle((int)position.X + Textures.Button.leftLightSide.Width, (int)position.Y, (int)size.X, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.Draw(Textures.Button.rightLightSide, new Rectangle((int)position.X + Textures.Button.leftLightSide.Width + (int)size.X, (int)position.Y, Textures.Button.rightLightSide.Width, Textures.Button.rightLightSide.Height), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.DrawString(Fonts.lucidaConsole24Regular, state, new Vector2(position.X + Textures.Button.leftLightSide.Width, position.Y + 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
        }

        public void DrawDarkButton(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.Button.leftDarkSide, new Rectangle((int)position.X, (int)position.Y, Textures.Button.leftDarkSide.Width, Textures.Button.leftDarkSide.Height), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.Draw(Textures.Button.middleDark, new Rectangle((int)position.X + Textures.Button.leftDarkSide.Width, (int)position.Y, (int)size.X, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.Draw(Textures.Button.rightDarkSide, new Rectangle((int)position.X + Textures.Button.leftDarkSide.Width + (int)size.X, (int)position.Y, Textures.Button.rightDarkSide.Width, Textures.Button.rightDarkSide.Height), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.DrawString(Fonts.lucidaConsole24Regular, state, new Vector2(position.X + Textures.Button.leftDarkSide.Width, position.Y + 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
        }

        public Rectangle Bounds { get { return bounds; } set { bounds = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Size { get { return size; } set { size = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string State { get { return state; } set { state = value; } }
    }

    public class Button : Object
    {
        public Button(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string name, string state, float worth)
            : base(spriteID, position, size, colour, name, state, worth)
        {
            FPS = 40;
            if (spriteID != null)
            {
                AddAnimation(1, 0, 0, GVar.AnimStates.Button.def, spriteID.Width / 2, spriteID.Height, Vector2.Zero);
                AddAnimation(1, 0, spriteID.Width / 2, GVar.AnimStates.Button.mouseover, spriteID.Width / 2, spriteID.Height, Vector2.Zero);
                PlayAnimation(GVar.AnimStates.Button.def);
            }
        }

        public override void Update(float gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public override void HandleMovement(Vector2 pos, float gameTime)
        {
            
        }

        public override void AnimationDone(string animation)
        {

        }

        public static void CreateLocationButtons(LocationNode node)
        {
            if (node.State.Contains("Sub"))
            {
                Button exitLocationButton = new Button(Textures.Button.exitLocationButton, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", GVar.States.Button.locationbutton, 0f);
                Lists.locationButtons.Add(exitLocationButton);

                if (GVar.location != null && !GVar.location.Searched)
                {
                    Button lookEyeButton = new Button(Textures.Button.lookEye, node.Position, Vector.lookEyeSize, Color.White, "LookEyeButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(lookEyeButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasNPC)
                {
                    Button npcButton = new Button(Textures.Button.npcButton, node.Position, Vector.locationButtonSize, Color.White, "NPCButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(npcButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasShop)
                {
                    Button shopButton = new Button(Textures.Misc.pixel, node.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(shopButton);
                }
            }
            else if (node.State.Contains("Main"))
            {
                Button enterLocationButton = new Button(Textures.Button.enterLocationButton, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", GVar.States.Button.locationbutton, 0f);
                Lists.locationButtons.Add(enterLocationButton);
            }
            else
            {
                if (GVar.location != null && !GVar.location.Searched)
                {
                    Button lookEyeButton = new Button(Textures.Button.lookEye, node.Position, new Vector2(Vector.lookEyeSize.X, Vector.lookEyeSize.Y), Color.White, "LookEyeButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(lookEyeButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasNPC)
                {
                    Button npcButton = new Button(Textures.Button.npcButton, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "NPCButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(npcButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasShop)
                {
                    Button shopButton = new Button(Textures.Misc.pixel, node.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(shopButton);
                }
            }

            foreach (Object b in Lists.locationButtons)
            {
                b.ColourA = 5;
            }
        }

        /// <summary>
        /// Check if button state has been set to "delete", if so delete it.
        /// </summary>
        public static void CheckButtonsForDelete()
        {
            //cycle through MainWorldButtons.
            for (int i = 0; i < Lists.mainWorldButtons.Count; i++)
            {
                if (Lists.mainWorldButtons[i].State == "delete")//check button state if set to "delete".
                {
                    Lists.mainWorldButtons.RemoveAt(i);//delete button.
                }
            }
        }

        /// <summary>
        /// Update MainWorldButtons.
        /// </summary>
        /// <param name="gameTime">GameTime for smooth movement.</param>
        public static void UpdateMainWorldButtons(GameTime gameTime)
        {
            //cycle through MainWorldButtons.
            for (int j = 0; j < Lists.mainWorldButtons.Count; j++)
            {
                Updates.UpdateGameButtons(Lists.mainWorldButtons[j], GVar.player, gameTime);//Updates main buttons that are seen all the time.

                //check for mouse intersecting MainWorldButton and left mouse click and game not paused.
                if (MouseManager.mouseBounds.Intersects(Lists.mainWorldButtons[j].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                    GVar.LogDebugInfo("ButtonClicked: " + Lists.mainWorldButtons[j].Name, 2);

                    //if button is Quest Accept Button.
                    if (Lists.mainWorldButtons[j].Name == "QuestAcceptButton")
                    {
                        Quest.AcceptQuest(GVar.player);//accept the quest.
                        Lists.mainWorldButtons.RemoveAt(j);//remove Quest Accept Button.
                        break;
                    }
                    //if button is Hand In Qeust Button.
                    else if (Lists.mainWorldButtons[j].Name == "HandInQuestButton")
                    {
                        Quest.HandInQuest(GVar.player);//hand in the quest.
                        Lists.mainWorldButtons.RemoveAt(j);//remove Hand In Quest Button.
                    }
                    //if button is Open Shop Button.
                    else if (Lists.mainWorldButtons[j].Name == "OpenShop")
                    {
                        Shop.LoadShopInventory(GVar.curLocNode);//load shop inventory of current location.

                        Button closeInv = new Button(Textures.Misc.pixel, new Vector2(), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);//create button to close inventory.
                        Lists.inventoryButtons.Add(closeInv);//add close button to InventoryButtons.

                        Lists.mainWorldButtons.RemoveAt(j);//remove Open Shop Button.

                        GVar.currentGameState = GVar.GameState.shop;//set current GameState to shop.
                        GVar.previousGameState = GVar.GameState.game;//set previous GameState to game.

                        UI.CloseNPCUI();//close NPC UI.
                    }
                    //if button is Display Quests.
                    else if (Lists.mainWorldButtons[j].Name == "DisplayQuests")
                    {
                        UI.DisplayQuests();//activate Quests UI.
                    }
                    //if button is Display Inventory.
                    else if (Lists.mainWorldButtons[j].Name == "DisplayInventory")
                    {
                        Button closeInv = new Button(Textures.Misc.pixel, new Vector2(), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);//create button to close inventory.
                        Lists.inventoryButtons.Add(closeInv);//add close button to InventoryButtons.
                        GVar.currentGameState = GVar.GameState.inventory;//set current GameState to inventory.
                        GVar.previousGameState = GVar.GameState.game;//set previous GameState to game.
                    }
                    //if button is Close Quests UI.
                    else if (Lists.mainWorldButtons[j].Name == "CloseQuestListUI")
                    {
                        UI.CloseQuestListUI();//deactiate Quests UI.
                        Lists.mainWorldButtons.RemoveAt(j);//remove Close Quests UI Button.
                        Lists.viewQuestInfoButtons.Clear();//delete Quest Info Buttons.
                    }
                    //if button is close Quest Info UI.
                    else if (Lists.mainWorldButtons[j].Name == "CloseQuestInfoUI")
                    {
                        UI.CloseQuestInfoUI();//deactivate Quest Info UI.
                        Lists.mainWorldButtons.RemoveAt(j);//remove Close Quest Info UI Button.
                    }
                    //if button is close NPC UI Button.
                    else if (Lists.mainWorldButtons[j].Name == "CloseNPCUIButton")
                    {
                        Lists.mainWorldButtons.RemoveAt(j);//delete Close NPC UI Button.
                        //cycle through MainWorldButtons.
                        for (int k = 0; k < Lists.mainWorldButtons.Count; k++)
                        {
                            if (Lists.mainWorldButtons[k].Name == "OpenShop")//if Button is Open Shop Button.
                                Lists.mainWorldButtons.RemoveAt(k);//delete Open Shop Button.
                        }
                        UI.CloseNPCUI();//deactivate NPC UI.
                        break;
                    }
                }
                //check for mouse intersecting MainWorldButtons and left mouse click and game is paused.
                else if (MouseManager.mouseBounds.Intersects(Lists.mainWorldButtons[j].Bounds) && InputManager.IsLMPressed() && GVar.gamePaused)
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);

                    //if button is Main Menu Button.
                    if (Lists.mainWorldButtons[j].Name == "MainMenu")
                    {
                        Save.SaveGame(GVar.savedGameLocation, GVar.player, Lists.quests);//Save the game.
                        GVar.changeToMainMenu = true;//change to menu bool to true.
                        Colours.drawBlackFade = true;//draw black fade in bool to true.
                        Colours.fadeIn = true;//fade in bool to true.
                        GVar.playerName = string.Empty;//reset players name.
                    }
                    //if button is Options Button.
                    else if (Lists.mainWorldButtons[j].Name == "Options")
                    {
                        Button backToGame = new Button(Textures.Misc.pixel, new Vector2(1130, 50), new Vector2(100, 50), Color.Yellow, "BackToGame", "Alive", 0f);//create Back to Game Button.
                        Lists.optionsButtons.Add(backToGame);//add Back to Game Button to Options Button.
                        GVar.changeToOptions = true;//change to options bool to true.
                        Colours.drawBlackFade = true;//draw black fade in bool to true.
                        Colours.fadeIn = true;//fade in bool to true.
                    }
                }
            }
        }

        /// <summary>
        /// Update View Quest Info Buttons.
        /// </summary>
        /// <param name="gameTime">GameTime for smooth movement.</param>
        public static void UpdateViewQuestButtons(GameTime gameTime)
        {
            //cycle through View Quest Info Buttons.
            for (int j = 0; j < Lists.viewQuestInfoButtons.Count; j++)
            {
                Lists.viewQuestInfoButtons[j].Update((float)gameTime.ElapsedGameTime.TotalSeconds);//update View Quest Info Buttons.

                if (MouseManager.mouseBounds.Intersects(Lists.viewQuestInfoButtons[j].Bounds) && InputManager.IsLMPressed())//check if mouse intersects View Quest Info Buttons and left mouse click.
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);

                    GVar.LogDebugInfo("ButtonClicked: " + Lists.viewQuestInfoButtons[j].Name, 2);

                    //cycle through UIElements.
                    for (int ui = 0; ui < Lists.uiElements.Count; ui++)
                    {
                        if (Lists.uiElements[ui].SpriteID == Textures.UI.questInfoUI && !Lists.uiElements[ui].Draw)//if Sprite is Quest Info UI and is not active.
                        {
                            Lists.mainWorldButtons.Add(new Button(Textures.Misc.pixel, new Vector2(), new Vector2(20, 20), Color.Red, "CloseQuestInfoUI", "Alive", 0f));//create Close Quest Info UI Button.
                            Lists.uiElements[ui].Draw = true;//activate View Quest Info UI.
                            GVar.questInfo = Lists.quests[j].Description;//set quest info to clicked active quest.
                            GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);//wrap quest info text to fit Quest Info UI.
                        }
                        else if (Lists.uiElements[ui].SpriteID == Textures.UI.questInfoUI && Lists.uiElements[ui].Draw)//if Sprite is Quest Info UI and is active.
                        {
                            GVar.questInfo = Lists.quests[j].Description;//set quest info to clicked active quest.
                            GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);//wrap quest info text to fit Quest Info UI.
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check for location buttons being clicked.
        /// </summary>
        /// <param name="i">index number from a for loop.</param>
        public static void CheckLocationButtonClick(int i)
        {
            //check if mouse intersects location button and left mouse click and game not paused.
            if (MouseManager.mouseBounds.Intersects(Lists.locationButtons[i].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
            {
                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                GVar.LogDebugInfo("ButtonClicked: " + Lists.locationButtons[i].Name, 2);

                //if button is Enter Location Button.
                if (Lists.locationButtons[i].Name == "EnterLocation")
                {
                    UI.CloseNPCUI();//deactivate NPC UI.

                    Colours.EnableFadeOutMap();//activate map fade out.

                    //cycle through sub location nodes of current location(only ever one i should change this).
                    for (int l = 0; l < GVar.player.CurrentLocation.SubLocNode.Count; l++)
                    {
                        //cycle through connected location nodes of the sub location node of the current location.
                        for (int slnc = 0; slnc < GVar.player.CurrentLocation.SubLocNode[l].LocNodeConnections.Count; slnc++)
                        {
                            ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.SubLocNode[l].LocNodeConnections[slnc]);//read the xml file of the connected location node of the sub location node of the current location node.
                            GVar.player.CurrentLocation.SubLocNode[l].LocNodeConnections[slnc].ColourA = 5;//set the alpha colour value of the connected location node of the sub location node of the current location ready for fade in.
                        }
                        ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.SubLocNode[l]);//read the xml file of the sub location node of the current location.
                        XmlNode tempNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");//grab the actions tag from the current locations xml file.
                        Quest.CheckAction(tempNode[GVar.XmlTags.Actions.enter].InnerText, GVar.player.CurrentLocation);//check actions for current active quests.

                        WorldMap.SelectNewMap(GVar.player.CurrentLocation.SubLocNode[l]);//set new map to the sub location of the current location.
                        GVar.player.CurrentLocation.SubLocNode[l].ColourA = 5;//set the alpha colour value of the sub location of the current location ready for fade in.

                        GVar.LogDebugInfo("LocationChange: " + GVar.player.CurrentLocation.SubLocNode[l].Name, 2);

                        GVar.npc = new NPC();//set NPC to nothing.
                        Lists.locationButtons.Clear();//clear location buttons ready for next location.
                        GVar.player.CurrentLocation = GVar.player.CurrentLocation.SubLocNode[l];//set the current location to the sub location of the current location.
                        CreateLocationButtons(GVar.player.CurrentLocation);//create location buttons for the new current location.
                    }
                }
                //if button is Exit Location Button.
                else if (Lists.locationButtons[i].Name == "ExitLocation")
                {
                    UI.CloseNPCUI();//deactivate NPC UI.

                    Colours.EnableFadeOutMap();//activate map fade out.

                    //cycle through current locations connected main locations(only ever one i should change this)
                    for (int l = 0; l < GVar.player.CurrentLocation.MainLocNode.Count; l++)
                    {
                        //cycle through current locations main locations connected main location nodes.
                        for (int slnc = 0; slnc < GVar.player.CurrentLocation.MainLocNode[l].LocNodeConnections.Count; slnc++)
                        {
                            ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode[l].LocNodeConnections[slnc]);//read current locations connected main locations main location node xml file.
                            GVar.player.CurrentLocation.MainLocNode[l].LocNodeConnections[slnc].ColourA = 5;//set the alpha colour value of the current locations main locations main location node ready for fade in.
                        }
                        ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode[l]);//read current location connected main location xml file.

                        GVar.worldMap.SpriteID = Textures.Misc.worldMap;//set current map to world map.
                        GVar.player.CurrentLocation.MainLocNode[l].ColourA = 5;//set the alpha colour value of the current locations connected main location ready for fade in.

                        GVar.LogDebugInfo("LocationChange: " + GVar.player.CurrentLocation.MainLocNode[l].Name, 2);

                        GVar.npc = new NPC();//set NPC to nothing.
                        Lists.locationButtons.Clear();//clear location buttons ready for next location.
                        //check if current locations connected main location is in fact a main location.
                        if (GVar.player.CurrentLocation.MainLocNode[l].State.Contains("Main"))
                        {
                            Button enterLocationButton = new Button(Textures.Button.enterLocationButton, GVar.player.CurrentLocation.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", "Alive", 0f);//create Enter Location Button.
                            enterLocationButton.PlayAnimation(GVar.AnimStates.Button.def);//set animation state of EnterLocationButton to default.
                            Lists.locationButtons.Add(enterLocationButton);//add EnterLocationButton to LocationButtons.
                        }
                        GVar.player.CurrentLocation = GVar.player.CurrentLocation.MainLocNode[l];//set current location to current locations connected main location.
                    }
                }

                //if current location is not null and current location has been searched.
                if (GVar.location != null && GVar.location.Searched)
                {
                    //if button is NPC Button.
                    if (Lists.locationButtons[i].Name == "NPCButton")
                    {
                        //cycle through MainWorldButtons.
                        for (int k = 0; k < Lists.mainWorldButtons.Count; k++)
                        {
                            //if button is Open Shop Button.
                            if (Lists.mainWorldButtons[k].Name == "OpenShop")
                            {
                                Lists.mainWorldButtons[k].State = "delete";//delete Open Shop Button.
                            }
                        }
                        try
                        {
                            XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");//grab npc tag from the current locations xml file.
                            GVar.npc = new NPC(locNPC[GVar.XmlTags.NPCTags.name].InnerText, "", Convert.ToBoolean(locNPC[GVar.XmlTags.NPCTags.hasquest].InnerText), Convert.ToBoolean(locNPC[GVar.XmlTags.QuestTags.questaccepted].InnerText), Convert.ToBoolean(locNPC[GVar.XmlTags.QuestTags.questfinished].InnerText), Convert.ToBoolean(locNPC[GVar.XmlTags.QuestTags.questcompleted].InnerText));//create new NPC with data from the current locations xml file.
                            locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting");//grab greeting tag inside npc tag from the current locations xml file. 


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
                            Quest.CheckAction(locNPC[GVar.XmlTags.Actions.talknpc].InnerText, GVar.player.CurrentLocation);

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
                    else if (Lists.locationButtons[i].Name == "ShopButton")
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
                    if (Lists.locationButtons[i].Name == "LookEyeButton")
                    {
                        if (GVar.player.CurrentLocation.State.Contains("Sub"))
                        {
                            for (int k = 0; k < GVar.player.CurrentLocation.MainLocNode.Count; k++)
                            {
                                Button exitLocationButton = new Button(Textures.Button.exitLocationButton, GVar.player.CurrentLocation.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", "Alive", 0f);
                                exitLocationButton.PlayAnimation(GVar.AnimStates.Button.def);
                                Lists.locationButtons.Add(exitLocationButton);
                                ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode[k]);
                                GVar.location.Searched = true;
                                GVar.player.CurrentLocation.MainLocNode[k].Searched = true;
                                XmlNode mainLocNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");
                                mainLocNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.location.Searched.ToString();
                                SaveXml.SaveLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode[k]);
                                GVar.location = null;
                            }
                        }
                        ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation);
                        GVar.location.Searched = true;
                        GVar.player.CurrentLocation.Searched = true;
                        XmlNode locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");
                        locNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.location.Searched.ToString();

                        locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");
                        Quest.CheckAction(locNode[GVar.XmlTags.Actions.explore].InnerText, GVar.player.CurrentLocation);

                        SaveXml.SaveLocationXmlFile(GVar.player, GVar.player.CurrentLocation);

                        if (GVar.location.HasNPC)
                        {
                            Button npcButton = new Button(Textures.Button.npcButton, GVar.player.CurrentLocation.Position, Vector.locationButtonSize, Color.White, "NPCButton", "Alive", 0f);
                            npcButton.PlayAnimation(GVar.AnimStates.Button.def);
                            Lists.locationButtons.Add(npcButton);
                        }

                        if (GVar.location.HasShop)
                        {
                            Button shopButton = new Button(Textures.Misc.pixel, GVar.player.CurrentLocation.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", "Alive", 0f);
                            shopButton.PlayAnimation(GVar.AnimStates.Button.def);
                            Lists.locationButtons.Add(shopButton);
                        }

                        Lists.locationButtons.RemoveAt(i);
                    }
                }
            }
        }
    }
}

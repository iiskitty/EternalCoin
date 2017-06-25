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

        /// <summary>
        /// Generates a button based on the string state that is passed in.
        /// </summary>
        /// <param name="position">Position of the button.</param>
        /// <param name="colour">Colour of the button.</param>
        /// <param name="name">Name of the button.</param>
        /// <param name="state">State of the button.</param>
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
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X + Textures.Button.leftLightSide.Width * 2, (int)size.Y);//sets rectangle(bounding box) based on position and size.
        }

        /// <summary>
        /// Draws light shaded button peices.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites.</param>
        public void DrawLightButton(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.Button.leftLightSide, new Rectangle((int)position.X, (int)position.Y, Textures.Button.leftLightSide.Width, Textures.Button.leftLightSide.Height), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.Draw(Textures.Button.middleLight, new Rectangle((int)position.X + Textures.Button.leftLightSide.Width, (int)position.Y, (int)size.X, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.Draw(Textures.Button.rightLightSide, new Rectangle((int)position.X + Textures.Button.leftLightSide.Width + (int)size.X, (int)position.Y, Textures.Button.rightLightSide.Width, Textures.Button.rightLightSide.Height), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.DrawString(Fonts.lucidaConsole24Regular, state, new Vector2(position.X + Textures.Button.leftLightSide.Width, position.Y + 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
        }

        /// <summary>
        /// Draws dark shaded button peices.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites.</param>
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
        /// <summary>
        /// Normal button.
        /// </summary>
        /// <param name="spriteID">Sprite/Texture of button.</param>
        /// <param name="position">Position of button.</param>
        /// <param name="size">Size of button.</param>
        /// <param name="colour">Colour of button.</param>
        /// <param name="name">Name of button.</param>
        /// <param name="state">State of button.</param>
        /// <param name="worth">Worth(The object Object has a variable Worth, I have never used this.)</param>
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
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);//sets rectangle(bounding box) based on position and size.
        }

        public override void HandleMovement(Vector2 pos, float gameTime)
        {
            
        }

        public override void AnimationDone(string animation)
        {

        }

        public static void CreateButtonsForLocations(LocationNode node)
        {
            //if current location is not nothing and has not been searched.
            if (GVar.location != null && !GVar.location.Searched)
            {
                Button lookEyeButton = new Button(Textures.Button.lookEye, node.Position, Vector.lookEyeSize, Color.White, "LookEyeButton", GVar.States.Button.locationbutton, 0f);//create Look Eye(search location) Button.
                Lists.locationButtons.Add(lookEyeButton);//add Look Eye Button to LocationButtons.
            }
            //if current location is not nothing and has been searched and has a NPC.
            if (GVar.location != null && GVar.location.Searched && GVar.location.HasNPC)
            {
                Button npcButton = new Button(Textures.Button.npcButton, node.Position, Vector.locationButtonSize, Color.White, "NPCButton", GVar.States.Button.locationbutton, 0f);//create NPC Button.
                Lists.locationButtons.Add(npcButton);//add NPC Button to LocationButtons.
            }
            //if current location is not nothing and has been searched and has a shop.
            if (GVar.location != null && GVar.location.Searched && GVar.location.HasShop)
            {
                Button shopButton = new Button(Textures.Misc.pixel, node.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", GVar.States.Button.locationbutton, 0f);//create a shop button.
                Lists.locationButtons.Add(shopButton);//add Shop Button to LocationButtons.
            }
        }

        /// <summary>
        /// Creates locations button based on the current location.
        /// </summary>
        /// <param name="node">current location.</param>
        public static void CreateLocationButtons(LocationNode node)
        {
            //if the current location is a Sub location(has an exit button to the main location).
            if (node.State.Contains("Sub"))
            {
                Button exitLocationButton = new Button(Textures.Button.exitLocationButton, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", GVar.States.Button.locationbutton, 0f);//create exit location Button.
                Lists.locationButtons.Add(exitLocationButton);//add exit location Button to LocationButtons.

                CreateButtonsForLocations(node);//create locations buttons
            }
            //if current location is a Main location(has an enter button to the sub locations).
            else if (node.State.Contains("Main"))
            {
                Button enterLocationButton = new Button(Textures.Button.enterLocationButton, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", GVar.States.Button.locationbutton, 0f);//create enter location button.
                Lists.locationButtons.Add(enterLocationButton);//add enter location button to LocationButtons.
            }
            //if current location is any other location(not Sub or Main).
            else
            {
                CreateButtonsForLocations(node);//create locations buttons.
            }

            //cycle through LocationButtons.
            for (int i = 0; i < Lists.locationButtons.Count; i++)
            {
                Lists.locationButtons[i].ColourA = 5;//set locations buttons alpha colour value ready for fade in.
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
                        Quest.AcceptQuest(GVar.player, GVar.npc.QuestID);//accept the quest.
                        Lists.mainWorldButtons.RemoveAt(j);//remove Quest Accept Button.
                        break;
                    }
                    //if button is Hand In Qeust Button.
                    else if (Lists.mainWorldButtons[j].Name == "HandInQuestButton")
                    {
                        Quest.HandInQuest(GVar.player, GVar.npc.QuestID);//hand in the quest.
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
                            if (Lists.mainWorldButtons[k].Name == "OpenShop" || Lists.mainWorldButtons[k].Name == "ViewQuests")//if Button is Open Shop Button.
                                Lists.mainWorldButtons.RemoveAt(k);//delete Open Shop Button.
                        }
                        Lists.NPCQuests.Clear();
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
        public static void UpdateQuestListButtons(GameTime gameTime)
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

                    //cycle through connected location nodes of the sub location node of the current location.
                    for (int slnc = 0; slnc < GVar.player.CurrentLocation.SubLocNode.LocNodeConnections.Count; slnc++)
                    {
                        ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.SubLocNode.LocNodeConnections[slnc]);//read the xml file of the connected location node of the sub location node of the current location node.
                        GVar.player.CurrentLocation.SubLocNode.LocNodeConnections[slnc].ColourA = 5;//set the alpha colour value of the connected location node of the sub location node of the current location ready for fade in.
                    }
                    ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.SubLocNode);//read the xml file of the sub location node of the current location.
                    XmlNode tempNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");//grab the actions tag from the current locations xml file.
                    Quest.CheckAction(tempNode[GVar.XmlTags.Actions.enter].InnerText, GVar.player.CurrentLocation);//check actions for current active quests.

                    WorldMap.SelectNewMap(GVar.player.CurrentLocation.SubLocNode);//set new map to the sub location of the current location.
                    GVar.player.CurrentLocation.SubLocNode.ColourA = 5;//set the alpha colour value of the sub location of the current location ready for fade in.

                    GVar.LogDebugInfo("LocationChange: " + GVar.player.CurrentLocation.SubLocNode.Name, 2);

                    GVar.npc = new NPC();//set NPC to nothing.
                    Lists.locationButtons.Clear();//clear location buttons ready for next location.
                    GVar.player.CurrentLocation = GVar.player.CurrentLocation.SubLocNode;//set the current location to the sub location of the current location.
                    CreateLocationButtons(GVar.player.CurrentLocation);//create location buttons for the new current location.
                }
                //if button is Exit Location Button.
                else if (Lists.locationButtons[i].Name == "ExitLocation")
                {
                    UI.CloseNPCUI();//deactivate NPC UI.

                    Colours.EnableFadeOutMap();//activate map fade out.

                    //cycle through current locations main locations connected main location nodes.
                    for (int slnc = 0; slnc < GVar.player.CurrentLocation.MainLocNode.LocNodeConnections.Count; slnc++)
                    {
                        ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode.LocNodeConnections[slnc]);//read current locations connected main locations main location node xml file.
                        GVar.player.CurrentLocation.MainLocNode.LocNodeConnections[slnc].ColourA = 5;//set the alpha colour value of the current locations main locations main location node ready for fade in.
                    }
                    ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode);//read current location connected main location xml file.

                    GVar.worldMap.SpriteID = Textures.Misc.worldMap;//set current map to world map.
                    GVar.player.CurrentLocation.MainLocNode.ColourA = 5;//set the alpha colour value of the current locations connected main location ready for fade in.

                    GVar.LogDebugInfo("LocationChange: " + GVar.player.CurrentLocation.MainLocNode.Name, 2);

                    GVar.npc = new NPC();//set NPC to nothing.
                    Lists.locationButtons.Clear();//clear location buttons ready for next location.
                    
                    //check if current locations connected main location is in fact a main location.
                    if (GVar.player.CurrentLocation.MainLocNode.State.Contains("Main"))
                    {
                        Button enterLocationButton = new Button(Textures.Button.enterLocationButton, GVar.player.CurrentLocation.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", "Alive", 0f);//create Enter Location Button.
                        enterLocationButton.PlayAnimation(GVar.AnimStates.Button.def);//set animation state of EnterLocationButton to default.
                        Lists.locationButtons.Add(enterLocationButton);//add EnterLocationButton to LocationButtons.
                    }
                    GVar.player.CurrentLocation = GVar.player.CurrentLocation.MainLocNode;//set current location to current locations connected main location.
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
                            Lists.NPCQuestButtons.Clear();
                            Lists.NPCQuests.Clear();
                            XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");//grab npc tag from the current locations xml file.
                            XmlNode locNPCGreeting = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting");
                            GVar.npc = new NPC(locNPC[GVar.XmlTags.NPCTags.name].InnerText, string.Empty, Convert.ToBoolean(locNPC[GVar.XmlTags.NPCTags.hasquest].InnerText), locNPC[GVar.XmlTags.NPCTags.currentquest].InnerText);//create new NPC with data from the current locations xml file.
                            GVar.npc.Greeting = locNPCGreeting[GVar.XmlTags.NPCTags.Greetings.normalgreeting].InnerText;
                            GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, 500);

                            XmlNodeList quests = GVar.curLocNode.DocumentElement.SelectNodes("/location/npc/quest");

                            for (int j = 0; j < quests.Count; j++)
                            {
                                if (!Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.unlocked].InnerText))
                                {
                                    for(int k = 0; k < Lists.completedQuests.Count; k++)
                                    {
                                        if (quests[j][GVar.XmlTags.QuestTags.unlockrequirement].InnerText == Lists.completedQuests[k])
                                        {
                                            quests[j][GVar.XmlTags.QuestTags.unlocked].InnerText = "true";
                                        }
                                    }
                                }
                                Lists.NPCQuestButtons.Add(new Button(Textures.Misc.pixel, Vector2.Zero, new Vector2(150, 40), Color.LightBlue, quests[j][GVar.XmlTags.QuestTags.shortdescription].InnerText, quests[j][GVar.XmlTags.QuestTags.questid].InnerText, 0f));
                                Lists.NPCQuests.Add(new Quest(quests[j][GVar.XmlTags.QuestTags.questid].InnerText, quests[j][GVar.XmlTags.QuestTags.description].InnerText, quests[j][GVar.XmlTags.QuestTags.shortdescription].InnerText, quests[j][GVar.XmlTags.QuestTags.completingaction].InnerText, quests[j][GVar.XmlTags.QuestTags.completinglocation].InnerText, Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.unlocked].InnerText), quests[j][GVar.XmlTags.QuestTags.unlockrequirement].InnerText, Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.accepted].InnerText), Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.completed].InnerText), Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.handedin].InnerText), Quest.GetLocationFilePath(GVar.player)));
                            }

                            for (int j = 0; j < Lists.uiElements.Count; j++)
                            {
                                //if Sprite is NPC Info UI && is not active.
                                if (Lists.uiElements[j].SpriteID == Textures.UI.NPCInfoUI && !Lists.uiElements[j].Draw)
                                {
                                    Button closeNPCUI = new Button(Textures.Button.closeButton, new Vector2(Lists.uiElements[j].Position.X + Lists.uiElements[j].Size.X - Textures.Button.closeButton.Width, Lists.uiElements[j].Position.Y), new Vector2(35, 35), Color.White, "CloseNPCUIButton", "Alive", 0f);//create close NPC UI Button.
                                    closeNPCUI.PlayAnimation(GVar.AnimStates.Button.def);//set animation state of button to default.
                                    Button viewQuests = new Button(Textures.Misc.pixel, new Vector2(Lists.uiElements[j].Position.X + Lists.uiElements[j].Size.X - 50, Lists.uiElements[j].Position.Y + Lists.uiElements[j].Size.Y - 20), new Vector2(50, 20), Color.LightBlue, "ViewQuests", "Alive", 0f);
                                    viewQuests.PlayAnimation(GVar.AnimStates.Button.def);
                                    Lists.mainWorldButtons.Add(closeNPCUI);//add close NPC UI button to MainWorldButton.
                                    Lists.mainWorldButtons.Add(viewQuests);
                                    Lists.uiElements[j].Draw = true;//activate NPC Info UI.
                                }
                            }

                            #region old quest stuff
                            //if(locNPC[GVar.XmlTags.NPCTags.currentquest].InnerText == "QUESTID")
                            //{
                            //    bool questToAccept = false;

                            //    for (int j = 0; j < quests.Count; j++)
                            //    {
                            //        if(!Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.unlocked].InnerText))
                            //        {
                            //            for (int k = 0; k < Lists.completedQuests.Count; k++)
                            //            {
                            //                if (quests[j][GVar.XmlTags.QuestTags.unlockrequirement].InnerText == Lists.completedQuests[k])
                            //                {
                            //                    quests[j][GVar.XmlTags.QuestTags.unlocked].InnerText = "true";
                            //                }
                            //            }
                            //        }
                            //        if (Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.unlocked].InnerText) && !Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.accepted].InnerText))
                            //        {
                            //            GVar.npc.QuestID = quests[j][GVar.XmlTags.QuestTags.questid].InnerText;
                            //            locNPCGreeting = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting/questid/" + GVar.npc.QuestID);
                            //            GVar.npc.Greeting = locNPCGreeting[GVar.XmlTags.NPCTags.Greetings.questunaccepted].InnerText;
                            //            questToAccept = true;
                            //            break;
                            //        }
                            //    }
                            //    if (questToAccept)
                            //    {
                            //        //cycle through UIElements.
                            //        for (int j = 0; j < Lists.uiElements.Count; j++)
                            //        {
                            //            //if Sprite is NPC Info UI and is not active.
                            //            if (Lists.uiElements[j].SpriteID == Textures.UI.NPCInfoUI && !Lists.uiElements[j].Draw)
                            //            {
                            //                Button acceptQuest = new Button(Textures.Misc.pixel, new Vector2(Lists.uiElements[j].Position.X, Lists.uiElements[j].Position.Y + Lists.uiElements[j].Size.Y - Textures.Misc.pixel.Width), new Vector2(25, 15), Color.Green, "QuestAcceptButton", "Alive", 0f);//create Accept Quest Button.
                            //                acceptQuest.PlayAnimation(GVar.AnimStates.Button.def);//set animation sate to default.
                            //                Lists.mainWorldButtons.Add(acceptQuest);//add Accept Quest Button to MainWorldButtons.
                            //            }
                            //        }
                            //    }
                            //}
                            ////if NPC has a quest and the quest had been accepted.
                            //else if (GVar.npc.HasQuest && locNPC[GVar.XmlTags.NPCTags.currentquest].InnerText != "QUESTID")
                            //{
                            //    //GVar.npc.Greeting = locNPC["greetings/questid/" + locNPC[GVar.XmlTags.NPCTags.currentquest].InnerText + "/" + GVar.XmlTags.NPCTags.Greetings.questaccepted].InnerText;//set NPC's greeting appropriately.
                            //    locNPCGreeting = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting/questid/" + locNPC[GVar.XmlTags.NPCTags.currentquest].InnerText);
                            //    GVar.npc.Greeting = locNPCGreeting[GVar.XmlTags.NPCTags.Greetings.questaccepted].InnerText;

                            //    for (int j = 0; j < quests.Count; j++)
                            //    {
                            //        if (quests[j][GVar.XmlTags.QuestTags.questid].InnerText == locNPC[GVar.XmlTags.NPCTags.currentquest].InnerText)
                            //        {
                            //            if (Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.completed].InnerText))
                            //            {
                            //                //GVar.npc.Greeting = locNPC["greetings/questid/" + locNPC[GVar.XmlTags.NPCTags.currentquest].InnerText + "/" + GVar.XmlTags.NPCTags.Greetings.questfinished].InnerText;
                            //                GVar.npc.Greeting = locNPCGreeting[GVar.XmlTags.NPCTags.Greetings.questfinished].InnerText;

                            //                //cycle through UIElements.
                            //                for (int k = 0; k < Lists.uiElements.Count; k++)
                            //                {
                            //                    //if Sprite is NPC Info UI and is not active.
                            //                    if (Lists.uiElements[k].SpriteID == Textures.UI.NPCInfoUI && !Lists.uiElements[k].Draw)
                            //                    {
                            //                        Button handInQuest = new Button(Textures.Misc.pixel, new Vector2(Lists.uiElements[k].Position.X, Lists.uiElements[k].Position.Y + Lists.uiElements[k].Size.Y - Textures.Misc.pixel.Width), new Vector2(25, 15), Color.Blue, "HandInQuestButton", "Alive", 0f);//create Hand In Quest Button.
                            //                        handInQuest.PlayAnimation(GVar.AnimStates.Button.def);//set animation state to default.
                            //                        Lists.mainWorldButtons.Add(handInQuest);//add button to MainWorldButtons.
                            //                    }
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                            ////if NPC does not have a quest and quest is completed.
                            //if (GVar.npc.Greeting == String.Empty)
                            //{
                            //    locNPCGreeting = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting");
                            //    GVar.npc.Greeting = locNPCGreeting[GVar.XmlTags.NPCTags.Greetings.normalgreeting].InnerText;
                            //}
                            //GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, GVar.npcTextWrapLength);//wrap NPC's greeting text to fit in NPC Info UI/.

                            ////grab actions tag from current locations xml file.
                            //locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");
                            ////check action(talk to npc) against any active quest.
                            //Quest.CheckAction(locNPC[GVar.XmlTags.Actions.talknpc].InnerText, GVar.player.CurrentLocation);

                            ////cycle through UIElements.
                            //for (int k = 0; k < Lists.uiElements.Count; k++)
                            //{
                            //    //if Sprite is NPC Info UI && is not active.
                            //    if (Lists.uiElements[k].SpriteID == Textures.UI.NPCInfoUI && !Lists.uiElements[k].Draw)
                            //    {
                            //        Button closeNPCUI = new Button(Textures.Button.closeButton, new Vector2(Lists.uiElements[k].Position.X + Lists.uiElements[k].Size.X - Textures.Button.closeButton.Width, Lists.uiElements[k].Position.Y), new Vector2(35, 35), Color.White, "CloseNPCUIButton", "Alive", 0f);//create close NPC UI Button.
                            //        closeNPCUI.PlayAnimation(GVar.AnimStates.Button.def);//set animation state of button to default.
                            //        Lists.mainWorldButtons.Add(closeNPCUI);//add close NPC UI button to MainWorldButton.
                            //        Lists.uiElements[k].Draw = true;//activate NPC Info UI.
                            //    }
                            //}
                            #endregion
                        }
                        catch (Exception e)
                        {
                            GVar.npc = new NPC();//set NPC to nothing if anything fails.
                            GVar.LogDebugInfo(e.ToString(), 1);
                        }
                    }
                    //if button is Shop Button.
                    else if (Lists.locationButtons[i].Name == "ShopButton")
                    {
                        XmlNode shopKeep = GVar.curLocNode.SelectSingleNode("/location/shop");//grab shop tag from current locations xml file.

                        string greeting = Text.WrapText(Fonts.lucidaConsole14Regular, shopKeep["greeting"].InnerText, GVar.npcTextWrapLength);//wrap ShopKeeps greeting to fit in UI.

                        GVar.npc = new NPC(shopKeep["name"].InnerText, greeting, false, "QUESTID");//create NPC(ShopKeep) with name and greeting.

                        Button openShop = new Button(Textures.Misc.pixel, Vector2.Zero, new Vector2(25, 15), Color.Yellow, "OpenShop", "Alive", 0f);//create OpenShop Button.
                        
                        Lists.mainWorldButtons.Add(openShop);//add Button to MainWorldButtons.

                        //cycle throughh UIElements.
                        for (int k = 0; k < Lists.uiElements.Count; k++)
                        {
                            //if Sprite is NPC Info UI and is not active.
                            if (Lists.uiElements[k].SpriteID == Textures.UI.NPCInfoUI && !Lists.uiElements[k].Draw)
                            {
                                Button closeNPCUI = new Button(Textures.Button.closeButton, new Vector2(Lists.uiElements[k].Position.X + Lists.uiElements[k].Size.X - Textures.Button.closeButton.Width, Lists.uiElements[k].Position.Y), new Vector2(35, 35), Color.White, "CloseNPCUIButton", "Alive", 0f);//create close NPC Info UI Button.
                                closeNPCUI.PlayAnimation(GVar.AnimStates.Button.def);//set animation state for button to default.
                                Lists.mainWorldButtons.Add(closeNPCUI);//add button to MainWorldButton.
                                Lists.uiElements[k].Draw = true;//activate NPC Info UI.
                            }
                        }
                    }
                }

                //if location in not nothing and has not been searched.
                if (GVar.location != null && !GVar.location.Searched)
                {
                    //if button is LookEyeButton.
                    if (Lists.locationButtons[i].Name == "LookEyeButton")
                    {
                        if (GVar.player.CurrentLocation.State.Contains("Sub"))//if location is a sub location.
                        {
                            Button exitLocationButton = new Button(Textures.Button.exitLocationButton, GVar.player.CurrentLocation.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", "Alive", 0f);//create exit location button.
                            exitLocationButton.PlayAnimation(GVar.AnimStates.Button.def);//set animation state of exit location button to default.
                            Lists.locationButtons.Add(exitLocationButton);//add exit location button to LocationButtons.
                            ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode);//read current locations connectiong main locations xml file.
                            GVar.location.Searched = true;//i have two fucking locations from the one location.
                            GVar.player.CurrentLocation.MainLocNode.Searched = true;//uh
                            XmlNode mainLocNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");//grab location tag from current locations xml file.
                            mainLocNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.location.Searched.ToString();//set the searched tag to true.
                            SaveXml.SaveLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode);//save the xml file.
                            GVar.location = null;//set location to null;
                        }
                        ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation);//read current locations xml file.
                        GVar.location.Searched = true;//what the fuck am I doing here with two locations???????????????
                        GVar.player.CurrentLocation.Searched = true;//huh?
                        XmlNode locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");//grab location tag from current locations xml file.
                        locNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.location.Searched.ToString();//set searched tag to true.

                        locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");//grab action tag from current locations xml file.
                        Quest.CheckAction(locNode[GVar.XmlTags.Actions.explore].InnerText, GVar.player.CurrentLocation);//check action against any active quests.

                        SaveXml.SaveLocationXmlFile(GVar.player, GVar.player.CurrentLocation);//save current locations xml file.

                        //if location has a NPC.
                        if (GVar.location.HasNPC)
                        {
                            Button npcButton = new Button(Textures.Button.npcButton, GVar.player.CurrentLocation.Position, Vector.locationButtonSize, Color.White, "NPCButton", "Alive", 0f);//create NPC button.
                            npcButton.PlayAnimation(GVar.AnimStates.Button.def);//set NPC Button animation state to default.
                            Lists.locationButtons.Add(npcButton);//add NPC Button to LocationButtong.
                        }

                        //if location has a shop.
                        if (GVar.location.HasShop)
                        {
                            Button shopButton = new Button(Textures.Misc.pixel, GVar.player.CurrentLocation.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", "Alive", 0f);//create Shop Button.
                            shopButton.PlayAnimation(GVar.AnimStates.Button.def);//set Shop Button animation state to default.
                            Lists.locationButtons.Add(shopButton);//add Shop Button to LocationButtons.
                        }

                        Lists.locationButtons.RemoveAt(i);//delete the LookEyeButton.
                    }
                }
            }
        }
    }
}

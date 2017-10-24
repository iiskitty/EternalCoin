using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace Eternal_Coin
{
    public class DisplayPicture
    {
        public Texture2D displayPic;
        public string displayPicID;

        public Vector2 position;

        public DisplayPicture(string ID, Texture2D pic)
        {
            displayPic = pic;
            displayPicID = ID;
            position = new Vector2();
        }
    }

    public class SavedGame
    {
        public string displayPicID;

        public string name;
        public string story;
        public string location;

        public Vector2 startPosition;

        public Button loadGame;
        public Button deleteGame;

        public List<Button> buttons;

        public SavedGame(string displayPicID, string name, string story, string location, Vector2 startPosition)
        {
            this.displayPicID = displayPicID;
            this.name = name;
            this.story = story;
            this.location = location;
            this.startPosition = startPosition;

            buttons = new List<Button>();

            loadGame = new Button(Textures.Button.loadButton, new Vector2(startPosition.X + 953, startPosition.Y + 12), new Vector2(Textures.Button.loadButton.Width / 2, Textures.Button.loadButton.Height), Color.White, "LoadGame", name, 0f);
            deleteGame = new Button(Textures.Button.deleteButton, new Vector2(startPosition.X + 1087, startPosition.Y + 12), new Vector2(Textures.Button.deleteButton.Width / 2, Textures.Button.deleteButton.Height), Color.White, "DeleteGame", name, 0f);
            buttons.Add(loadGame);
            buttons.Add(deleteGame);
        }
    }

    public class CreateCharacter
    {
        public static void UpdateCreateCharacter(GameTime gameTime)
        {

            for (int i = 0; i < Lists.chooseCharacterButtons.Count; i++)
            {
                Lists.chooseCharacterButtons[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                if (MouseManager.mouse.mouseBounds.Intersects(Lists.chooseCharacterButtons[i].Bounds) && InputManager.IsLMPressed())
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                    GVar.LogDebugInfo("ButtonClicked: " + Lists.chooseCharacterButtons[i].Name, 2);
                    
                    if (Lists.chooseCharacterButtons[i].Name == "NewCharacter")
                    {
                        GVar.creatingCharacter = true;
                        Lists.chooseCharacterButtons.Clear();
                        for (int j = 0; j < Lists.uiElements.Count; j++)
                        {
                            if (Lists.uiElements[j].SpriteID == Textures.UI.newGameUIBorder)
                            {
                                Button chooseDP = new Button(Textures.Misc.pixel, new Vector2(Lists.uiElements[j].Position.X + 3, Lists.uiElements[j].Position.Y + 3), Vector.newGameDPSize, Color.FromNonPremultiplied(0, 0, 0, 0), "ChooseDP", "Alive", 0f);
                                Button startGame = new Button(Textures.Button.startButton, new Vector2(GVar.gameScreenX / 2 - (Textures.Button.startButton.Width / 2) / 2, Lists.uiElements[j].Position.Y + Lists.uiElements[j].Size.Y + 10), new Vector2(Textures.Button.startButton.Width / 2, Textures.Button.startButton.Height), Color.Yellow, "StartGame", "Alive", 0f);
                                Lists.chooseCharacterButtons.Add(chooseDP);
                                Lists.chooseCharacterButtons.Add(startGame);
                            }
                        }
                    }
                    else if (Lists.chooseCharacterButtons[i].Name == "StartGame" && GVar.playerName != string.Empty && !GVar.choosingDP)
                    {
                        Lists.chooseCharacterButtons.RemoveAt(i);
                        
                        for (int j = 0; j < Lists.uiElements.Count; j++)
                        {
                            if (Lists.uiElements[j].SpriteID == Textures.UI.newGameUIBorder && Lists.uiElements[j].Draw)
                            {
                                Lists.uiElements[j].Draw = false;
                            }
                        }

                        GVar.creatingCharacter = false;

                        GVar.chooseStory = true;
                    }
                    else if (Lists.chooseCharacterButtons[i].Name == "MainMenu")
                    {
                        GVar.changeToMainMenu = true;
                        Colours.drawBlackFade = true;
                        Colours.fadeIn = true;
                    }
                    else if (Lists.chooseCharacterButtons[i].Name == "ChooseDP" && !GVar.choosingDP)
                    {
                        GVar.preventclick = 1;
                        GVar.choosingDP = true;
                        Vector2 pos = new Vector2(150, 100);
                        for (int j = 0; j < Lists.displayPictureIDs.Count; j++)
                        {
                            Dictionaries.displayPictures[Lists.displayPictureIDs[j]].position = pos;
                            Lists.displayPictureButtons.Add(new Button(Textures.Misc.pixel, Dictionaries.displayPictures[Lists.displayPictureIDs[j]].position, new Vector2(150, 150), Color.FromNonPremultiplied(0, 0, 0, 0), "ChooseDisplayPicture", Dictionaries.displayPictures[Lists.displayPictureIDs[j]].displayPicID, 0f));
                            pos.X += 200;
                        }
                    }
                }
            }

            for (int i = 0; i < Lists.savedGames.Count; i++)
            {
                for (int j = 0; j < Lists.savedGames[i].buttons.Count; j++)
                {
                    Lists.savedGames[i].buttons[j].Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                    if (MouseManager.mouse.mouseBounds.Intersects(Lists.savedGames[i].buttons[j].Bounds) && InputManager.IsLMPressed())
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        if (Lists.savedGames[i].buttons[j].Name == "LoadGame")
                        {
                            GVar.playerName = Lists.savedGames[i].buttons[j].State;
                            GVar.storyName = Lists.savedGames[i].story;
                            Load.LoadLocationNodes(GVar.storyName);
                            GVar.LogDebugInfo("GameLoaded: " + GVar.playerName, 2);
                            GVar.loadGame = true;
                            Colours.fadeIn = true;
                            Colours.drawBlackFade = true;
                        }
                        else if (Lists.savedGames[i].buttons[j].Name == "DeleteGame")
                        {
                            string playerName = Lists.savedGames[i].buttons[j].State;
                            GVar.LogDebugInfo("GameDeleted: " + playerName, 2);
                            for (int k = 0; k < Lists.savedGamesXmlDoc.Count; k++)
                            {
                                XmlNode saveNode = Lists.savedGamesXmlDoc[k].DocumentElement.SelectSingleNode("/savedgame");
                                if (playerName == saveNode[GVar.XmlTags.Player.name].InnerText)
                                {
                                    string saveFileLocation = saveNode["filename"].InnerText;
                                    string locationFilesLocation = GVar.gameFilesLocation + playerName;
                                    Lists.savedGamesXmlDoc.Clear();
                                    Delete.DeleteGame(saveFileLocation, locationFilesLocation);
                                }
                            }
                            Lists.chooseCharacterButtons.Clear();
                            Lists.availableStoriesButtons.Clear();
                            Lists.savedGames.Clear();
                            LoadCreateCharacter();
                            break;
                        }
                    }
                }
            }

            if (GVar.chooseStory)
            {
                for (int i = 0; i < Lists.availableStoriesButtons.Count; i++)
                {
                    Lists.availableStoriesButtons[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                    if (MouseManager.mouse.mouseBounds.Intersects(Lists.availableStoriesButtons[i].Bounds) && InputManager.IsLMPressed())
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                        GVar.LogDebugInfo("GameCreated: " + GVar.playerName, 2);
                        GVar.storyName = Lists.availableStoriesButtons[i].Name;
                        Textures.Misc.worldMap = Dictionaries.worldMaps[GVar.storyName + "Map"];
                        Vector.SetWorldMapVectors();
                        Load.LoadLocationNodes(GVar.storyName);
                        GVar.chooseStory = false;
                        GVar.startGame = true;
                        Colours.fadeIn = true;
                        Colours.drawBlackFade = true;
                        Lists.availableStoriesButtons.Clear();
                    }
                }
            }

            if (GVar.creatingCharacter)
            {
                if (GVar.choosingDP)
                {
                    for (int i = 0; i < Lists.displayPictureButtons.Count; i++)
                    {
                        Lists.displayPictureButtons[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                        if (MouseManager.mouse.mouseBounds.Intersects(Lists.displayPictureButtons[i].Bounds) && InputManager.IsLMPressed() && GVar.preventclick == 0)
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                            GVar.displayPicID = Lists.displayPictureButtons[i].State;
                            GVar.choosingDP = false;
                            break;
                        }
                    }
                }
                
                if (!GVar.choosingDP)
                    Text.RecordText();
            }
            if (GVar.preventclick == 1)
                GVar.preventclick = 0;
        }

        public static void DrawCreateCharacter(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Textures.Misc.background, new Rectangle(0, 0, (int)GVar.gameScreenX, (int)GVar.gameScreenY), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);

            for (int i = 0; i < Lists.chooseCharacterButtons.Count; i++)
            {
                Lists.chooseCharacterButtons[i].Update(gameTime);
                Lists.chooseCharacterButtons[i].Draw(spriteBatch, Lists.chooseCharacterButtons[i].SpriteID, Lists.chooseCharacterButtons[i].Bounds, 0.18f, 0f, Vector2.Zero);
                
                if (MouseManager.mouse.mouseBounds.Intersects(Lists.chooseCharacterButtons[i].Bounds))
                {
                    if (Lists.chooseCharacterButtons[i].CurrentAnimation != GVar.AnimStates.Button.mouseover && !GVar.chooseStory)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.buttonmouseover]);
                    }
                    Lists.chooseCharacterButtons[i].PlayAnimation(GVar.AnimStates.Button.mouseover);
                }
                if (Lists.chooseCharacterButtons[i].CurrentAnimation == GVar.AnimStates.Button.mouseover && !MouseManager.mouse.mouseBounds.Intersects(Lists.chooseCharacterButtons[i].Bounds))
                {
                    Lists.chooseCharacterButtons[i].PlayAnimation(GVar.AnimStates.Button.def);
                }
            }

            if (GVar.chooseStory)
            {
                for (int i = 0; i < Lists.availableStoriesButtons.Count; i++)
                {
                    Lists.availableStoriesButtons[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    if (MouseManager.mouse.mouseBounds.Intersects(Lists.availableStoriesButtons[i].Bounds))
                    {
                        if (!Lists.availableStoriesButtons[i].mouseOver)
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.buttonmouseover]);
                        }
                        Lists.availableStoriesButtons[i].mouseOver = true;
                        Lists.availableStoriesButtons[i].DrawDarkButton(spriteBatch);
                    }
                    else if (!MouseManager.mouse.mouseBounds.Intersects(Lists.availableStoriesButtons[i].Bounds))
                    {
                        Lists.availableStoriesButtons[i].mouseOver = false;
                        Lists.availableStoriesButtons[i].DrawLightButton(spriteBatch);
                    }
                }

                spriteBatch.DrawString(Fonts.lucidaConsole18Bold, "Want to see your own story on this list? Head to http://jskgames.wix.com/jskgames \nto see the tutorials on how to get started.", new Vector2(10, GVar.gameScreenY - 60), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
            }

            if (!GVar.creatingCharacter && !GVar.chooseStory)
            {
                for (int i = 0; i < Lists.uiElements.Count; i++)
                {
                    if (Lists.uiElements[i].SpriteID == Textures.UI.newGameUIBorder && Lists.uiElements[i].Draw)
                    {
                        Lists.uiElements[i].Draw = false;
                    }
                }

                for (int i = 0; i < Lists.savedGames.Count; i++)
                {
                    spriteBatch.Draw(Dictionaries.displayPictures[Lists.savedGames[i].displayPicID].displayPic, new Rectangle((int)Lists.savedGames[i].startPosition.X + 4, (int)Lists.savedGames[i].startPosition.Y + 4, 54, 56), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
                    spriteBatch.Draw(Textures.UI.savedGameUIBorder, new Rectangle((int)Lists.savedGames[i].startPosition.X, (int)Lists.savedGames[i].startPosition.Y, 1240, 62), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                    spriteBatch.Draw(Textures.UI.savedGameUIInner, new Rectangle((int)Lists.savedGames[i].startPosition.X, (int)Lists.savedGames[i].startPosition.Y, 1240, 62), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.15f);
                    spriteBatch.DrawString(Fonts.lucidaConsole24Regular, Lists.savedGames[i].name, new Vector2(Lists.savedGames[i].startPosition.X + 80, Lists.savedGames[i].startPosition.Y + 20), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                    spriteBatch.DrawString(Fonts.lucidaConsole24Regular, Lists.savedGames[i].story, new Vector2(Lists.savedGames[i].startPosition.X + 350, Lists.savedGames[i].startPosition.Y + 20), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                    spriteBatch.DrawString(Fonts.lucidaConsole24Regular, Lists.savedGames[i].location, new Vector2(Lists.savedGames[i].startPosition.X + 660, Lists.savedGames[i].startPosition.Y + 20), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

                    for (int j = 0; j < Lists.savedGames[i].buttons.Count; j++)
                    {
                        Lists.savedGames[i].buttons[j].Update(gameTime);
                        Lists.savedGames[i].buttons[j].Draw(spriteBatch, Lists.savedGames[i].buttons[j].SpriteID, Lists.savedGames[i].buttons[j].Bounds, 0.18f, 0f, Vector2.Zero);

                        if (MouseManager.mouse.mouseBounds.Intersects(Lists.savedGames[i].buttons[j].Bounds))
                        {
                            if (Lists.savedGames[i].buttons[j].CurrentAnimation != GVar.AnimStates.Button.mouseover)
                            {
                                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.buttonmouseover]);
                            }
                            Lists.savedGames[i].buttons[j].PlayAnimation(GVar.AnimStates.Button.mouseover);
                        }
                        if (Lists.savedGames[i].buttons[j].CurrentAnimation == GVar.AnimStates.Button.mouseover && !MouseManager.mouse.mouseBounds.Intersects(Lists.savedGames[i].buttons[j].Bounds))
                        {
                            Lists.savedGames[i].buttons[j].PlayAnimation(GVar.AnimStates.Button.def);
                        }
                    }
                }
            }

            if (GVar.creatingCharacter)
            {
                for (int i = 0; i < Lists.uiElements.Count; i++)
                {
                    if (Lists.uiElements[i].SpriteID == Textures.UI.newGameUIBorder && !Lists.uiElements[i].Draw)
                    {
                        Lists.uiElements[i].Draw = true;
                    }
                    if (Lists.uiElements[i].SpriteID == Textures.UI.newGameUIBorder)
                    {
                        spriteBatch.Draw(Dictionaries.displayPictures[GVar.displayPicID].displayPic, new Rectangle((int)Lists.uiElements[i].Position.X + 3, (int)Lists.uiElements[i].Position.Y + 3, (int)Vector.newGameDPSize.X, (int)Vector.newGameDPSize.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
                        spriteBatch.DrawString(Fonts.lucidaConsole24Regular, GVar.playerName, new Vector2((int)Lists.uiElements[i].Position.X + 71, (int)Lists.uiElements[i].Position.Y + 276), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                    }
                }
                spriteBatch.Draw(Textures.UI.newGameUIInner, new Rectangle((int)GVar.gameScreenX / 2 - Textures.UI.newGameUIInner.Width / 2, (int)GVar.gameScreenY / 2 - Textures.UI.newGameUIInner.Height / 2, Textures.UI.newGameUIInner.Width, Textures.UI.newGameUIInner.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.15f);

                

                if (GVar.choosingDP)
                {
                    spriteBatch.Draw(Textures.Misc.pixel, new Rectangle(0, 0, (int)GVar.gameScreenX, (int)GVar.gameScreenY), null, Color.FromNonPremultiplied(0, 0, 0, 125), 0f, Vector2.Zero, SpriteEffects.None, 0.2f);

                    for (int i = 0; i < Lists.displayPictureButtons.Count; i++)
                    {
                        Lists.displayPictureButtons[i].Update(gameTime);
                        Lists.displayPictureButtons[i].Draw(spriteBatch, Lists.displayPictureButtons[i].SpriteID, Lists.displayPictureButtons[i].Bounds, 0.201f, 0f, Vector2.Zero);
                    }

                    for (int i = 0; i < Lists.displayPictureIDs.Count; i++)
                    {
                        spriteBatch.Draw(Dictionaries.displayPictures[Lists.displayPictureIDs[i]].displayPic, new Rectangle((int)Dictionaries.displayPictures[Lists.displayPictureIDs[i]].position.X, (int)Dictionaries.displayPictures[Lists.displayPictureIDs[i]].position.Y, 150, 150), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.21f);
                    }
                }
            }
        }

        public static void LoadCreateCharacter()
        {
            LoadSavedGames();
            LoadStoryButtons();

            Vector2 pos = new Vector2(20, 80);

            for (int i = 0; i < Lists.savedGamesXmlDoc.Count; i++)
            {
                pos.Y += 75;
            }

            Button newCharacter = new Button(Textures.Button.newButton, pos, new Vector2(Textures.Button.newButton.Width / 2, Textures.Button.newButton.Height), Color.White, "NewCharacter", "Alive", 0f);
            Button back = new Button(Textures.Button.backButton, new Vector2(20, 20), new Vector2(Textures.Button.backButton.Width / 2, Textures.Button.backButton.Height), Color.White, "MainMenu", "Alive", 0f);
            Lists.chooseCharacterButtons.Add(back);
            Lists.chooseCharacterButtons.Add(newCharacter);
        }

        public static void LoadStoryButtons()
        {
            XmlDocument storiesDoc = new XmlDocument();
            storiesDoc.Load("./Content/AvailableStories.xml");
            XmlNodeList stories = storiesDoc.SelectNodes("stories/story");
            foreach (XmlNode story in stories)
            {
                GeneratedButton storyButton = new GeneratedButton(Vector2.Zero, Color.White, 20, "LoadStory", story["name"].InnerText, "LoadStory");
                Lists.availableStoriesButtons.Add(storyButton);
            }

            Vector2 pos = new Vector2(20, 80);

            for (int i = 0; i < Lists.availableStoriesButtons.Count; i++)
            {
                Lists.availableStoriesButtons[i].Position = pos;
                pos.X += Lists.availableStoriesButtons[i].Size.X + 30;
            }
        }

        public static void LoadSavedGames()
        {
            try
            {
                Vector2 startPos = new Vector2(20, 80);
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        XmlDocument tempDoc = new XmlDocument();
                        tempDoc.Load(GVar.savedGameLocation + i.ToString() + ".xml");
                        XmlNode saveNode = tempDoc.SelectSingleNode("/savedgame");

                        SavedGame save = new SavedGame(saveNode["dpid"].InnerText, saveNode["name"].InnerText, saveNode["storyname"].InnerText, saveNode["currentlocation"].InnerText, startPos);

                        startPos.Y += 75;

                        Lists.savedGames.Add(save);
                        Lists.savedGamesXmlDoc.Add(tempDoc);
                    }
                    catch 
                    {
                        GVar.LogDebugInfo("!!!File [" + GVar.savedGameLocation + i.ToString() + ".xml] Not Found!!!", 1);
                    }
                }
            }
            catch { }
            GVar.numSavedGames = Lists.savedGamesXmlDoc.Count;
            Vector2 buttonPos = new Vector2(GVar.gameScreenX - 230, 80);
            try
            {
                for (int i = 0; i < Lists.savedGamesXmlDoc.Count; i++)
                {
                    XmlNode saveNode = Lists.savedGamesXmlDoc[i].DocumentElement.SelectSingleNode("/savedgame");
                    buttonPos.Y += 110;
                }
            }
            catch { }
        }
    }
}
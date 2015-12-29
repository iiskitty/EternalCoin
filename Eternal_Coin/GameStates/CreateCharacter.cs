using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Xml;
using System.Text;

namespace Eternal_Coin
{
    public class DisplayPicture
    {
        public Texture2D displayPic;
        public string displayPicID;

        public Vector2 position;

        public DisplayPicture(string ID, Texture2D pic)
        {
            this.displayPic = pic;
            this.displayPicID = ID;
            position = new Vector2();
        }
    }

    public class SavedGame
    {
        //public static Texture2D displayPic;
        public string displayPicID;

        public string name;

        public Vector2 startPosition;

        public Button loadGame;
        public Button deleteGame;

        public List<Button> buttons;

        public SavedGame(string displayPicID, string name, Vector2 startPosition)
        {
            this.displayPicID = displayPicID;
            this.name = name;
            this.startPosition = startPosition;

            buttons = new List<Button>();

            loadGame = new Button(Textures.pixel, new Vector2(startPosition.X + 1020, startPosition.Y), new Vector2(100, 100), Color.Blue, "LoadGame", name, 0f);
            deleteGame = new Button(Textures.pixel, new Vector2(startPosition.X + 1140, startPosition.Y), new Vector2(100, 100), Color.Red, "DeleteGame", name, 0f);
            buttons.Add(loadGame);
            buttons.Add(deleteGame);
        }
    }

    public class CreateCharacter
    {
        public static void UpdateCreateCharacter(GameTime gameTime)
        {
            if (InputManager.IsLMDown())
                GVar.preventclick = 1;
            else if (InputManager.IsLMReleased())
                GVar.preventclick = 0;

            for (int i = 0; i < Lists.chooseCharacterButtons.Count; i++)
            {
                Lists.chooseCharacterButtons[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                if (MouseManager.mouseBounds.Intersects(Lists.chooseCharacterButtons[i].Bounds) && InputManager.IsLMPressed())
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                    GVar.LogDebugInfo("ButtonClicked: " + Lists.chooseCharacterButtons[i].Name, 2);
                    
                    if (Lists.chooseCharacterButtons[i].Name == "NewCharacter")
                    {
                        GVar.creatingCharacter = true;
                        Lists.chooseCharacterButtons.Clear();
                        Button startGame = new Button(Textures.pixel, new Vector2(GVar.gameScreenX / 2, 20), new Vector2(100, 50), Color.Yellow, "StartGame", "Alive", 0f);
                        Button chooseDP = new Button(Textures.pixel, new Vector2(20, 30), new Vector2(100, 100), Color.FromNonPremultiplied(0, 0, 0, 0), "ChooseDP", "Alive", 0f);
                        Lists.chooseCharacterButtons.Add(chooseDP);
                        Lists.chooseCharacterButtons.Add(startGame);
                    }
                    else if (Lists.chooseCharacterButtons[i].Name == "StartGame" && GVar.playerName != string.Empty && !GVar.choosingDP)
                    {
                        Lists.chooseCharacterButtons.RemoveAt(i);
                        

                        GVar.creatingCharacter = false;

                        GVar.chooseStory = true;
                    }
                    else if (Lists.chooseCharacterButtons[i].Name == "MainMenu")
                    {
                        Lists.savedGamesXmlDoc.Clear();
                        Lists.mainMenuButtons.Clear();
                        GVar.changeToMainMenu = true;
                        Colours.drawBlackFade = true;
                        Colours.fadeIn = true;
                    }
                    else if (Lists.chooseCharacterButtons[i].Name == "ChooseDP")
                    {
                        GVar.choosingDP = true;

                        Vector2 pos = new Vector2(150, 100);
                        for (int j = 0; j < Lists.displayPictureIDs.Count; j++)
                        {
                            Dictionaries.displayPictures[Lists.displayPictureIDs[j]].position = pos;
                            Lists.displayPictureButtons.Add(new Button(Textures.pixel, Dictionaries.displayPictures[Lists.displayPictureIDs[j]].position, new Vector2(150, 150), Color.FromNonPremultiplied(0, 0, 0, 0), "ChooseDisplayPicture", Dictionaries.displayPictures[Lists.displayPictureIDs[j]].displayPicID, 0f));
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

                    if (MouseManager.mouseBounds.Intersects(Lists.savedGames[i].buttons[j].Bounds) && InputManager.IsLMPressed())
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        if (Lists.savedGames[i].buttons[j].Name == "LoadGame")
                        {
                            GVar.playerName = Lists.savedGames[i].buttons[j].State;
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
                                    string locationFilesLocation = "./Content/GameFiles/" + playerName;
                                    Lists.savedGamesXmlDoc.Clear();
                                    Delete.DeleteGame(saveFileLocation, locationFilesLocation);
                                }
                            }
                            Lists.chooseCharacterButtons.Clear();
                            Lists.availableStoriesButtons.Clear();
                            Lists.savedGames.Clear();
                            CreateCharacter.LoadCreateCharacter();
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

                    if (MouseManager.mouseBounds.Intersects(Lists.availableStoriesButtons[i].Bounds) && InputManager.IsLMPressed())
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        GVar.LogDebugInfo("GameCreated: " + GVar.playerName, 2);
                        GVar.storyName = Lists.availableStoriesButtons[i].State;
                        //GVar.loadData = true;
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

                        if (MouseManager.mouseBounds.Intersects(Lists.displayPictureButtons[i].Bounds) && InputManager.IsLMPressed())
                        {
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                            GVar.displayPicID = Lists.displayPictureButtons[i].State;
                            GVar.choosingDP = false;
                        }
                    }
                }

                if (!GVar.choosingDP)
                    Text.RecordText();
            }
        }

        public static void DrawCreateCharacter(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Object b in Lists.chooseCharacterButtons)
            {
                b.Update(gameTime);
                b.Draw(spriteBatch, b.SpriteID, b.Bounds, 0.18f, 0f, Vector2.Zero);
                
                if (b.Name == "NewCharacter")
                {
                    spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "New", new Vector2(b.Position.X + b.Size.X / 4, b.Position.Y + b.Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                }
                if (b.Name == "StartGame")
                {
                    spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Start", new Vector2(b.Position.X + b.Size.X / 4, b.Position.Y + b.Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                }
                if (b.Name == "MainMenu")
                {
                    spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Back", new Vector2(b.Position.X + b.Size.X / 4, b.Position.Y + b.Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                }
            }

            if (GVar.chooseStory)
            {
                foreach (Object B in Lists.availableStoriesButtons)
                {
                    B.Update(gameTime);
                    B.Draw(spriteBatch, B.SpriteID, B.Bounds, 0.18f, 0f, Vector2.Zero);
                    spriteBatch.DrawString(Fonts.lucidaConsole14Regular, B.State, new Vector2(B.Position.X + B.Size.X / 4, B.Position.Y + B.Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                }
            }

            Vector2 pos = new Vector2(20, 80);

            if (!GVar.creatingCharacter && !GVar.chooseStory && !Colours.fadeIn)
            {
                for (int i = 0; i < Lists.savedGames.Count; i++)
                {
                    spriteBatch.Draw(Dictionaries.displayPictures[Lists.savedGames[i].displayPicID].displayPic, new Rectangle((int)Lists.savedGames[i].startPosition.X, (int)Lists.savedGames[i].startPosition.Y, 100, 100), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
                    spriteBatch.Draw(Textures.pixel, new Rectangle((int)Lists.savedGames[i].startPosition.X, (int)Lists.savedGames[i].startPosition.Y, 1240, 100), null, Color.Gray, 0f, Vector2.Zero, SpriteEffects.None, 0.15f);
                    spriteBatch.DrawString(Fonts.lucidaConsole14Regular, Lists.savedGames[i].name, new Vector2(Lists.savedGames[i].startPosition.X + 150, Lists.savedGames[i].startPosition.Y + 45), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

                    for (int j = 0; j < Lists.savedGames[i].buttons.Count; j++)
                    {
                        Lists.savedGames[i].buttons[j].Update(gameTime);
                        Lists.savedGames[i].buttons[j].Draw(spriteBatch, Lists.savedGames[i].buttons[j].SpriteID, Lists.savedGames[i].buttons[j].Bounds, 0.18f, 0f, Vector2.Zero);

                        if (MouseManager.mouseBounds.Intersects(Lists.savedGames[i].buttons[j].Bounds))
                        {
                            GVar.DrawBoundingBox(Lists.savedGames[i].buttons[j].Bounds, spriteBatch, Textures.pixel, 1, 0.19f, Color.Green);
                        }

                        if (Lists.savedGames[i].buttons[j].Name.Contains("DeleteGame"))
                        {
                            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Delete", new Vector2(Lists.savedGames[i].buttons[j].Position.X + Lists.savedGames[i].buttons[j].Size.X / 4, Lists.savedGames[i].buttons[j].Position.Y + Lists.savedGames[i].buttons[j].Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                        }
                        if (Lists.savedGames[i].buttons[j].Name.Contains("LoadGame"))
                        {
                            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Load", new Vector2(Lists.savedGames[i].buttons[j].Position.X + Lists.savedGames[i].buttons[j].Size.X / 4, Lists.savedGames[i].buttons[j].Position.Y + Lists.savedGames[i].buttons[j].Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                        }
                    }
                }
            }

            if (GVar.creatingCharacter)
            {
                spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Enter Your Name", new Vector2(5, 5), Color.Black);
                spriteBatch.DrawString(Fonts.lucidaConsole16Bold, GVar.playerName, new Vector2(110, 50), Color.Black);

                spriteBatch.Draw(Dictionaries.displayPictures[GVar.displayPicID].displayPic, new Rectangle(20, 30, 100, 100), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);

                if (GVar.choosingDP)
                {
                    spriteBatch.Draw(Textures.pixel, new Rectangle(0, 0, (int)GVar.gameScreenX, (int)GVar.gameScreenY), null, Color.FromNonPremultiplied(0, 0, 0, 125), 0f, Vector2.Zero, SpriteEffects.None, 0.2f);

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
                pos.Y += 110;
            }

            Button newCharacter = new Button(Textures.pixel, pos, new Vector2(100, 50), Color.Yellow, "NewCharacter", "Alive", 0f);
            Button back = new Button(Textures.pixel, new Vector2(20, 20), new Vector2(100, 50), Color.Yellow, "MainMenu", "Alive", 0f);
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
                Button storyButton = new Button(Textures.pixel, Vector2.Zero, new Vector2(150, 50), Color.Yellow, "LoadStory", story["name"].InnerText, 0f);
                Lists.availableStoriesButtons.Add(storyButton);
            }

            Vector2 pos = new Vector2(20, 80);

            foreach (Object B in Lists.availableStoriesButtons)
            {
                B.Position = pos;
                pos.X += 170;
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

                        SavedGame save = new SavedGame(saveNode["dpid"].InnerText, saveNode["name"].InnerText, startPos);

                        startPos.Y += 110;

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
                    //Button loadGame = new Button(Textures.pixel, buttonPos, new Vector2(100, 100), Color.Blue, "LoadGame", saveNode[GVar.XmlTags.Player.name].InnerText, 0f);
                    //Button deleteGame = new Button(Textures.pixel, new Vector2(buttonPos.X + 110, buttonPos.Y), new Vector2(100, 100), Color.Red, "DeleteGame", saveNode[GVar.XmlTags.Player.name].InnerText, 0f);
                    //Lists.chooseCharacterButtons.Add(deleteGame);
                    //Lists.chooseCharacterButtons.Add(loadGame);
                    buttonPos.Y += 110;
                    //GVar.LogDebugInfo("SavedGameFound: " + loadGame.State, 2);
                }
            }
            catch { }
        }
    }
}
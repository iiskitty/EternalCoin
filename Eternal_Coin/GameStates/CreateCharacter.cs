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

    public List<Object> buttons;

    public SavedGame(string displayPicID, string name, string story, string location, Vector2 startPosition)
    {
      this.displayPicID = displayPicID;
      this.name = name;
      this.story = story;
      this.location = location;
      this.startPosition = startPosition;

      buttons = new List<Object>();

      loadGame = new Button(Textures.Button.loadButton, new Vector2(startPosition.X + 953, startPosition.Y + 12), new Vector2(Textures.Button.loadButton.Width / 2, Textures.Button.loadButton.Height), Color.White, "LoadGame", name, 0f);
      loadGame.extraInfo = story;
      deleteGame = new Button(Textures.Button.deleteButton, new Vector2(startPosition.X + 1087, startPosition.Y + 12), new Vector2(Textures.Button.deleteButton.Width / 2, Textures.Button.deleteButton.Height), Color.White, "DeleteGame", name, 0f);
      buttons.Add(loadGame);
      buttons.Add(deleteGame);
    }
  }

  public class CreateCharacter
  {
    public static void UpdateCreateCharacter(GameTime gameTime)
    {
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
        if (!GVar.choosingDP)
          Text.RecordText();
      }
      if (GVar.preventclick == 1)
        GVar.preventclick = 0;
    }

    public static void DrawCreateCharacter(SpriteBatch spriteBatch, GameTime gameTime)
    {
      spriteBatch.Draw(Textures.Misc.background, new Rectangle(0, 0, (int)GVar.gameScreenX, (int)GVar.gameScreenY), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);

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
using Microsoft.Xna.Framework;
using System;
using System.Xml;

namespace Eternal_Coin
{
  public class NewCharButtons
  {
    public static void NewCharacter()
    {
      GVar.creatingCharacter = true;
      Lists.chooseCharacterButtons.Clear();
      for (int j = 0; j < Lists.uiElements.Count; j++)
      {
        if (Lists.uiElements[j].SpriteID == Textures.UI.newGameUIBorder)
        {
          Button chooseDP = new Button(Textures.Misc.pixel, new Vector2(Lists.uiElements[j].Position.X + 3, Lists.uiElements[j].Position.Y + 3), Vector.newGameDPSize, Color.FromNonPremultiplied(0, 0, 0, 0), "ChooseDP", "Alive", 0f);
          Button startGame = new Button(Textures.Button.startButton, new Vector2(GVar.currentScreenX / 2 - (Textures.Button.startButton.Width / 2) / 2, Lists.uiElements[j].Position.Y + Lists.uiElements[j].Size.Y + 10), new Vector2(Textures.Button.startButton.Width / 2, Textures.Button.startButton.Height), Color.Yellow, "StartGame", "Alive", 0f);
          Lists.chooseCharacterButtons.Add(chooseDP);
          Lists.chooseCharacterButtons.Add(startGame);
        }
      }
    }

    public static void StartGame(Button button)
    {
      Lists.chooseCharacterButtons.Remove(button);

      if (UIElement.IsUIElementActive(Textures.UI.newGameUIBorder))
        UIElement.DeActivateUIElement(Textures.UI.newGameUIBorder);

      GVar.creatingCharacter = false;
      GVar.chooseStory = true;
    }

    public static void LoadGame(Button button)
    {
      GVar.playerName = button.State;
      GVar.storyName = button.extraInfo;
      Textures.Misc.worldMap = Dictionaries.worldMaps[GVar.storyName + "Map"];
      Vector.SetWorldMapVectors();
      Load.LoadLocationNodes(GVar.storyName);

      GVar.LogDebugInfo("GameLoaded: " + GVar.playerName, 2);
      GVar.loadGame = true;
      Colours.fadeIn = true;
      Colours.drawBlackFade = true;
    }

    public static void DeleteGame(Button button)
    {
      string playerName = button.State;
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
      CreateCharacter.LoadCreateCharacter();
    }

    public static void ChoosingDP()
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

    public static void ChooseDP(Button button)
    {
      GVar.displayPicID = button.State;
      GVar.choosingDP = false;
    }
  }
}

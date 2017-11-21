using Microsoft.Xna.Framework;
using System;
using System.Xml;

namespace Eternal_Coin
{
  public class Updates
  {
    public static void CloseInventory()
    {
      if (GVar.currentGameState == GVar.GameState.shop)
      {
        Shop.SaveShopInventory(GVar.curLocNode, GVar.player.CurrentLocation);//save shops inventory.

        //cycle through inventory itemslots.
        for (int j = 0; j < 40; j++)
          if (InventoryManager.shopInventory.ItemSlots[j].item != null)//if item is not null.
            InventoryManager.shopInventory.ItemSlots[j].item = null;//delete the item.

        InventoryManager.shopInventory = new Inventory(new Vector2(862, 51), GVar.InventoryParentNames.shop);//create new shop inventory(deleting current one)
        Lists.shopItems.Clear();//clear shops items.
      }

      Lists.inventoryButtons.Clear();
      GVar.previousGameState = GVar.currentGameState;
      GVar.currentGameState = GVar.GameState.game;
    }

    public static void ChangeToOptions()
    {
      GVar.changeToOptions = true;
      Colours.drawBlackFade = true;
      Colours.fadeIn = true;
    }

    public static void ExitGame()
    {
      GVar.exitAfterFade = true;
      Colours.drawBlackFade = true;
      Colours.fadeIn = true;
    }

    public static void ChangeToChooseCharacter()
    {
      GVar.changeToCreateCharacter = true;
      Colours.drawBlackFade = true;
      Colours.fadeIn = true;
    }

    public static void ChangeToMainMenu()
    {
      GVar.changeToMainMenu = true;
      Colours.drawBlackFade = true;
      Colours.fadeIn = true;
      Lists.mainMenuButtons.Clear();
    }

    public static void BackToGame()
    {
      GVar.changeBackToGame = true;
      Colours.drawBlackFade = true;
      Colours.fadeIn = true;
    }

    public static void ToggleFullScreen()
    {
      GVar.toggleFullScreen = true;
    }

    public static void ToggleDebugLog()
    {
      GVar.debugLogEnabled = !GVar.debugLogEnabled;
      if (GVar.debugLogEnabled)
        GVar.CreateDebugLog();
      XmlDocument options = new XmlDocument();
      options.Load("./Content/Options.xml");
      XmlNode optionsNode = options.DocumentElement.SelectSingleNode("/options/enabledebuglog");
      optionsNode.InnerText = Convert.ToString(GVar.debugLogEnabled);
      options.Save("./Content/Options.xml");
    }

    public static void NewCharacter()
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

    public static void StartGame(Button button)
    {
      Lists.chooseCharacterButtons.Remove(button);
      for (int j = 0; j < Lists.uiElements.Count; j++)
        if (Lists.uiElements[j].SpriteID == Textures.UI.newGameUIBorder && Lists.uiElements[j].Draw)
          Lists.uiElements[j].Draw = false;

      GVar.creatingCharacter = false;
      GVar.chooseStory = true;
    }

    public static void LoadGame(Button button)
    {
      GVar.playerName = button.State;
      GVar.storyName = button.extraInfo;
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

    public static void OpenShop(Button button)
    {
      Shop.LoadShopInventory(GVar.curLocNode);//load shop inventory of current location.
      Button closeInv = new Button(Textures.Misc.pixel, new Vector2(GVar.gameScreenX - button.Size.X, 0), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);//create button to close inventory.
      Lists.inventoryButtons.Add(closeInv);//add close button to InventoryButtons.
      Lists.mainWorldButtons.Remove(button);//remove Open Shop Button.
      GVar.currentGameState = GVar.GameState.shop;//set current GameState to shop.
      GVar.previousGameState = GVar.GameState.game;//set previous GameState to game.
      UI.CloseNPCUI();//close NPC UI.
    }

    public static void ViewQuests()
    {
      for (int i = 0; i < Lists.uiElements.Count; i++)
      {
        if (Lists.uiElements[i].SpriteID == Textures.UI.NPCQuestListUI && !Lists.uiElements[i].Draw)
          UI.DisplayNPCQuestList();
        else if (Lists.uiElements[i].SpriteID == Textures.UI.NPCQuestListUI && Lists.uiElements[i].Draw)
          UI.CloseNPCQuestListUI();
      }
    }

    public static void DisplayInventory()
    {
      Button closeInv = new Button(Textures.Misc.pixel, new Vector2(GVar.gameScreenX - 25, 0), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);//create button to close inventory.
      Lists.inventoryButtons.Add(closeInv);//add close button to InventoryButtons.
      GVar.currentGameState = GVar.GameState.inventory;//set current GameState to inventory.
      GVar.previousGameState = GVar.GameState.game;//set previous GameState to game.
    }

    public static void CloseQuestListUI(Button button)
    {
      UI.CloseQuestListUI();//deactiate Quests UI.
      Lists.mainWorldButtons.Remove(button);//remove Close Quests UI Button.
      Lists.viewQuestInfoButtons.Clear();//delete Quest Info Buttons.
    }

    public static void CloseQuestInfoUI(Button button)
    {
      UI.CloseQuestInfoUI();//deactivate Quest Info UI.
      Lists.mainWorldButtons.Remove(button);//remove Close Quest Info UI Button.
    }

    public static void CloseNPCUI(Button button)
    {
      Lists.mainWorldButtons.Remove(button);//delete Close NPC UI Button.
      for (int k = 0; k < Lists.mainWorldButtons.Count; k++)//cycle through MainWorldButtons.
        if (Lists.mainWorldButtons[k].Name == "OpenShop" || Lists.mainWorldButtons[k].Name == "ViewQuests")//if Button is Open Shop Button.
          Lists.mainWorldButtons.RemoveAt(k);//delete Open Shop Button.

      Lists.NPCQuests.Clear();
      UI.CloseNPCUI();//deactivate NPC UI.
      UI.CloseNPCQuestListUI();
    }

    public static void QuitGame()
    {
      Save.SaveGame(GVar.savedGameLocation, GVar.player, Lists.quests);//Save the game.
      GVar.changeToMainMenu = true;//change to menu bool to true.
      Colours.drawBlackFade = true;//draw black fade in bool to true.
      Colours.fadeIn = true;//fade in bool to true.
      GVar.playerName = string.Empty;//reset players name.
    }

    /// <summary>
    /// checks for state changes (gameState)
    /// </summary>
    public static void CheckForStateChange()
    {
      if (GVar.changeToBattle && !Colours.fadeIn)
      {
        Battle.LoadBattle(GVar.curLocNode);
        GVar.changeToBattle = false;
        GVar.currentGameState = GVar.GameState.battle;
        GVar.previousGameState = GVar.GameState.game;
        GVar.LogDebugInfo("GameState Change: battle", 2);
      }

      if (GVar.changeToCreateCharacter && !Colours.fadeIn)
      {
        CreateCharacter.LoadCreateCharacter();
        GVar.changeToCreateCharacter = false;
        GVar.currentGameState = GVar.GameState.chooseCharacter;
        GVar.previousGameState = GVar.GameState.mainMenu;
        GVar.LogDebugInfo("GameState Change: chooseCharacter", 2);
      }

      if (GVar.changeToOptions && !Colours.fadeIn)
      {
        GVar.changeToOptions = false;
        GVar.previousGameState = GVar.currentGameState;
        GVar.currentGameState = GVar.GameState.options;
        Options.LoadOptions();
        GVar.LogDebugInfo("GameState Change: options", 2);
      }

      if (GVar.startGame && !Colours.fadeIn)
      {
        Lists.chooseCharacterButtons.Clear();
        Player player = new Player(Textures.Misc.pixel, new Vector2(GVar.gameScreenX / 2, GVar.gameScreenY / 2), new Vector2(20, 20), GVar.playerName, "Alive", Vector2.Zero, Color.Green, 100, 2, 2);

        XmlDocument doc = new XmlDocument();
        doc.Load("Content/LoadData/StoryNodes/" + GVar.storyName + ".xml");
        XmlNode node = doc.DocumentElement.SelectSingleNode("/locationnode/startinglocation");

        player.CurrentLocation = Load.SetStartingLocation(node.InnerText);
        GVar.player = player;
        Save.SaveGame(GVar.savedGameLocation, player, Lists.quests);
        MainWorld.LoadMainWorld();
        GVar.previousGameState = GVar.GameState.chooseCharacter;
        GVar.currentGameState = GVar.GameState.game;
        GVar.startGame = false;
        GVar.LogDebugInfo("GameState Change: game", 2);
      }

      if (GVar.loadGame && !Colours.fadeIn)
      {
        Load.LoadGame(GVar.playerName);
        MainWorld.LoadMainWorld();
        Lists.chooseCharacterButtons.Clear();
        GVar.previousGameState = GVar.GameState.chooseCharacter;
        GVar.currentGameState = GVar.GameState.game;
        GVar.loadGame = false;
        GVar.loadData = true;
        GVar.LogDebugInfo("GameState Change: game", 2);
      }

      if (GVar.changeToMainMenu && !Colours.fadeIn)
      {
        Lists.savedGamesXmlDoc.Clear();
        Lists.mainMenuButtons.Clear();
        WorldMap.ResetMap();
        MainMenu.LoadMainMenu();
        GVar.currentGameState = GVar.GameState.mainMenu;
        GVar.previousGameState = GVar.GameState.chooseCharacter;
        GVar.changeToMainMenu = false;
        GVar.storyName = string.Empty;
        Lists.optionsButtons.Clear();
        Lists.chooseCharacterButtons.Clear();
        Lists.ClearGameLists();
        Dictionaries.ClearDictionaries();
        Lists.savedGamesXmlDoc.Clear();
        Lists.savedGames.Clear();
        Lists.availableStoriesButtons.Clear();

        UI.CloseNPCUI();
        UI.CloseQuestInfoUI();
        UI.CloseQuestListUI();
        InventoryManager.ClearInventories();
        foreach (UIElement ui in Lists.uiElements)
        {
          if (ui.SpriteID == Textures.UI.pauseUI && ui.Draw)
          {
            ui.Draw = false;
          }
        }
        GVar.gamePaused = false;

        GVar.LogDebugInfo("GameState Change: mainMenu", 2);
      }

      if (GVar.changeBackToGame && !Colours.fadeIn)
      {
        Battle.battleEnemy = null;
        Battle.battlePlayer = null;
        Battle.battleWon = false;
        Battle.loot.Clear();
        Battle.silverReward = 0;
        InventoryManager.enemyInventory = new EquipInventory(GVar.InventoryParentNames.enemy);
        Lists.optionsButtons.Clear();
        GVar.currentGameState = GVar.GameState.game;
        GVar.previousGameState = GVar.GameState.options;
        GVar.changeBackToGame = false;
        GVar.LogDebugInfo("GameState Change: game", 2);
      }
    }

    /// <summary>
    /// Updates buttons when in inventory
    /// </summary>
    /// <param name="button">The button to update</param>
    /// <param name="gameTime">gameTime for smoother stuff</param>
    public static void UpdateInventoryButtons(Object button, GameTime gameTime)
    {
      if (button.Name == "CloseInventory")
      {
        button.Position = new Vector2(GVar.gameScreenX - button.Size.X, 0);
        button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      }
    }

    public static void UpdateNPCQuestButtons(GameTime gameTime)
    {
      for (int i = 0; i < Lists.uiElements.Count; i++)
      {
        if (Lists.uiElements[i].SpriteID == Textures.UI.NPCQuestListUI && Lists.uiElements[i].Draw)
        {
          Vector2 startPos = new Vector2(Lists.uiElements[i].Position.X + 5, Lists.uiElements[i].Position.Y + 40);

          for (int j = 0; j < Lists.NPCQuestButtons.Count; j++)
          {
            Lists.NPCQuestButtons[j].Position = startPos;
            Lists.NPCQuestButtons[j].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            startPos.Y += Lists.NPCQuestButtons[j].Size.Y + 2;

            if (Lists.NPCQuestButtons[j].ID == "QuestAcceptButton")
            {
              Lists.NPCQuestButtons[j].Position = new Vector2(Lists.uiElements[i].Position.X + Lists.uiElements[i].Size.X - (Lists.NPCQuestButtons[j].Size.X + (Textures.Button.rightLightSide.Width * 2)), Lists.uiElements[i].Position.Y + Lists.uiElements[i].Size.Y - Lists.NPCQuestButtons[j].Size.Y);
              Lists.NPCQuestButtons[j].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (Lists.NPCQuestButtons[j].ID == "HandInQuestButton")
            {
              Lists.NPCQuestButtons[j].Position = new Vector2(Lists.uiElements[i].Position.X + Lists.uiElements[i].Size.X - (Lists.NPCQuestButtons[j].Size.X + (Textures.Button.rightLightSide.Width * 2)), Lists.uiElements[i].Position.Y + Lists.uiElements[i].Size.Y - Lists.NPCQuestButtons[j].Size.Y);
              Lists.NPCQuestButtons[j].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
          }
        }
      }
      GeneratedButton questButton = null;
      for (int i = 0; i < Lists.NPCQuestButtons.Count; i++)
      {

        if (MouseManager.mouse.mouseBounds.Intersects(Lists.NPCQuestButtons[i].Bounds) && InputManager.IsLMPressed())
        {
          for (int j = 0; j < Lists.NPCQuests.Count; j++)
          {
            if (Lists.NPCQuests[j].QuestID == Lists.NPCQuestButtons[i].State)
            {
              GVar.NPCQuestName = Lists.NPCQuests[j].ShortDescription;
              GVar.NPCQuestDescription = Lists.NPCQuests[j].Description;
              GVar.NPCQuestUnlocked = Lists.NPCQuests[j].Unlocked;
              GVar.NPCQuestDescription = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.NPCQuestDescription, 350);
              GVar.drawNPCQuestInfo = true;
            }
          }

          if (Lists.NPCQuestButtons[i].ID == "QuestAcceptButton")
          {
            Quest.AcceptQuest(GVar.player, Lists.NPCQuestButtons[i].State);//accept the quest.
            Quest.CreateNPCQuestsButtons();
            break;
          }
          else if (Lists.NPCQuestButtons[i].ID == "HandInQuestButton")
          {
            Quest.HandInQuest(GVar.player, Lists.NPCQuestButtons[i].State);//hand in the quest.
            Quest.CreateNPCQuestsButtons();
          }
          else
          {
            questButton = (GeneratedButton)Lists.NPCQuestButtons[i];
          }

          SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);


        }
      }
      if (questButton != null)
      {
        for (int i = 0; i < Lists.NPCQuestButtons.Count; i++)
        {
          if (Lists.NPCQuestButtons[i].ID == "QuestAcceptButton" || Lists.NPCQuestButtons[i].ID == "HandInQuestButton")
          {
            Lists.NPCQuestButtons[i].State = "delete";
          }
        }
        Quest.AddAcceptOrHandInQuestButtons(questButton);
      }
    }

    /// <summary>
    /// Updates buttons when playing the game
    /// </summary>
    /// <param name="button">Button to update</param>
    /// <param name="P">the player entity used for positioning of location buttons</param>
    /// <param name="gameTime"></param>
    public static void UpdateGameButtons(Object button, Entity P, GameTime gameTime)
    {
      if (button.Name == "MainMenu")
      {
        foreach (UIElement ui in Lists.uiElements)
        {
          if (ui.SpriteID == Textures.UI.pauseUI)
          {
            button.Position = new Vector2(ui.Position.X + 53, ui.Position.Y + 34);
            button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
          }
        }
      }
      if (button.Name == "Options")
      {
        foreach (UIElement ui in Lists.uiElements)
        {
          if (ui.SpriteID == Textures.UI.pauseUI)
          {
            button.Position = new Vector2(ui.Position.X + 53, ui.Position.Y + 99);
            button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
          }
        }
      }
      //if (button.Name == "DisplayQuests")
      //{
      //  foreach (UIElement ui in Lists.uiElements)
      //  {
      //    if (ui.SpriteID == Textures.UI.locationInfoUI)
      //    {
      //      button.Position = new Vector2(ui.Position.X + ui.Size.X - button.Size.X, ui.Position.Y + 22);
      //      button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      //    }
      //  }
      //}
      //if (button.Name == "DisplayInventory")
      //{
      //  foreach (UIElement ui in Lists.uiElements)
      //  {
      //    if (ui.SpriteID == Textures.UI.locationInfoUI)
      //    {
      //      button.Position = new Vector2(ui.Position.X + ui.Size.X - (button.Size.X * 2.2f), ui.Position.Y + 22);
      //      button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      //    }
      //  }
      //}
      if (button.Name == "CloseQuestListUI")
      {
        foreach (UIElement ui in Lists.uiElements)
        {
          if (ui.SpriteID == Textures.UI.questListUI)
          {
            button.Position = new Vector2(ui.Position.X + ui.Size.X - button.Size.X, ui.Position.Y);
            button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
          }
        }
      }
      if (button.Name == "LookEyeButton")
      {
        button.Position = new Vector2(P.Position.X + P.Size.X / 2 - button.Size.X / 2, P.Position.Y - button.Size.Y - 5);
        button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      }
      if (button.Name == "NPCButton")
      {
        button.Position = new Vector2(P.Position.X - button.Size.X * 1.5f, P.Position.Y + P.Size.Y / 2 - button.Size.Y / 2);
        button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      }
      if (button.Name == "ShopButton")
      {
        button.Position = new Vector2(P.Position.X + P.Size.X + button.Size.X / 2, P.Position.Y + P.Size.Y / 2 - button.Size.Y / 2);
        button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      }
      if (button.Name == "OpenShop")
      {
        foreach (UIElement ui in Lists.uiElements)
        {
          if (ui.SpriteID == Textures.UI.NPCInfoUI)
          {
            button.Position = new Vector2(ui.Position.X, ui.Position.Y + ui.Size.Y - button.Size.Y);
          }
        }
        button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      }
      if (button.Name == "EnterLocation" || button.Name == "ExitLocation")
      {
        button.Position = new Vector2(P.Position.X + P.Size.X / 2 - button.Size.X / 2, P.Position.Y + P.Size.Y / 2 - button.Size.Y - 45);
        button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      }
      if (button.Name == "CloseQuestInfoUI")
      {
        foreach (UIElement ui in Lists.uiElements)
        {
          if (ui.SpriteID == Textures.UI.questInfoUI)
          {
            button.Position = new Vector2(ui.Position.X + ui.Size.X - button.Size.X, 0);
          }
        }
        button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      }
      if (button.Name == "CloseNPCUIButton")
      {
        foreach (UIElement ui in Lists.uiElements)
        {
          if (ui.SpriteID == Textures.UI.NPCInfoUI)
          {
            button.Position = new Vector2(ui.Position.X + ui.Size.X - button.Size.X, ui.Position.Y);
          }
        }
        button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      }
      if (button.Name == "ViewQuests")
      {
        foreach (UIElement ui in Lists.uiElements)
        {
          if (ui.SpriteID == Textures.UI.NPCInfoUI)
          {
            button.Position = new Vector2(ui.Position.X + ui.Size.X - button.Size.X, ui.Position.Y + ui.Size.Y - button.Size.Y);
          }
        }
        button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      }


    }
  }
}

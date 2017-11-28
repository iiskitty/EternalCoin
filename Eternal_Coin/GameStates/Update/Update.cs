using Microsoft.Xna.Framework;
using System;
using System.Xml;

namespace Eternal_Coin
{
  public class Updates
  {
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
            questButton = Lists.NPCQuestButtons[i];
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

    
  }
}

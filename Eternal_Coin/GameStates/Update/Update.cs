using Microsoft.Xna.Framework;
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
                Options.LoadOptions();
                GVar.changeToOptions = false;
                GVar.currentGameState = GVar.GameState.options;
                GVar.previousGameState = GVar.GameState.mainMenu;
                GVar.LogDebugInfo("GameState Change: options", 2);
            }

            if (GVar.startGame && !Colours.fadeIn)
            {
                Lists.chooseCharacterButtons.Clear();
                Player player = new Player(Textures.Misc.pixel, new Vector2(GVar.gameScreenX / 2, GVar.gameScreenY / 2), new Vector2(20, 20), GVar.playerName, "Alive", Vector2.Zero, Color.Green, 100, 2, 2);

                XmlDocument doc = new XmlDocument();
                doc.Load("Content/LoadData/Data.xml");
                XmlNode node = doc.DocumentElement.SelectSingleNode("/data/createlocationnodes/locationnode/" + GVar.storyName + "/startinglocation");

                player.CurrentLocation = Load.SetStartingLocation(node.InnerText);
                Lists.entity.Add(player);
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
                InventoryManager.enemyInventory = new EnemyInventory();
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
            if (button.Name == "DisplayQuests")
            {
                foreach (UIElement ui in Lists.uiElements)
                {
                    if (ui.SpriteID == Textures.UI.locationInfoUI)
                    {
                        button.Position = new Vector2(ui.Position.X + ui.Size.X - button.Size.X, ui.Position.Y + 22);
                        button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }
            }
            if (button.Name == "DisplayInventory")
            {
                foreach (UIElement ui in Lists.uiElements)
                {
                    if (ui.SpriteID == Textures.UI.locationInfoUI)
                    {
                        button.Position = new Vector2(ui.Position.X + ui.Size.X - (button.Size.X * 2.2f), ui.Position.Y + 22);
                        button.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }
            }
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
            if (button.Name == "QuestAcceptButton")
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
            if (button.Name == "HandInQuestButton")
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
            
        }
    }
}

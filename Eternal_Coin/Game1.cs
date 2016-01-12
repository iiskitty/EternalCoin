using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using System.Text;

namespace Eternal_Coin
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            XmlDocument options = new XmlDocument();
            options.Load("./Content/Options.xml");
            XmlNode optionsNode = options.DocumentElement.SelectSingleNode("/options");

            if (Convert.ToBoolean(optionsNode["fullscreen"].InnerText) == true)
            {
                graphics.IsFullScreen = true;
            }
            else
            {
                graphics.IsFullScreen = false;
            }

            if (Convert.ToBoolean(optionsNode["enabledebuglog"].InnerText) == true)
            {
                GVar.debugLogEnabled = true;
                GVar.debugLevel = Convert.ToInt32(optionsNode["debuglevel"].InnerText);
                GVar.CreateDebugLog();
            }
            
            GVar.trueScreenX = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            GVar.trueScreenY = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            
            GVar.screenToRefX = (GVar.gameScreenX / GVar.trueScreenX);
            GVar.screenToRefY = (GVar.gameScreenY / GVar.trueScreenY);
            GVar.refToScreenX = (GVar.trueScreenX / GVar.gameScreenX);
            GVar.refToScreenY = (GVar.trueScreenY / GVar.gameScreenY);
            GVar.exitGame = false;

            GVar.Volume.Audio.volume = 0.3f;
            GVar.Volume.Audio.pan = 0f;
            GVar.Volume.Audio.pitch = 0f;
            
            GVar.currentGameState = GVar.GameState.mainMenu;
            GVar.LogDebugInfo("TrueScreen X,Y: " + GVar.trueScreenX.ToString() + " " + GVar.trueScreenY.ToString(), 1);
            GVar.LogDebugInfo("GameScreen X,Y: " + GVar.gameScreenX.ToString() + " " + GVar.gameScreenY.ToString(), 1);
            if (GVar.trueScreenX < GVar.gameScreenX)
            {
                GVar.gameScreenX = GVar.trueScreenX;
            }

            if (GVar.trueScreenY < GVar.gameScreenY)
            {
                GVar.gameScreenY = GVar.trueScreenY;
            }

            graphics.PreferredBackBufferWidth = (int)GVar.gameScreenX;
            graphics.PreferredBackBufferHeight = (int)GVar.gameScreenY;

            GVar.LogDebugInfo("Set Game Screen X,Y: " + GVar.gameScreenX.ToString() + " " + GVar.gameScreenY.ToString(), 1);

            graphics.ApplyChanges();

            Window.Position = new Point(10, 10);
            
            Lists.InitializeLists();
            Dictionaries.InitializeDictionaries();
            base.Initialize();
            Item.CreateItems();
            Vector.InitilizeVectors();
            Lists.uiElements = UIElement.AddUIElements(Lists.uiElements);
            InventoryManager.CreateInventories();
            MainMenu.LoadMainMenu();
            //Load.LoadLocationNodes();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Load.LoadItemData(Content);
            Textures.LoadTextures(Content);
            Fonts.LoadFonts(Content);
            Sounds.LoadSounds(Content);
            Load.LoadDisplayPictures(Content);
            Attack.LoadAttacks(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.R) && GVar.currentGameState == GVar.GameState.game)
            {
                for (int i = 0; i < 40; i++)
                {
                    if (InventoryManager.playerInventory.itemSlots[i].item == null)
                    {
                        Item item;
                        item = ItemBuilder.BuildItem(Dictionaries.items["Iron Ring"]);
                        Lists.playerItems.Add(item);
                        InventoryManager.playerInventory.itemSlots[i].item = item;
                        item = ItemBuilder.BuildItem(Dictionaries.items["Bronze Leggings"]);
                        Lists.playerItems.Add(item);
                        InventoryManager.playerInventory.itemSlots[i + 1].item = item;
                        break;
                    }
                }
            }

            if (GVar.loadData)
            {
                Load.LoadWorldMaps(Content);
                foreach (Entity e in Lists.entity)
                {
                    WorldMap.SelectNewMap(e.CurrentLocation[0]);
                }
                
                GVar.loadData = false;
            }

            GVar.windowIsActive = this.IsActive;
            if (GVar.exitGame)
            {
                if (graphics.IsFullScreen)
                {
                    graphics.ToggleFullScreen();
                }
                Exit();
            }

            if (GVar.toggleFullScreen)
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
                if (!graphics.IsFullScreen)
                {
                    Window.Position = new Point(10, 10);
                }
                XmlDocument options = new XmlDocument();
                options.Load("./Content/Options.xml");
                XmlNode optionsNode = options.DocumentElement.SelectSingleNode("/options/fullscreen");
                optionsNode.InnerText = Convert.ToString(graphics.IsFullScreen);
                options.Save("./Content/Options.xml");
                GVar.toggleFullScreen = false;
            }

            Updates.CheckForStateChange();
            Colours.UpdateColours(gameTime);
            InputManager.Update();
            MouseManager.Update(graphics.IsFullScreen);

            SoundManager.CheckSounds();

            switch (GVar.currentGameState)
            {
                case GVar.GameState.mainMenu:
                    MainMenu.UpdateMainMenu(gameTime);
                    break;

                case GVar.GameState.options:
                    Options.UpdateOptions(gameTime);
                    break;

                case GVar.GameState.game:
                    MainWorld.UpdateMainWorld(gameTime);
                    break;

                case GVar.GameState.chooseCharacter:
                    CreateCharacter.UpdateCreateCharacter(gameTime);
                    break;

                case GVar.GameState.inventory:
                    InventoryManager.ManagePlayerInventories(gameTime, InventoryManager.playerInventory, InventoryManager.mouseInventory, InventoryManager.characterInventory);
                    break;

                case GVar.GameState.shop:
                    InventoryManager.ManageShopInventories(gameTime, InventoryManager.playerInventory, InventoryManager.mouseInventory, InventoryManager.shopInventory);
                    break;

                case GVar.GameState.battle:
                    Battle.UpdateBattle(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Crimson);

            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.Draw(Textures.cursor, new Rectangle(MouseManager.mouseBounds.X, MouseManager.mouseBounds.Y, 48, 48), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.5f);
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.GameState == GVar.currentGameState && ui.Draw)
                {
                    UI.DrawUIElement(spriteBatch, ui.SpriteID, ui.Position, ui.Size, ui.Layer);
                }
            }

            if (Colours.drawBlackFade)
            {
                Colours.DrawBlackFadeInOut(spriteBatch);
            }

            if (Colours.drawFadeMap)
                Colours.DrawMapFadeOut(spriteBatch);

            Location.DrawLocationNames(spriteBatch, gameTime);

            switch (GVar.currentGameState)
            {
                case GVar.GameState.mainMenu:
                    MainMenu.DrawMainMenu(spriteBatch, gameTime);
                    break;

                case GVar.GameState.options:
                    Options.DrawOptions(spriteBatch, gameTime);
                    break;

                case GVar.GameState.game:
                    MainWorld.DrawMainWorld(spriteBatch, gameTime);
                    break;

                case GVar.GameState.chooseCharacter:
                    CreateCharacter.DrawCreateCharacter(spriteBatch, gameTime);
                    break;
                case GVar.GameState.inventory:
                    InventoryManager.DrawPlayerInventories(spriteBatch, gameTime, InventoryManager.playerInventory, InventoryManager.mouseInventory, InventoryManager.characterInventory);
                    break;

                case GVar.GameState.shop:
                    InventoryManager.DrawShopInventories(spriteBatch, gameTime, InventoryManager.playerInventory, InventoryManager.mouseInventory, InventoryManager.shopInventory);
                    break;

                case GVar.GameState.battle:
                    Battle.DrawBattle(spriteBatch, gameTime);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

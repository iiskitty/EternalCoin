﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Xml;

//TODO ONGOING read through all the code, comment the code, fix and tweak the code where necessary

//TODO make a way for quests to require other quests to be completed. (possibly done, need more testing.)

//TODO Make different resolutions above 1280x720 possible. setting positions based on true screen size.


namespace Eternal_Coin
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    private void ToggleFullScreen(bool fullScreen)
    {
      graphics.IsFullScreen = fullScreen;
      GVar.isFullScreen = fullScreen;

      if (!graphics.IsFullScreen)
      {
        graphics.PreferredBackBufferHeight = (int)GVar.gameScreenY;
        graphics.PreferredBackBufferWidth = (int)GVar.gameScreenX;
        GVar.currentScreenX = GVar.gameScreenX;
        GVar.currentScreenY = GVar.gameScreenY;
        Window.IsBorderless = false;
        
        Window.Position = new Point(150, 50);
      }
      else if (graphics.IsFullScreen)
      {
        graphics.PreferredBackBufferHeight = (int)GVar.trueScreenY;
        graphics.PreferredBackBufferWidth = (int)GVar.trueScreenX;
        GVar.currentScreenX = GVar.trueScreenX;
        GVar.currentScreenY = GVar.trueScreenY;
        Window.IsBorderless = true;
        
      }
      if (GVar.player != null)
        GVar.player.ResetLocation(new Vector2(GVar.currentScreenX, GVar.currentScreenY));
      UIElement.SetUIPositions(new Vector2(GVar.currentScreenX, GVar.currentScreenY));
      MainWorldButtons.SetMainWorldButtonPositions();
      InventoryManager.ResetItemSlotPositions();

      graphics.ApplyChanges();
      //opens Options.xml and saves the new value for fullscreen
      XmlDocument options = new XmlDocument(); //creating an xml document
      options.Load("./Content/Options.xml"); //loading the Options.xml document with the created one
      XmlNode optionsNode = options.DocumentElement.SelectSingleNode("/options/fullscreen"); //creating a node and setting to the fullscreen node in the loaded document
      optionsNode.InnerText = Convert.ToString(graphics.IsFullScreen); //setting the value of the node to the fullscreen value(true or false)
      options.Save("./Content/Options.xml"); //saving the document
      GVar.toggleFullScreen = false;
    }

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
      MouseManager.mouse.InventoryItemClicked += InventoryManager.OnMouseClicked; //Add OnMouseClicked Function in InventoryManager to InventoryItemClicked Delegate.
      MouseManager.mouse.ButtonClicked += Button.OnButtonClicked; //Add OnButtonClicked function in Button to ButtonClicked delegate.

      IsMouseVisible = false;
      //Loads Options.xml to 
      XmlDocument options = new XmlDocument();
      options.Load("./Content/Options.xml");
      XmlNode optionsNode = options.DocumentElement.SelectSingleNode("/options");

      string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);//sets the directory for GameSave files.
      GVar.savedGameLocation = Path.Combine(saveFolder, "EternalCoin\\GameSaves\\");

      string fileFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);//sets the directory for GameFiles corresponding to GameSave's.
      GVar.gameFilesLocation = Path.Combine(fileFolder, "EternalCoin\\GameFiles\\");

      string debugFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);//sets the directory for debug logs.
      GVar.debugFilesLocation = Path.Combine(debugFolder, "EternalCoin\\DebugFiles\\");

      

      if (!Directory.Exists(GVar.savedGameLocation))//if the GameSave directory doesn't exist, create it.
        Directory.CreateDirectory(GVar.savedGameLocation);
      GVar.savedGameLocation += "GameSave";//add GameSave to the end of the path so files will be named GameSave0.xml, GameSave1.xml etc.

      

      //debuglog option check
      if (Convert.ToBoolean(optionsNode["enabledebuglog"].InnerText) == true)
      {
        GVar.debugLogEnabled = true;
        GVar.debugLevel = Convert.ToInt32(optionsNode["debuglevel"].InnerText);
        GVar.CreateDebugLog();
      }

      //getting x&y size of the screen
      GVar.trueScreenX = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
      GVar.trueScreenY = GraphicsDevice.Adapter.CurrentDisplayMode.Height;

      //game will exit if true
      GVar.exitGame = false;

      //setting volume for sounds
      GVar.Volume.Audio.volume = 0.3f;
      GVar.Volume.Audio.pan = 0f;
      GVar.Volume.Audio.pitch = 0f;

      //setting volume for music
      GVar.Volume.Music.volume = 0.3f;
      GVar.Volume.Music.pan = 0f;
      GVar.Volume.Music.pitch = 0f;

      //setting gamestate to start game on mainmenu
      GVar.currentGameState = GVar.GameState.mainMenu;

      //logging size of screen and size of game
      GVar.LogDebugInfo("TrueScreen X,Y: " + GVar.trueScreenX.ToString() + " " + GVar.trueScreenY.ToString(), 1);
      GVar.LogDebugInfo("GameScreen X,Y: " + GVar.gameScreenX.ToString() + " " + GVar.gameScreenY.ToString(), 1);

      //checking if size of game is larger than size of screen
      //if game is larger than screen X or Y set the size of the game the same as the screen
      if (GVar.trueScreenX < GVar.gameScreenX)
        GVar.gameScreenX = GVar.trueScreenX;

      if (GVar.trueScreenY < GVar.gameScreenY)
        GVar.gameScreenY = GVar.trueScreenY;

      //logging the game size after change(if any)
      GVar.LogDebugInfo("Set Game Screen X,Y: " + GVar.gameScreenX.ToString() + " " + GVar.gameScreenY.ToString(), 1);

      //graphics.ApplyChanges();

      //setting position of game window to top left corner
      Window.Position = new Point(150, 50);

      //creating lists for game use
      Lists.InitializeLists();
      //creating dictionaries for game use
      Dictionaries.InitializeDictionaries();
      base.Initialize();
      //creating items used in game
      Item.CreateItems();
      //creating vectors for game use(even though I rarely use them)
      Vector.InitilizeVectors();
      //creating all UI elements for game use
      Lists.uiElements = UIElement.AddUIElements(Lists.uiElements);
      ToggleFullScreen(Convert.ToBoolean(optionsNode["fullscreen"].InnerText));
      //creating inventories for game use
      InventoryManager.CreateInventories();
      //loading the main menu
      MainMenu.LoadMainMenu();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(GraphicsDevice);
      //loading item data (materials and types)
      Load.LoadItemData(Content);
      //loading projectiles(magic and physical)
      Load.LoadProjectiles(Content);
      //loading textures for game
      Textures.LoadTextures(Content);
      //loading fonts for game
      Fonts.LoadFonts(Content);
      //loading sounds for game
      Sounds.LoadSounds(Content);
      //loading music for game
      Music.LoadMusic(Content);
      //loading display pictures(for choosing characters)
      Load.LoadDisplayPictures(Content);
      //loading all attacks for all display pictures(characters)
      for (int i = 0; i < Lists.displayPictureIDs.Count; i++)
        Attack.LoadAttacks(Content, Lists.displayPictureIDs[i]);
      //loading all attacks for all ememies
      for (int i = 0; i < Lists.eDisplayPictureIDs.Count; i++)
        Attack.LoadEnemyAttacks(Content, Lists.eDisplayPictureIDs[i]);

      SoundManager.PlaySong(Music.menuMusic);
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent() { }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      //this is my hacks, don't worry about it.
      #region HAX
      if (InputManager.IsKeyPressed(Keys.R) && GVar.currentGameState == GVar.GameState.game)
      {
        Item item;
        item = ItemBuilder.BuildItem(Dictionaries.items["Iron Ring"]);
        Lists.playerItems.Add(item);
        InventoryManager.playerInventory.ItemSlots[38].item = item;
        item = ItemBuilder.BuildItem(Dictionaries.items["Fire Ball"]);
        Lists.playerItems.Add(item);
        InventoryManager.playerInventory.ItemSlots[37].item = item;

      }
      if (InputManager.IsKeyPressed(Keys.M) && GVar.currentGameState == GVar.GameState.game)
      {
        GVar.silverMoney += 1000;
      }
      #endregion

      //maps need to be loaded after eveything because shit fucks up
      if (GVar.loadData)
      {
        Load.LoadWorldMaps(Content);
        //setting map based on players current location
        WorldMap.SelectNewMap(GVar.player.CurrentLocation);
        
        GVar.loadData = false;
      }

      //checking if the game window is active(tabbed out or not)
      GVar.windowIsActive = IsActive;

      //checks if should exit game or not
      if (GVar.exitGame)
      {
        //if (graphics.IsFullScreen)
          //ToggleFullScreen(false);//toggles full screen if is fullscreen
        Exit();//exit game
      }

      //sets fullscreen value to the opposite 
      if (GVar.toggleFullScreen)
        ToggleFullScreen(!graphics.IsFullScreen);

      Button.UpdateButtons(gameTime);
      //checks for GameState changes(main menu to choose charater, game to inventory etc.)
      Updates.CheckForStateChange();
      //updates colours for game objects that use fading and the black fade between state changes
      Colours.UpdateColours(gameTime);
      //updates the InputManager that checks for inputs from user
      InputManager.Update();
      //updates the MouseManager keeping the mouse position and giving it a rectangle for clicking on things
      MouseManager.mouse.Update();
      //checks for sounds that are no longer being used and...sends them off to live on a farm
      SoundManager.CheckSounds();

      //calls the Update function for the current gameState
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
          InventoryManager.UpdateInventories(gameTime);
          break;
        case GVar.GameState.shop:
          Shop.UpdateShopInventories(gameTime, InventoryManager.playerInventory, InventoryManager.mouseInventory, InventoryManager.shopInventory);
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
      //cornflowerblue is so last season
      GraphicsDevice.Clear(Color.Crimson);

      //spritebatch begin with sorting front to back(layering)
      spriteBatch.Begin(SpriteSortMode.FrontToBack);

      MouseManager.mouse.Draw(spriteBatch);
      
      //if the gamestate is the same as the one a UIElement has and its draw boolean is true, it will be drawn
      for (int i = 0; i < Lists.uiElements.Count; i++)
        if (Lists.uiElements[i].GameState == GVar.currentGameState && Lists.uiElements[i].Draw)
          UI.DrawUIElement(spriteBatch, Lists.uiElements[i].SpriteID, Lists.uiElements[i].Position, Lists.uiElements[i].Size, Lists.uiElements[i].Layer);

      //if true draws the black fade in and out between gamestats
      if (Colours.drawBlackFade)
        Colours.DrawBlackFadeInOut(spriteBatch);

      Button.DrawButtons(spriteBatch);

      //when changing main locations this fades the map to the new one
      if (Colours.drawFadeMap)
        Colours.DrawMapFadeOut(spriteBatch);

      //draws current and connecting location node names
      Location.DrawLocationNames(spriteBatch);

      //calls the Draw function for the current gamestate
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
          InventoryManager.DrawPlayerInventories(spriteBatch, gameTime);
          if (graphics.IsFullScreen)
            MainWorld.DrawMainWorld(spriteBatch, gameTime);
          break;
        case GVar.GameState.shop:
          Shop.DrawShopInventories(spriteBatch, gameTime);
          if (graphics.IsFullScreen)
            MainWorld.DrawMainWorld(spriteBatch, gameTime);
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

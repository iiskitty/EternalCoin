using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eternal_Coin
{
  public class GeneratedButton
  {
    public bool mouseOver = false;

    Rectangle bounds;

    Color colour;

    SpriteFont font;

    string id;
    int fontSize;
    string name;
    string state;

    Vector2 position;
    Vector2 size;

    /// <summary>
    /// Generates a button based on the string state that is passed in.
    /// </summary>
    /// <param name="position">Position of the button.</param>
    /// <param name="colour">Colour of the button.</param>
    /// <param name="fontSize">Size of the font determines size of button(10, 12, 14, 16, 18 or 20)</param>
    /// <param name="id">identification for the button.</param>
    /// <param name="name">Name of the button.</param>
    /// <param name="state">State of the button.</param>
    public GeneratedButton(Vector2 position, Color colour, int fontSize, string id, string name, string state)
    {
      this.position = position;
      this.colour = colour;
      this.fontSize = fontSize;
      this.id = id;
      this.name = name;
      this.state = state;

      switch (fontSize)
      {
        case 10:
          font = Fonts.lucidaConsole10Regular;
          size = new Vector2(name.Length * 8, Textures.Button.middleLight.Height - 13);
          break;
        case 12:
          font = Fonts.lucidaConsole12Regular;
          size = new Vector2(name.Length * 10, Textures.Button.middleLight.Height - 10);
          break;
        case 14:
          font = Fonts.lucidaConsole14Regular;
          size = new Vector2(name.Length * 11, Textures.Button.middleLight.Height - 8);
          break;
        case 16:
          font = Fonts.lucidaConsole16Regular;
          size = new Vector2(name.Length * 13, Textures.Button.middleLight.Height - 6);
          break;
        case 18:
          font = Fonts.lucidaConsole18Regular;
          size = new Vector2(name.Length * 14, Textures.Button.middleLight.Height - 5);
          break;
        case 20:
          font = Fonts.lucidaConsole20Regular;
          size = new Vector2(name.Length * 16, Textures.Button.middleLight.Height);
          break;
      }


    }

    public void Update(float gameTime) => bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X + Textures.Button.leftLightSide.Width * 2, (int)size.Y);//sets rectangle(bounding box) based on position and size.

    /// <summary>
    /// Draws light shaded button peices.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch to draw sprites.</param>
    public void DrawLightButton(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(Textures.Button.leftLightSide, new Rectangle((int)position.X, (int)position.Y, Textures.Button.leftDarkSide.Width, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
      spriteBatch.Draw(Textures.Button.middleLight, new Rectangle((int)position.X + Textures.Button.leftLightSide.Width, (int)position.Y, (int)size.X, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
      spriteBatch.Draw(Textures.Button.rightLightSide, new Rectangle((int)position.X + Textures.Button.leftLightSide.Width + (int)size.X, (int)position.Y, Textures.Button.rightLightSide.Width, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
      spriteBatch.DrawString(font, name, new Vector2(position.X + Textures.Button.leftLightSide.Width, position.Y + 6), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
    }

    /// <summary>
    /// Draws dark shaded button peices.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch to draw sprites.</param>
    public void DrawDarkButton(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(Textures.Button.leftDarkSide, new Rectangle((int)position.X, (int)position.Y, Textures.Button.leftDarkSide.Width, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
      spriteBatch.Draw(Textures.Button.middleDark, new Rectangle((int)position.X + Textures.Button.leftDarkSide.Width, (int)position.Y, (int)size.X, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
      spriteBatch.Draw(Textures.Button.rightDarkSide, new Rectangle((int)position.X + Textures.Button.leftDarkSide.Width + (int)size.X, (int)position.Y, Textures.Button.rightDarkSide.Width, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
      spriteBatch.DrawString(font, name, new Vector2(position.X + Textures.Button.leftDarkSide.Width, position.Y + 6), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
    }

    public Rectangle Bounds { get { return bounds; } set { bounds = value; } }
    public Vector2 Position { get { return position; } set { position = value; } }
    public Vector2 Size { get { return size; } set { size = value; } }
    public int FontSize { get { return fontSize; } set { fontSize = value; } }
    public string ID { get { return id; } set { id = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string State { get { return state; } set { state = value; } }
  }

  public class Button : Object
  {
    public enum ButtonPosition
    {
      topleft,
      topcenter,
      topright,
      middleleft,
      middlecenter,
      middleright,
      bottomleft,
      bottomcenter,
      bottomright
    }

    public string extraInfo;
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

    public override void Update(float gameTime) => bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);//sets rectangle(bounding box) based on position and size.

    public override void HandleMovement(Vector2 pos, float gameTime) { }

    public override void AnimationDone(string animation) { }

    public static Button CreateButton(Texture2D sprite, UIElement uiElement, Vector2 size, Vector2 padding, string name, string state, ButtonPosition position)
    {
      Button button = new Button(sprite, SetButtonPosition(uiElement, size, padding, position), size, Color.White, name, state, 0f);
      button.PlayAnimation(GVar.AnimStates.Button.def);

      return button;
    }

    public static Vector2 SetButtonPosition(UIElement uiElement, Vector2 buttonSize, Vector2 padding, ButtonPosition buttonPosition)
    {
      Vector2 pos = new Vector2();
      switch (buttonPosition)
      {
        case ButtonPosition.topleft:
          pos = new Vector2(uiElement.Position.X + padding.X, uiElement.Position.Y + padding.Y);
          break;
        case ButtonPosition.topcenter:
          pos = new Vector2(uiElement.Position.X + (uiElement.Size.X / 2 - buttonSize.X / 2) + padding.X, uiElement.Position.Y + padding.Y);
          break;
        case ButtonPosition.topright:
          pos = new Vector2(uiElement.Position.X + (uiElement.Size.X - buttonSize.X) + padding.X, uiElement.Position.Y + padding.Y);
          break;
        case ButtonPosition.middleleft:
          pos = new Vector2(uiElement.Position.X + padding.X, uiElement.Position.Y + (uiElement.Size.Y / 2 - buttonSize.Y) + padding.Y);
          break;
        case ButtonPosition.middlecenter:
          pos = new Vector2(uiElement.Position.X + (uiElement.Size.X / 2 - buttonSize.X / 2) + padding.X, uiElement.Position.Y + (uiElement.Size.Y / 2 - buttonSize.Y / 2) + padding.Y);
          break;
        case ButtonPosition.middleright:
          pos = new Vector2(uiElement.Position.X + (uiElement.Size.X - buttonSize.X) + padding.X, uiElement.Position.Y + (uiElement.Size.Y / 2 - buttonSize.Y / 2) + padding.Y);
          break;
        case ButtonPosition.bottomleft:
          pos = new Vector2(uiElement.Position.X + padding.X, uiElement.Position.Y + (uiElement.Size.Y - buttonSize.Y) + padding.Y);
          break;
        case ButtonPosition.bottomcenter:
          pos = new Vector2(uiElement.Position.X + (uiElement.Size.X / 2 - buttonSize.X / 2) + padding.X, uiElement.Position.Y + (uiElement.Size.Y - buttonSize.Y) + padding.Y);
          break;
        case ButtonPosition.bottomright:
          pos = new Vector2(uiElement.Position.X + (uiElement.Size.X - buttonSize.X) + padding.X, uiElement.Position.Y + (uiElement.Size.Y - buttonSize.Y) + padding.Y);
          break;
      }
      return pos;
    }
    
    /// <summary>
    /// Function will be called on left mouse click, only does stuff when a button is colliding with the mouse.
    /// </summary>
    /// <param name="button">Button being clicked.</param>
    public static void OnButtonClicked(Button button)
    {
      if (button != null && MouseManager.mouse.mouseBounds.Intersects(button.Bounds))
      {
        switch (GVar.currentGameState)
        {
          case GVar.GameState.mainMenu:
            if (button.name.Contains("PlayButton"))
              MainMenuButtons.ChangeToChooseCharacter();
            else if (button.name.Contains("OptionsButton"))
              UniversalButtons.ChangeToOptions();
            else if (button.name.Contains("ExitButton"))
              MainMenuButtons.ExitGame();
            break;
          case GVar.GameState.chooseCharacter:
            if (button.name.Contains("NewCharacter"))
              NewCharButtons.NewCharacter();
            else if (button.name.Contains("StartGame") && GVar.playerName != string.Empty && !GVar.choosingDP)
              NewCharButtons.StartGame(button);
            else if (button.name.Contains("MainMenu"))
              UniversalButtons.ChangeToMainMenu();
            else if (button.name.Contains("ChooseDP") && !GVar.choosingDP)
              NewCharButtons.ChoosingDP();
            else if (button.name.Contains("LoadGame"))
              NewCharButtons.LoadGame(button);
            else if (button.name.Contains("DeleteGame"))
              NewCharButtons.DeleteGame(button);
            else if (button.name.Contains("ChooseDisplayPicture") && GVar.preventclick == 0)
              NewCharButtons.ChooseDP(button);
            break;
          case GVar.GameState.game:
            if (button.name.Contains("OpenShop") && !GVar.gamePaused)
              MainWorldButtons.OpenShop(button);
            else if (button.name.Contains("DisplayQuests") && !GVar.gamePaused)
              UI.DisplayQuests();
            else if (button.name.Contains("ViewQuests") && !GVar.gamePaused)
              MainWorldButtons.ViewQuests();
            else if (button.name.Contains("DisplayInventory") && !GVar.gamePaused)
              MainWorldButtons.DisplayInventory();
            else if (button.name.Contains("CloseQuestListUI") && !GVar.gamePaused)
              MainWorldButtons.CloseQuestListUI(button);
            else if (button.name.Contains("CloseQuestInfoUI") && !GVar.gamePaused)
              MainWorldButtons.CloseQuestInfoUI(button);
            else if (button.name.Contains("CloseNPCUIButton") && !GVar.gamePaused)
              MainWorldButtons.CloseNPCUI(button);
            else if (button.name.Contains("MainMenu") && GVar.gamePaused)
              MainWorldButtons.QuitGame();
            else if (button.name.Contains("Options") && GVar.gamePaused)
              UniversalButtons.ChangeToOptions();
            if (!UIElement.IsUIElementActive(Textures.UI.NPCInfoUI))
            {
              if (button.name.Contains("LookEyeButton") && !GVar.gamePaused)
                LocationButtons.SearchLocation();
              else if (button.name.Contains("EnterLocation") && !GVar.gamePaused)
                LocationButtons.EnterLocation();
              else if (button.name.Contains("ExitLocation") && !GVar.gamePaused)
                LocationButtons.ExitLocation();
              else if (button.name.Contains("NPCButton") && !GVar.gamePaused)
                LocationButtons.OpenNPCUI();
              else if (button.name.Contains("ShopButton") && !GVar.gamePaused)
                LocationButtons.OpenShopUI();
            }
            break;
          case GVar.GameState.shop:
          case GVar.GameState.inventory:
            if (button.name.Contains("CloseInventory"))
              InventoryButtons.CloseInventory();
            break;
          case GVar.GameState.battle:

            break;
          case GVar.GameState.options:
            if (button.name.Contains("MainMenu"))
              UniversalButtons.ChangeToMainMenu();
            else if (button.name.Contains("BackToGame"))
              OptionsButtons.BackToGame();
            else if (button.name.Contains("ToggleFullScreen"))
              OptionsButtons.ToggleFullScreen();
            else if (button.name.Contains("ToggleDebugLog"))
              OptionsButtons.ToggleDebugLog();
            break;
        }
      }
      //TODO set up button click event
    }

    /// <summary>
    /// Updates a list of buttons.
    /// </summary>
    /// <param name="buttons">List of buttons.</param>
    /// <param name="gameTime"></param>
    private static void UpdateButtons(List<Object> buttons, GameTime gameTime)
    {
      for (int i = 0; i < buttons.Count; i++)
      {
        buttons[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        buttons[i].Update(gameTime);

        if (MouseManager.mouse.mouseBounds.Intersects(buttons[i].Bounds))
        {
          GVar.mouseHoveredButton = (Button)buttons[i];
          
          if (buttons[i].CurrentAnimation != GVar.AnimStates.Button.mouseover)
          {
            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.buttonmouseover]);
            buttons[i].PlayAnimation(GVar.AnimStates.Button.mouseover);
          }
        }
        else if (!MouseManager.mouse.mouseBounds.Intersects(buttons[i].Bounds))
        {
          buttons[i].PlayAnimation(GVar.AnimStates.Button.def);
        }
      }
    }
    
    /// <summary>
    /// Updates all button based on current GameState.
    /// </summary>
    /// <param name="gameTime"></param>
    public static void UpdateButtons(GameTime gameTime) //TODO complete this
    {
      switch (GVar.currentGameState)
      {
        case GVar.GameState.mainMenu:
          UpdateButtons(Lists.mainMenuButtons, gameTime);
          break;
        case GVar.GameState.chooseCharacter:
          UpdateButtons(Lists.chooseCharacterButtons, gameTime);
          Lists.savedGames.ForEach(savedGame => UpdateButtons(savedGame.buttons, gameTime));
          if (GVar.choosingDP)
            UpdateButtons(Lists.displayPictureButtons, gameTime);
          break;
        case GVar.GameState.game:
          UpdateButtons(Lists.mainWorldButtons, gameTime);
          if (GVar.player.Bounds.Intersects(GVar.player.CurrentLocation.PlayerPort))
            UpdateButtons(Lists.locationButtons, gameTime);
          break;
        case GVar.GameState.shop:
        case GVar.GameState.inventory:
          UpdateButtons(Lists.inventoryButtons, gameTime);
          break;
        case GVar.GameState.battle:

          break;
        case GVar.GameState.options:
          UpdateButtons(Lists.optionsButtons, gameTime);
          break;
      }
    }

    /// <summary>
    /// Draws a list of buttons.
    /// </summary>
    /// <param name="spriteBatch"></param>
    /// <param name="buttons">List of buttons.</param>
    private static void DrawButtons(SpriteBatch spriteBatch, List<Object> buttons, float layer) => buttons.ForEach(button => button.Draw(spriteBatch, button.SpriteID, button.Bounds, layer, 0f, Vector2.Zero));

    private static void TempDrawOptionButtons(SpriteBatch spriteBatch, List<Object> buttons) //TODO Make sprites and UI for options button and get rid of this function
    {
      for (int i = 0; i < buttons.Count; i++)
      {
        buttons[i].Draw(spriteBatch, buttons[i].SpriteID, buttons[i].Bounds, 0.19f, 0f, Vector2.Zero);

        if (buttons[i].Name == "ToggleFullScreen")
          spriteBatch.DrawString(Fonts.lucidaConsole18Regular, "FullScreen", new Vector2(buttons[i].Position.X + buttons[i].Size.X / 6, buttons[i].Position.Y + buttons[i].Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
        else if (buttons[i].Name == "ToggleDebugLog")
          spriteBatch.DrawString(Fonts.lucidaConsole18Regular, "DebugLog", new Vector2(buttons[i].Position.X + buttons[i].Size.X / 4, buttons[i].Position.Y + buttons[i].Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
        else if (buttons[i].Name == "MainMenu" || buttons[i].Name == "BackToGame")
          spriteBatch.DrawString(Fonts.lucidaConsole18Regular, "Back", new Vector2(buttons[i].Position.X + buttons[i].Size.X / 4, buttons[i].Position.Y + buttons[i].Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);

        if (MouseManager.mouse.mouseBounds.Intersects(buttons[i].Bounds))
          GVar.DrawBoundingBox(buttons[i].Bounds, spriteBatch, Textures.Misc.pixel, 2, 0.2f, Color.Green);
      }
      spriteBatch.Draw(Textures.Misc.background, new Rectangle(0, 0, (int)GVar.currentScreenX, (int)GVar.currentScreenY), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
    }

    /// <summary>
    /// Draws all buttons based on current GameState.
    /// </summary>
    /// <param name="spriteBatch"></param>
    public static void DrawButtons(SpriteBatch spriteBatch) //TODO complete this also
    {
      switch (GVar.currentGameState)
      {
        case GVar.GameState.mainMenu:
          DrawButtons(spriteBatch, Lists.mainMenuButtons, 0.19f);
          break;
        case GVar.GameState.chooseCharacter:
          DrawButtons(spriteBatch, Lists.chooseCharacterButtons, 0.19f);
          if (!GVar.creatingCharacter && !GVar.chooseStory)
            Lists.savedGames.ForEach(savedGame => DrawButtons(spriteBatch, savedGame.buttons, 0.19f));
          if (GVar.choosingDP)
            DrawButtons(spriteBatch, Lists.displayPictureButtons, 0.19f);
          break;
        case GVar.GameState.game:
          DrawButtons(spriteBatch, Lists.mainWorldButtons, 0.19f);
          if (GVar.player.Bounds.Intersects(GVar.player.CurrentLocation.PlayerPort))
            DrawButtons(spriteBatch, Lists.locationButtons, 0.17f);
          break;
        case GVar.GameState.shop:
        case GVar.GameState.inventory:
          DrawButtons(spriteBatch, Lists.inventoryButtons, 0.19f);
          break;
        case GVar.GameState.battle:

          break;
        case GVar.GameState.options:
          TempDrawOptionButtons(spriteBatch, Lists.optionsButtons);
          break;
      }
    }

    //TODO find all update and draw calls for all buttons and delete it, redo it all here^^^^^

    /// <summary>
    /// Creates locations button based on the current location.
    /// </summary>
    /// <param name="node">current location.</param>
    public static void CreateLocationButtons(LocationNode node)
    {
      //if the current location is a Sub location(has an exit button to the main location).
      if (node.State.Contains("Sub") && node.Searched)
      {
        Button exitLocationButton = new Button(Textures.Button.exitLocationButton, new Vector2(GVar.player.Position.X + GVar.player.Size.X / 2 - Vector.locationButtonSize.X / 2, GVar.player.Position.Y + GVar.player.Size.Y / 2 - Vector.locationButtonSize.Y - 45), new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", GVar.States.Button.locationbutton, 0f);//create exit location Button.
        Lists.locationButtons.Add(exitLocationButton);//add exit location Button to LocationButtons.

        CreateButtonsForLocations(node);//create locations buttons
      }
      //if current location is a Main location(has an enter button to the sub locations).
      else if (node.State.Contains("Main") && node.Searched)
      {
        Button enterLocationButton = new Button(Textures.Button.enterLocationButton, new Vector2(GVar.player.Position.X + GVar.player.Size.X / 2 - Vector.locationButtonSize.X / 2, GVar.player.Position.Y + GVar.player.Size.Y / 2 - Vector.locationButtonSize.Y - 45), new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", GVar.States.Button.locationbutton, 0f);//create enter location button.
        Lists.locationButtons.Add(enterLocationButton);//add enter location button to LocationButtons.
      }
      //if current location is any other location(not Sub or Main).
      else
        CreateButtonsForLocations(node);//create locations buttons.

      //cycle through LocationButtons.
      for (int i = 0; i < Lists.locationButtons.Count; i++)
        Lists.locationButtons[i].ColourA = 5;//set locations buttons alpha colour value ready for fade in.
    }

    private static void CreateButtonsForLocations(LocationNode node)
    {
      //if current location is not nothing and has not been searched.
      if (node != null && !node.Searched)
      {
        Button lookEyeButton = new Button(Textures.Button.lookEye, new Vector2(GVar.player.Position.X + GVar.player.Size.X / 2 - Vector.lookEyeSize.X / 2, GVar.player.Position.Y - Vector.lookEyeSize.Y - 5), Vector.lookEyeSize, Color.White, "LookEyeButton", GVar.States.Button.locationbutton, 0f);//create Look Eye(search location) Button.
        Lists.locationButtons.Add(lookEyeButton);//add Look Eye Button to LocationButtons.
      }
      //if current location is not nothing and has been searched and has a NPC.
      if (node != null && node.Searched && node.HasNPC)
      {
        Button npcButton = new Button(Textures.Button.npcButton, new Vector2(GVar.player.Position.X - Vector.locationButtonSize.X * 1.5f, GVar.player.Position.Y + GVar.player.Size.Y / 2 - Vector.locationButtonSize.Y / 2), Vector.locationButtonSize, Color.White, "NPCButton", GVar.States.Button.locationbutton, 0f);//create NPC Button.
        Lists.locationButtons.Add(npcButton);//add NPC Button to LocationButtons.
      }
      //if current location is not nothing and has been searched and has a shop.
      if (node != null && node.Searched && node.HasShop)
      {
        Button shopButton = new Button(Textures.Misc.pixel, new Vector2(GVar.player.Position.X + GVar.player.Size.X + Vector.locationButtonSize.X / 2, GVar.player.Position.Y + GVar.player.Size.Y / 2 - Vector.locationButtonSize.Y / 2), Vector.locationButtonSize, Color.Blue, "ShopButton", GVar.States.Button.locationbutton, 0f);//create a shop button.
        Lists.locationButtons.Add(shopButton);//add Shop Button to LocationButtons.
      }
    }

    /// <summary>
    /// Check if button state has been set to "delete", if so delete it.
    /// </summary>
    public static void CheckButtonsForDelete()
    {
      //cycle through MainWorldButtons.
      for (int i = 0; i < Lists.mainWorldButtons.Count; i++)
        if (Lists.mainWorldButtons[i].State == "delete")//check button state if set to "delete".
          Lists.mainWorldButtons.RemoveAt(i);//delete button.

      for (int i = 0; i < Lists.NPCQuestButtons.Count; i++)
        if (Lists.NPCQuestButtons[i].State == "delete")
          Lists.NPCQuestButtons.RemoveAt(i);
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

        if (MouseManager.mouse.mouseBounds.Intersects(Lists.viewQuestInfoButtons[j].Bounds) && InputManager.IsLMPressed())//check if mouse intersects View Quest Info Buttons and left mouse click.
        {
          SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);

          GVar.LogDebugInfo("ButtonClicked: " + Lists.viewQuestInfoButtons[j].Name, 2);

          if (!UIElement.IsUIElementActive(Textures.UI.questInfoUI))
          {
            Lists.mainWorldButtons.Add(CreateButton(Textures.Misc.pixel, UIElement.GetUIElement(Textures.UI.questInfoUI), new Vector2(20, 20), Vector2.Zero, "CloseQuestInfoUI", "Alive", ButtonPosition.topleft));
            UIElement.ActivateUIElement(Textures.UI.questInfoUI);
            GVar.questInfo = Lists.quests[j].Description;//set quest info to clicked active quest.
            GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);//wrap quest info text to fit Quest Info UI.
          }
          else if (UIElement.IsUIElementActive(Textures.UI.questInfoUI))
          {
            GVar.questInfo = Lists.quests[j].Description;//set quest info to clicked active quest.
            GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);//wrap quest info text to fit Quest Info UI.
          }
        }
      }
    }
  }
}

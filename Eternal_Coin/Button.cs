using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
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

    public void Update(float gameTime)
    {
      bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X + Textures.Button.leftLightSide.Width * 2, (int)size.Y);//sets rectangle(bounding box) based on position and size.
    }

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

    public override void Update(float gameTime)
    {
      bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);//sets rectangle(bounding box) based on position and size.
    }

    public override void HandleMovement(Vector2 pos, float gameTime) { }

    public override void AnimationDone(string animation) { }

    public static void CreateButtonsForLocations(LocationNode node)
    {
      //if current location is not nothing and has not been searched.
      if (GVar.location != null && !GVar.location.Searched)
      {
        Button lookEyeButton = new Button(Textures.Button.lookEye, node.Position, Vector.lookEyeSize, Color.White, "LookEyeButton", GVar.States.Button.locationbutton, 0f);//create Look Eye(search location) Button.
        Lists.locationButtons.Add(lookEyeButton);//add Look Eye Button to LocationButtons.
      }
      //if current location is not nothing and has been searched and has a NPC.
      if (GVar.location != null && GVar.location.Searched && GVar.location.HasNPC)
      {
        Button npcButton = new Button(Textures.Button.npcButton, node.Position, Vector.locationButtonSize, Color.White, "NPCButton", GVar.States.Button.locationbutton, 0f);//create NPC Button.
        Lists.locationButtons.Add(npcButton);//add NPC Button to LocationButtons.
      }
      //if current location is not nothing and has been searched and has a shop.
      if (GVar.location != null && GVar.location.Searched && GVar.location.HasShop)
      {
        Button shopButton = new Button(Textures.Misc.pixel, node.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", GVar.States.Button.locationbutton, 0f);//create a shop button.
        Lists.locationButtons.Add(shopButton);//add Shop Button to LocationButtons.
      }
    }
    
    public static void OnButtonClicked(Button button)
    {
      if (MouseManager.mouse.mouseBounds.Intersects(button.Bounds))
      {
        switch (GVar.currentGameState)
        {
          case GVar.GameState.mainMenu:
            if (button.name.Contains("PlayButton"))
              Updates.ChangeToChooseCharacter();
            else if (button.name.Contains("OptionsButton"))
              Updates.ChangeToOptions();
            else if (button.name.Contains("ExitButton"))
              Updates.ExitGame();
            break;
          case GVar.GameState.chooseCharacter:
            if (button.name.Contains("NewCharacter"))
              Updates.NewCharacter();
            else if (button.name.Contains("StartGame") && GVar.playerName != string.Empty && !GVar.choosingDP)
              Updates.StartGame(button);
            else if (button.name.Contains("MainMenu"))
              Updates.ChangeToMainMenu();
            else if (button.name.Contains("ChooseDP") && !GVar.choosingDP)
              Updates.ChoosingDP();
            else if (button.name.Contains("LoadGame"))
              Updates.LoadGame(button);
            else if (button.name.Contains("DeleteGame"))
              Updates.DeleteGame(button);
            else if (button.name.Contains("ChooseDisplayPicture") && GVar.preventclick == 0)
              Updates.ChooseDP(button);
            break;
          case GVar.GameState.game:
            if (button.name.Contains("OpenShop") && !GVar.gamePaused)
              Updates.OpenShop(button);
            else if (button.name.Contains("DisplayWuests") && !GVar.gamePaused)
              UI.DisplayQuests();
            else if (button.name.Contains("ViewQuests") && !GVar.gamePaused)
              Updates.ViewQuests();
            else if (button.name.Contains("DisplayInventory") && !GVar.gamePaused)
              Updates.DisplayInventory();
            else if (button.name.Contains("CloseQuestListUI") && !GVar.gamePaused)
              Updates.CloseQuestListUI(button);
            else if (button.name.Contains("CloseQuestInfoUI") && !GVar.gamePaused)
              Updates.CloseQuestInfoUI(button);
            else if (button.name.Contains("CloseNPCUIButton") && !GVar.gamePaused)
              Updates.CloseNPCUI(button);
            else if (button.name.Contains("MainMenu") && GVar.gamePaused)
              Updates.QuitGame();
            else if (button.name.Contains("Options") && GVar.gamePaused)
              Updates.ChangeToOptions();
            break;
          case GVar.GameState.shop:
          case GVar.GameState.inventory:
            if (button.name.Contains("CloseInventory"))
              Updates.CloseInventory();
            break;
          case GVar.GameState.battle:

            break;
          case GVar.GameState.options:
            if (button.name.Contains("MainMenu"))
              Updates.ChangeToMainMenu();
            else if (button.name.Contains("BackToGame"))
              Updates.BackToGame();
            else if (button.name.Contains("ToggleFullScreen"))
              Updates.ToggleFullScreen();
            else if (button.name.Contains("ToggleDebugLog"))
              Updates.ToggleDebugLog();
            break;
        }
      }
      //TODO set up button click event
    }

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

    private static void DrawButtons(SpriteBatch spriteBatch, List<Object> buttons)
    {
      for (int i = 0; i < buttons.Count; i++)
      {
        buttons[i].Draw(spriteBatch, buttons[i].SpriteID, buttons[i].Bounds, 0.19f, 0f, Vector2.Zero);
      }
    }

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
      spriteBatch.Draw(Textures.Misc.background, new Rectangle(0, 0, (int)GVar.gameScreenX, (int)GVar.gameScreenY), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
    }

    public static void DrawButtons(SpriteBatch spriteBatch) //TODO complete this also
    {
      switch (GVar.currentGameState)
      {
        case GVar.GameState.mainMenu:
          DrawButtons(spriteBatch, Lists.mainMenuButtons);
          break;
        case GVar.GameState.chooseCharacter:
          DrawButtons(spriteBatch, Lists.chooseCharacterButtons);
          Lists.savedGames.ForEach(savedGame => DrawButtons(spriteBatch, savedGame.buttons));
          if (GVar.choosingDP)
            DrawButtons(spriteBatch, Lists.displayPictureButtons);
          break;
        case GVar.GameState.game:
          DrawButtons(spriteBatch, Lists.mainWorldButtons);
          break;
        case GVar.GameState.shop:
        case GVar.GameState.inventory:
          DrawButtons(spriteBatch, Lists.inventoryButtons);
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
      if (node.State.Contains("Sub"))
      {
        Button exitLocationButton = new Button(Textures.Button.exitLocationButton, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", GVar.States.Button.locationbutton, 0f);//create exit location Button.
        Lists.locationButtons.Add(exitLocationButton);//add exit location Button to LocationButtons.

        CreateButtonsForLocations(node);//create locations buttons
      }
      //if current location is a Main location(has an enter button to the sub locations).
      else if (node.State.Contains("Main"))
      {
        Button enterLocationButton = new Button(Textures.Button.enterLocationButton, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", GVar.States.Button.locationbutton, 0f);//create enter location button.
        Lists.locationButtons.Add(enterLocationButton);//add enter location button to LocationButtons.
      }
      //if current location is any other location(not Sub or Main).
      else
        CreateButtonsForLocations(node);//create locations buttons.

      //cycle through LocationButtons.
      for (int i = 0; i < Lists.locationButtons.Count; i++)
        Lists.locationButtons[i].ColourA = 5;//set locations buttons alpha colour value ready for fade in.
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

          //cycle through UIElements.
          for (int ui = 0; ui < Lists.uiElements.Count; ui++)
          {
            if (Lists.uiElements[ui].SpriteID == Textures.UI.questInfoUI && !Lists.uiElements[ui].Draw)//if Sprite is Quest Info UI and is not active.
            {
              Lists.mainWorldButtons.Add(new Button(Textures.Misc.pixel, new Vector2(), new Vector2(20, 20), Color.Red, "CloseQuestInfoUI", "Alive", 0f));//create Close Quest Info UI Button.
              Lists.uiElements[ui].Draw = true;//activate View Quest Info UI.
              GVar.questInfo = Lists.quests[j].Description;//set quest info to clicked active quest.
              GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);//wrap quest info text to fit Quest Info UI.
            }
            else if (Lists.uiElements[ui].SpriteID == Textures.UI.questInfoUI && Lists.uiElements[ui].Draw)//if Sprite is Quest Info UI and is active.
            {
              GVar.questInfo = Lists.quests[j].Description;//set quest info to clicked active quest.
              GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);//wrap quest info text to fit Quest Info UI.
            }
          }
        }
      }
    }

    /// <summary>
    /// Check for location buttons being clicked.
    /// </summary>
    /// <param name="i">index number from a for loop.</param>
    public static void CheckLocationButtonClick(int i)
    {
      //check if mouse intersects location button and left mouse click and game not paused.
      if (MouseManager.mouse.mouseBounds.Intersects(Lists.locationButtons[i].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
      {
        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
        GVar.LogDebugInfo("ButtonClicked: " + Lists.locationButtons[i].Name, 2);

        //if button is Enter Location Button.
        if (Lists.locationButtons[i].Name == "EnterLocation")
        {
          UI.CloseNPCUI();//deactivate NPC UI.

          Colours.EnableFadeOutMap();//activate map fade out.

          //cycle through connected location nodes of the sub location node of the current location.
          for (int slnc = 0; slnc < GVar.player.CurrentLocation.SubLocNode.LocNodeConnections.Count; slnc++)
          {
            ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.SubLocNode.LocNodeConnections[slnc]);//read the xml file of the connected location node of the sub location node of the current location node.
            GVar.player.CurrentLocation.SubLocNode.LocNodeConnections[slnc].ColourA = 5;//set the alpha colour value of the connected location node of the sub location node of the current location ready for fade in.
          }
          ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.SubLocNode);//read the xml file of the sub location node of the current location.
          XmlNode tempNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");//grab the actions tag from the current locations xml file.
          Quest.CheckAction(tempNode[GVar.XmlTags.Actions.enter].InnerText, GVar.player.CurrentLocation);//check actions for current active quests.

          WorldMap.SelectNewMap(GVar.player.CurrentLocation.SubLocNode);//set new map to the sub location of the current location.
          GVar.player.CurrentLocation.SubLocNode.ColourA = 5;//set the alpha colour value of the sub location of the current location ready for fade in.

          GVar.LogDebugInfo("LocationChange: " + GVar.player.CurrentLocation.SubLocNode.Name, 2);

          GVar.npc = new NPC();//set NPC to nothing.
          Lists.locationButtons.Clear();//clear location buttons ready for next location.
          GVar.player.CurrentLocation = GVar.player.CurrentLocation.SubLocNode;//set the current location to the sub location of the current location.
          CreateLocationButtons(GVar.player.CurrentLocation);//create location buttons for the new current location.
        }
        //if button is Exit Location Button.
        else if (Lists.locationButtons[i].Name == "ExitLocation")
        {
          UI.CloseNPCUI();//deactivate NPC UI.

          Colours.EnableFadeOutMap();//activate map fade out.

          //cycle through current locations main locations connected main location nodes.
          for (int slnc = 0; slnc < GVar.player.CurrentLocation.MainLocNode.LocNodeConnections.Count; slnc++)
          {
            ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode.LocNodeConnections[slnc]);//read current locations connected main locations main location node xml file.
            GVar.player.CurrentLocation.MainLocNode.LocNodeConnections[slnc].ColourA = 5;//set the alpha colour value of the current locations main locations main location node ready for fade in.
          }
          ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode);//read current location connected main location xml file.

          GVar.worldMap.SpriteID = Textures.Misc.worldMap;//set current map to world map.
          GVar.player.CurrentLocation.MainLocNode.ColourA = 5;//set the alpha colour value of the current locations connected main location ready for fade in.

          GVar.LogDebugInfo("LocationChange: " + GVar.player.CurrentLocation.MainLocNode.Name, 2);

          GVar.npc = new NPC();//set NPC to nothing.
          Lists.locationButtons.Clear();//clear location buttons ready for next location.

          //check if current locations connected main location is in fact a main location.
          if (GVar.player.CurrentLocation.MainLocNode.State.Contains("Main"))
          {
            Button enterLocationButton = new Button(Textures.Button.enterLocationButton, GVar.player.CurrentLocation.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", "Alive", 0f);//create Enter Location Button.
            enterLocationButton.PlayAnimation(GVar.AnimStates.Button.def);//set animation state of EnterLocationButton to default.
            Lists.locationButtons.Add(enterLocationButton);//add EnterLocationButton to LocationButtons.
          }
          GVar.player.CurrentLocation = GVar.player.CurrentLocation.MainLocNode;//set current location to current locations connected main location.
        }

        //if current location is not null and current location has been searched.
        if (GVar.location != null && GVar.location.Searched)
        {
          //if button is NPC Button.
          if (Lists.locationButtons[i].Name == "NPCButton")
          {
            //cycle through MainWorldButtons.
            for (int k = 0; k < Lists.mainWorldButtons.Count; k++)
            {
              //if button is Open Shop Button.
              if (Lists.mainWorldButtons[k].Name == "OpenShop")
              {
                Lists.mainWorldButtons[k].State = "delete";//delete Open Shop Button.
              }
            }

            XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");//grab npc tag from the current locations xml file.
            XmlNode locNPCGreeting = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting");
            GVar.npc = new NPC(locNPC[GVar.XmlTags.NPCTags.name].InnerText, string.Empty, Convert.ToBoolean(locNPC[GVar.XmlTags.NPCTags.hasquest].InnerText), locNPC[GVar.XmlTags.NPCTags.currentquest].InnerText);//create new NPC with data from the current locations xml file.
            GVar.npc.Greeting = locNPCGreeting[GVar.XmlTags.NPCTags.Greetings.normalgreeting].InnerText;
            GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, 500);

            for (int j = 0; j < Lists.uiElements.Count; j++)
            {
              //if Sprite is NPC Info UI && is not active.
              if (Lists.uiElements[j].SpriteID == Textures.UI.NPCInfoUI && !Lists.uiElements[j].Draw)
              {
                Button closeNPCUI = new Button(Textures.Button.closeButton, new Vector2(Lists.uiElements[j].Position.X + Lists.uiElements[j].Size.X - Textures.Button.closeButton.Width, Lists.uiElements[j].Position.Y), new Vector2(35, 35), Color.White, "CloseNPCUIButton", "Alive", 0f);//create close NPC UI Button.
                closeNPCUI.PlayAnimation(GVar.AnimStates.Button.def);//set animation state of button to default.
                Button viewQuests = new Button(Textures.Button.questsButton, new Vector2(Lists.uiElements[j].Position.X + Lists.uiElements[j].Size.X - 50, Lists.uiElements[j].Position.Y + Lists.uiElements[j].Size.Y - 20), new Vector2(80, 30), Color.LightBlue, "ViewQuests", "Alive", 0f);
                viewQuests.PlayAnimation(GVar.AnimStates.Button.def);
                Lists.mainWorldButtons.Add(closeNPCUI);//add close NPC UI button to MainWorldButton.
                Lists.mainWorldButtons.Add(viewQuests);
                Lists.uiElements[j].Draw = true;//activate NPC Info UI.
              }
            }
          }
          //if button is Shop Button.
          else if (Lists.locationButtons[i].Name == "ShopButton")
          {
            XmlNode shopKeep = GVar.curLocNode.SelectSingleNode("/location/shop");//grab shop tag from current locations xml file.

            string greeting = Text.WrapText(Fonts.lucidaConsole14Regular, shopKeep["greeting"].InnerText, GVar.npcTextWrapLength);//wrap ShopKeeps greeting to fit in UI.

            GVar.npc = new NPC(shopKeep["name"].InnerText, greeting, false, "QUESTID");//create NPC(ShopKeep) with name and greeting.

            Button openShop = new Button(Textures.Misc.pixel, Vector2.Zero, new Vector2(25, 15), Color.Yellow, "OpenShop", "Alive", 0f);//create OpenShop Button.

            Lists.mainWorldButtons.Add(openShop);//add Button to MainWorldButtons.

            //cycle throughh UIElements.
            for (int k = 0; k < Lists.uiElements.Count; k++)
            {
              //if Sprite is NPC Info UI and is not active.
              if (Lists.uiElements[k].SpriteID == Textures.UI.NPCInfoUI && !Lists.uiElements[k].Draw)
              {
                Button closeNPCUI = new Button(Textures.Button.closeButton, new Vector2(Lists.uiElements[k].Position.X + Lists.uiElements[k].Size.X - Textures.Button.closeButton.Width, Lists.uiElements[k].Position.Y), new Vector2(35, 35), Color.White, "CloseNPCUIButton", "Alive", 0f);//create close NPC Info UI Button.
                closeNPCUI.PlayAnimation(GVar.AnimStates.Button.def);//set animation state for button to default.
                Lists.mainWorldButtons.Add(closeNPCUI);//add button to MainWorldButton.
                Lists.uiElements[k].Draw = true;//activate NPC Info UI.
              }
            }
          }
        }

        //if location in not nothing and has not been searched.
        if (GVar.location != null && !GVar.location.Searched)
        {
          //if button is LookEyeButton.
          if (Lists.locationButtons[i].Name == "LookEyeButton")
          {
            if (GVar.player.CurrentLocation.State.Contains("Sub"))//if location is a sub location.
            {
              Button exitLocationButton = new Button(Textures.Button.exitLocationButton, GVar.player.CurrentLocation.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", "Alive", 0f);//create exit location button.
              exitLocationButton.PlayAnimation(GVar.AnimStates.Button.def);//set animation state of exit location button to default.
              Lists.locationButtons.Add(exitLocationButton);//add exit location button to LocationButtons.
              ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode);//read current locations connectiong main locations xml file.
              GVar.location.Searched = true;//i have two fucking locations from the one location.
              GVar.player.CurrentLocation.MainLocNode.Searched = true;//uh
              XmlNode mainLocNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");//grab location tag from current locations xml file.
              mainLocNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.location.Searched.ToString();//set the searched tag to true.
              SaveXml.SaveLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode);//save the xml file.
              GVar.location = null;//set location to null;
            }
            ReadXml.ReadLocationXmlFile(GVar.player, GVar.player.CurrentLocation);//read current locations xml file.
            GVar.location.Searched = true;//what the fuck am I doing here with two locations???????????????
            GVar.player.CurrentLocation.Searched = true;//huh?
            XmlNode locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");//grab location tag from current locations xml file.
            locNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.location.Searched.ToString();//set searched tag to true.

            locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");//grab action tag from current locations xml file.
            Quest.CheckAction(locNode[GVar.XmlTags.Actions.explore].InnerText, GVar.player.CurrentLocation);//check action against any active quests.

            SaveXml.SaveLocationXmlFile(GVar.player, GVar.player.CurrentLocation);//save current locations xml file.

            //if location has a NPC.
            if (GVar.location.HasNPC)
            {
              Button npcButton = new Button(Textures.Button.npcButton, GVar.player.CurrentLocation.Position, Vector.locationButtonSize, Color.White, "NPCButton", "Alive", 0f);//create NPC button.
              npcButton.PlayAnimation(GVar.AnimStates.Button.def);//set NPC Button animation state to default.
              Lists.locationButtons.Add(npcButton);//add NPC Button to LocationButtong.
            }

            //if location has a shop.
            if (GVar.location.HasShop)
            {
              Button shopButton = new Button(Textures.Misc.pixel, GVar.player.CurrentLocation.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", "Alive", 0f);//create Shop Button.
              shopButton.PlayAnimation(GVar.AnimStates.Button.def);//set Shop Button animation state to default.
              Lists.locationButtons.Add(shopButton);//add Shop Button to LocationButtons.
            }

            Lists.locationButtons.RemoveAt(i);//delete the LookEyeButton.
          }
        }
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eternal_Coin
{
    public class GeneratedButton
    {
        public bool mouseOver = false;

        Rectangle bounds;

        Color colour;

        string name;
        string state;

        Vector2 position;
        Vector2 size;

        public GeneratedButton(Vector2 position, Color colour, string name, string state)
        {
            this.position = position;
            this.colour = colour;
            this.name = name;
            this.state = state;

            size = new Vector2(state.Length * 19, Textures.Button.middleLight.Height);
        }

        public void Update(float gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X + Textures.Button.leftLightSide.Width * 2, (int)size.Y);
        }

        public void DrawLightButton(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.Button.leftLightSide, new Rectangle((int)position.X, (int)position.Y, Textures.Button.leftLightSide.Width, Textures.Button.leftLightSide.Height), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.Draw(Textures.Button.middleLight, new Rectangle((int)position.X + Textures.Button.leftLightSide.Width, (int)position.Y, (int)size.X, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.Draw(Textures.Button.rightLightSide, new Rectangle((int)position.X + Textures.Button.leftLightSide.Width + (int)size.X, (int)position.Y, Textures.Button.rightLightSide.Width, Textures.Button.rightLightSide.Height), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.DrawString(Fonts.lucidaConsole24Regular, state, new Vector2(position.X + Textures.Button.leftLightSide.Width, position.Y + 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
        }

        public void DrawDarkButton(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.Button.leftDarkSide, new Rectangle((int)position.X, (int)position.Y, Textures.Button.leftDarkSide.Width, Textures.Button.leftDarkSide.Height), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.Draw(Textures.Button.middleDark, new Rectangle((int)position.X + Textures.Button.leftDarkSide.Width, (int)position.Y, (int)size.X, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.Draw(Textures.Button.rightDarkSide, new Rectangle((int)position.X + Textures.Button.leftDarkSide.Width + (int)size.X, (int)position.Y, Textures.Button.rightDarkSide.Width, Textures.Button.rightDarkSide.Height), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
            spriteBatch.DrawString(Fonts.lucidaConsole24Regular, state, new Vector2(position.X + Textures.Button.leftDarkSide.Width, position.Y + 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
        }

        public Rectangle Bounds { get { return bounds; } set { bounds = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Size { get { return size; } set { size = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string State { get { return state; } set { state = value; } }
    }

    public class Button : Object
    {
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
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public override void HandleMovement(Vector2 pos, float gameTime)
        {
            
        }

        public override void AnimationDone(string animation)
        {

        }

        public static void CreateLocationButtons(LocationNode node)
        {
            if (node.State.Contains("Sub"))
            {
                Button exitLocationButton = new Button(Textures.Button.exitLocationButton, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", GVar.States.Button.locationbutton, 0f);
                Lists.locationButtons.Add(exitLocationButton);

                if (GVar.location != null && !GVar.location.Searched)
                {
                    Button lookEyeButton = new Button(Textures.Button.lookEye, node.Position, Vector.lookEyeSize, Color.White, "LookEyeButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(lookEyeButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasNPC)
                {
                    Button npcButton = new Button(Textures.Button.npcButton, node.Position, Vector.locationButtonSize, Color.White, "NPCButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(npcButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasShop)
                {
                    Button shopButton = new Button(Textures.Misc.pixel, node.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(shopButton);
                }
            }
            else if (node.State.Contains("Main"))
            {
                Button enterLocationButton = new Button(Textures.Button.enterLocationButton, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", GVar.States.Button.locationbutton, 0f);
                Lists.locationButtons.Add(enterLocationButton);
            }
            else
            {
                if (GVar.location != null && !GVar.location.Searched)
                {
                    Button lookEyeButton = new Button(Textures.Button.lookEye, node.Position, new Vector2(Vector.lookEyeSize.X, Vector.lookEyeSize.Y), Color.White, "LookEyeButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(lookEyeButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasNPC)
                {
                    Button npcButton = new Button(Textures.Button.npcButton, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "NPCButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(npcButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasShop)
                {
                    Button shopButton = new Button(Textures.Misc.pixel, node.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(shopButton);
                }
            }

            foreach (Object b in Lists.locationButtons)
            {
                b.ColourA = 5;
            }
        }

        /// <summary>
        /// Check if button state has been set to "delete", if so delete it.
        /// </summary>
        public static void CheckButtonsForDelete()
        {
            for (int i = 0; i < Lists.mainWorldButtons.Count; i++)
            {
                if (Lists.mainWorldButtons[i].State == "delete")//check button state if set to "delete"
                {
                    Lists.mainWorldButtons.RemoveAt(i);//delete button.
                }
            }
        }

        public static void UpdateMainWorldButtons(GameTime gameTime)
        {
            for (int j = 0; j < Lists.mainWorldButtons.Count; j++)
            {
                Updates.UpdateGameButtons(Lists.mainWorldButtons[j], GVar.player, gameTime);
                if (MouseManager.mouseBounds.Intersects(Lists.mainWorldButtons[j].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                    GVar.LogDebugInfo("ButtonClicked: " + Lists.mainWorldButtons[j].Name, 2);
                    if (Lists.mainWorldButtons.Count > 0 && Lists.mainWorldButtons[j].Name == "QuestAcceptButton")
                    {
                        Quest.AcceptQuest(GVar.player);
                        Lists.mainWorldButtons.RemoveAt(j);
                        break;
                    }
                    else if (Lists.mainWorldButtons.Count > 0 && Lists.mainWorldButtons[j].Name == "HandInQuestButton")
                    {
                        Quest.HandInQuest(GVar.player);
                        Lists.mainWorldButtons.RemoveAt(j);
                    }
                    else if (Lists.mainWorldButtons[j].Name == "OpenShop")
                    {
                        Shop.LoadShopInventory(GVar.curLocNode);

                        Button closeInv = new Button(Textures.Misc.pixel, new Vector2(), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);
                        Lists.inventoryButtons.Add(closeInv);

                        Lists.mainWorldButtons.RemoveAt(j);

                        GVar.currentGameState = GVar.GameState.shop;
                        GVar.previousGameState = GVar.GameState.game;

                        UI.CloseNPCUI();
                    }
                    else if (Lists.mainWorldButtons[j].Name == "DisplayQuests")
                    {
                        UI.DisplayQuests();
                    }
                    else if (Lists.mainWorldButtons[j].Name == "DisplayInventory")
                    {
                        Button closeInv = new Button(Textures.Misc.pixel, new Vector2(), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);
                        Lists.inventoryButtons.Add(closeInv);
                        GVar.currentGameState = GVar.GameState.inventory;
                        GVar.previousGameState = GVar.GameState.game;
                    }
                    else if (Lists.mainWorldButtons[j].Name == "CloseQuestListUI")
                    {
                        UI.CloseQuestListUI();
                        Lists.mainWorldButtons.RemoveAt(j);
                        Lists.viewQuestInfoButtons.Clear();
                    }
                    else if (Lists.mainWorldButtons[j].Name == "CloseQuestInfoUI")
                    {
                        UI.CloseQuestInfoUI();
                        Lists.mainWorldButtons.RemoveAt(j);
                    }
                    else if (Lists.mainWorldButtons[j].Name == "CloseNPCUIButton")
                    {
                        Lists.mainWorldButtons.RemoveAt(j);
                        for (int k = 0; k < Lists.mainWorldButtons.Count; k++)
                        {
                            if (Lists.mainWorldButtons[k].Name == "OpenShop")
                                Lists.mainWorldButtons.RemoveAt(k);
                        }
                        UI.CloseNPCUI();
                        break;
                    }
                }
                else if (MouseManager.mouseBounds.Intersects(Lists.mainWorldButtons[j].Bounds) && InputManager.IsLMPressed() && GVar.gamePaused)
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                    if (Lists.mainWorldButtons[j].Name == "MainMenu")
                    {
                        Save.SaveGame(GVar.savedGameLocation, GVar.player, Lists.quests);
                        GVar.changeToMainMenu = true;
                        Colours.drawBlackFade = true;
                        Colours.fadeIn = true;
                        GVar.playerName = string.Empty;
                    }
                    else if (Lists.mainWorldButtons[j].Name == "Options")
                    {
                        Button backToGame = new Button(Textures.Misc.pixel, new Vector2(1130, 50), new Vector2(100, 50), Color.Yellow, "BackToGame", "Alive", 0f);
                        Lists.optionsButtons.Add(backToGame);
                        GVar.changeToOptions = true;
                        Colours.drawBlackFade = true;
                        Colours.fadeIn = true;
                    }
                }
            }
        }

        public static void UpdateViewQuestButtons(GameTime gameTime)
        {
            Vector2 viewQuestInfoButtonPosition = new Vector2();


            for (int j = 0; j < Lists.uiElements.Count; j++)
            {
                if (Lists.uiElements[j].SpriteID == Textures.UI.questListUI)
                {
                    viewQuestInfoButtonPosition = new Vector2(Lists.uiElements[j].SpriteID.Width - 277, Lists.uiElements[j].SpriteID.Height - 366);
                }
            }


            for (int j = 0; j < Lists.viewQuestInfoButtons.Count; j++)
            {
                Lists.viewQuestInfoButtons[j].Position = viewQuestInfoButtonPosition;
                Lists.viewQuestInfoButtons[j].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                viewQuestInfoButtonPosition.Y += 18;
                if (MouseManager.mouseBounds.Intersects(Lists.viewQuestInfoButtons[j].Bounds) && InputManager.IsLMPressed())
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
                    GVar.LogDebugInfo("ButtonClicked: " + Lists.viewQuestInfoButtons[j].Name, 2);
                    for (int ui = 0; ui < Lists.uiElements.Count; ui++)
                    {
                        if (Lists.uiElements[ui].SpriteID == Textures.UI.questInfoUI && !Lists.uiElements[ui].Draw)
                        {
                            Lists.mainWorldButtons.Add(new Button(Textures.Misc.pixel, new Vector2(), new Vector2(20, 20), Color.Red, "CloseQuestInfoUI", "Alive", 0f));
                            Lists.uiElements[ui].Draw = true;
                            GVar.questInfo = Lists.quests[j].Description;
                            GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);
                        }
                        else if (Lists.uiElements[ui].SpriteID == Textures.UI.questInfoUI && Lists.uiElements[ui].Draw)
                        {
                            GVar.questInfo = Lists.quests[j].Description;
                            GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);
                        }
                    }
                }
            }
        }
    }
}

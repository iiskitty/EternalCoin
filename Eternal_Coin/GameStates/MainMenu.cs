using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using System.Text;

namespace Eternal_Coin
{
    public class MainMenu
    {
        public static void UpdateMainMenu(GameTime gameTime)
        {
            for (int i = 0; i < Lists.mainMenuButtons.Count; i++)
            {
                Lists.mainMenuButtons[i].Update(gameTime);

                if (MouseManager.mouseBounds.Intersects(Lists.mainMenuButtons[i].Bounds) && InputManager.IsLMPressed() && Lists.mainMenuButtons[i].CurrentAnimation != GVar.AnimStates.Button.SpinAnim)
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickcoin], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "CoinClick", false);
                    Lists.mainMenuButtons[i].PlayAnimation(GVar.AnimStates.Button.SpinAnim);
                    GVar.LogDebugInfo("ButtonClicked: " + Lists.mainMenuButtons[i].Name, 2);
                    if (Lists.mainMenuButtons[i].Name == "PlayButton")
                    {
                        GVar.changeToCreateCharacter = true;
                        Colours.drawBlackFade = true;
                        Colours.fadeIn = true;
                    }
                    if (Lists.mainMenuButtons[i].Name == "ExitButton")
                    {
                        Colours.drawBlackFade = true;
                        Colours.fadeIn = true;
                    }
                    if (Lists.mainMenuButtons[i].Name == "OptionsButton")
                    {
                        Button mainMenu = new Button(Textures.pixel, new Vector2(1130, 50), new Vector2(100, 50), Color.Yellow, "MainMenu", "Alive", 0f);
                        Lists.optionsButtons.Add(mainMenu);
                        GVar.changeToOptions = true;
                        Colours.drawBlackFade = true;
                        Colours.fadeIn = true;
                    }
                }
            }
        }

        public static void DrawMainMenu(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Object b in Lists.mainMenuButtons)
            {
                b.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                b.Draw(spriteBatch, b.SpriteID, b.Bounds, 0.2f, 0f, Vector2.Zero);
            }
        }

        public static void LoadMainMenu()
        {
            Button optionsCoin = new Button(Textures.optionsButtonSpinAnim, new Vector2((GVar.gameScreenX / 2 - Vector.buttonSize.X / 2), (GVar.gameScreenY / 1.5f - Vector.buttonSize.Y / 2)), Vector.buttonSize, Color.White, "OptionsButton", "Coin", 0f);
            Button playCoin = new Button(Textures.playButtonSpinAnim, new Vector2((GVar.gameScreenX / 2 - Vector.buttonSize.X / 2), (GVar.gameScreenY / 2.5f - Vector.buttonSize.Y / 2)), Vector.buttonSize, Color.White, "PlayButton", "Coin", 0f);
            Button exitCoin = new Button(Textures.exitButtonSpinAnim, new Vector2((GVar.gameScreenX / 2 - Vector.buttonSize.X / 2), (GVar.gameScreenY / 1.9f - Vector.buttonSize.Y / 2)), Vector.buttonSize, Color.White, "ExitButton", "Coin", 0f);
            Lists.mainMenuButtons.Add(optionsCoin);
            Lists.mainMenuButtons.Add(playCoin);
            Lists.mainMenuButtons.Add(exitCoin);
        }
    }
}

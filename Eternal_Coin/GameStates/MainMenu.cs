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

                if (MouseManager.mouseBounds.Intersects(Lists.mainMenuButtons[i].Bounds))
                {
                    if (Lists.mainMenuButtons[i].CurrentAnimation != GVar.AnimStates.Button.mouseover)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.buttonmouseover], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "MouseOverButton", false);
                    }
                    Lists.mainMenuButtons[i].PlayAnimation(GVar.AnimStates.Button.mouseover);

                    if (InputManager.IsLMPressed())
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "CoinClick", false);
                        GVar.LogDebugInfo("ButtonClicked: " + Lists.mainMenuButtons[i].Name, 2);
                        if (Lists.mainMenuButtons[i].Name == "PlayButton")
                        {
                            GVar.changeToCreateCharacter = true;
                            Colours.drawBlackFade = true;
                            Colours.fadeIn = true;
                        }
                        if (Lists.mainMenuButtons[i].Name == "ExitButton")
                        {
                            GVar.exitAfterFade = true;
                            Colours.drawBlackFade = true;
                            Colours.fadeIn = true;

                        }
                        if (Lists.mainMenuButtons[i].Name == "OptionsButton")
                        {
                            Button mainMenu = new Button(Textures.Misc.pixel, new Vector2(1130, 50), new Vector2(100, 50), Color.Yellow, "MainMenu", "Alive", 0f);
                            Lists.optionsButtons.Add(mainMenu);
                            GVar.changeToOptions = true;
                            Colours.drawBlackFade = true;
                            Colours.fadeIn = true;
                        }
                    }
                }
                if (!MouseManager.mouseBounds.Intersects(Lists.mainMenuButtons[i].Bounds) && Lists.mainMenuButtons[i].CurrentAnimation != GVar.AnimStates.Button.def)
                {
                    Lists.mainMenuButtons[i].PlayAnimation(GVar.AnimStates.Button.def);
                }
            }
        }

        public static void DrawMainMenu(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, GVar.verNum, new Vector2(560, 200), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

            spriteBatch.Draw(Textures.Misc.background, new Rectangle(0, 0, 1280, 720), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(Textures.Misc.title, new Rectangle(50, 10, 1180, Textures.Misc.title.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);

            foreach (Object b in Lists.mainMenuButtons)
            {
                b.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                b.Draw(spriteBatch, b.SpriteID, b.Bounds, 0.2f, 0f, Vector2.Zero);
            }
        }

        public static void LoadMainMenu()
        {
            Button optionsCoin = new Button(Textures.Button.optionsButton, new Vector2((GVar.gameScreenX / 2.5f - (Textures.Button.optionsButton.Width / 2) / 2), 460), new Vector2(Textures.Button.optionsButton.Width / 2, Textures.Button.optionsButton.Height), Color.White, "OptionsButton", "Coin", 0f);
            Button playCoin = new Button(Textures.Button.playButton, new Vector2((GVar.gameScreenX / 2.5f - (Textures.Button.playButton.Width / 2) / 2), 400), new Vector2(Textures.Button.playButton.Width / 2, Textures.Button.playButton.Height), Color.White, "PlayButton", "Coin", 0f);
            Button exitCoin = new Button(Textures.Button.exitButton, new Vector2((GVar.gameScreenX / 2.5f - (Textures.Button.exitButton.Width / 2) / 2), 520), new Vector2(Textures.Button.exitButton.Width / 2, Textures.Button.exitButton.Height), Color.White, "ExitButton", "Coin", 0f);
            Lists.mainMenuButtons.Add(optionsCoin); 
            Lists.mainMenuButtons.Add(playCoin);
            Lists.mainMenuButtons.Add(exitCoin);
        }
    }
}

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Eternal_Coin
{
    public class Options
    {
        public static void UpdateOptions(GameTime gameTime)
        {
            for (int i = 0; i < Lists.optionsButtons.Count; i++)
            {
                Lists.optionsButtons[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (MouseManager.mouseBounds.Intersects(Lists.optionsButtons[i].Bounds) && InputManager.IsLMPressed())
                {
                    SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                    GVar.LogDebugInfo("ButtonClicked: " + Lists.optionsButtons[i].Name, 2);
                    if (Lists.optionsButtons[i].Name == "MainMenu")
                    {
                        GVar.changeToMainMenu = true;
                        Colours.drawBlackFade = true;
                        Colours.fadeIn = true;
                        Lists.mainMenuButtons.Clear();
                    }
                    else if (Lists.optionsButtons[i].Name == "BackToGame")
                    {
                        GVar.changeBackToGame = true;
                        Colours.drawBlackFade = true;
                        Colours.fadeIn = true;
                    }
                    else if (Lists.optionsButtons[i].Name == "ToggleFullScreen")
                    {
                        GVar.toggleFullScreen = true;
                    }
                    else if (Lists.optionsButtons[i].Name == "ToggleDebugLog")
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
                }
            }
        }

        public static void DrawOptions(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < Lists.optionsButtons.Count; i++)
            {
                Lists.optionsButtons[i].Update(gameTime);
                Lists.optionsButtons[i].Draw(spriteBatch, Lists.optionsButtons[i].SpriteID, Lists.optionsButtons[i].Bounds, 0.19f, 0f, Vector2.Zero);

                if (Lists.optionsButtons[i].Name == "ToggleFullScreen")
                {
                    spriteBatch.DrawString(Fonts.lucidaConsole18Regular, "FullScreen", new Vector2(Lists.optionsButtons[i].Position.X + Lists.optionsButtons[i].Size.X / 6,  Lists.optionsButtons[i].Position.Y + Lists.optionsButtons[i].Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                }
                else if (Lists.optionsButtons[i].Name == "ToggleDebugLog")
                {
                    spriteBatch.DrawString(Fonts.lucidaConsole18Regular, "DebugLog", new Vector2(Lists.optionsButtons[i].Position.X + Lists.optionsButtons[i].Size.X / 4, Lists.optionsButtons[i].Position.Y + Lists.optionsButtons[i].Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                }
                else if (Lists.optionsButtons[i].Name == "MainMenu" || Lists.optionsButtons[i].Name == "BackToGame")
                {
                    spriteBatch.DrawString(Fonts.lucidaConsole18Regular, "Back", new Vector2(Lists.optionsButtons[i].Position.X + Lists.optionsButtons[i].Size.X / 4, Lists.optionsButtons[i].Position.Y + Lists.optionsButtons[i].Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                }

                if (MouseManager.mouseBounds.Intersects(Lists.optionsButtons[i].Bounds))
                {
                    GVar.DrawBoundingBox(Lists.optionsButtons[i].Bounds, spriteBatch, Textures.Misc.pixel, 2, 0.2f, Color.Green);
                }
            }
        }

        public static void LoadOptions()
        {
            Button fullScreen = new Button(Textures.Misc.pixel, new Vector2(50, 50), new Vector2(200, 50), Color.Yellow, "ToggleFullScreen", "Alive", 0f);
            Button debugLog = new Button(Textures.Misc.pixel, new Vector2(300, 50), new Vector2(200, 50), Color.Yellow, "ToggleDebugLog", "Alive", 0f);
            Lists.optionsButtons.Add(fullScreen);
            Lists.optionsButtons.Add(debugLog);
        }
    }
}

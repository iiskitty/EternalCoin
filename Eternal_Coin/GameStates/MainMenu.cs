using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
    public class MainMenu
    {
    public static void UpdateMainMenu(GameTime gameTime) { }

        public static void DrawMainMenu(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, GVar.verNum, new Vector2(560, 200), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

            spriteBatch.Draw(Textures.Misc.background, new Rectangle(0, 0, (int)GVar.currentScreenX, (int)GVar.currentScreenY), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(Textures.Misc.title, new Rectangle(0 + (int)GVar.currentScreenX / 2 - 1180 / 2, 10, 1180, Textures.Misc.title.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
        }

        public static void LoadMainMenu()
        {
            Button optionsCoin = new Button(Textures.Button.optionsButton, new Vector2((GVar.currentScreenX / 2.5f - (Textures.Button.optionsButton.Width / 2) / 2), 460), new Vector2(Textures.Button.optionsButton.Width / 2, Textures.Button.optionsButton.Height), Color.White, "OptionsButton", "Coin", 0f);
            Button playCoin = new Button(Textures.Button.playButton, new Vector2((GVar.currentScreenX / 2.5f - (Textures.Button.playButton.Width / 2) / 2), 400), new Vector2(Textures.Button.playButton.Width / 2, Textures.Button.playButton.Height), Color.White, "PlayButton", "Coin", 0f);
            Button exitCoin = new Button(Textures.Button.exitButton, new Vector2((GVar.currentScreenX / 2.5f - (Textures.Button.exitButton.Width / 2) / 2), 520), new Vector2(Textures.Button.exitButton.Width / 2, Textures.Button.exitButton.Height), Color.White, "ExitButton", "Coin", 0f);
            Lists.mainMenuButtons.Add(optionsCoin); 
            Lists.mainMenuButtons.Add(playCoin);
            Lists.mainMenuButtons.Add(exitCoin);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;

namespace Eternal_Coin
{
  public class Options
  {
    public static void UpdateOptions(GameTime gameTime)
    {
    }

    public static void DrawOptions(SpriteBatch spriteBatch, GameTime gameTime)
    {
    }

    public static void LoadOptions()
    {
      switch(GVar.previousGameState)
      {
        case GVar.GameState.mainMenu:
          Button mainMenu = new Button(Textures.Misc.pixel, new Vector2(1130, 50), new Vector2(100, 50), Color.Yellow, "MainMenu", "Alive", 0f);
          Lists.optionsButtons.Add(mainMenu);
          break;
        case GVar.GameState.game:
          Button backToGame = new Button(Textures.Misc.pixel, new Vector2(1130, 50), new Vector2(100, 50), Color.Yellow, "BackToGame", "Alive", 0f);//create Back to Game Button.
          Lists.optionsButtons.Add(backToGame);//add Back to Game Button to Options Button.
          break;
      }

      Button fullScreen = new Button(Textures.Misc.pixel, new Vector2(50, 50), new Vector2(200, 50), Color.Yellow, "ToggleFullScreen", "Alive", 0f);
      Button debugLog = new Button(Textures.Misc.pixel, new Vector2(300, 50), new Vector2(200, 50), Color.Yellow, "ToggleDebugLog", "Alive", 0f);
      Lists.optionsButtons.Add(fullScreen);
      Lists.optionsButtons.Add(debugLog);
    }
  }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
  public class Colours
  {
    /// <summary>
    /// alpha value for black fade (between gamestates)
    /// </summary>
    public static int blackFadeAlpha = 5;

    /// <summary>
    /// alpha value for map fade
    /// </summary>
    public static int mapFadeAlpha = 250;

    /// <summary>
    /// alpha value for location names
    /// </summary>
    public static byte locNameAlpha = 5;

    /// <summary>
    /// texture for fading previous map
    /// </summary>
    public static Texture2D mapFadeTex;
    /// <summary>
    /// position to draw the fading map
    /// </summary>
    public static Vector2 mapFadePos;

    /// <summary>
    /// fade in boolean for black fade
    /// </summary>
    public static bool fadeIn = false;
    /// <summary>
    /// fade out boolean for black fade
    /// </summary>
    public static bool fadeOut = false;
    /// <summary>
    /// black fade will be drawn if true
    /// </summary>
    public static bool drawBlackFade = false;
    /// <summary>
    /// map fade will be drawn if true
    /// </summary>
    public static bool drawFadeMap = false;
    /// <summary>
    /// colour for black fade (its black)
    /// </summary>
    static Color blackFadeColour;
    /// <summary>
    /// colour for map fade (its transparent)
    /// </summary>
    static Color mapFadeColour;
    /// <summary>
    /// colour for location names (its black)
    /// </summary>
    public static Color locNameColour;
    /// <summary>
    /// update all colours
    /// </summary>
    /// <param name="gameTime">game time for smooth fading (delta time)</param>
    public static void UpdateColours(GameTime gameTime)
    {
      blackFadeColour = Color.FromNonPremultiplied(0, 0, 0, blackFadeAlpha);
      mapFadeColour = Color.FromNonPremultiplied(255, 255, 255, mapFadeAlpha);

      if (fadeIn && fadeOut)
      {
        fadeIn = false; //in the event fadeIn and fadeOut are both true, set fade in to false (this has happened)
      }

      if (fadeIn && blackFadeAlpha < 251)
      {
        blackFadeAlpha += 10;
        if (blackFadeAlpha >= 250)
        {
          fadeIn = false;
          fadeOut = true;
        }
      }
      if (fadeOut && blackFadeAlpha > 4)
      {
        if (GVar.exitAfterFade)
          GVar.exitGame = true;
        blackFadeAlpha -= 10;
        if (blackFadeAlpha <= 5)
        {
          fadeOut = false;
          drawBlackFade = false;
          blackFadeAlpha = 4;
        }
      }

      if (fadeOut && drawFadeMap)
      {
        mapFadeAlpha -= 10;
        if (mapFadeAlpha <= 5)
        {
          fadeOut = false;
          drawFadeMap = false;
          mapFadeAlpha = 250;
          mapFadeTex = null;
        }
      }


    }
    /// <summary>
    /// updates alpha values for location nodes, location buttons and location node names (i think)
    /// </summary>
    /// <param name="node"></param>
    public static void UpdateMainAlphas(LocationNode node)
    {
      for (int i = 0; i < Lists.locationButtons.Count; i++)
      {
        if (Lists.locationButtons[i].ColourA < 250)
        {
          Lists.locationButtons[i].ColourA += 10;
        }
      }

      if (node.ColourA < 250)
        node.ColourA += 10;

      for (int i = 0; i < node.LocNodeConnections.Count; i++)
      {
        if (node.LocNodeConnections[i].ColourA < 250)
          node.LocNodeConnections[i].ColourA += 10;
      }
    }

    /// <summary>
    /// enables map fade
    /// </summary>
    public static void EnableFadeOutMap()
    {
      drawFadeMap = true;
      fadeOut = true;
      mapFadeTex = GVar.worldMap.SpriteID;
      mapFadePos = GVar.worldMap.Position;
    }

    public static void DrawMapFadeOut(SpriteBatch spriteBatch) => spriteBatch.Draw(mapFadeTex, new Rectangle((int)mapFadePos.X, (int)mapFadePos.Y, mapFadeTex.Width, mapFadeTex.Height), null, mapFadeColour, 0f, Vector2.Zero, SpriteEffects.None, 0.11f);

    public static void DrawBlackFadeInOut(SpriteBatch spriteBatch) => spriteBatch.Draw(Textures.Misc.pixel, new Rectangle(0, 0, (int)GVar.currentScreenX, (int)GVar.currentScreenY), null, blackFadeColour, 0f, Vector2.Zero, SpriteEffects.None, 0.21f);
  }
}

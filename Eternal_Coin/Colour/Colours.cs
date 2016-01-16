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
    public class Colours
    {
        public static int blackFadeAlpha = 5;
        public static int mapFadeAlpha = 250;

        public static byte locNameAlpha = 5;

        public static Texture2D mapFadeTex;
        public static Vector2 mapFadePos;

        public static bool fadeIn = false, fadeOut = false, drawBlackFade = false, drawFadeMap = false;

        static Color blackFadeColour;
        static Color mapFadeColour;

        public static Color locNameColour;

        public static void UpdateColours(GameTime gameTime)
        {
            blackFadeColour = Color.FromNonPremultiplied(0, 0, 0, blackFadeAlpha);
            mapFadeColour = Color.FromNonPremultiplied(255, 255, 255, mapFadeAlpha);

            if (fadeIn && fadeOut)
            {
                fadeIn = false;
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

        public static void UpdateMainAlphas(LocationNode node)
        {

            foreach (Object LB in Lists.locationButtons)
            {
                if (LB.ColourA < 250)
                {
                    LB.ColourA += 10;
                }
            }

            if (node.ColourA < 250)
                node.ColourA += 10;

            foreach (LocationNode LN in node.LocNodeConnections)
            {
                if (LN.ColourA < 250)
                    LN.ColourA += 10;
            }
        }

        public static void EnableFadeOutMap()
        {
            drawFadeMap = true;
            fadeOut = true;
            mapFadeTex = GVar.worldMap.SpriteID;
            mapFadePos = GVar.worldMap.Position;
        }

        public static void DrawMapFadeOut(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mapFadeTex, new Rectangle((int)mapFadePos.X, (int)mapFadePos.Y, mapFadeTex.Width, mapFadeTex.Height), null, mapFadeColour, 0f, Vector2.Zero, SpriteEffects.None, 0.171f);
        }

        public static void DrawBlackFadeInOut(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.Misc.pixel, new Rectangle(0, 0, (int)GVar.gameScreenX, (int)GVar.gameScreenY), null, blackFadeColour, 0f, Vector2.Zero, SpriteEffects.None, 0.21f);  
        }
    }
}

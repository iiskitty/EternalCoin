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
    public class Vector
    {
        public static Vector2 worldMapPosition;
        public static Vector2 worldMapSize;
        public static Vector2 buttonSize;
        public static Vector2 lookEyeSize;
        public static Vector2 locationButtonSize;
        public static Vector2 itemNormalSize;
        public static Vector2 newGameDPSize;
        public static Vector2 middleScreenPosition;
        public static Vector2 mainLocationSize;
        public static Vector2 subLocationSize;

        public static void InitilizeVectors()
        {
            worldMapPosition = new Vector2((0 - Textures.Misc.worldMapTex.Width / 2 + GVar.gameScreenX / 2), (0 - Textures.Misc.worldMapTex.Height / 2 + GVar.gameScreenY / 2));
            worldMapSize = new Vector2(Textures.Misc.worldMapTex.Width, Textures.Misc.worldMapTex.Height);
            lookEyeSize = new Vector2(50, 25);
            locationButtonSize = new Vector2(30, 30);
            mainLocationSize = new Vector2(30, 30);
            subLocationSize = new Vector2(20, 20);
            buttonSize = new Vector2(75, 75);
            itemNormalSize = new Vector2(71, 71);
            newGameDPSize = new Vector2(329, 232);
        }
    }
}

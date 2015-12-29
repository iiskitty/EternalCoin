﻿using Microsoft.Xna.Framework;
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

        public static void InitilizeVectors()
        {
            worldMapPosition = new Vector2((0 - Textures.worldMapTex.Width / 2 + GVar.gameScreenX / 2), (0 - Textures.worldMapTex.Height / 2 + GVar.gameScreenY / 2));
            worldMapSize = new Vector2(Textures.worldMapTex.Width, Textures.worldMapTex.Height);
            lookEyeSize = new Vector2(30, 15);
            locationButtonSize = new Vector2(20, 20);
            buttonSize = new Vector2(75, 75);
        }
    }
}
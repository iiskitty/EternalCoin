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
    public class Fonts
    {
        public static SpriteFont lucidaConsole24Bold;
        public static SpriteFont lucidaConsole24Regular;
        public static SpriteFont lucidaConsole20Bold;
        public static SpriteFont lucidaConsole20Regular;
        public static SpriteFont lucidaConsole18Bold;
        public static SpriteFont lucidaConsole18Regular;
        public static SpriteFont lucidaConsole16Bold;
        public static SpriteFont lucidaConsole16Regular;
        public static SpriteFont lucidaConsole14Bold;
        public static SpriteFont lucidaConsole14Regular;
        public static SpriteFont lucidaConsole12Bold;
        public static SpriteFont lucidaConsole12Regular;
        public static SpriteFont lucidaConsole10Bold;
        public static SpriteFont lucidaConsole10Regular;

        public static void LoadFonts(ContentManager Content)
        {
            lucidaConsole24Bold = Content.Load<SpriteFont>("Fonts/LucidaConsole24Bold");
            lucidaConsole24Regular = Content.Load<SpriteFont>("Fonts/LucidaConsole24Regular");
            lucidaConsole20Bold = Content.Load<SpriteFont>("Fonts/LucidaConsole20Bold");
            lucidaConsole20Regular = Content.Load<SpriteFont>("Fonts/LucidaConsole20Regular");
            lucidaConsole18Bold = Content.Load<SpriteFont>("Fonts/LucidaConsole18Bold");
            lucidaConsole18Regular = Content.Load<SpriteFont>("Fonts/LucidaConsole18Regular");
            lucidaConsole16Bold = Content.Load<SpriteFont>("Fonts/LucidaConsole16Bold");
            lucidaConsole16Regular = Content.Load<SpriteFont>("Fonts/LucidaConsole16Regular");
            lucidaConsole14Bold = Content.Load<SpriteFont>("Fonts/LucidaConsole14Bold");
            lucidaConsole14Regular = Content.Load<SpriteFont>("Fonts/LucidaConsole14Regular");
            lucidaConsole12Bold = Content.Load<SpriteFont>("Fonts/LucidaConsole12Bold");
            lucidaConsole12Regular = Content.Load<SpriteFont>("Fonts/LucidaConsole12Regular");
            lucidaConsole10Bold = Content.Load<SpriteFont>("Fonts/LucidaConsole10Bold");
            lucidaConsole10Regular = Content.Load<SpriteFont>("Fonts/LucidaConsole10Regular");
        }
    }
}

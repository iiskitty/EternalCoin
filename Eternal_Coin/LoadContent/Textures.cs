﻿using Microsoft.Xna.Framework;
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
    public class Textures
    {
        public static Texture2D clearPixel;
        public static Texture2D pixel;
        public static Texture2D locationNodeTex;
        public static Texture2D worldMapTex;
        public static Texture2D enterLocationButtonTex;
        public static Texture2D exitLocationButtonTex;
        public static Texture2D tickTex;
        public static Texture2D crossTex;
        public static Texture2D playButtonSpinAnim;
        public static Texture2D exitButtonSpinAnim;
        public static Texture2D optionsButtonSpinAnim;
        public static Texture2D lookEyeTex;
        public static Texture2D npcButtonTex;
        public static Texture2D coinBackTex;
        public static Texture2D locationInfoUITex;
        public static Texture2D NPCInfoUITex;
        public static Texture2D questListUI;
        public static Texture2D questInfoUI;
        public static Texture2D inventoryUI;
        public static Texture2D shopInventoryUI;
        public static Texture2D battleUI;
        public static Texture2D pauseUI;
        public static Texture2D cursor;

        public static void LoadTextures(ContentManager Content)
        {
            XmlDocument texDoc = new XmlDocument();
            texDoc.Load("./Content/LoadData/LoadTextures.xml");
            XmlNodeList texList = texDoc.SelectNodes("/load/texture");

            foreach (XmlNode tex in texList)
            {
                Dictionaries.textures.Add(tex["id"].InnerText, Content.Load<Texture2D>(tex["filepath"].InnerText));
            }

            XmlDocument setTexDoc = new XmlDocument();
            setTexDoc.Load("./Content/LoadData/SetTextures.xml");
            XmlNode texID = setTexDoc.SelectSingleNode("/set");

            cursor = Dictionaries.textures[texID["cursor"].InnerText];
            pixel = Dictionaries.textures[texID["pixel"].InnerText];
            clearPixel = Dictionaries.textures[texID["cpixel"].InnerText];
            worldMapTex = Dictionaries.textures[texID["worldmap"].InnerText];
            locationNodeTex = Dictionaries.textures[texID["locnode"].InnerText];
            tickTex = Dictionaries.textures[texID["tick"].InnerText];
            crossTex = Dictionaries.textures[texID["cross"].InnerText];
            playButtonSpinAnim = Dictionaries.textures[texID["playbut"].InnerText];
            exitButtonSpinAnim = Dictionaries.textures[texID["exitbut"].InnerText];
            optionsButtonSpinAnim = Dictionaries.textures[texID["optionsbut"].InnerText];
            lookEyeTex = Dictionaries.textures[texID["lookeye"].InnerText];
            npcButtonTex = Dictionaries.textures[texID["npcbut"].InnerText];
            enterLocationButtonTex = Dictionaries.textures[texID["enterloc"].InnerText];
            exitLocationButtonTex = Dictionaries.textures[texID["exitloc"].InnerText];
            coinBackTex = Dictionaries.textures[texID["coinback"].InnerText];
            locationInfoUITex = Dictionaries.textures[texID["UIlocinfo"].InnerText];
            NPCInfoUITex = Dictionaries.textures[texID["UInpcinfo"].InnerText];
            questListUI = Dictionaries.textures[texID["UIquestlist"].InnerText];
            questInfoUI = Dictionaries.textures[texID["UIquestinfo"].InnerText];
            inventoryUI = Dictionaries.textures[texID["UIinv"].InnerText];
            shopInventoryUI = Dictionaries.textures[texID["UIshopinv"].InnerText];
            pauseUI = Dictionaries.textures[texID["UIpause"].InnerText];
            battleUI = Dictionaries.textures[texID["UIBattle"].InnerText];
        }
    }
}
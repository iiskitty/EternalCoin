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
        public static Texture2D startButton;
        public static Texture2D backButton;
        public static Texture2D closeButton;
        public static Texture2D continueButton;
        public static Texture2D newButton;
        public static Texture2D loadButton;
        public static Texture2D deleteButton;
        public static Texture2D inventoryButton;
        public static Texture2D playButton;
        public static Texture2D exitButton;
        public static Texture2D optionsButton;
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
        public static Texture2D endBattleUI;
        public static Texture2D pauseUI;
        public static Texture2D itemInfoUI;
        public static Texture2D newGameUIBorder;
        public static Texture2D newGameUIInner;
        public static Texture2D savedGameUIBorder;
        public static Texture2D savedGameUIInner;
        public static Texture2D cursor;
        public static Texture2D title;
        public static Texture2D background;
        public static Texture2D leftLightSide;
        public static Texture2D leftDarkSide;
        public static Texture2D rightLightSide;
        public static Texture2D rightDarkSide;
        public static Texture2D middleLight;
        public static Texture2D middleDark;

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

            itemInfoUI = Dictionaries.textures[texID["UIiteminfo"].InnerText];
            continueButton = Dictionaries.textures[texID["continuebut"].InnerText];
            newGameUIBorder = Dictionaries.textures[texID["UInewgameborder"].InnerText];
            newGameUIInner = Dictionaries.textures[texID["UInewgameinner"].InnerText];
            savedGameUIBorder = Dictionaries.textures[texID["UIsavedgameborder"].InnerText];
            savedGameUIInner = Dictionaries.textures[texID["UIsavedgameinner"].InnerText];
            background = Dictionaries.textures[texID["background"].InnerText];
            title = Dictionaries.textures[texID["title"].InnerText];
            cursor = Dictionaries.textures[texID["cursor"].InnerText];
            pixel = Dictionaries.textures[texID["pixel"].InnerText];
            clearPixel = Dictionaries.textures[texID["cpixel"].InnerText];
            worldMapTex = Dictionaries.textures[texID["worldmap"].InnerText];
            locationNodeTex = Dictionaries.textures[texID["locnode"].InnerText];
            tickTex = Dictionaries.textures[texID["tick"].InnerText];
            crossTex = Dictionaries.textures[texID["cross"].InnerText];
            startButton = Dictionaries.textures[texID["startbut"].InnerText];
            backButton = Dictionaries.textures[texID["backbut"].InnerText];
            newButton = Dictionaries.textures[texID["newbut"].InnerText];
            loadButton = Dictionaries.textures[texID["loadbut"].InnerText];
            deleteButton = Dictionaries.textures[texID["delbut"].InnerText];
            inventoryButton = Dictionaries.textures[texID["invbut"].InnerText];
            closeButton = Dictionaries.textures[texID["closebut"].InnerText];
            playButton = Dictionaries.textures[texID["playbut"].InnerText];
            exitButton = Dictionaries.textures[texID["exitbut"].InnerText];
            optionsButton = Dictionaries.textures[texID["optionsbut"].InnerText];
            lookEyeTex = Dictionaries.textures[texID["lookeye"].InnerText];
            npcButtonTex = Dictionaries.textures[texID["npcbut"].InnerText];
            enterLocationButtonTex = Dictionaries.textures[texID["enterloc"].InnerText];
            exitLocationButtonTex = Dictionaries.textures[texID["exitloc"].InnerText];
            leftLightSide = Dictionaries.textures[texID["lightleftgenbut"].InnerText];
            middleLight = Dictionaries.textures[texID["midlightgenbut"].InnerText];
            rightLightSide = Dictionaries.textures[texID["rightlightgenbut"].InnerText];
            leftDarkSide = Dictionaries.textures[texID["leftdarkgenbut"].InnerText];
            middleDark = Dictionaries.textures[texID["middarkgenbut"].InnerText];
            rightDarkSide = Dictionaries.textures[texID["rightdarkgenbut"].InnerText];
            coinBackTex = Dictionaries.textures[texID["coinback"].InnerText];
            locationInfoUITex = Dictionaries.textures[texID["UIlocinfo"].InnerText];
            NPCInfoUITex = Dictionaries.textures[texID["UInpcinfo"].InnerText];
            questListUI = Dictionaries.textures[texID["UIquestlist"].InnerText];
            questInfoUI = Dictionaries.textures[texID["UIquestinfo"].InnerText];
            inventoryUI = Dictionaries.textures[texID["UIinv"].InnerText];
            shopInventoryUI = Dictionaries.textures[texID["UIshopinv"].InnerText];
            pauseUI = Dictionaries.textures[texID["UIpause"].InnerText];
            battleUI = Dictionaries.textures[texID["UIBattle"].InnerText];
            endBattleUI = Dictionaries.textures[texID["UIendbattle"].InnerText];
        }
    }
}

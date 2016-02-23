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
        public struct Button
        {
            public static Texture2D locationNodeTex;
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
            public static Texture2D npcButtonTex;
            public static Texture2D enterLocationButtonTex;
            public static Texture2D exitLocationButtonTex;
            public static Texture2D leftLightSide;
            public static Texture2D leftDarkSide;
            public static Texture2D rightLightSide;
            public static Texture2D rightDarkSide;
            public static Texture2D middleLight;
            public static Texture2D middleDark;
            public static Texture2D lookEyeTex;
        };

        public struct UI
        {
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
        };

        public struct Misc
        {
            public static Texture2D clearPixel;
            public static Texture2D pixel;
            public static Texture2D worldMapTex;
            public static Texture2D tickTex;
            public static Texture2D crossTex;
            public static Texture2D cursor;
            public static Texture2D title;
            public static Texture2D background;
        };

        public static void LoadTextures(ContentManager Content)
        {
            XmlDocument texDoc = new XmlDocument();
            texDoc.Load("./Content/LoadData/Data.xml");
            XmlNodeList texList = texDoc.SelectNodes("/data/textures/load/texture");

            foreach (XmlNode tex in texList)
            {
                Dictionaries.textures.Add(tex["id"].InnerText, Content.Load<Texture2D>(tex["filepath"].InnerText));
            }

            XmlDocument setTexDoc = new XmlDocument();
            setTexDoc.Load("./Content/LoadData/Data.xml");
            XmlNode texID = setTexDoc.SelectSingleNode("/data/textures/set");

            UI.itemInfoUI = Dictionaries.textures[texID["UIiteminfo"].InnerText];
            UI.newGameUIBorder = Dictionaries.textures[texID["UInewgameborder"].InnerText];
            UI.newGameUIInner = Dictionaries.textures[texID["UInewgameinner"].InnerText];
            UI.savedGameUIBorder = Dictionaries.textures[texID["UIsavedgameborder"].InnerText];
            UI.savedGameUIInner = Dictionaries.textures[texID["UIsavedgameinner"].InnerText];
            UI.locationInfoUITex = Dictionaries.textures[texID["UIlocinfo"].InnerText];
            UI.NPCInfoUITex = Dictionaries.textures[texID["UInpcinfo"].InnerText];
            UI.questListUI = Dictionaries.textures[texID["UIquestlist"].InnerText];
            UI.questInfoUI = Dictionaries.textures[texID["UIquestinfo"].InnerText];
            UI.inventoryUI = Dictionaries.textures[texID["UIinv"].InnerText];
            UI.shopInventoryUI = Dictionaries.textures[texID["UIshopinv"].InnerText];
            UI.pauseUI = Dictionaries.textures[texID["UIpause"].InnerText];
            UI.battleUI = Dictionaries.textures[texID["UIBattle"].InnerText];
            UI.endBattleUI = Dictionaries.textures[texID["UIendbattle"].InnerText];

            Misc.background = Dictionaries.textures[texID["background"].InnerText];
            Misc.title = Dictionaries.textures[texID["title"].InnerText];
            Misc.cursor = Dictionaries.textures[texID["cursor"].InnerText];
            Misc.pixel = Dictionaries.textures[texID["pixel"].InnerText];
            Misc.clearPixel = Dictionaries.textures[texID["cpixel"].InnerText];
            Misc.worldMapTex = Dictionaries.textures[texID["worldmap"].InnerText];
            Misc.tickTex = Dictionaries.textures[texID["tick"].InnerText];
            Misc.crossTex = Dictionaries.textures[texID["cross"].InnerText];

            Button.locationNodeTex = Dictionaries.textures[texID["locnode"].InnerText];
            Button.continueButton = Dictionaries.textures[texID["continuebut"].InnerText];
            Button.startButton = Dictionaries.textures[texID["startbut"].InnerText];
            Button.backButton = Dictionaries.textures[texID["backbut"].InnerText];
            Button.newButton = Dictionaries.textures[texID["newbut"].InnerText];
            Button.loadButton = Dictionaries.textures[texID["loadbut"].InnerText];
            Button.deleteButton = Dictionaries.textures[texID["delbut"].InnerText];
            Button.inventoryButton = Dictionaries.textures[texID["invbut"].InnerText];
            Button.closeButton = Dictionaries.textures[texID["closebut"].InnerText];
            Button.playButton = Dictionaries.textures[texID["playbut"].InnerText];
            Button.exitButton = Dictionaries.textures[texID["exitbut"].InnerText];
            Button.optionsButton = Dictionaries.textures[texID["optionsbut"].InnerText];
            Button.lookEyeTex = Dictionaries.textures[texID["lookeye"].InnerText];
            Button.npcButtonTex = Dictionaries.textures[texID["npcbut"].InnerText];
            Button.enterLocationButtonTex = Dictionaries.textures[texID["enterloc"].InnerText];
            Button.exitLocationButtonTex = Dictionaries.textures[texID["exitloc"].InnerText];
            Button.leftLightSide = Dictionaries.textures[texID["lightleftgenbut"].InnerText];
            Button.middleLight = Dictionaries.textures[texID["midlightgenbut"].InnerText];
            Button.rightLightSide = Dictionaries.textures[texID["rightlightgenbut"].InnerText];
            Button.leftDarkSide = Dictionaries.textures[texID["leftdarkgenbut"].InnerText];
            Button.middleDark = Dictionaries.textures[texID["middarkgenbut"].InnerText];
            Button.rightDarkSide = Dictionaries.textures[texID["rightdarkgenbut"].InnerText];
            
        }
    }
}

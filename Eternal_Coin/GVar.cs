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
    public class GVar
    {
        public static string verNum = "Alpha 0.01";

        public static XmlDocument curLocNode = new XmlDocument();

        public static int preventclick = 0;

        public static int silverMoney = 0;
        public static int numSavedGames = 0;
        public static string questInfo = "";
        public static string storyName = "";
        public static string displayPicID = "Default";
        public static string eDisplayPicID = string.Empty;
        public static bool loadData = false;
        public static bool startGame = false;
        public static bool loadGame = false;
        public static bool changeToOptions = false;
        public static bool changeToMainMenu = false;
        public static bool chooseStory = false;
        public static bool changeToCreateCharacter = false;
        public static bool changeToBattle = false;
        public static bool toggleFullScreen = false;
        public static bool debugLogEnabled = false;
        public static bool changeBackToGame = false;
        public static bool windowIsActive;
        public static bool choosingDP = false;
        public static WorldMap worldMap;
        public static Location location;
        public static NPC npc;

        public struct SoundIDs
        {
            public static string clickcoin;
            public static string clicklocnode;
            public static string clickbutton;
        };

        public class States
        {
            public struct Button
            {
                public static string locationbutton = "locationbutton";
            };
        }

        public class AnimStates
        {
            public struct Button
            {
                public static string def = "default";
                public static string mouseover = "mouseover";
            };
        }

        public class XmlTags
        {
            public struct Player
            {
                public static string name = "name";
                public static string currentlocation = "currentlocation";
            };

            public struct LocationTags
            {
                public static string name = "name";
                public static string hasnpc = "hasnpc";
                public static string hasshop = "hasshop";
                public static string hasenemy = "hasenemy";
                public static string description = "description";
                public static string searched = "searched";
            };

            public struct NPCTags
            {
                public static string name = "name";

                public struct Greetings
                {
                    public static string acceptquest = "acceptquest";
                    public static string handinquest = "handinquest";
                    public static string questunaccepted = "questunaccepted";
                    public static string questaccepted = "questaccepted";
                    public static string questfinished = "questfinished";
                    public static string questcompleted = "questcompleted";
                };
            };

            public struct QuestTags
            {
                public static string hasquest = "hasquest";
                public static string questfinished = "questfinished";
                public static string questcompleted = "questcompleted";
                public static string questaccepted = "questaccepted";
                public static string description = "description";
                public static string shortdescription = "shortdescription";
                public static string completingaction = "completingaction";
                public static string completinglocation = "completinglocation";
                public static string completed = "completed";
                public static string locationfilepath = "locationfilepath";
            };

            public struct Actions
            {
                public static string explore = "explore";
                public static string enter = "enter";
                public static string talknpc = "talknpc";
                public static string acceptquest = "acceptquest";
                public static string handinquest = "handinquest";
            };

            public struct ItemTags
            {
                public static string itemclass = "class";
                public static string itemtype = "itemtype";
                public static string itemname = "itemname";
                public static string itemmaterial = "itemmaterial";
                public static string inventoryslot = "inventoryslot";
                public static string playerinventoryslot = "playerinventoryslot";
            };
        }

        public struct Materials
        {
            public static string wood = "Wood";
            public static string cloth = "Cloth";
            public static string stone = "Stone";
            public static string bronze = "Bronze";
            public static string iron = "Iron";
            public static string steel = "Steel";
            public static string gold = "Gold";
        };

        public struct AttackAnimStates
        {
            public static string idle = "Idle";
            public static string buildUp = "BuildUp";
            public static string attack = "Attack";
            public static string attacked = "Attacked";
            public static string retreat = "Retreat";
        };

        public struct AttackType
        {
            public static string physical = "Physical";
            public static string magic = "Magic";
        };

        public struct ItemClassName
        {
            public const string weapon = "Weapon";
            public const string armor = "Armor";
            public const string jewellry = "Jewellry";
        };

        public struct InventorySlot
        {
            public static string leftHandWeapon = "LeftHandWeapon";
            public static string rightHandWeapon = "RightHandWeapon";
            public static string leftBoot = "LeftBoot";
            public static string rightBoot = "RightBoot";
            public static string leftGauntlet = "LeftGauntlet";
            public static string rightGauntlet = "RightGauntlet";
            public static string helmet = "Helmet";
            public static string chestplate = "Chestplate";
            public static string leggings = "Leggings";
            public static string RingOne = "Ring1";
            public static string RingTwo = "Ring2";
            public static string RingThree = "Ring3";
            public static string RingFour = "Ring4";
            public static string RingFive = "Ring5";
            public static string RingSix = "Ring6";
            public static string RingSeven = "Ring7";
            public static string RingEight = "Ring8";

        };

        /// <summary>
        /// Volume, pitch, pan and islooped for music and audio/sound effects
        /// </summary>
        public struct Volume
        {
            /// <summary>
            /// Music volume, pitch, pan and islooped 
            /// </summary>
            public struct Music
            {
                public static float volume;
                public static float pitch;
                public static float pan;
            }

            /// <summary>
            /// Audio/Sound Effect volume, pitch, pan and islooped 
            /// </summary>
            public struct Audio
            {
                public static float volume;
                public static float pitch;
                public static float pan;
            }
        }

        /// <summary>
        /// GameStates used to switch between game screens or 'states'
        /// </summary>
        public enum GameState
        {
            mainMenu,
            options,
            game,
            chooseCharacter,
            inventory,
            shop,
            battle
        }

        public static GameState currentGameState, previousGameState;

        /// <summary>
        /// Global variable to exit game
        /// </summary>
        public static bool exitGame;
        public static bool exitAfterFade;
        public static bool gamePaused = false;

        /// <summary>
        /// X size of the game
        /// </summary>
        public static float gameScreenX = 1280;

        /// <summary>
        /// Y size of the game
        /// </summary>
        public static float gameScreenY = 720;

        /// <summary>
        /// X size of the users screen resolution
        /// </summary>
        public static float trueScreenX;

        /// <summary>
        /// Y size of the users screen resolution
        /// </summary>
        public static float trueScreenY;

        /// <summary>
        /// Times any X value by this
        /// </summary>
        public static float refToScreenX;

        /// <summary>
        /// Times any Y value by this
        /// </summary>
        public static float refToScreenY;

        public static float screenToRefX;
        public static float screenToRefY;

        public static string savedGameLocation = "Content/SavedGames/SavedGame";
        public static bool creatingCharacter = false;
        public static string playerName = "";

        public static string debugText;
        static string debugFilePath;
        public static int debugLevel;

        static FileStream debugFile;

        public static string GetDate()
        {
            string dateTime = "[";
            dateTime += DateTime.Now.Day.ToString() + "-";
            dateTime += DateTime.Now.Month.ToString() + "-";
            dateTime += DateTime.Now.Year.ToString() + "]";
            return dateTime;
        }

        public static string GetTime()
        {
            string time = "[";
            int hour = DateTime.Now.Hour;
            if (hour > 12)
                hour -= 12;
            time += hour.ToString() + "-";
            time += DateTime.Now.Minute.ToString() + "-";
            time += DateTime.Now.Second.ToString() + "]";
            return time;
        }

        public static void CreateDebugLog()
        {
            if (!Directory.Exists("Content/DebugLogs"))
            {
                Directory.CreateDirectory("Content/DebugLogs");
            }

            debugFilePath = "Content/DebugLogs/DebugLog" + GetDate() + GetTime() + ".txt";
            debugFile = new FileStream(debugFilePath, FileMode.Create);
            debugFile.Close();
        }
        public static void LogDebugInfo(string debugText, int level)
        {
            if (debugLogEnabled && level <= debugLevel)
            {
                File.AppendAllText(debugFilePath, GetTime() + debugText + Environment.NewLine);
            }
        }

        /// <summary>
        /// Draws a box around any rectangle
        /// </summary>
        /// <param name="bounds">Rectangle of an object</param>
        /// <param name="spriteBatch">SpriteBatch used for drawing the box</param>
        /// <param name="pixel">Can be any sprite, works best with a pixel</param>
        /// <param name="size">Size of the edges of the box</param>
        /// <param name="layer">Layer the box is drawn at</param>
        /// <param name="colour">Color of the box</param>
        public static void DrawBoundingBox(Rectangle bounds, SpriteBatch spriteBatch, Texture2D pixel, int size, float layer, Color colour)
        {
            spriteBatch.Draw(pixel, new Rectangle(bounds.X, bounds.Y - 1, bounds.Size.X, size), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(pixel, new Rectangle(bounds.X - 1, bounds.Y, size, bounds.Size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(pixel, new Rectangle(bounds.X + bounds.Size.X, bounds.Y, size, bounds.Size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(pixel, new Rectangle(bounds.X, bounds.Y + bounds.Size.Y, bounds.Size.X, size), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.5f);
        }
    }
}

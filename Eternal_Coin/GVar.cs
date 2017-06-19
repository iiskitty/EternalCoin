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

        public static Player player = null;

        /// <summary>
        /// version number for Eternal Coin
        /// </summary>
        public static string verNum = "Alpha 0.34";

        /// <summary>
        /// Set to what ever location is passed into ReadLocationXmlFile function in ReadXml.cs.
        /// </summary>
        public static XmlDocument curLocNode = new XmlDocument();

        /// <summary>
        /// preventclick is to stop clicks from happening right after a user clicks on something
        /// </summary>
        public static int preventclick = 0;

        /// <summary>
        /// npc dialog text wrapping length
        /// </summary>
        public static int npcTextWrapLength = 500;

        /// <summary>
        /// temporary money holder
        /// </summary>
        public static int silverMoney = 0;

        /// <summary>
        /// number of saved games
        /// </summary>
        public static int numSavedGames = 0;

        /// <summary>
        /// used when user clicks on an active quest in the quest list in game
        /// </summary>
        public static string questInfo = "";

        /// <summary>
        /// name of the story user is playing
        /// </summary>
        public static string storyName = "";

        /// <summary>
        /// ID of the display picture the user is using
        /// </summary>
        public static string displayPicID = "Default";

        /// <summary>
        /// ID of the display picture of an enemy the user may run into
        /// </summary>
        public static string eDisplayPicID = string.Empty;

        /// <summary>
        /// loadData is used to load the maps after everything else has loaded
        /// </summary>
        public static bool loadData = false;

        /// <summary>
        /// used to start game with new character
        /// </summary>
        public static bool startGame = false;

        /// <summary>
        /// used to start game with loaded character
        /// </summary>
        public static bool loadGame = false;

        /// <summary>
        /// used to change to options (check Update.cs)
        /// </summary>
        public static bool changeToOptions = false;

        /// <summary>
        /// used to change to main menu (check Update.cs)
        /// </summary>
        public static bool changeToMainMenu = false;

        /// <summary>
        /// used when user has created a character and is choosing a story
        /// </summary>
        public static bool chooseStory = false;
        
        /// <summary>
        /// used to change to create character (check Update.cs)
        /// </summary>
        public static bool changeToCreateCharacter = false;

        /// <summary>
        /// used to change to battle (check Update.cs)
        /// </summary>
        public static bool changeToBattle = false;

        /// <summary>
        /// used to toggle full screen
        /// </summary>
        public static bool toggleFullScreen = false;

        /// <summary>
        /// all debug log calls will work if this is true
        /// </summary>
        public static bool debugLogEnabled = false;

        /// <summary>
        /// used to change back to game (from a battle, from options screen)
        /// </summary>
        public static bool changeBackToGame = false;

        /// <summary>
        /// true if game window is active (not tabbed out)
        /// </summary>
        public static bool windowIsActive;

        /// <summary>
        /// true if user is choosing a different display picture
        /// </summary>
        public static bool choosingDP = false;

        /// <summary>
        /// worldMaps texture is changed when user changes main locations
        /// </summary>
        public static WorldMap worldMap;

        /// <summary>
        /// information for players current location is kept here
        /// </summary>
        public static Location location;

        /// <summary>
        /// information for an npc the player may be talking to is kept here
        /// </summary>
        public static NPC npc;

        /// <summary>
        /// sound ID's for all sounds that are loaded into the game
        /// </summary>
        public struct SoundIDs
        {
            public static string clickcoin;
            public static string clicklocnode;
            public static string clickbutton;
            public static string buttonmouseover;
        };

        /// <summary>
        /// no idea will come back to this
        /// </summary>
        public class States
        {
            public struct Button
            {
                public static string locationbutton = "locationbutton";
            };
        }

        /// <summary>
        /// animation states
        /// </summary>
        public class AnimStates
        {
            /// <summary>
            /// buttons animation states
            /// </summary>
            public struct Button
            {
                /// <summary>
                /// default animation
                /// </summary>
                public static string def = "default";
                /// <summary>
                /// mouse over animation
                /// </summary>
                public static string mouseover = "mouseover";
            };
        }

        /// <summary>
        /// xml tags used in all documents
        /// </summary>
        public class XmlTags
        {
            /// <summary>
            /// xml tags for player
            /// </summary>
            public struct Player
            {
                /// <summary>
                /// tag that hold the players name
                /// </summary>
                public static string name = "name";
                /// <summary>
                /// tag that hold the name of the players current location
                /// </summary>
                public static string currentlocation = "currentlocation";
            };

            /// <summary>
            /// xml tags for locations
            /// </summary>
            public struct LocationTags
            {
                /// <summary>
                /// tag that holds the locations name
                /// </summary>
                public static string name = "name";
                /// <summary>
                /// tag that holds true or false for if the location has a npc
                /// </summary>
                public static string hasnpc = "hasnpc";
                /// <summary>
                /// tag that holds true or false for if the location has a shop
                /// </summary>
                public static string hasshop = "hasshop";
                /// <summary>
                /// tag that holds true or false for if the location has a enemy
                /// </summary>
                public static string hasenemy = "hasenemy";
                /// <summary>
                /// tag that holds the description of the location
                /// </summary>
                public static string description = "description";
                /// <summary>
                /// tag that holds true or false for if the location has been searched or not
                /// </summary>
                public static string searched = "searched";
            };

            /// <summary>
            /// xml tags for npcs
            /// </summary>
            public struct NPCTags
            {
                /// <summary>
                /// tag that holds the name of the npc
                /// </summary>
                public static string name = "name";

                /// <summary>
                /// tag that holds true or false for if npc has a quest
                /// </summary>
                public static string hasquest = "hasquest";

                /// <summary>
                /// greeting tags for the npc
                /// </summary>
                public struct Greetings
                {
                    /// <summary>
                    /// tag that holds the greeting for when the player accepts the npc's quest
                    /// </summary>
                    public static string acceptquest = "acceptquest";
                    /// <summary>
                    /// tag that holds the greeting for when the player hands in a completed quest
                    /// </summary>
                    public static string handinquest = "handinquest";
                    /// <summary>
                    /// tag that holds the greeting for when the player has not accepted the npc's quest
                    /// </summary>
                    public static string questunaccepted = "questunaccepted";
                    /// <summary>
                    /// tag that hold the greeting for when the player has accepted the quest
                    /// </summary>
                    public static string questaccepted = "questaccepted";
                    /// <summary>
                    /// tag that holds the greeting for when the player has finished the quest
                    /// </summary>
                    public static string questfinished = "questfinished";
                    /// <summary>
                    /// tag the holds the greeting for when the play has finished and handed in the npc's quest
                    /// </summary>
                    public static string questcompleted = "questcompleted";
                };
            };

            /// <summary>
            /// xml tags for quests
            /// </summary>
            public struct QuestTags
            {
                /// <summary>
                /// tag that holds true or false for if the quest is finished
                /// </summary>
                public static string questfinished = "questfinished";
                /// <summary>
                /// tag that holds true or false for if the quest is completed
                /// </summary>
                public static string questcompleted = "questcompleted";
                /// <summary>
                /// tag that hold true or false for if the quest is accepted or not
                /// </summary>
                public static string questaccepted = "questaccepted";
                /// <summary>
                /// tag that holds the description for the quest
                /// </summary>
                public static string description = "description";
                /// <summary>
                /// tag that holds a short description for the quest
                /// </summary>
                public static string shortdescription = "shortdescription";
                /// <summary>
                /// tag that holds the completing action for the quest
                /// </summary>
                public static string completingaction = "completingaction";
                /// <summary>
                /// tag that holds the completing location for the quest
                /// </summary>
                public static string completinglocation = "completinglocation";
                /// <summary>
                /// tag that holds true or false for is the quest is completed or not
                /// </summary>
                public static string completed = "completed";
                /// <summary>
                /// tag that holds the location's file path for the quest
                /// </summary>
                public static string locationfilepath = "locationfilepath";
            };

            /// <summary>
            /// xml tags for actions
            /// </summary>
            public struct Actions
            {
                /// <summary>
                /// tag that holds the string for exploring a location
                /// </summary>
                public static string explore = "explore";
                /// <summary>
                /// tag that holds the string for entering a location
                /// </summary>
                public static string enter = "enter";
                /// <summary>
                /// tag that holds the string for talking to an npc
                /// </summary>
                public static string talknpc = "talknpc";
            };

            /// <summary>
            /// xml tags for items
            /// </summary>
            public struct ItemTags
            {
                /// <summary>
                /// tag that holds the string for the class of the item
                /// </summary>
                public static string itemclass = "class";
                /// <summary>
                /// tag that holds the string for the type of the item
                /// </summary>
                public static string itemtype = "itemtype";
                /// <summary>
                /// tag that holds the string for the name of the item
                /// </summary>
                public static string itemname = "itemname";
                /// <summary>
                /// tag that holds the string for the material of the item
                /// </summary>
                public static string itemmaterial = "itemmaterial";
                /// <summary>
                /// tag that holds the string for the inventory slot of the item
                /// </summary>
                public static string inventoryslot = "inventoryslot";
                /// <summary>
                /// tag that holds the number of the player inventory slot of the item
                /// </summary>
                public static string playerinventoryslot = "playerinventoryslot";
            };
        }

        /// <summary>
        /// names of materials for items
        /// </summary>
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

        /// <summary>
        /// attack animation states
        /// </summary>
        public struct AttackAnimStates
        {
            public static string idle = "Idle";
            public static string buildUp = "BuildUp";
            public static string attack = "Attack";
            public static string attacked = "Attacked";
            public static string defend = "Defend";
            public static string retreat = "Retreat";
        };

        /// <summary>
        /// type of attack
        /// </summary>
        public struct AttackType
        {
            public static string physical = "Physical";
            public static string magic = "Magic";
        };

        /// <summary>
        /// class of items
        /// </summary>
        public struct ItemClassName
        {
            public const string weapon = "Weapon";
            public const string armor = "Armor";
            public const string jewellry = "Jewellry";
            public const string eternalcoin = "EternalCoin";
        };

        /// <summary>
        /// inventory slots of items
        /// </summary>
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

        public static string savedGameLocation = string.Empty;
        public static string gameFilesLocation = string.Empty;
        public static string debugFilesLocation = string.Empty;
        public static bool creatingCharacter = false;
        public static string playerName = "";

        public static string debugText;
        static string debugFilePath;
        public static int debugLevel;

        static FileStream debugFile;

        /// <summary>
        /// Gets the current date
        /// </summary>
        /// <returns>the date built into a string</returns>
        public static string GetDate()
        {
            string dateTime = "[";
            dateTime += DateTime.Now.Day.ToString() + "-";
            dateTime += DateTime.Now.Month.ToString() + "-";
            dateTime += DateTime.Now.Year.ToString() + "]";
            return dateTime;
        }

        /// <summary>
        /// Gets the current time
        /// </summary>
        /// <returns>the time built into a string</returns>
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

        /// <summary>
        /// creates a debug log text file
        /// </summary>
        public static void CreateDebugLog()
        {
            if (!Directory.Exists(debugFilesLocation))
            {
                Directory.CreateDirectory(debugFilesLocation);
            }

            debugFilePath = debugFilesLocation + "DebugLog" + GetDate() + GetTime() + ".jsk";
            debugFile = new FileStream(debugFilePath, FileMode.Create);
            debugFile.Close();
        }
        /// <summary>
        /// logs passed in information to debug log text file
        /// </summary>
        /// <param name="debugText">information to be logged</param>
        /// <param name="level">importance of the information</param>
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

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
    public class Dictionaries
    {
        public static Dictionary<string, Texture2D> maps;
        public static Dictionary<string, Texture2D> textures;
        public static Dictionary<string, DisplayPicture> displayPictures;
        public static Dictionary<string, DisplayPicture> eDisplayPictures;
        public static Dictionary<string, Material> materials;
        public static Dictionary<string, ItemType> itemTypes;
        public static Dictionary<string, Texture2D> itemTextures;
        public static Dictionary<string, LocationNode> locNodes;
        public static Dictionary<string, SoundEffect> sounds;
        public static Dictionary<string, Attack> attacks;
        public static Dictionary<string, Attack> enemyAttacks;
        public static Dictionary<string, Attack> availableAttacks;
        public static Dictionary<string, Item> items;

        public static void InitializeDictionaries()
        {
            materials = new Dictionary<string, Material>();
            itemTypes = new Dictionary<string, ItemType>();
            maps = new Dictionary<string, Texture2D>();
            textures = new Dictionary<string, Texture2D>();
            itemTextures = new Dictionary<string, Texture2D>();
            locNodes = new Dictionary<string, LocationNode>();
            sounds = new Dictionary<string, SoundEffect>();
            attacks = new Dictionary<string, Attack>();
            availableAttacks = new Dictionary<string, Attack>();
            enemyAttacks = new Dictionary<string, Attack>();
            displayPictures = new Dictionary<string, DisplayPicture>();
            eDisplayPictures = new Dictionary<string, DisplayPicture>();
            items = new Dictionary<string, Item>();
        }

        public static void ClearDictionaries()
        {
            locNodes.Clear();
            enemyAttacks.Clear();
            availableAttacks.Clear();
            maps.Clear();
        }
    }
}

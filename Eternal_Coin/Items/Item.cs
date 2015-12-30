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
    public abstract class Item : AnimationManager
    {
        protected List<string> attacks;
        protected Material material;
        protected ItemType type;
        protected Texture2D spriteID;
        protected Rectangle bounds;
        protected Vector2 position;
        protected Vector2 size;
        protected string itemName;
        protected string itemClass;
        protected string inventorySlot;
        protected int playerInventorySlot;
        protected int cost;

        public Item() { }

        public Item(Item item) 
            : base(item.Position, item.colour)
        {
            this.spriteID = item.spriteID;
            this.position = item.Position;
            this.size = item.Size;
            this.itemName = item.ItemName;
            this.inventorySlot = item.InventorySlot;
            this.playerInventorySlot = item.PlayerInventorySlot;
            this.itemClass = item.ItemClass;
            this.material = item.Material;
            this.type = item.Type;
            this.cost = item.Cost;
            AddAnimation(1, 0, 0, GVar.AnimStates.Button.def, spriteID.Width, spriteID.Height, Vector2.Zero);
            PlayAnimation(GVar.AnimStates.Button.def);
            attacks = new List<string>();
        }

        public Item(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string itemClass, int cost, string itemName, int playerInventorySlot, string inventorySlot, Material material, ItemType type) 
            : base(position, colour)
        {
            AddAnimation(1, 0, 0, GVar.AnimStates.Button.def, spriteID.Width, spriteID.Height, Vector2.Zero);
            PlayAnimation(GVar.AnimStates.Button.def);
            this.spriteID = spriteID;
            this.position = position;
            this.size = size;
            this.itemName = itemName;
            this.inventorySlot = inventorySlot;
            this.playerInventorySlot = playerInventorySlot;
            this.itemClass = itemClass;
            this.material = material;
            this.type = type;
            this.cost = cost;
            attacks = new List<string>();
        }

        public Item(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string itemClass, string itemName, int cost, string inventorySlot, Material material, ItemType type) 
            : base(position, colour)
        {
            AddAnimation(1, 0, 0, GVar.AnimStates.Button.def, spriteID.Width, spriteID.Height, Vector2.Zero);
            PlayAnimation(GVar.AnimStates.Button.def);
            this.spriteID = spriteID;
            this.position = position;
            this.size = size;
            this.itemClass = itemClass;
            this.itemName = itemName;
            this.cost = cost;
            this.inventorySlot = inventorySlot;
            this.material = material;
            this.type = type;
        }

        public static void DrawItemInfo(SpriteBatch spriteBatch, Weapon w)
        {
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, w.ItemName, new Vector2(45, 386), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Damage: " + w.Damage.ToString(), new Vector2(45, 410), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Purchase Price: " + w.Cost.ToString(), new Vector2(45, 436), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            int cost = w.Cost / 2;
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Selling Price: " + cost.ToString(), new Vector2(45, 460), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
        }
        public static void DrawItemInfo(SpriteBatch spriteBatch, Armor a)
        {
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, a.ItemName, new Vector2(45, 386), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Armor: " + a.ArmorValue.ToString(), new Vector2(45, 410), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Purchase Price: " + a.Cost.ToString(), new Vector2(45, 436), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            int cost = a.Cost / 2;
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Selling Price: " + cost.ToString(), new Vector2(45, 460), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
        }
        public static void DrawItemInfo(SpriteBatch spriteBatch, Jewellry j)
        {
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, j.ItemName, new Vector2(45, 386), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Purchase Price: " + j.Cost.ToString(), new Vector2(45, 436), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            int cost = j.Cost / 2;
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Selling Price: " + cost.ToString(), new Vector2(45, 460), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
        }

        public abstract void Update(float gameTime);

        public override void AnimationDone(string animation)
        {
            
        }

        public static void CreateItems()
        {
            XmlDocument itemsDoc = new XmlDocument();
            itemsDoc.Load("./Content/LoadData/CreateItems.xml");
            XmlNodeList weaponItems = itemsDoc.SelectNodes("/items/weapons/item");

            foreach (XmlNode weapon in weaponItems)
            {
                try
                {
                    Item item = ItemBuilder.BuildItem(weapon[GVar.XmlTags.ItemTags.itemclass].InnerText, 0, weapon[GVar.XmlTags.ItemTags.inventoryslot].InnerText, weapon[GVar.XmlTags.ItemTags.itemname].InnerText, Dictionaries.itemTypes[weapon[GVar.XmlTags.ItemTags.itemtype].InnerText], Dictionaries.materials[weapon[GVar.XmlTags.ItemTags.itemmaterial].InnerText]);

                    XmlNodeList attacks = itemsDoc.SelectNodes("/items/weapons/item/" + item.Type.name + "attack");
                    foreach (XmlNode attack in attacks)
                    {
                        item.Attacks.Add(attack.InnerText);
                    }

                    Dictionaries.items.Add(item.itemName, item);
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!! failed to create weapon. [" + e + "]", 1);
                }
            }

            XmlNodeList armorItems = itemsDoc.SelectNodes("/items/armor/item");

            foreach (XmlNode armor in armorItems)
            {
                try
                {
                    Item item = ItemBuilder.BuildItem(armor[GVar.XmlTags.ItemTags.itemclass].InnerText, 0, armor[GVar.XmlTags.ItemTags.inventoryslot].InnerText, armor[GVar.XmlTags.ItemTags.itemname].InnerText, Dictionaries.itemTypes[armor[GVar.XmlTags.ItemTags.itemtype].InnerText], Dictionaries.materials[armor[GVar.XmlTags.ItemTags.itemmaterial].InnerText]);

                    Dictionaries.items.Add(item.itemName, item);
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!! failed to create armor. [" + e + "]", 1);
                }
            }

            XmlNodeList jewellryItems = itemsDoc.SelectNodes("/items/jewellry/item");

            foreach (XmlNode jewellry in jewellryItems)
            {
                try
                {
                    Item item = ItemBuilder.BuildItem(jewellry[GVar.XmlTags.ItemTags.itemclass].InnerText, 0, jewellry[GVar.XmlTags.ItemTags.inventoryslot].InnerText, jewellry[GVar.XmlTags.ItemTags.itemname].InnerText, Dictionaries.itemTypes[jewellry[GVar.XmlTags.ItemTags.itemtype].InnerText], Dictionaries.materials[jewellry[GVar.XmlTags.ItemTags.itemmaterial].InnerText]);
                    
                    Dictionaries.items.Add(item.itemName, item);
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!! failed to create jewellry. [" + e + "]", 1);
                }
            }
        }

        public Material Material { get { return material; } set { material = value; } }
        public ItemType Type { get { return type; } set { type = value; } }
        public Texture2D SpriteID { get { return spriteID; } set { spriteID = value; } }
        public Rectangle Bounds { get { return bounds; } set { bounds = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Size { get { return size; } set { size = value; } }
        public string ItemName { get { return itemName; } set { itemName = value; } }
        public string ItemClass { get { return itemClass; } set { itemClass = value; } }
        public string InventorySlot { get { return inventorySlot; } set { inventorySlot = value; } }
        public int PlayerInventorySlot { get { return playerInventorySlot; } set { playerInventorySlot = value; } }
        public int Cost { get { return cost; } set { cost = value; } }
        public List<string> Attacks { get { return attacks; } set { attacks = value; } }
    }

    public class Weapon : Item
    {
        int damage;

        public Weapon() { }

        public Weapon(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string itemClass, int cost, int damage, string weaponName, int playerInventorySlot, string inventorySlot, Material material, ItemType type) 
            : base(spriteID, position, size, colour, itemClass, cost, weaponName, playerInventorySlot, inventorySlot, material, type)
        {
            this.damage = damage;
        }

        public override void Update(float gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public override void AnimationDone(string animation)
        {
            
        }

        public int Damage { get { return damage; } set { damage = value; } }
    }

    public class Armor : Item
    {
        int armorValue;

        public Armor() { }

        public Armor(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string itemClass, int cost, int armor, string armorName, int playerInventorySlot, string inventorySlot, Material material, ItemType type)
            : base(spriteID, position, size, colour, itemClass, cost, armorName, playerInventorySlot, inventorySlot, material, type)
        {
            this.armorValue = armor;
        }

        public override void Update(float gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public override void AnimationDone(string animation)
        {
            
        }

        public int ArmorValue { get { return armorValue; } set { armorValue = value; } }
    }

    public class Jewellry : Item
    {
        public ItemSlot eternalCoinSlot;
        Vector2 eternalCoinSlotPosition;
        Vector2 eternalCoinSlotSize;

        public Jewellry(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string itemClass, string itemName, int cost, string inventorySlot, Material material, ItemType type) 
            : base(spriteID, position, size, colour, itemClass, itemName, cost, inventorySlot, material, type)
        {
            this.spriteID = SpriteID;
            if (inventorySlot.Contains("Ring"))
            {
                eternalCoinSlotPosition = new Vector2(position.X + 5, position.Y + 5);
                eternalCoinSlotSize = new Vector2(15, 15);
                eternalCoinSlot = new ItemSlot(eternalCoinSlotPosition, Vector2.Zero, eternalCoinSlotSize, Vector2.Zero, "EternalCoin");
            }
        }

        public Jewellry(Item item) 
            : base(item)
        {
            this.spriteID = item.SpriteID;
        }

        public override void Update(float gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            
            //if (inventorySlot.Contains("Ring"))
            //{
            //    eternalCoinSlot.position = new Vector2(position.X, position.Y);
            //    eternalCoinSlot.size = new Vector2(15, 15);
            //    eternalCoinSlot.position = eternalCoinSlotPosition;
            //    eternalCoinSlot.size = eternalCoinSlotSize;
            //}
        }

        public override void AnimationDone(string animation)
        {
            
        }
    }
}

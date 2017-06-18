using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;

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

        /// <summary>
        /// Create a copy of another item
        /// </summary>
        /// <param name="item"></param>
        public Item(Item item) 
            : base(item.Position, item.colour)
        {
            spriteID = item.spriteID;
            position = item.Position;
            size = item.Size;
            itemName = item.ItemName;
            inventorySlot = item.InventorySlot;
            playerInventorySlot = item.PlayerInventorySlot;
            itemClass = item.ItemClass;
            material = item.Material;
            type = item.Type;
            cost = item.Cost;
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
            attacks = new List<string>();
        }

        public static void DrawItemInfo(SpriteBatch spriteBatch, Weapon w)
        {
            spriteBatch.Draw(w.SpriteID, new Rectangle(114, 92, 200, 200), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, w.ItemName, new Vector2(45, 386), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Damage: " + w.Damage.ToString(), new Vector2(45, 410), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Purchase Price: " + w.Cost.ToString(), new Vector2(45, 436), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            int cost = w.Cost / 2;
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Selling Price: " + cost.ToString(), new Vector2(45, 460), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
        }
        public static void DrawItemInfo(SpriteBatch spriteBatch, Armor a)
        {
            spriteBatch.Draw(a.SpriteID, new Rectangle(114, 92, 200, 200), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, a.ItemName, new Vector2(45, 386), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Armor: " + a.ArmorValue.ToString(), new Vector2(45, 410), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Purchase Price: " + a.Cost.ToString(), new Vector2(45, 436), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            int cost = a.Cost / 2;
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Selling Price: " + cost.ToString(), new Vector2(45, 460), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
        }
        public static void DrawItemInfo(SpriteBatch spriteBatch, Jewellry j)
        {
            spriteBatch.Draw(j.SpriteID, new Rectangle(114, 92, 200, 200), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, j.ItemName, new Vector2(45, 386), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Purchase Price: " + j.Cost.ToString(), new Vector2(45, 436), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            int cost = j.Cost / 2;
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Selling Price: " + cost.ToString(), new Vector2(45, 460), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

            if (j.eternalCoinSlot.item != null)
            {
                spriteBatch.Draw(j.eternalCoinSlot.item.SpriteID, new Rectangle(200, 98, 60, 60), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                spriteBatch.DrawString(Fonts.lucidaConsole14Regular, j.eternalCoinSlot.item.ItemName, new Vector2(45, 484), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Purchasing Price: " + j.eternalCoinSlot.item.Cost.ToString(), new Vector2(45, 508), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                int ECcost = j.eternalCoinSlot.item.Cost / 2;
                spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Selling Price: " + ECcost.ToString(), new Vector2(45, 532), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            }
        }
        public static void DrawItemInfo(SpriteBatch spriteBatch, EternalCoin e)
        {
            spriteBatch.Draw(e.SpriteID, new Rectangle(114, 92, 200, 200), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, e.ItemName, new Vector2(45, 386), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Purchasing Price: " + e.Cost.ToString(), new Vector2(45, 436), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            int cost = e.Cost / 2;
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Selling Price: " + cost.ToString(), new Vector2(45, 460), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
        }

        public static void ToPlayer(Item item, int i)
        {
            Lists.playerItems.Add(item);
            InventoryManager.playerInventory.ItemSlots[i].item = item;
        }

        public static void FromPlayer(Item item, int i)
        {
            Lists.playerItems.Remove(item);
            InventoryManager.playerInventory.ItemSlots[i].item = null;
        }

        public static void ToCharacter(Item item, string invSlot)
        {
            Lists.characterItems.Add(item);
            InventoryManager.characterInventory.ItemSlots[invSlot].item = item;
        }

        public static void FromCharacter(Item item, string invSlot)
        {
            Lists.characterItems.Remove(item);
            InventoryManager.characterInventory.ItemSlots[invSlot].item = null;
        }

        public static void ToShop(Item item, int i)
        {
            Lists.shopItems.Add(item);
            InventoryManager.shopInventory.ItemSlots[i].item = item;
        }

        public static void FromShop(Item item, int i)
        {
            Lists.shopItems.Remove(item);
            InventoryManager.shopInventory.ItemSlots[i].item = null;
        }

        public abstract void Update(float gameTime);

        public override void AnimationDone(string animation)
        {
            
        }

        /// <summary>
        /// Create all items from Data.xml
        /// </summary>
        public static void CreateItems()
        {
            XmlDocument itemsDoc = new XmlDocument();
            itemsDoc.Load("./Content/LoadData/Data.xml");
            XmlNodeList weaponItems = itemsDoc.SelectNodes("/data/createitems/weapons/item");

            foreach (XmlNode weapon in weaponItems)
            {
                try
                {
                    Item item = ItemBuilder.BuildItem(weapon[GVar.XmlTags.ItemTags.itemclass].InnerText, 0, weapon[GVar.XmlTags.ItemTags.inventoryslot].InnerText, weapon[GVar.XmlTags.ItemTags.itemname].InnerText, Dictionaries.itemTypes[weapon[GVar.XmlTags.ItemTags.itemtype].InnerText], Dictionaries.materials[weapon[GVar.XmlTags.ItemTags.itemmaterial].InnerText]);

                    XmlNodeList attacks = itemsDoc.SelectNodes("/data/createitems/weapons/item/" + item.Type.name + "attack");
                    foreach (XmlNode attack in attacks)
                    {
                        item.Attacks.Add(attack.InnerText);
                    }

                    Dictionaries.items.Add(item.itemName, item);
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
            }

            XmlNodeList armorItems = itemsDoc.SelectNodes("/data/createitems/armor/item");

            foreach (XmlNode armor in armorItems)
            {
                try
                {
                    Item item = ItemBuilder.BuildItem(armor[GVar.XmlTags.ItemTags.itemclass].InnerText, 0, armor[GVar.XmlTags.ItemTags.inventoryslot].InnerText, armor[GVar.XmlTags.ItemTags.itemname].InnerText, Dictionaries.itemTypes[armor[GVar.XmlTags.ItemTags.itemtype].InnerText], Dictionaries.materials[armor[GVar.XmlTags.ItemTags.itemmaterial].InnerText]);

                    Dictionaries.items.Add(item.itemName, item);
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
            }

            XmlNodeList jewellryItems = itemsDoc.SelectNodes("/data/createitems/jewellry/item");

            foreach (XmlNode jewellry in jewellryItems)
            {
                try
                {
                    Item item = ItemBuilder.BuildItem(jewellry[GVar.XmlTags.ItemTags.itemclass].InnerText, 0, jewellry[GVar.XmlTags.ItemTags.inventoryslot].InnerText, jewellry[GVar.XmlTags.ItemTags.itemname].InnerText, Dictionaries.itemTypes[jewellry[GVar.XmlTags.ItemTags.itemtype].InnerText], Dictionaries.materials[jewellry[GVar.XmlTags.ItemTags.itemmaterial].InnerText]);
                    
                    Dictionaries.items.Add(item.itemName, item);
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
            }

            XmlNodeList eternalCoinItems = itemsDoc.SelectNodes("/data/createitems/eternalcoins/item");

            foreach (XmlNode eternalCoin in eternalCoinItems)
            {
                try
                {
                    Item item = ItemBuilder.BuildItem(eternalCoin[GVar.XmlTags.ItemTags.itemclass].InnerText, 0, eternalCoin[GVar.XmlTags.ItemTags.inventoryslot].InnerText, eternalCoin[GVar.XmlTags.ItemTags.itemname].InnerText, Dictionaries.itemTypes[eternalCoin[GVar.XmlTags.ItemTags.itemtype].InnerText], Dictionaries.materials[eternalCoin[GVar.XmlTags.ItemTags.itemmaterial].InnerText]);

                    XmlNodeList attacks = itemsDoc.SelectNodes("/data/createitems/eternalcoins/item/" + item.Type.name + "Attack");
                    foreach (XmlNode attack in attacks)
                    {
                        item.Attacks.Add(attack.InnerText);
                    }

                    Dictionaries.items.Add(item.itemName, item);
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
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
}

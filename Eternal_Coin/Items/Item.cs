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

    public static void DrawItemInfo(SpriteBatch spriteBatch, Item item)
    {
      spriteBatch.Draw(item.SpriteID, new Rectangle(114, 92, 200, 200), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
      spriteBatch.DrawString(Fonts.lucidaConsole14Regular, item.ItemName, new Vector2(45, 386), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
      spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Purchase Price: " + item.Cost.ToString(), new Vector2(45, 436), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
      int cost = item.Cost / 2;
      spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Selling Price: " + cost.ToString(), new Vector2(45, 460), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

      switch (item.ItemClass)
      {
        case GVar.ItemClassName.weapon:
          Weapon W = (Weapon)item;
          spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Damage: " + W.Damage.ToString(), new Vector2(45, 410), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
          break;
        case GVar.ItemClassName.armor:
          Armor A = (Armor)item;
          spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Armor: " + A.ArmorValue.ToString(), new Vector2(45, 410), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
          break;
        case GVar.ItemClassName.jewellry:
          Jewellry J = (Jewellry)item;
          if (J.eternalCoinSlot.item != null)
          {
            spriteBatch.Draw(J.eternalCoinSlot.item.SpriteID, new Rectangle(200, 98, 60, 60), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, J.eternalCoinSlot.item.ItemName, new Vector2(45, 484), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Purchasing Price: " + J.eternalCoinSlot.item.Cost.ToString(), new Vector2(45, 508), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
            int ECcost = J.eternalCoinSlot.item.Cost / 2;
            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Selling Price: " + ECcost.ToString(), new Vector2(45, 532), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
          }
          break;
      }
    }

    public static bool ToEternalCoinSlot(Jewellry jewl, Item item)
    {
      if (jewl.eternalCoinSlot.item == null)
      {
        jewl.eternalCoinSlot.item = item;
        return true;
      }
      return false;
    }

    public static Item FromEternalCoinSlot(Jewellry jewl)
    {
      Item item = null;
      if (jewl.eternalCoinSlot.item != null)
      {
        item = jewl.eternalCoinSlot.item;
        jewl.eternalCoinSlot.item = null;
      }
      return item;
    }

    /// <summary>
    /// Puts an item into an inventory based on the ItemSlot that was clicked on.
    /// </summary>
    /// <param name="itemSlot">The ItemSlot that was clicked on.</param>
    /// <param name="item">The Item that is being going into an inventory.</param>
    /// <returns></returns>
    public static bool ToInventory(ItemSlot itemSlot, Item item)
    {
      if (itemSlot.inventorySlot.Length > 2 && itemSlot.inventorySlot.Contains(item.inventorySlot)) //if the inventoryslot length is greater than 2, it is going to the characters inventory.
      {
        ToCharacter(itemSlot, item);
        return true;
      }
      else if (itemSlot.inventorySlot.Length <= 2) // to inventory or shop
      {
        switch (itemSlot.parentInventory)
        {
          case GVar.InventoryParentNames.inventory:
            ToPlayer(itemSlot, item);
            return true;
          case GVar.InventoryParentNames.shop:
            ToShop(itemSlot, item);
            return true;
        }
      }
      return false;
    }
    /// <summary>
    /// Takes an item from an inventory based on the ItemSlot that was clicked on.
    /// </summary>
    /// <param name="itemSlot">The ItemSlot that was clicked on.</param>
    /// <param name="item">The Item that is being taken from an inventory.</param>
    public static void FromInventory(ItemSlot itemSlot, Item item)
    {
      if (itemSlot.inventorySlot.Length > 2) // from character
      {
        FromCharacter(itemSlot, item);
      }
      else // from inventory or shop
      {
        switch (itemSlot.parentInventory)
        {
          case GVar.InventoryParentNames.inventory:
            FromPlayer(itemSlot, item);
            break;
          case GVar.InventoryParentNames.shop:
            FromShop(itemSlot, item);
            break;
        }
      }
    }

    private static void ToPlayer(ItemSlot itemSlot, Item item)
    {
      item.PlayerInventorySlot = Convert.ToInt32(itemSlot.inventorySlot);
      Lists.playerItems.Add(item);
      itemSlot.item = item;
    }

    private static void FromPlayer(ItemSlot itemSlot, Item item)
    {
      Lists.playerItems.Remove(item);
      itemSlot.item = null;
    }

    private static void ToCharacter(ItemSlot itemSlot, Item item)
    {
      Lists.characterItems.Add(item);
      itemSlot.item = item;
      Attack.AddAvailableAttacks(item);
      GVar.player.AddItemStats(item);
    }

    private static void FromCharacter(ItemSlot itemSlot, Item item)
    {
      Lists.characterItems.Remove(item);
      itemSlot.item = null;
      Attack.TakeAvailableAttacks(item);
      GVar.player.TakeItemStats(item);
    }

    private static void ToShop(ItemSlot itemSlot, Item item)
    {
      Lists.shopItems.Add(item);
      itemSlot.item = item;
      GVar.silverMoney += item.cost / 2;
    }

    private static void FromShop(ItemSlot itemSlot, Item item)
    {
      Lists.shopItems.Remove(item);
      itemSlot.item = null;
      GVar.silverMoney -= item.cost;
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
      itemsDoc.Load("./Content/LoadData/ItemData.xml");
      XmlNodeList weaponItems = itemsDoc.SelectNodes("/items/createitems/weapons/item");

      foreach (XmlNode weapon in weaponItems)
      {
        try
        {
          Item item = ItemBuilder.BuildItem(weapon[GVar.XmlTags.ItemTags.itemclass].InnerText, 0, weapon[GVar.XmlTags.ItemTags.inventoryslot].InnerText, weapon[GVar.XmlTags.ItemTags.itemname].InnerText, Dictionaries.itemTypes[weapon[GVar.XmlTags.ItemTags.itemtype].InnerText], Dictionaries.materials[weapon[GVar.XmlTags.ItemTags.itemmaterial].InnerText]);

          XmlNodeList attacks = itemsDoc.SelectNodes("/items/createitems/weapons/item/" + item.Type.name + "attack");
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

      XmlNodeList armorItems = itemsDoc.SelectNodes("/items/createitems/armor/item");

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

      XmlNodeList jewellryItems = itemsDoc.SelectNodes("/items/createitems/jewellry/item");

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

      XmlNodeList eternalCoinItems = itemsDoc.SelectNodes("/items/createitems/eternalcoins/item");

      foreach (XmlNode eternalCoin in eternalCoinItems)
      {
        try
        {
          Item item = ItemBuilder.BuildItem(eternalCoin[GVar.XmlTags.ItemTags.itemclass].InnerText, 0, eternalCoin[GVar.XmlTags.ItemTags.inventoryslot].InnerText, eternalCoin[GVar.XmlTags.ItemTags.itemname].InnerText, Dictionaries.itemTypes[eternalCoin[GVar.XmlTags.ItemTags.itemtype].InnerText], Dictionaries.materials[eternalCoin[GVar.XmlTags.ItemTags.itemmaterial].InnerText]);

          XmlNodeList attacks = itemsDoc.SelectNodes("/items/createitems/eternalcoins/item/" + item.Type.name + "Attack");
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

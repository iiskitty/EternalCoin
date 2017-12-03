using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Eternal_Coin
{
  public class InventoryManager
  {
    public static MouseInventory mouseInventory;
    public static Inventory playerInventory;
    public static Inventory shopInventory;
    public static EquipInventory characterInventory;
    public static EquipInventory enemyInventory;

    /// <summary>
    /// Creates all inventories ready for use.
    /// </summary>
    public static void CreateInventories()
    {
      mouseInventory = new MouseInventory();
      playerInventory = new Inventory(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(437, 51)), GVar.InventoryParentNames.inventory);
      enemyInventory = new EquipInventory(GVar.InventoryParentNames.enemy);
      shopInventory = new Inventory(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.shopInventoryUI), new Vector2(862, 51)), GVar.InventoryParentNames.shop);
      characterInventory = new EquipInventory(GVar.InventoryParentNames.character);
    }

    public static void ResetItemSlotPositions()
    {
      try
      {
        playerInventory.ResetItemSlotPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(437, 51)));
        shopInventory.ResetItemSlotPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.shopInventoryUI), new Vector2(862, 51)));
        characterInventory.ResetItemSlotPositions();
      }
      catch { }
    }

    /// <summary>
    /// clears all player inventories.
    /// </summary>
    public static void ClearInventories()
    {
      for (int i = 0; i < 40; i++)
      {
        if (playerInventory.ItemSlots[i].item != null)
        {
          Lists.playerItems.Remove(playerInventory.ItemSlots[i].item);
          playerInventory.ItemSlots[i].item = null;
        }
        if (shopInventory.ItemSlots[i].item != null)
        {
          Lists.shopItems.Remove(shopInventory.ItemSlots[i].item);
          shopInventory.ItemSlots[i].item = null;
        }
      }

      for (int i = 0; i < Lists.inventorySlots.Count; i++)
      {
        if (characterInventory.ItemSlots[Lists.inventorySlots[i]].item != null)
        {
          GVar.player.TakeItemStats(characterInventory.ItemSlots[Lists.inventorySlots[i]].item);
          Lists.characterItems.Remove(characterInventory.ItemSlots[Lists.inventorySlots[i]].item);
          characterInventory.ItemSlots[Lists.inventorySlots[i]].item = null;
        }
      }
    }

    /// <summary>
    /// Draws mini display of character and enemy character inventory on battle screen.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch to draw sprites.</param>
    /// <param name="inventoryName">Name of inventory to be drawn.</param>
    public static void DrawMiniInventory(SpriteBatch spriteBatch, string inventoryName)
    {
      switch (inventoryName)
      {
        case GVar.InventoryParentNames.character:
          for (int i = 0; i < Lists.inventorySlots.Count; i++)
          {
            if (characterInventory.ItemSlots[Lists.inventorySlots[i]].item != null)
              spriteBatch.Draw(characterInventory.ItemSlots[Lists.inventorySlots[i]].item.SpriteID, new Rectangle((int)characterInventory.ItemSlots[Lists.inventorySlots[i]].miniPosition.X, (int)characterInventory.ItemSlots[Lists.inventorySlots[i]].miniPosition.Y, (int)characterInventory.ItemSlots[Lists.inventorySlots[i]].miniSize.X, (int)characterInventory.ItemSlots[Lists.inventorySlots[i]].miniSize.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
          }
          break;
        case GVar.InventoryParentNames.enemy:
          for (int i = 0; i < Lists.inventorySlots.Count; i++)
          {
            if (enemyInventory.ItemSlots[Lists.inventorySlots[i]].item != null)
              spriteBatch.Draw(enemyInventory.ItemSlots[Lists.inventorySlots[i]].item.SpriteID, new Rectangle((int)enemyInventory.ItemSlots[Lists.inventorySlots[i]].miniPosition.X + 851, (int)enemyInventory.ItemSlots[Lists.inventorySlots[i]].miniPosition.Y, (int)enemyInventory.ItemSlots[Lists.inventorySlots[i]].miniSize.X, (int)enemyInventory.ItemSlots[Lists.inventorySlots[i]].miniSize.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
          }
          break;
      }
    }

    public static void OnMouseClicked(ItemSlot itemSlot)
    {
      if (GVar.currentGameState == GVar.GameState.inventory || GVar.currentGameState == GVar.GameState.shop)
      {
        if (itemSlot != null && itemSlot.item == null && mouseInventory.heldItem != null && MouseManager.mouse.mouseBounds.Intersects(itemSlot.bounds))
        {
          if (Item.ToInventory(itemSlot, mouseInventory.heldItem))
            mouseInventory.heldItem = null;
        }
        else if (itemSlot != null && itemSlot.item != null && mouseInventory.heldItem != null && MouseManager.mouse.mouseBounds.Intersects(itemSlot.bounds))
        {
          if (itemSlot.item.ItemClass == GVar.ItemClassName.jewellry && mouseInventory.heldItem.ItemClass == GVar.ItemClassName.eternalcoin)
          {
            if (Item.ToEternalCoinSlot((Jewellry)itemSlot.item, mouseInventory.heldItem))
              mouseInventory.heldItem = null;
          }
          else
          {
            Item tItem = mouseInventory.heldItem;
            mouseInventory.heldItem = itemSlot.item;
            Item.FromInventory(itemSlot, itemSlot.item);
            Item.ToInventory(itemSlot, tItem);
          }
        }
        else if (itemSlot != null && itemSlot.item != null && mouseInventory.heldItem == null && MouseManager.mouse.mouseBounds.Intersects(itemSlot.item.Bounds))
        {
          if (itemSlot.item.ItemClass == GVar.ItemClassName.jewellry)
          {
            Jewellry jewl = (Jewellry)itemSlot.item;

            if (MouseManager.mouse.mouseBounds.Intersects(jewl.eternalCoinSlot.bounds) && jewl.eternalCoinSlot.item != null)
              mouseInventory.heldItem = Item.FromEternalCoinSlot(jewl);
            else
            {
              mouseInventory.heldItem = itemSlot.item;
              Item.FromInventory(itemSlot, itemSlot.item);
            }
          }
          else if (itemSlot != null)
          {
            mouseInventory.heldItem = itemSlot.item;
            Item.FromInventory(itemSlot, itemSlot.item);
          }
        }
      }
    }

    /// <summary>
    /// Updates inventory items and itemslots.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="inventoryName">name of inventory to update.</param>
    public static void UpdateInventory(GameTime gameTime, string inventoryName)
    {
      UpdateInventoryItems(gameTime, GetItems(inventoryName));
      UpdateItemSlots(gameTime, GetItemSlots(inventoryName));
    }

    /// <summary>
    /// Returns a list of items from an inventory.
    /// </summary>
    /// <param name="inventoryName">name of inventory to get items from</param>
    /// <returns></returns>
    public static List<Item> GetItems(string inventoryName)
    {
      switch(inventoryName)
      {
        case GVar.InventoryParentNames.character:
          return Lists.characterItems;
        case GVar.InventoryParentNames.inventory:
          return Lists.playerItems;
        case GVar.InventoryParentNames.shop:
          return Lists.shopItems;
        case GVar.InventoryParentNames.mouse:
          return new List<Item>
          {
            mouseInventory.heldItem
          };
      }
      return null;
    }

    /// <summary>
    /// Returns a list of ItemSlots from an inventory.
    /// </summary>
    /// <param name="inventoryName">name of inventory to get itemslots from.</param>
    /// <returns></returns>
    public static List<ItemSlot> GetItemSlots(string inventoryName)
    {
      List<ItemSlot> itemSlots = new List<ItemSlot>();
      Inventory inv = null;
      switch(inventoryName)
      {
        case GVar.InventoryParentNames.character:
          foreach (var key in characterInventory.ItemSlots.Keys)
            itemSlots.Add(characterInventory.ItemSlots[key]);
          return itemSlots;
        case GVar.InventoryParentNames.inventory:
          inv = playerInventory;
          break;
        case GVar.InventoryParentNames.shop:
          inv = shopInventory;
          break;
      }

      foreach (var key in inv.ItemSlots.Keys)
        itemSlots.Add(inv.ItemSlots[key]);

      return itemSlots;
    }

    /// <summary>
    /// Cycles through an inventories itemslots and returns the first empty slot, or the specified slot if empty.
    /// </summary>
    /// <param name="inventorySlot">inventory slot to find</param>
    /// <param name="inventoryName">name of inventory to find empty slot in</param>
    /// <returns></returns>
    public static ItemSlot GetEmptyItemSlot(string inventorySlot, string inventoryName)
    {
      Inventory inv = null;
      ItemSlot itemSlot = null;

      switch (inventoryName)
      {
        case GVar.InventoryParentNames.character:
          foreach (string key in Lists.inventorySlots)
          {
            if (key.Contains(inventorySlot) && characterInventory.ItemSlots[key].item == null)
            {
              itemSlot = characterInventory.ItemSlots[key];
            }
          }
          return itemSlot;
        case GVar.InventoryParentNames.inventory:
          inv = playerInventory;
          break;
        case GVar.InventoryParentNames.shop:
          inv = shopInventory;
          break;
      }

      foreach (int key in inv.ItemSlots.Keys)
      {
        if (inv.ItemSlots[key].item == null)
        {
          itemSlot = inv.ItemSlots[key];
          break;
        }
      }
      return itemSlot;
    }

    public static void UpdateMouseInventory(GameTime gameTime)
    {
      if (mouseInventory.heldItem != null)
      {
        UpdateInventoryItems(gameTime, GetItems(GVar.InventoryParentNames.mouse));
        if (mouseInventory.heldItem.Size.X < Vector.itemNormalSize.X && mouseInventory.heldItem.Size.Y < Vector.itemNormalSize.Y)
          mouseInventory.heldItem.Size = Vector.itemNormalSize;
        mouseInventory.heldItem.Position = new Vector2(MouseManager.mouse.GetMousePosition().X - mouseInventory.heldItem.Size.X, MouseManager.mouse.GetMousePosition().Y - mouseInventory.heldItem.Size.Y);
      }
    }

    public static void DrawMouseInventory(SpriteBatch spriteBatch, GameTime gameTime)
    {
      if (mouseInventory.heldItem != null)
      {
        DrawInventoryItems(spriteBatch, GetItems(GVar.InventoryParentNames.mouse), 0.2f);

        HighlightEquipableSlots(spriteBatch);

        if (mouseInventory.heldItem != null)
          Item.DrawItemInfo(spriteBatch, mouseInventory.heldItem);
      }
    }

    private static void HighlightEquipableSlots(SpriteBatch spriteBatch)
    {
      if (GVar.currentGameState == GVar.GameState.inventory)
      {
        if (mouseInventory.heldItem != null && mouseInventory.heldItem.ItemClass != GVar.ItemClassName.eternalcoin)
        {
          for (int i = 0; i < Lists.inventorySlots.Count; i++)
          {
            if (Lists.inventorySlots[i].Contains(mouseInventory.heldItem.InventorySlot))
            {
              Vector2 pos = characterInventory.ItemSlots[Lists.inventorySlots[i]].position;
              Vector2 size = characterInventory.ItemSlots[Lists.inventorySlots[i]].size;
              spriteBatch.Draw(Textures.Misc.clearPixel, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), null, Color.Green, 0f, Vector2.Zero, SpriteEffects.None, 0.192f);
            }
          }
        }
        else if (mouseInventory.heldItem != null && mouseInventory.heldItem.ItemClass == GVar.ItemClassName.eternalcoin)
        {
          for (int i = 0; i < Lists.playerItems.Count; i++)
          {
            if (Lists.playerItems[i].ItemClass == GVar.ItemClassName.jewellry)
            {
              Jewellry jewl = (Jewellry)Lists.playerItems[i];
              Vector2 pos = jewl.eternalCoinSlot.position;
              Vector2 size = jewl.eternalCoinSlot.size;
              spriteBatch.Draw(Textures.Misc.clearPixel, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), null, Color.Green, 0f, Vector2.Zero, SpriteEffects.None, 0.194f);
            }
          }
        }
      }
    }

    private static void UpdateItemSlots(GameTime gameTime, List<ItemSlot> itemSlots)
    {
      for (int i = 0; i < itemSlots.Count; i++)
      {
        itemSlots[i].Update(gameTime);
        if (MouseManager.mouse.mouseBounds.Intersects(itemSlots[i].bounds))
          GVar.mouseHoveredItem = itemSlots[i];
      }
    }

    /// <summary>
    /// Updates a single Item.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="item"></param>
    public static void UpdateInventoryItems(GameTime gameTime, List<Item> items)
    {
      for (int i = 0; i < items.Count; i++)
      {
        items[i].Update(gameTime);
        items[i].Update((float)gameTime.TotalGameTime.TotalSeconds);
        if (items[i].ItemClass == GVar.ItemClassName.jewellry)
        {
          Jewellry jewl = (Jewellry)items[i];
          jewl.eternalCoinSlot.Update(gameTime);
        }
      }
    }

    /// <summary>
    /// Draws a list of items
    /// </summary>
    /// <param name="spriteBatch"></param>
    /// <param name="item"></param>
    public static void DrawInventoryItems(SpriteBatch spriteBatch, List<Item> items, float layer)
    {
      for (int i = 0; i < items.Count; i++)
      {
        items[i].Draw(spriteBatch, items[i].SpriteID, items[i].Bounds, layer, 0f, Vector2.Zero);

        if (items[i].ItemClass == GVar.ItemClassName.jewellry)
        {
          Jewellry jewl = (Jewellry)items[i];
          if (jewl.eternalCoinSlot.item != null)
            jewl.eternalCoinSlot.item.Draw(spriteBatch, jewl.eternalCoinSlot.item.SpriteID, jewl.eternalCoinSlot.item.Bounds, 0.2f, 0f, Vector2.Zero);
          GVar.DrawBoundingBox(jewl.eternalCoinSlot.bounds, spriteBatch, Textures.Misc.pixel, 1, 0.2f, Color.Green);
        }

        if (MouseManager.mouse.mouseBounds.Intersects(items[i].Bounds))
        {
          spriteBatch.Draw(Textures.Misc.clearPixel, items[i].Bounds, null, Color.Gold, 0f, Vector2.Zero, SpriteEffects.None, 0.191f);
          if (mouseInventory.heldItem == null)
            Item.DrawItemInfo(spriteBatch, items[i]);
        }
      }
    }

    /// <summary>
    /// Draws both player inventories.
    /// </summary>
    /// <param name="spriteBatch"></param>
    /// <param name="gameTime"></param>
    public static void DrawPlayerInventories(SpriteBatch spriteBatch, GameTime gameTime)
    {
      spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Damage: " + GVar.player.Damage.ToString(), new Vector2(879, 572), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
      spriteBatch.DrawString(Fonts.lucidaConsole14Regular, "Armor: " + GVar.player.Armour.ToString(), new Vector2(879, 590), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

      DrawMouseInventory(spriteBatch, gameTime);
      DrawInventoryItems(spriteBatch, GetItems(GVar.InventoryParentNames.inventory), 0.19f);
      DrawInventoryItems(spriteBatch, GetItems(GVar.InventoryParentNames.character), 0.19f);

    }

    /// <summary>
    /// Updates both player inventories.
    /// </summary>
    /// <param name="gameTime"></param>
    public static void UpdateInventories(GameTime gameTime)
    {
      UpdateInventory(gameTime, GVar.InventoryParentNames.inventory);
      UpdateInventory(gameTime, GVar.InventoryParentNames.character);
      UpdateMouseInventory(gameTime);

      if (InputManager.IsKeyPressed(Keys.I))
      {
        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
        GVar.currentGameState = GVar.GameState.game;
        GVar.previousGameState = GVar.GameState.inventory;
      }
    }
  }
}

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
      playerInventory = new Inventory(new Vector2(437, 51), GVar.InventoryParentNames.inventory);
      enemyInventory = new EquipInventory(GVar.InventoryParentNames.enemy);
      shopInventory = new Inventory(new Vector2(862, 51), GVar.InventoryParentNames.shop);
      characterInventory = new EquipInventory(GVar.InventoryParentNames.character);
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
        if (itemSlot.item == null && mouseInventory.heldItem != null && MouseManager.mouse.mouseBounds.Intersects(itemSlot.bounds))
        {
          if (Item.ToInventory(itemSlot, mouseInventory.heldItem))
            mouseInventory.heldItem = null;
        }
        else if (itemSlot.item != null && mouseInventory.heldItem != null && MouseManager.mouse.mouseBounds.Intersects(itemSlot.bounds))
        {
          Item tItem = mouseInventory.heldItem;
          mouseInventory.heldItem = itemSlot.item;
          Item.FromInventory(itemSlot, itemSlot.item);
          Item.ToInventory(itemSlot, tItem);
        }
        else if (itemSlot.item != null && mouseInventory.heldItem == null && MouseManager.mouse.mouseBounds.Intersects(itemSlot.item.Bounds))
        {
          mouseInventory.heldItem = itemSlot.item;
          Item.FromInventory(itemSlot, itemSlot.item);
        }
      }
    }

    /// <summary>
    /// Draws inventory items and item info on mouse over.
    /// </summary>
    /// <param name="spriteBatch"></param>
    /// <param name="inventoryName"></param>
    public static void DrawInventory(SpriteBatch spriteBatch, string inventoryName)
    {
      DrawInventoryItems(spriteBatch, GetItems(inventoryName), 0.19f);
    }

    /// <summary>
    /// Updates inventory items and itemslots.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="inventoryName"></param>
    public static void UpdateInventory(GameTime gameTime, string inventoryName)
    {
      UpdateInventoryItems(gameTime, GetItems(inventoryName));
      UpdateItemSlots(gameTime, GetItemSlots(inventoryName));
    }

    private static List<Item> GetItems(string inventoryName)
    {
      List<Item> items = new List<Item>();
      switch(inventoryName)
      {
        case GVar.InventoryParentNames.character:
          return Lists.characterItems;
        case GVar.InventoryParentNames.inventory:
          return Lists.playerItems;
        case GVar.InventoryParentNames.shop:
          return Lists.shopItems;
        case GVar.InventoryParentNames.mouse:
          items.Add(mouseInventory.heldItem);
          return items;
      }
      return null;
    }

    private static List<ItemSlot> GetItemSlots(string inventoryName)
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
    /// Draws a single Item.
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

      for (int i = 0; i < Lists.inventoryButtons.Count; i++)
      {
        Lists.inventoryButtons[i].Update(gameTime);
        Lists.inventoryButtons[i].Draw(spriteBatch, Lists.inventoryButtons[i].SpriteID, Lists.inventoryButtons[i].Bounds, 0.19f, 0f, Vector2.Zero);
        if (MouseManager.mouse.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds))
          GVar.DrawBoundingBox(Lists.inventoryButtons[i].Bounds, spriteBatch, Textures.Misc.pixel, 1, 0.19f, Color.Green);
      }

      DrawMouseInventory(spriteBatch, gameTime);
      DrawInventory(spriteBatch, GVar.InventoryParentNames.inventory);
      DrawInventory(spriteBatch, GVar.InventoryParentNames.character);

    }

    /// <summary>
    /// Updates both player inventories.
    /// </summary>
    /// <param name="gameTime"></param>
    public static void UpdateInventories(GameTime gameTime)
    {
      for (int i = 0; i < Lists.inventoryButtons.Count; i++)
      {
        Updates.UpdateInventoryButtons(Lists.inventoryButtons[i], gameTime);
        //if (MouseManager.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds) && InputManager.IsLMPressed())
        //{
        //    if (Lists.inventoryButtons[i].Name == "CloseInventory")
        //    {
        //        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
        //        Lists.inventoryButtons.Clear();
        //        GVar.currentGameState = GVar.GameState.game;
        //        GVar.previousGameState = GVar.GameState.inventory;
        //    }
        //}
      }
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

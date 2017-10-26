using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;

namespace Eternal_Coin
{
  public class Shop
  {
    /// <summary>
    /// Loads a shops invemtory from the current locations xml file.
    /// </summary>
    /// <param name="shopDoc">the locations xml file that contains a shop tag.</param>
    public static void LoadShopInventory(XmlDocument shopDoc)
    {
      XmlNodeList itemList = shopDoc.SelectNodes("/location/shop/inventory/item");//select item tags in shops/inventory tags.

      //cycle through the items.
      for (int i = 0; i < itemList.Count; i++)
      {
        Item shopItem = ItemBuilder.BuildItem(Dictionaries.items[itemList[i][GVar.XmlTags.ItemTags.itemname].InnerText]);//create a item from the list.
        int invSlot = 99;//set to 99 to check for no set inventory slot.
        try
        {
          invSlot = Convert.ToInt32(itemList[i][GVar.XmlTags.ItemTags.inventoryslot].InnerText);//try set inventory slot from xml document.
        }
        catch (Exception e)
        {
          GVar.LogDebugInfo(e.ToString(), 2);//log the error in case there is no set inventory slot.
        }

        AddItemToShopInventory(shopItem, invSlot);
      }
    }

    /// <summary>
    /// If the item has a set inventory slot number(1-40) it will be added accordingly.
    /// If the item does not have a set inventory slot number, an empty slot will be found.
    /// </summary>
    /// <param name="item">the item being added.</param>
    /// <param name="invSlot">the slot for the item to be placed in.</param>
    private static void AddItemToShopInventory(Item item, int invSlot)
    {
      if (invSlot <= 40)
      {
        InventoryManager.shopInventory.ItemSlots[invSlot].item = ItemBuilder.BuildItem(item);
        Lists.shopItems.Add(InventoryManager.shopInventory.ItemSlots[invSlot].item);
      }
      else
      {
        for (int j = 0; j < 40; j++)
        {
          if (InventoryManager.shopInventory.ItemSlots[j].item == null)
          {
            InventoryManager.shopInventory.ItemSlots[j].item = item;
            Lists.shopItems.Add(InventoryManager.shopInventory.ItemSlots[j].item);
            break;
          }
        }
      }
    }

    /// <summary>
    /// Updates player inventory, mouse inventory and shop inventory.
    /// </summary>
    /// <param name="gameTime">GameTime for stuff.</param>
    /// <param name="playerInventory">Players Inventory.</param>
    /// <param name="mouseInventory">Mouse Inventory.</param>
    /// <param name="shopInventory">Shop Inventory.</param>
    public static void UpdateShopInventories(GameTime gameTime, Inventory playerInventory, MouseInventory mouseInventory, Inventory shopInventory)
    {
      InventoryManager.UpdateInventory(gameTime, GVar.InventoryParentNames.shop);
      InventoryManager.UpdateInventory(gameTime, GVar.InventoryParentNames.inventory);
      InventoryManager.UpdateMouseInventory(gameTime);

      //cycle through InventoryButtons.
      for (int i = 0; i < Lists.inventoryButtons.Count; i++)
      {
        Updates.UpdateInventoryButtons(Lists.inventoryButtons[i], gameTime);//update buttons.
                                                                            //if mouse bounds intersects button bounds and left mouse is pressed.
        if (MouseManager.mouse.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds) && InputManager.IsLMPressed())
        {
          //if button name is CloseInventory.
          if (Lists.inventoryButtons[i].Name == "CloseInventory")
          {
            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton]);
            Lists.inventoryButtons.Clear();//clear InventoryButtons.
            SaveShopInventory(GVar.curLocNode, GVar.player.CurrentLocation);//save shops inventory.

            //cycle through inventory itemslots.
            for (int j = 0; j < 40; j++)
            {
              if (shopInventory.ItemSlots[j].item != null)//if item is not null.
                shopInventory.ItemSlots[j].item = null;//delete the item.
            }

            shopInventory = new Inventory(new Vector2(862, 51), GVar.InventoryParentNames.shop);//create new shop inventory(deleting current one)
            Lists.shopItems.Clear();//clear shops items.
            GVar.currentGameState = GVar.GameState.game;//set current GameState to game.
            GVar.previousGameState = GVar.GameState.inventory;
          }
        }
      }
    }

    /// <summary>
    /// Draws players inventory, mouse inventory and shop inventory.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch to draw sprites.</param>
    /// <param name="gameTime">GameTime for updating sprites.</param>
    /// <param name="playerInventory">players inventory.</param>
    /// <param name="mouseInventory">mouse inventory.</param>
    /// <param name="shopInventory">shops inventory.</param>
    public static void DrawShopInventories(SpriteBatch spriteBatch, GameTime gameTime)
    {
      InventoryManager.DrawInventory(spriteBatch, GVar.InventoryParentNames.shop);
      InventoryManager.DrawInventory(spriteBatch, GVar.InventoryParentNames.inventory);
      InventoryManager.DrawMouseInventory(spriteBatch, gameTime);

      spriteBatch.DrawString(Fonts.lucidaConsole14Regular, GVar.silverMoney.ToString(), new Vector2(50, 670), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);

      //cycle through and update and draw InventoryButtons.
      for (int i = 0; i < Lists.inventoryButtons.Count; i++)
      {
        Lists.inventoryButtons[i].Update(gameTime);
        Lists.inventoryButtons[i].Draw(spriteBatch, Lists.inventoryButtons[i].SpriteID, Lists.inventoryButtons[i].Bounds, 0.19f, 0f, Vector2.Zero);
        if (MouseManager.mouse.mouseBounds.Intersects(Lists.inventoryButtons[i].Bounds))
        {
          GVar.DrawBoundingBox(Lists.inventoryButtons[i].Bounds, spriteBatch, Textures.Misc.pixel, 1, 0.19f, Color.Green);
        }
      }
    }

    /// <summary>
    /// saves shops inventory to its locations xml file.
    /// </summary>
    /// <param name="shopDoc"></param>
    /// <param name="locationNode"></param>
    public static void SaveShopInventory(XmlDocument shopDoc, LocationNode locationNode)
    {
      XmlNode item = shopDoc.SelectSingleNode("/location/shop/inventory");

      item.RemoveAll();

      //cycle through shops inventory item slots, if item slots item is not null, create item xml element and append it to item xml node.
      for (int i = 0; i < 40; i++)
      {
        if (InventoryManager.shopInventory.ItemSlots[i].item != null)
        {
          item.AppendChild(SaveXml.CreateItemXmlElement(InventoryManager.shopInventory.ItemSlots[i].item, shopDoc));
        }
      }
      string fileDir = GVar.gameFilesLocation + GVar.player.Name + "/" + locationNode.LocatoinFilePath;
      shopDoc.Save(fileDir);
    }
  }
}

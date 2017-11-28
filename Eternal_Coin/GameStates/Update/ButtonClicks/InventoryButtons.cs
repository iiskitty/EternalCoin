using Microsoft.Xna.Framework;
using System;

namespace Eternal_Coin
{
  public class InventoryButtons
  {
    public static void CloseInventory()
    {
      if (GVar.currentGameState == GVar.GameState.shop)
      {
        Shop.SaveShopInventory(GVar.curLocNode, GVar.player.CurrentLocation);//save shops inventory.

        //cycle through inventory itemslots.
        for (int j = 0; j < 40; j++)
          if (InventoryManager.shopInventory.ItemSlots[j].item != null)//if item is not null.
            InventoryManager.shopInventory.ItemSlots[j].item = null;//delete the item.

        InventoryManager.shopInventory = new Inventory(new Vector2(862, 51), GVar.InventoryParentNames.shop);//create new shop inventory(deleting current one)
        Lists.shopItems.Clear();//clear shops items.
      }

      Lists.inventoryButtons.Clear();
      GVar.previousGameState = GVar.currentGameState;
      GVar.currentGameState = GVar.GameState.game;
    }
  }
}

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eternal_Coin
{
  public class EquipInventory
  {
    Dictionary<string, ItemSlot> itemSlots;

    public EquipInventory(string parentInventory)
    {
      itemSlots = new Dictionary<string, ItemSlot>();
      for (int i = 0; i < Lists.uiElements.Count; i++)
      {
        if (Lists.uiElements[i].SpriteID == Textures.UI.battleUI)
        {
          itemSlots.Add(GVar.InventorySlot.helmet, new ItemSlot(new Vector2(1032, 55), new Vector2(Lists.uiElements[i].Position.X + 202, Lists.uiElements[i].Position.Y + 191), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.helmet, parentInventory));
          itemSlots.Add(GVar.InventorySlot.chestplate, new ItemSlot(new Vector2(1032, 245), new Vector2(Lists.uiElements[i].Position.X + 202, Lists.uiElements[i].Position.Y + 261), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.chestplate, parentInventory));
          itemSlots.Add(GVar.InventorySlot.leggings, new ItemSlot(new Vector2(1032, 340), new Vector2(Lists.uiElements[i].Position.X + 202, Lists.uiElements[i].Position.Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leggings, parentInventory));
          itemSlots.Add(GVar.InventorySlot.leftBoot, new ItemSlot(new Vector2(969, 435), new Vector2(Lists.uiElements[i].Position.X + 179, Lists.uiElements[i].Position.Y + 331), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftBoot, parentInventory));
          itemSlots.Add(GVar.InventorySlot.rightBoot, new ItemSlot(new Vector2(1095, 435), new Vector2(Lists.uiElements[i].Position.X + 225, Lists.uiElements[i].Position.Y + 331), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightBoot, parentInventory));
          itemSlots.Add(GVar.InventorySlot.leftGauntlet, new ItemSlot(new Vector2(909, 340), new Vector2(Lists.uiElements[i].Position.X + 156, Lists.uiElements[i].Position.Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftGauntlet, parentInventory));
          itemSlots.Add(GVar.InventorySlot.rightGauntlet, new ItemSlot(new Vector2(1155, 340), new Vector2(Lists.uiElements[i].Position.X + 246, Lists.uiElements[i].Position.Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightGauntlet, parentInventory));
          itemSlots.Add(GVar.InventorySlot.leftHandWeapon, new ItemSlot(new Vector2(927, 140), new Vector2(Lists.uiElements[i].Position.X + 163, Lists.uiElements[i].Position.Y + 222), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftHandWeapon, parentInventory));
          itemSlots.Add(GVar.InventorySlot.rightHandWeapon, new ItemSlot(new Vector2(1137, 140), new Vector2(Lists.uiElements[i].Position.X + 240, Lists.uiElements[i].Position.Y + 222), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightHandWeapon, parentInventory));
          itemSlots.Add(GVar.InventorySlot.RingOne, new ItemSlot(new Vector2(865, 343), new Vector2(Lists.uiElements[i].Position.X + 140, Lists.uiElements[i].Position.Y + 297), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingOne, parentInventory));
          itemSlots.Add(GVar.InventorySlot.RingTwo, new ItemSlot(new Vector2(871, 384), new Vector2(Lists.uiElements[i].Position.X + 142, Lists.uiElements[i].Position.Y + 312), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingTwo, parentInventory));
          itemSlots.Add(GVar.InventorySlot.RingThree, new ItemSlot(new Vector2(887, 421), new Vector2(Lists.uiElements[i].Position.X + 148, Lists.uiElements[i].Position.Y + 326), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingThree, parentInventory));
          itemSlots.Add(GVar.InventorySlot.RingFour, new ItemSlot(new Vector2(928, 427), new Vector2(Lists.uiElements[i].Position.X + 163, Lists.uiElements[i].Position.Y + 328), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingFour, parentInventory));
          itemSlots.Add(GVar.InventorySlot.RingFive, new ItemSlot(new Vector2(1244, 343), new Vector2(Lists.uiElements[i].Position.X + 279, Lists.uiElements[i].Position.Y + 297), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingFive, parentInventory));
          itemSlots.Add(GVar.InventorySlot.RingSix, new ItemSlot(new Vector2(1238, 384), new Vector2(Lists.uiElements[i].Position.X + 277, Lists.uiElements[i].Position.Y + 312), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingSix, parentInventory));
          itemSlots.Add(GVar.InventorySlot.RingSeven, new ItemSlot(new Vector2(1222, 421), new Vector2(Lists.uiElements[i].Position.X + 271, Lists.uiElements[i].Position.Y + 326), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingSeven, parentInventory));
          itemSlots.Add(GVar.InventorySlot.RingEight, new ItemSlot(new Vector2(1181, 427), new Vector2(Lists.uiElements[i].Position.X + 256, Lists.uiElements[i].Position.Y + 328), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingEight, parentInventory));
        }
      }
    }

    //public void UpdateInventoryBounds(GameTime gameTime)
    //{
    //  for (int i = 0; i < Lists.inventorySlots.Count; i++)
    //  {
    //    itemSlots[Lists.inventorySlots[i]].Update(gameTime);
    //  }
    //}

    public Dictionary<string, ItemSlot> ItemSlots { get { return itemSlots; } set { itemSlots = value; } }
  }
}

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eternal_Coin
{
  public class EquipInventory
  {
    Dictionary<string, ItemSlot> itemSlots;

    public EquipInventory(string parentInventory)
    {
      itemSlots = new Dictionary<string, ItemSlot>
      {
        { GVar.InventorySlot.helmet, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 202, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 191), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.helmet, parentInventory) },
        { GVar.InventorySlot.chestplate, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 202, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 261), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.chestplate, parentInventory) },
        { GVar.InventorySlot.leggings, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 202, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leggings, parentInventory) },
        { GVar.InventorySlot.leftBoot, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 179, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 331), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftBoot, parentInventory) },
        { GVar.InventorySlot.rightBoot, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 225, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 331), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightBoot, parentInventory) },
        { GVar.InventorySlot.leftGauntlet, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 156, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftGauntlet, parentInventory) },
        { GVar.InventorySlot.rightGauntlet, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 246, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightGauntlet, parentInventory) },
        { GVar.InventorySlot.leftHandWeapon, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 163, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 222), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftHandWeapon, parentInventory) },
        { GVar.InventorySlot.rightHandWeapon, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 240, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 222), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightHandWeapon, parentInventory) },
        { GVar.InventorySlot.RingOne, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 140, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 297), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingOne, parentInventory) },
        { GVar.InventorySlot.RingTwo, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 142, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 312), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingTwo, parentInventory) },
        { GVar.InventorySlot.RingThree, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 148, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 326), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingThree, parentInventory) },
        { GVar.InventorySlot.RingFour, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 163, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 328), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingFour, parentInventory) },
        { GVar.InventorySlot.RingFive, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 279, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 297), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingFive, parentInventory) },
        { GVar.InventorySlot.RingSix, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 277, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 312), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingSix, parentInventory) },
        { GVar.InventorySlot.RingSeven, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 271, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 326), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingSeven, parentInventory) },
        { GVar.InventorySlot.RingEight, new ItemSlot(Vector2.Zero, new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 256, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 328), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingEight, parentInventory) }
      };

      ResetItemSlotPositions();
    }

    public void ResetItemSlotPositions()
    {
      itemSlots[GVar.InventorySlot.helmet].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1030, 55)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 202, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 191));
      itemSlots[GVar.InventorySlot.chestplate].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1030, 245)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 202, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 261));
      itemSlots[GVar.InventorySlot.leggings].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1030, 340)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 202, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 296));
      itemSlots[GVar.InventorySlot.leftBoot].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(969, 435)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 179, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 331));
      itemSlots[GVar.InventorySlot.rightBoot].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1091, 435)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 225, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 331));
      itemSlots[GVar.InventorySlot.leftGauntlet].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(915, 340)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 156, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 296));
      itemSlots[GVar.InventorySlot.rightGauntlet].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1145, 340)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 246, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 296));
      itemSlots[GVar.InventorySlot.leftHandWeapon].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(925, 140)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 163, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 222));
      itemSlots[GVar.InventorySlot.rightHandWeapon].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1135, 140)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 240, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 222));
      itemSlots[GVar.InventorySlot.RingOne].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(867, 343)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 140, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 297));
      itemSlots[GVar.InventorySlot.RingTwo].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(871, 384)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 142, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 312));
      itemSlots[GVar.InventorySlot.RingThree].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(887, 425)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 148, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 326));
      itemSlots[GVar.InventorySlot.RingFour].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(928, 434)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 163, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 328));
      itemSlots[GVar.InventorySlot.RingFive].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1238, 343)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 279, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 297));
      itemSlots[GVar.InventorySlot.RingSix].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1234, 384)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 277, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 312));
      itemSlots[GVar.InventorySlot.RingSeven].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1218, 425)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 271, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 326));
      itemSlots[GVar.InventorySlot.RingEight].ResetPositions(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1177, 434)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 256, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 328));
    }

    public Dictionary<string, ItemSlot> ItemSlots { get { return itemSlots; } set { itemSlots = value; } }
  }
}

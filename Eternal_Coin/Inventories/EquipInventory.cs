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
        { GVar.InventorySlot.helmet, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1032, 55)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 202, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 191), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.helmet, parentInventory) },
        { GVar.InventorySlot.chestplate, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1032, 245)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 202, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 261), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.chestplate, parentInventory) },
        { GVar.InventorySlot.leggings, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1032, 340)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 202, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leggings, parentInventory) },
        { GVar.InventorySlot.leftBoot, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(969, 435)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 179, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 331), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftBoot, parentInventory) },
        { GVar.InventorySlot.rightBoot, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1095, 435)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 225, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 331), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightBoot, parentInventory) },
        { GVar.InventorySlot.leftGauntlet, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(909, 340)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 156, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftGauntlet, parentInventory) },
        { GVar.InventorySlot.rightGauntlet, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1155, 340)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 246, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 296), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightGauntlet, parentInventory) },
        { GVar.InventorySlot.leftHandWeapon, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(927, 140)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 163, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 222), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.leftHandWeapon, parentInventory) },
        { GVar.InventorySlot.rightHandWeapon, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1137, 140)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 240, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 222), new Vector2(71, 71), new Vector2(26, 26), GVar.InventorySlot.rightHandWeapon, parentInventory) },
        { GVar.InventorySlot.RingOne, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(865, 343)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 140, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 297), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingOne, parentInventory) },
        { GVar.InventorySlot.RingTwo, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(871, 384)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 142, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 312), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingTwo, parentInventory) },
        { GVar.InventorySlot.RingThree, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(887, 421)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 148, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 326), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingThree, parentInventory) },
        { GVar.InventorySlot.RingFour, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(928, 427)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 163, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 328), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingFour, parentInventory) },
        { GVar.InventorySlot.RingFive, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1244, 343)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 279, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 297), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingFive, parentInventory) },
        { GVar.InventorySlot.RingSix, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1238, 384)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 277, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 312), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingSix, parentInventory) },
        { GVar.InventorySlot.RingSeven, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1222, 421)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 271, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 326), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingSeven, parentInventory) },
        { GVar.InventorySlot.RingEight, new ItemSlot(ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1181, 427)), new Vector2(UIElement.GetUIPosition(Textures.UI.battleUI).X + 256, UIElement.GetUIPosition(Textures.UI.battleUI).Y + 328), new Vector2(26, 26), new Vector2(9, 9), GVar.InventorySlot.RingEight, parentInventory) }
      };
    }

    public void ResetItemSlotPositions()
    {
      itemSlots[GVar.InventorySlot.helmet].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1032, 55));
      itemSlots[GVar.InventorySlot.chestplate].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1032, 245));
      itemSlots[GVar.InventorySlot.leggings].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1032, 340));
      itemSlots[GVar.InventorySlot.leftBoot].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(969, 435));
      itemSlots[GVar.InventorySlot.rightBoot].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1095, 435));
      itemSlots[GVar.InventorySlot.leftGauntlet].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(909, 340));
      itemSlots[GVar.InventorySlot.rightGauntlet].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1155, 340));
      itemSlots[GVar.InventorySlot.leftHandWeapon].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(927, 140));
      itemSlots[GVar.InventorySlot.rightHandWeapon].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1137, 140));
      itemSlots[GVar.InventorySlot.RingOne].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(865, 343));
      itemSlots[GVar.InventorySlot.RingTwo].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(871, 384));
      itemSlots[GVar.InventorySlot.RingThree].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(887, 421));
      itemSlots[GVar.InventorySlot.RingFour].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(928, 427));
      itemSlots[GVar.InventorySlot.RingFive].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1244, 343));
      itemSlots[GVar.InventorySlot.RingSix].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1238, 384));
      itemSlots[GVar.InventorySlot.RingSeven].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1222, 421));
      itemSlots[GVar.InventorySlot.RingEight].position = ItemSlot.SetItemSlotPosition(UIElement.GetUIPosition(Textures.UI.inventoryUI), new Vector2(1181, 427));
    }

    public Dictionary<string, ItemSlot> ItemSlots { get { return itemSlots; } set { itemSlots = value; } }
  }
}

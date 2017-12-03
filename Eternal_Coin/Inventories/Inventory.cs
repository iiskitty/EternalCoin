using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eternal_Coin
{
  public class Inventory
  {
    Dictionary<int, ItemSlot> itemSlots;
    bool fullInventory;
    Vector2 position;

    public Inventory(Vector2 position, string parentInventory)
    {
      this.position = position;
      itemSlots = new Dictionary<int, ItemSlot>();
      int itemCount = 0;
      Vector2 size = new Vector2(71, 71);

      for (int i = 0; i < 40; i++)
      {
        itemCount++;

        itemSlots.Add(i, new ItemSlot(position, Vector2.Zero, size, Vector2.Zero, i.ToString(), parentInventory));

        if (itemCount == 5)
        {
          itemCount = 0;
          position.X = this.position.X;
          position.Y += 84;
        }
        else
        {
          position.X += 84;
        }
      }

      fullInventory = false;
    }

    public void ResetItemSlotPositions(Vector2 position)
    {
      int itemCount = 0;
      float oX = position.X;
      foreach (int key in itemSlots.Keys)
      {
        itemCount++;
        itemSlots[key].position = position;

        if (itemCount == 5)
        {
          itemCount = 0;
          position.X = oX;
          position.Y += 84;
        }
        else
          position.X += 84;
      }
    }

    public Dictionary<int, ItemSlot> ItemSlots { get { return itemSlots; } set { itemSlots = value; } }
    public bool FullInventory { get { return fullInventory; } set { fullInventory = value; } }
    public Vector2 Position { get { return position; } set { position = value; } }
  }
}

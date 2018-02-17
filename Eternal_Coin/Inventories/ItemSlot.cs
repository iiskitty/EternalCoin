using Microsoft.Xna.Framework;

namespace Eternal_Coin
{
  public class ItemSlot
  {
    public Rectangle bounds;
    public Vector2 position;
    public Vector2 size;
    public Vector2 miniPosition;
    public Vector2 miniSize;
    public Item item;
    public string inventorySlot;
    public string parentInventory;

    public ItemSlot(Vector2 position, Vector2 miniPosition, Vector2 size, Vector2 miniSize, string inventorySlot, string parentInventory)
    {
      this.position = position;
      this.miniPosition = miniPosition;
      this.size = size;
      this.miniSize = miniSize;
      this.inventorySlot = inventorySlot;
      this.parentInventory = parentInventory;
    }

    public static Vector2 SetItemSlotPosition(Vector2 position, Vector2 padding)
    {
      return new Vector2(position.X + padding.X, position.Y + padding.Y);
    }

    public void ResetPositions(Vector2 position, Vector2 miniPosition)
    {
      this.position = position;
      this.miniPosition = miniPosition;
    }

    public void Update(GameTime gameTime)
    {
      if (item != null)
      {
        item.Position = position;
        item.Size = size;
        item.Bounds = bounds;
      }
      bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
    }

  }
}

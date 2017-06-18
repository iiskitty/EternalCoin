using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eternal_Coin
{
    public class Inventory
    {
        Dictionary<int, ItemSlot> itemSlots;
        bool fullInventory;
        Vector2 position;

        public Inventory(Vector2 position)
        {
            this.position = position;
            itemSlots = new Dictionary<int, ItemSlot>();
            int itemCount = 0;
            Vector2 size = new Vector2(71, 71);

            for (int i = 0; i < 40; i++)
            {
                itemCount++;

                itemSlots.Add(i, new ItemSlot(position, Vector2.Zero, size, Vector2.Zero, i.ToString()));

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

        public void UpdateInventoryBounds(GameTime gameTime)
        {
            for (int i = 0; i < 40; i++)
            {
                itemSlots[i].Update(gameTime);
                if (itemSlots[i].item != null)
                {
                    itemSlots[i].item.PlayerInventorySlot = i;
                }
            }
        }

        public Dictionary<int, ItemSlot> ItemSlots { get { return itemSlots; } set { itemSlots = value; } }
        public bool FullInventory { get { return fullInventory; } set { fullInventory = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
    }
}

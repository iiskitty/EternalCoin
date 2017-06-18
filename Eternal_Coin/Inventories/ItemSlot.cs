using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ItemSlot(Vector2 position, Vector2 miniPosition, Vector2 size, Vector2 miniSize, string inventorySlot)
        {
            this.position = position;
            this.miniPosition = miniPosition;
            this.size = size;
            this.miniSize = miniSize;
            this.inventorySlot = inventorySlot;
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

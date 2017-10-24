using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
    public class Jewellry : Item
    {
        public ItemSlot eternalCoinSlot;

        Vector2 jPos;
        Vector2 jSize;

        public Jewellry(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string itemClass, string itemName, int cost, string inventorySlot, Material material, ItemType type)
            : base(spriteID, position, size, colour, itemClass, itemName, cost, inventorySlot, material, type)
        {
            this.spriteID = SpriteID;
            eternalCoinSlot = new ItemSlot(new Vector2(position.X - 10, position.Y - 2), Vector2.Zero, new Vector2(15, 15), Vector2.Zero, "EternalCoin", itemName);
        }

        public Jewellry(Item item)
            : base(item)
        {
            spriteID = item.SpriteID;
        }

        public override void Update(float gameTime)
        {
            jPos = new Vector2(position.X + size.X / 2.2f, position.Y + 2);
            jSize = new Vector2(size.X / 3, size.Y / 3);

            eternalCoinSlot.position = jPos;

            eternalCoinSlot.bounds = new Rectangle((int)eternalCoinSlot.position.X, (int)eternalCoinSlot.position.Y, (int)eternalCoinSlot.size.X, (int)eternalCoinSlot.size.Y);
            eternalCoinSlot.size = jSize;

            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public override void AnimationDone(string animation)
        {

        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
    public class EternalCoin : Item
    {
        public EternalCoin(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string itemClass, string itemName, int cost, string inventorySlot, Material material, ItemType type)
            : base(spriteID, position, size, colour, itemClass, itemName, cost, inventorySlot, material, type)
        {

        }

        public EternalCoin(Item item)
            : base(item)
        {

        }

        public override void Update(float gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public override void AnimationDone(string animation)
        {

        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
  public class Armor : Item
  {
    int armorValue;

    public Armor() { }

    public Armor(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string itemClass, int cost, int armor, string armorName, int playerInventorySlot, string inventorySlot, Material material, ItemType type)
        : base(spriteID, position, size, colour, itemClass, cost, armorName, playerInventorySlot, inventorySlot, material, type)
    {
      armorValue = armor;
    }

    public override void Update(float gameTime) => bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

    public override void AnimationDone(string animation) { }

    public int ArmorValue { get { return armorValue; } set { armorValue = value; } }
  }
}

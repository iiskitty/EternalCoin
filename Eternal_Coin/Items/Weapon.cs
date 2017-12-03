using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
  public class Weapon : Item
  {
    int damage;

    public Weapon() { }

    public Weapon(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string itemClass, int cost, int damage, string weaponName, int playerInventorySlot, string inventorySlot, Material material, ItemType type)
        : base(spriteID, position, size, colour, itemClass, cost, weaponName, playerInventorySlot, inventorySlot, material, type)
    {
      this.damage = damage;
    }

    public override void Update(float gameTime) => bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

    public override void AnimationDone(string animation) { }

    public int Damage { get { return damage; } set { damage = value; } }
  }
}

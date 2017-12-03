using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
  public class Player : Entity
  {
    public Player(Texture2D spriteID, Vector2 position, Vector2 size, string name, string state, Vector2 velocity, Color colour, int health, int armor, int damage)
        : base(spriteID, position, size, name, state, velocity, colour, health, armor, damage)
    {
      AddAnimation(1, 0, 0, "Player", spriteID.Width, spriteID.Height, Vector2.Zero);
      PlayAnimation("Player");
    }

    public override void Update(float gameTime) => bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

    public void ResetLocation(Vector2 pos)
    {
      position = new Vector2(pos.X / 2 - size.X / 2, pos.Y / 2 - size.Y / 2);
      GVar.worldMap.SetMapPosition(GVar.player.CurrentLocation);
      Lists.locationButtons.Clear();
      Button.CreateLocationButtons(GVar.player.CurrentLocation);
    }

    public override void HandleMovement(Vector2 pos, float gameTime) { }

    public override void AnimationDone(string animation) { }

    public void TakeItemStats(Item item)
    {
      if (item.ItemClass == GVar.ItemClassName.weapon)
      {
        Weapon weapon = (Weapon)item;
        this.damage -= weapon.Damage;
      }
      else if (item.ItemClass == GVar.ItemClassName.armor)
      {
        Armor armor = (Armor)item;
        this.armor -= armor.ArmorValue;
      }
    }

    public void AddItemStats(Item item)
    {
      if (item.ItemClass == GVar.ItemClassName.weapon)
      {
        Weapon weapon = (Weapon)item;
        this.damage += weapon.Damage;
      }
      else if (item.ItemClass == GVar.ItemClassName.armor)
      {
        Armor armor = (Armor)item;
        this.armor += armor.ArmorValue;
      }
    }
  }
}
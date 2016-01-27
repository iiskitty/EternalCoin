using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using System.Text;

namespace Eternal_Coin
{
    public class Projectile : Object
    {
        Vector2 direction;
        int damage;

        public Projectile(Texture2D spriteID, Vector2 position, Vector2 direction, Vector2 size, Color colour, string name, string state, int damage, float worth) 
            : base(spriteID, position, size, colour, name, state, worth)
        {
            FPS = 10;
            AddAnimation(8, 0, 0, "def", (int)size.X, (int)size.Y, Vector2.Zero);
            PlayAnimation("def");
            this.direction = direction;
            this.damage = damage;
        }

        public override void Update(float gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            HandleMovement(position, gameTime);
        }

        public override void HandleMovement(Vector2 pos, float gameTime)
        {
            position += direction;
        }

        public override void AnimationDone(string animation)
        {
            
        }

        public static void CreateProjectile()
        {

        }

        public Vector2 Direction { get { return direction; } set { direction = value; } }
        public int Damage { get { return damage; } set { damage = value; } }
    }

    public class ProjectileBuilder
    {
        public static Projectile BuildProjectile(Projectile proj, Vector2 position, Vector2 direction, string state)
        {
            proj = new Projectile(proj.SpriteID, position, direction, proj.Size, Color.White, proj.Name, state, proj.Damage, 0f);
            return proj;
        }
    }
}

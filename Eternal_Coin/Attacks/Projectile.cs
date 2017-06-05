using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
    public class Projectile : Object
    {
        int damage;

        /// <summary>
        /// Create a projectile
        /// </summary>
        /// <param name="spriteID">sprite of projectile</param>
        /// <param name="position">position of projectile</param>
        /// <param name="direction">direction of projectile</param>
        /// <param name="size">size of projectile</param>
        /// <param name="colour">colour of projectile</param>
        /// <param name="name">name of projectile</param>
        /// <param name="state">state of projectile</param>
        /// <param name="damage">damage of projectile</param>
        /// <param name="worth">projectiles don't need worth</param>
        public Projectile(Texture2D spriteID, Vector2 position, Vector2 direction, Vector2 size, Color colour, string name, string state, int damage, float worth) 
            : base(spriteID, position, size, colour, name, state, worth)
        {
            FPS = 10;
            AddAnimation(8, 0, 0, "def", (int)size.X, (int)size.Y, Vector2.Zero);
            PlayAnimation("def");
            Direction = direction;
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

        /// <summary>
        /// why have I done this
        /// </summary>
        public static void CreateProjectile()
        {

        }

        /// <summary>
        /// Damage of projectile
        /// </summary>
        public int Damage { get { return damage; } set { damage = value; } }
    }

    public class ProjectileBuilder
    {
        /// <summary>
        /// Builds and returns a projectile
        /// </summary>
        /// <param name="proj">pass in a pre-built projectile from dictionary</param>
        /// <param name="position">position of projectile</param>
        /// <param name="direction">direction of projectile</param>
        /// <param name="state">state of projectile</param>
        /// <returns></returns>
        public static Projectile BuildProjectile(Projectile proj, Vector2 position, Vector2 direction, string state)
        {
            proj = new Projectile(proj.SpriteID, position, direction, proj.Size, Color.White, proj.Name, state, proj.Damage, 0f);
            return proj;
        }
    }
}

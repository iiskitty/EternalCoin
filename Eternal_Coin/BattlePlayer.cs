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
    public class BattlePlayer : Entity
    {
        Attack atk;

        public BattlePlayer(Attack atk, Vector2 position, Vector2 size, string name, string state, Vector2 velocity, Color colour, float health, float armor, float damage) 
            : base(atk.Anim, position, size, name, state, velocity, colour, health, armor, damage)
        {
            this.atk = atk;
        }

        public override void Update(float gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            HandleMovement(position, gameTime);
        }

        public override void HandleMovement(Vector2 pos, float gameTime)
        {
            if (CurrentAnimation == GVar.AttackAnimStates.buildUp && Battle.currentAttackType == GVar.AttackType.physical)
            {
                position.X += 5;
            }
            else if (CurrentAnimation == GVar.AttackAnimStates.retreat && Battle.currentAttackType == GVar.AttackType.physical)
            {
                position.X -= 5;
            }
            else if (CurrentAnimation == GVar.AttackAnimStates.idle && Battle.battleEnemy.Bounds.Intersects(Battle.enemyAttackRec) && Battle.battleEnemy.CurrentAnimation == GVar.AttackAnimStates.attack)
            {
                PlayAnimation(GVar.AttackAnimStates.attacked);
            }
        }

        public override void AnimationDone(string animation)
        {
            if (CurrentAnimation == GVar.AttackAnimStates.attacked)
            {
                PlayAnimation(GVar.AttackAnimStates.idle);
                Battle.currentAttackType = "";
            }

            if (CurrentAnimation == GVar.AttackAnimStates.attack)
            {
                Random rand = new Random();
                float damage = Battle.battlePlayer.Damage;
                float armor = Battle.battleEnemy.Armor;

                damage -= damage / 100 * armor;

                if (damage < 0)
                    damage = 1;

                Battle.battleEnemy.Health -= damage;

                PlayAnimation(GVar.AttackAnimStates.retreat);
            }
            
        }

        public void SetAttack(Attack atk)
        {
            this.atk = atk;
            this.spriteID = atk.Anim;
        }

        public Attack ATK { get { return atk; } set { atk = value; } }
    }
}

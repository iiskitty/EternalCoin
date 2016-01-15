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
    public class BattleEnemy : Enemy
    {
        Attack atk;

        public BattleEnemy(Attack atk, Vector2 position, Vector2 size, string name, string state, Vector2 velocity, Color colour, float health, float armor, float damage, int score) 
            : base(atk.Anim, position, size, name, state, velocity, colour, health, armor, damage, score)
        {
            this.atk = atk;
        }

        public static void LoadInventory(XmlNode invNode)
        {
            
        }

        public override void Update(float gameTime)
        {
            HandleMovement(position, gameTime);
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public override void HandleMovement(Vector2 pos, float gameTime)
        {
            if (CurrentAnimation == GVar.AttackAnimStates.buildUp)
                position.X -= 5;
            else if (CurrentAnimation == GVar.AttackAnimStates.retreat)
                position.X += 5;
            else if (CurrentAnimation == GVar.AttackAnimStates.idle && Battle.battlePlayer.Bounds.Intersects(Battle.playerAttackRec) && Battle.battlePlayer.CurrentAnimation == GVar.AttackAnimStates.attack)
            {
                PlayAnimation(GVar.AttackAnimStates.attacked);
            }

            if (health < 0)
            {
                health = 0;
                Battle.battleWon = true;
                foreach (UIElement ui in Lists.uiElements)
                {
                    if (ui.SpriteID == Textures.endBattleUI && !ui.Draw)
                    {
                        ui.Draw = true;
                        Button conbut = new Button(Textures.continueButton, new Vector2(ui.Position.X + 108, ui.Position.Y + 154), new Vector2(Textures.continueButton.Width / 2, Textures.continueButton.Height), Color.White, "Continue", "Alive", 0f);
                        Lists.battleSceneButtons.Add(conbut);
                    }
                }
            }
        }

        public override void AnimationDone(string animation)
        {
            if (CurrentAnimation == GVar.AttackAnimStates.attacked)
                PlayAnimation(GVar.AttackAnimStates.idle);

            if (CurrentAnimation == GVar.AttackAnimStates.attack)
            {
                float damage;
                float armor;
                damage = Battle.battleEnemy.Damage;
                armor = Battle.battlePlayer.Armour;

                damage -= damage / 100 * armor;

                if (damage < 0)
                    damage = 1;

                Battle.battlePlayer.Health -= (int)damage;

                PlayAnimation(GVar.AttackAnimStates.retreat);
            }
            
        }

        public void SetAttack(Attack atk)
        {
            this.atk = atk;
            this.spriteID = atk.Anim;
        }

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

        public Attack ATK { get { return atk; } set { atk = value; } }
    }
}

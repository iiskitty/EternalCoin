using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using System.Text;

namespace Eternal_Coin
{
    public class ItemType
    {
        public string name;
        public int cost;
        public int armor;
        public int damage;

        public ItemType(string name, int cost, int armor, int damage)
        {
            this.name = name;
            this.cost = cost;
            this.armor = armor;
            this.damage = damage;
        }

        public ItemType(string name, int cost)
        {
            this.name = name;
            this.cost = cost;
        }
    }
}

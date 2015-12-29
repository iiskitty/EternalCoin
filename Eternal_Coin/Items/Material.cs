﻿using Microsoft.Xna.Framework;
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
    public class Material
    {
        public string name;
        public int cost;
        public int armor;
        public int damage;

        public Material(string name, int cost, int armor, int damage)
        {
            this.name = name;
            this.cost = cost;
            this.armor = armor;
            this.damage = damage;
        }
    }
}

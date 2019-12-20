using System;
using System.Collections.Generic;
using System.Text;

namespace BootlegDiablo
{
    public abstract class Enemy
    {
        public int HP { get; set; }
        public int Damage { get; set; }

        // Accepts a seed to generate hp and damage
        public Enemy()
        {

        }
    }
}

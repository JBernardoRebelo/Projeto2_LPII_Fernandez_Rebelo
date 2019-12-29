using GameEngine;

namespace BootlegDiablo
{
    public abstract class Enemy : GameObject
    {
        public int HP { get; set; }
        public int Damage { get; set; }
        public Transform Transform { get; set; }

        // Accepts a seed to generate hp and damage
        public Enemy()
        {

        }
    }
}

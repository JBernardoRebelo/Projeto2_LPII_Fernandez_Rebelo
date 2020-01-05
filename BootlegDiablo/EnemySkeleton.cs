using System;

namespace BootlegDiablo
{
    /// <summary>
    /// EnemySkeleton, inherits from enemy
    /// </summary>
    public class EnemySkeleton : Enemy
    {
        /// <summary>
        /// EnemySkeleton constructor
        /// </summary>
        /// <param name="rndm"> Accepts a Random to define HP and Damage
        /// </param>
        public EnemySkeleton(Random rndm)
        {
            HP = rndm.Next(1, 3); // 10, 30
            Damage = rndm.Next(5, 10);
        }
    }
}

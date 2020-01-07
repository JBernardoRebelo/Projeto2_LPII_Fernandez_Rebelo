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
            Name = "Skeleton";
            HP = rndm.Next(75, 120);
            Damage = rndm.Next(5, 10);
        }
    }
}

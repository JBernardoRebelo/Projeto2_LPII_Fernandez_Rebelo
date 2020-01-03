using System;
namespace BootlegDiablo
{
    public class EnemySkeleton : Enemy
    {
        public EnemySkeleton(Random rndm)
        {
            HP = rndm.Next(0, 3); // 10, 30
            Damage = rndm.Next(5, 10);
        }
    }
}

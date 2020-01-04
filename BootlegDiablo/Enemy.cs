using GameEngine;

namespace BootlegDiablo
{
    public abstract class Enemy : GameObject
    {
        public int HP { get; set; }
        public int Damage { get; set; }

        public virtual void Attack()
        {

        }

        /// <summary>
        /// Enemy update
        /// </summary>
        public override void Update()
        {
            base.Update();

            // Enemy death
            if (HP <= 0)
            {
                Finish();

                //DEBUG

                //System.Console.WriteLine("ENEMY DIED");

                //END DEBUG
            }
        }
    }
}

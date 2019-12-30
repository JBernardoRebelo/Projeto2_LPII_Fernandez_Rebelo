using GameEngine;

namespace BootlegDiablo
{
    public abstract class Enemy : GameObject
    {
        public int HP { get; set; }
        public int Damage { get; set; }
        public Transform Transform { get; set; }
        public virtual void Attack()
        {
            //// For each enemy in the room try attack
            //for (int i = 0; i < dr.Enemies.Length; i++)
            //{
            //    _enemy = dr.Enemies[i];

            //    // Check adjacent position of enemy
            //    if (_enemy.Transform.Pos.X == Transform.Pos.X - 1
            //        || _enemy.Transform.Pos.X == Transform.Pos.X + 1
            //        || _enemy.Transform.Pos.Y == Transform.Pos.Y + 1
            //        || _enemy.Transform.Pos.Y == Transform.Pos.Y - 1)
            //    {
            //        // Damage to recieve
            //        _enemy.HP -= Damage;
            //    }
            //}
        }
    }
}

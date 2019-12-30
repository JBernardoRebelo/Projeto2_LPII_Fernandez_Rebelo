using GameEngine;
using System.Numerics;

namespace BootlegDiablo
{
    public class Player : GameObject
    {
        // Variables
        private Dungeon _dungeon;
        private Enemy _enemy;
        public Transform _transform;

        // Player position for attack
        private Vector2 _playerLeft;
        private Vector2 _playerRight;
        private Vector2 _playerDown;
        private Vector2 _playerUp;
        private Vector2 _playerPos;

        private int _lvlUpExp;

        // Properties
        public int Life { get; set; }
        public int Damage { get; set; } // = Strength + weapon damage
        public int Dexterity { get; set; }
        public int Strength { get; set; }
        public int Lvl { get; set; }
        public int Exp { get; set; }
        public Role Role { get; set; }
        public Weapon Weapon { get; set; }

        /// <summary>
        /// Player constructor, assigns properties,
        /// updates stats based on role
        /// </summary>
        /// <param name="role"> Accepts a role to assign </param>
        /// <param name="name"> Accepts a name to assign </param>
        public Player(Role role, string name)
        {
            Role = role;
            Name = name;
            Lvl = 1;
            Exp = 0;
            _lvlUpExp = 2000;

            // Add weapon to player
            Weapon = new ShortSword();

            // Apply initial stats
            RoleApply(Role);

            Damage = Strength + Weapon.MaxDamage;
        }

        public override void Start()
        {
            base.Start();
            _transform = GetComponent<Transform>();
            _dungeon = ParentScene.FindGameObjectByName("Dungeon") as Dungeon;
        }

        // Attack based on pressed
        public void Attack()
        {
            // Enemy transform
            Transform enemyTransform;
            Vector2 enemyPos;

            // Player directions
            _playerLeft = new Vector2((int)_transform.Pos.X - 1,
                (int)_transform.Pos.Y);

            _playerRight = new Vector2((int)_transform.Pos.X + 1,
                (int)_transform.Pos.Y);

            _playerDown = new Vector2((int)_transform.Pos.X,
                (int)_transform.Pos.Y + 1);

            _playerUp = new Vector2((int)_transform.Pos.X,
                (int)_transform.Pos.Y - 1);

            _playerPos = new Vector2((int)_transform.Pos.X, (int)_transform.Pos.Y);

            // Check rooms in dungeon
            foreach (DungeonRoom dr in _dungeon.Rooms)
            {
                // For each enemy in the room try attack
                for (int i = 0; i < dr.Enemies.Length; i++)
                {
                    _enemy = dr.Enemies[i];

                    // Get enemy transform and position
                    enemyTransform = _enemy.GetComponent<Transform>();
                    enemyPos = new Vector2((int)enemyTransform.Pos.X,
                        (int)enemyTransform.Pos.Y);

                    // Check adjacent position of enemy
                    if (enemyPos == _playerLeft || enemyPos == _playerRight
                        || enemyPos == _playerDown || enemyPos == _playerUp
                        || enemyPos == _playerPos)
                    {
                        // Damage to recieve
                        _enemy.HP -= Damage;

                        // Enemy death
                        if (_enemy.HP <= 0)
                        {
                            Exp += _enemy.Damage * 10;
                        }

                        System.Console.WriteLine("I HIT SOMETHING");
                    }
                }
            }
        }

        /// <summary>
        /// Updates starter stats based on input
        /// </summary>
        /// <param name="role"> Accepts a role </param>
        public void RoleApply(Role role)
        {
            if (role == Role.Warrior)
            {
                Strength = 30;
                Life = 70;
                Dexterity = 20;
                /*
                 * Life: 70 - 316
                 * Mana: 10 - 98
                 * Strength: 30 - 250
                 * Magic: 10 - 50
                 * Dexterity: 20 - 60
                 * Vitality: 25 - 100
                 */
            }
            if (role == Role.Rogue)
            {
                Life = 45;
                Strength = 20;
                Dexterity = 30;
                /*
                 * Life: 45 - 201
                 * Mana: 22 - 173
                 * Strength: 20 - 55
                 * Magic: 15 - 70
                 * Dexterity: 30 - 250
                 * Vitality: 20 - 80
                 */
            }
        }

        /// <summary>
        /// Levels up the character based on role
        /// </summary>
        /// <param name="role"> Accepts a role </param>
        public void LevelUp(Role role)
        {
            // Levels up to level 2
            if (Lvl == 1 && Exp >= _lvlUpExp)
            {
                if (role == Role.Warrior)
                {
                    Life *= 2;

                    //Life: + 2 per Vitality

                }
                if (role == Role.Rogue)
                {
                    Life *= 2;

                    //Life: + 2
                }

                // Level scaller
                _lvlUpExp *= 2;

                Lvl++;
            }
        }

        /// <summary>
        /// Increments level and player stats based on input
        /// </summary>
        /// <param name="life"> Accepts points to increment that will
        /// be multiplied by three</param>
        /// <param name="strength"> Strength to add </param>
        /// <param name="dexterity"> Dexterity to add </param>
        public void LevelUp(int life, int strength, int dexterity)
        {
            life *= 3;

            Life += life;
            Strength += strength;
            Dexterity += dexterity;
        }
    }
}

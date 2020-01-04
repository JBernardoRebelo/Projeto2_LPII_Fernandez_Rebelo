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

        // Player position for attack and door open
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

        /// <summary>
        /// Opens a door and enters a room
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void OpenDoor(out float x, int xx, out float y, int yy)
        {
            Vector2 doorPos;

            x = xx;
            y = yy;

            // Player directions
            _playerLeft = new Vector2((int)_transform.Pos.X - 1,
                (int)_transform.Pos.Y);

            _playerRight = new Vector2((int)_transform.Pos.X + 1,
                (int)_transform.Pos.Y);

            _playerDown = new Vector2((int)_transform.Pos.X,
                (int)_transform.Pos.Y + 1);

            _playerUp = new Vector2((int)_transform.Pos.X,
                (int)_transform.Pos.Y - 1);

            _playerPos = new Vector2((int)_transform.Pos.X,
                (int)_transform.Pos.Y);
            //

            foreach (DungeonRoom room in _dungeon.Rooms)
            {
                for (int i = 0; i < room.Doors.Length; i++)
                {
                    doorPos = new Vector2(
                        (int)room.Doors[i].GetComponent<Transform>().Pos.X,
                        (int)room.Doors[i].GetComponent<Transform>().Pos.Y);

                    System.Console.Write($"{room.Doors[i].Name}{doorPos} | ");
                    System.Console.WriteLine($"{_playerPos}");

                    if (doorPos == _playerLeft) x -= 2;
                    if (doorPos == _playerRight) x += 2;
                    if (doorPos == _playerDown) y += 2;
                    if (doorPos == _playerUp) y -= 2;
                }
            }
        }

        /// <summary>
        /// Player attacks enemies in adjacent positions
        /// </summary>
        public void Attack()
        {
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

            _playerPos = new Vector2((int)_transform.Pos.X,
                (int)_transform.Pos.Y);
            //

            // Check rooms in dungeon
            foreach (DungeonRoom dr in _dungeon.Rooms)
            {
                // For each enemy in the room try attack
                for (int i = 0; i < dr.Enemies.Length; i++)
                {
                    _enemy = dr.Enemies[i];

                    // Get enemy transform and position
                    if (_enemy.TryGetComponent(
                        out Transform enemyTransform))
                    {
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
                                Exp += _enemy.Damage * 150;
                            }

                            // DEBUG

                            //System.Console.WriteLine("I HIT SOMETHING");

                            // END DEBUG
                        }
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
            }
            if (role == Role.Rogue)
            {
                Life = 45;
                Strength = 20;
                Dexterity = 30;
            }
        }

        /// <summary>
        /// Levels up the character based on role
        /// </summary>
        /// <param name="role"> Accepts a role </param>
        public void LevelUp(Role role)
        {
            // Levels up to level 2
            if (Exp >= _lvlUpExp)
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

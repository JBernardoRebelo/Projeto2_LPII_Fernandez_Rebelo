﻿using GameEngine;

namespace BootlegDiablo
{
    public class Player : GameObject
    {
        // Variables
        private Dungeon _dungeon;
        private Enemy _enemy;

        // Properties
        public int Life { get; set; }
        public int Damage { get; set; } // = Strength + weapon damage
        public int Dexterity { get; set; }
        public int Strength { get; set; }
        public int Lvl { get; set; }
        public int Exp { get; set; }
        public Role Role { get; set; }
        public Weapon Weapon { get; set; }
        public Transform Transform { get; set; }

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
            Transform = new Transform(0, 0, 0);

            // Add weapon to player
            Weapon = new ShortSword();

            // Apply initial stats
            RoleApply(Role);

            Damage = Strength + Weapon.MaxDamage;
        }

        public override void Start()
        {
            base.Start();
            _dungeon = ParentScene.FindGameObjectByName("Dungeon") as Dungeon;
        }

        /// <summary>
        /// Increments level and player stats based on input
        /// </summary>
        /// <param name="life"> Accepts points to increment that will
        /// be multiplied by three</param>
        /// <param name="strength"> Strength to add </param>
        /// <param name="dexterity"> Dexterity to add </param>
        public void LvlUp(int life, int strength, int dexterity)
        {
            life *= 3;

            Life += life;
            Strength += strength;
            Dexterity += dexterity;
        }

        // Attack based on pressed
        public void Attack()
        {
            // Check rooms in dungeon
            foreach (DungeonRoom dr in _dungeon.Rooms)
            {
                // For each enemy in the room try attack
                for (int i = 0; i < dr.Enemies.Length; i++)
                {
                    _enemy = dr.Enemies[i];

                    // Check adjacent position of enemy
                    if (_enemy.Transform.Pos.X == Transform.Pos.X - 1
                        || _enemy.Transform.Pos.X == Transform.Pos.X + 1
                        || _enemy.Transform.Pos.Y == Transform.Pos.Y + 1
                        || _enemy.Transform.Pos.Y == Transform.Pos.Y - 1)
                    {
                        // Damage to recieve
                        _enemy.HP -= Damage;
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
            if (Lvl == 1 && Exp >= 2000)
            {

            }

            if (role == Role.Warrior)
            {
                /*
                 * Per Level Up: ***
                 * Life: + 2 per Vitality
                 * Mana: + 1 per Magic
                 */
            }
            if (role == Role.Rogue)
            {
                /*
                 * Per level up: ***
                 * Life: + 2
                 * Mana: + 2 
                 */
            }
            Lvl++;
        }
    }
}

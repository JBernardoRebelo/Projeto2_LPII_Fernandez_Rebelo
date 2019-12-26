using GameEngine;

namespace BootlegDiablo
{
    public class Player
    {
        public int Life { get; set; }
        public int Damage { get; set; } // = Strength + weapon damage
        public int Dexterity { get; set; }
        public int Strength { get; set; }
        public int Lvl { get; set; }
        public int Exp { get; set; }
        public string Name { get; private set; }
        public Role Role { get; set; }
        public Weapon Weapon { get; set; }

        //public Vector2 pos { get; set; } // Usar vector2 do stor

        public Player(Role role, string name)
        {
            Role = role;
            Name = name;
            Lvl = 1;
            Exp = 0;

            // Add weapon to player
            Weapon = new ShortSword();

            // Apply initial stats
            RoleApply(Role);
        }

        // Increments level and player stats based on input
        public void LvlUp(int dmg, int Hp)
        {

        }

        // Attack based on a char pressed
        public void Attack(char skill)
        {

        }

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

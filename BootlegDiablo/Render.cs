using System;
using System.IO;
using System.Text;
using static System.Console;

namespace BootlegDiablo
{
    public class Render
    {
        UTF8Encoding utf8 = new UTF8Encoding();

        /// <summary>
        /// Displays Start menu with options
        /// </summary>
        public void StartMenu(out Role role)
        {
            string option;

            DisplayLogo();

            // Será que daria para escolher com as setas e com o enter?
            // O cursor seria o pentagrama, mas entretanto vou fazer com texto

            // Show menu options
            WriteLine();
            WriteLine("⛧ SINGLE PLAYER ⛧");
            WriteLine("⛧ SHOW CREDITS ⛧");
            WriteLine("⛧ EXIT ⛧");

            option = ReadLine().ToLower();

            while (option == "e" || option == "credits")
            {
                switch (option)
                {
                    case "credits":
                        Credits();
                        break;
                    case "e":
                        Environment.Exit(0);
                        break;
                }
            }

            role = RolePick();
        }

        /// <summary>
        /// Show character stats on 'C' press
        /// </summary>
        /// <param name="player"> Accepts a player </param>
        public void CharInformationScreen(Player player)
        {
            WriteLine($"{player.Name} | {player.Role}");
            WriteLine();
            WriteLine($"Level: {player.Lvl} Exp: {player.Exp}");

            //How much experience the character needs to achieve the next level

            WriteLine();
            WriteLine($"Strength: {player.Strength}");
            WriteLine($"Dexterity: {player.Dexterity}");
            WriteLine($"Life: {player.Life}");
            WriteLine();
            WriteLine($"{player.Weapon.Name.ToUpper()}");
            WriteLine($"Damage: {player.Weapon.MinDamage}" +
                $" - {player.Weapon.MaxDamage}");
            WriteLine($"Durability: {player.Weapon.Durability}");
        }

        /// <summary>
        /// Displays pause menu on 'Esc'
        /// </summary>
        public void PauseMenu()
        {
            DisplayLogo();

            // Show menu options
            WriteLine();
            WriteLine("⛧ RESUME GAME ⛧");
            WriteLine("⛧ EXIT ⛧");
        }

        /// <summary>
        /// Ask for name
        /// </summary>
        /// <returns> Returns a name to be assigned to the player </returns>
        public string AssignName()
        {
            WriteLine("What is your name?");
            return ReadLine().ToUpper();
        }

        /// <summary>
        /// Displays Role picker menu
        /// </summary>
        private Role RolePick()
        {
            string roleOption;
            Role role = default;

            Clear();
            DisplayLogo();
            WriteLine();
            WriteLine("⛧ WARRIOR ⛧");
            WriteLine("⛧ ROGUE ⛧");
            roleOption = ReadLine().ToLower();

            // Ensure the right information is inputed
            while (roleOption != "warrior" && roleOption != "rogue")
            {
                WriteLine("Try again");
                roleOption = ReadLine();
            }

            // Assign proper values to the variable
            if (roleOption == "warrior") role = Role.Warrior;
            if (roleOption == "rogue") role = Role.Rogue;

            return role;
        }

        /// <summary>
        /// Display Credits
        /// </summary>
        private void Credits()
        {
            WriteLine("Game Design by:\n - Blizzard North\n");
            WriteLine("Code by:\n - Joao Rebelo\n - Miguel Fernandez\n");
            WriteLine("External Engine Library by:\n - Nuno Fachada\n");
            ReadLine();
        }

        /// <summary>
        /// Displays Game Logo
        /// </summary>
        private void DisplayLogo()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(File.ReadAllText("LogoDiablo.txt"));
            ForegroundColor = ConsoleColor.Gray;
        }
    }
}

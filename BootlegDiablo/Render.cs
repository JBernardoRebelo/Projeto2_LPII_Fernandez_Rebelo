using System;
using static System.Console;

namespace BootlegDiablo
{
    public class Render
    {
        /// <summary>
        /// Displays Start menu with options
        /// </summary>
        public void StartMenu(out Role role)
        {
            string option = null;

            // Será que daria para escolher com as setas e com o enter?
            // O cursor seria o pentagrama, mas entretanto vou fazer com texto

            while (option != "s" || option != null)
            {
                DisplayLogo();

                // Show menu options
                WriteLine();
                WriteLine($"⛧ SINGLE PLAYER ⛧");
                WriteLine("⛧ SHOW CREDITS ⛧");
                WriteLine("⛧ INSTRUCTIONS ⛧");
                WriteLine("⛧ EXIT ⛧");

                Write("-> ");
                option = ReadLine().ToLower();

                if (option == "credits" || option == "c")
                {
                    Credits();
                    Clear();
                }
                else if (option == "instructions" || option == "i")
                {
                    Instructions();
                    Clear();
                }
                else if (option == "exit" || option == "e")
                {
                    WriteLine("Goodbye");
                    Environment.Exit(0);
                }
                else
                {
                    break;
                }
            }

            WriteLine("\nPick a class...\n");
            role = RolePick();
        }

        /// <summary>
        /// Show character stats on 'C' press
        /// </summary>
        /// <param name="player"> Accepts a player </param>
        public void CharInformationScreen(Player player)
        {
            WriteLine($"{player.ChosenName} | {player.Role}");
            WriteLine();
            WriteLine($"Level: {player.Lvl} Exp: {player.Exp}");

            //How much experience the character needs to achieve the next level

            WriteLine();
            Write($"Strength: {player.Strength}  |  ");
            WriteLine($"{player.Weapon.Name.ToUpper()}");
            Write($"Dexterity: {player.Dexterity} |      ");
            WriteLine($"Damage: {player.Weapon.MinDamage}" +
                $" - {player.Weapon.MaxDamage}");
            Write($"Life: {player.Life}      |      ");
            WriteLine($"Durability: {player.Weapon.Durability}");

        }

        public void EssencialInfo(Player player)
        {
            WriteLine($"{player.ChosenName} | HP: {player.Life} |" +
                $"Level: {player.Lvl}");
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
        /// Clear console and Victory screen
        /// </summary>
        public void Victory()
        {
            Clear();
            WriteLine("Congrats, you cleared the console!");
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
            Clear();
            DisplayLogo();

            WriteLine();
            WriteLine("Code by:\n - Joao Rebelo\n - Miguel Fernandez\n");
            WriteLine("Based on Engine Library by:\n - Nuno Fachada\n");
            ReadLine();
        }

        /// <summary>
        /// Output instructions
        /// </summary>
        private void Instructions()
        {
            Clear();
            DisplayLogo();

            WriteLine("\nHOW TO PLAY:");
            WriteLine("\n -> In the menus, type the wanted option to go");
            WriteLine(" -> In game...");
            WriteLine("    -> Attack enemies to win XP and level up");
            WriteLine("        - (Attacks reach enemies in your position," +
                " up," + " down, left and right)");
            WriteLine("    -> By killing every enemy you win, if you die," +
                " you lose");
            WriteLine("\nCONTROLS:");
            WriteLine("\n- 'WASD' to move");
            WriteLine("- 'Space' to attack");
            WriteLine("- 'C' show character details");
            WriteLine("- 'E' to go through doors");

            ReadLine();
        }

        /// <summary>
        ///  Displays Game Logo
        /// </summary>
        private void DisplayLogo()
        {
            ForegroundColor = ConsoleColor.Red;
            Console.Write(
                " ______	 _________ _______  ______   _	      _______\n" +
                "(  __  \\ \\__   __/(  ___  )(  ___ \\ ( \\      (  ___  )\n" +
                "| (  \\  )   ) (   | (   ) || (   ) )| (      | (   ) |\n" +
                "| |   ) |   | |   | (___) || (__/ / | |      | |   | |\n" +
                "| |   | |   | |   |  ___  ||  __ (  | |      | |   | |\n" +
                "| |   ) |   | |   | (   ) || (  \\ \\ | |      | |   | |\n" +
                "| (__/  )___) (___| )   ( || )___) )| (____/\\| (___) |\n" +
                "(______/ \\_______/|/     \\||/ \\___/ (_______/(_______) " +
                "TM\n");

            ForegroundColor = ConsoleColor.Gray;
        }
    }
}

using System;

namespace BootlegDiablo
{
    public class GameLoop
    {
        private Player _player;
        private Render _render = new Render();
        private Dungeon _dungeon;
        private Random _rnd;

        /// <summary>
        /// Starts game by showing menu and instantiating player
        /// </summary>
        public void Start()
        {
            // Instance variables for player
            Role role;
            string name;

            // Instantiate random
            _rnd = new Random();

            // Instantiate dungeon with number of rooms
            _dungeon = new Dungeon(_rnd.Next(1, 10));

            // Render Start menu with options
            _render.StartMenu(out role);
            name = _render.AssignName();

            // Instantiate player
            _player = new Player(role, name, _dungeon);

            // Start debug game loop
            DebugGameLoop();
        }

        // GameLoop used for debug
        public void DebugGameLoop()
        {
            Console.Clear();

            // Debug
            _render.CharInformationScreen(_player);
            //**

            while(true)
            {
                // Input checker

                // Do stuff

                // Render Frame
            }
        }
    }
}

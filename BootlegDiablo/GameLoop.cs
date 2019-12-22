using System;

namespace BootlegDiablo
{
    public class GameLoop
    {
        private Player _player;
        private Render _render = new Render();

        /// <summary>
        /// Starts game by showing menu and instantiating player
        /// </summary>
        public void Start()
        {
            Role role;
            string name;

            // Render Start menu with options
            _render.StartMenu(out role);
            name = _render.AssignName();

            // Instantiate player
            _player = new Player(role, name);

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

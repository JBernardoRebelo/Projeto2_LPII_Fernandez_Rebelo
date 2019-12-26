using System;
using GameEngine;

namespace BootlegDiablo
{
    public class Game
    {
        private Player _player;

        // Render and Random
        private Render _render;
        private Random _rnd;

        // World dimensions
        private int _x = 100;
        private int _y = 50;

        // Frame duration in miliseconds
        private int frameLength = 100;

        // The (only) game scene
        private Scene _scene;

        /// <summary>
        /// Game Constructor, start instance variables
        /// </summary>
        public Game()
        {
            // Instantiate scene
            _scene = new Scene(_x, _y);

            // Instantiate render and random
            _render = new Render();
            _rnd = new Random();
        }

        /// <summary>
        /// Starts game by showing menu and instantiating player
        /// </summary>
        public void Start()
        {
            // Instance variables for player
            Role role;
            string name;

            // Instantiate dungeon with number of rooms
            Dungeon _dungeon = new Dungeon(_rnd.Next(1, 10));
            _scene.AddGameObject(_dungeon);

            // Render Start menu with options
            _render.StartMenu(out role);
            name = _render.AssignName();

            // Instantiate player
            _player = new Player(role, name, _dungeon);
            _scene.AddGameObject(_player);

            // Start debug game loop
            DebugGameLoop();
        }

        // GameLoop used for debug
        public void DebugGameLoop()
        {
            Console.Clear();

            // Debug
            _render.CharInformationScreen(_player, _scene);
            //**

            while (true)
            {
                // Input checker

                // Do stuff

                // Render Frame
            }
        }
    }
}

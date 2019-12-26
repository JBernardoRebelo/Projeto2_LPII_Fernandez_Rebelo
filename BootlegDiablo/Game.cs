using System;
using GameEngine;
using System.Collections.Generic;
using System.Numerics;

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
        private int _frameLength = 100;

        // The (only) game scene
        private Scene _scene;

        /// <summary>
        /// Game Constructor, start instance variables
        /// </summary>
        public Game()
        {
            // Instantiate scene
            ConsoleKey[] quitKeys = new ConsoleKey[] { ConsoleKey.Escape };

            _scene = new Scene(_x, _y,
                new InputHandler(quitKeys),
                new ConsoleRenderer(_x, _y, new ConsolePixel('.')),
                new CollisionHandler(_x, _y));

            // Instantiate render and random
            _render = new Render();
            _rnd = new Random(1);
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
            Dungeon _dungeon;
            _dungeon = new Dungeon(_rnd.Next(1, 10));
            _scene.AddGameObject(_dungeon);

            // Render Start menu with options
            _render.StartMenu(out role);
            name = _render.AssignName();

            // Instantiate player
            _player = new Player(role, name);
            _scene.AddGameObject(_player);

            // Start debug game loop
            //DebugGameLoop();

            CreateWalls(_scene);

            _scene.GameLoop(_frameLength);
        }

        private void CreateWalls(Scene scene)
        {
            Dictionary<Vector2, ConsolePixel> wallPixels;

            GameObject wall = new DungeonWall();
            // Foreach wall does this

            ConsolePixel wallPixel =
                new ConsolePixel('-', ConsoleColor.Yellow, ConsoleColor.Gray);
            wallPixels = new Dictionary<Vector2, ConsolePixel>();

            for (int x = 0; x < _x; x++)
                wallPixels[new Vector2(x, 0)] = wallPixel;
            for (int x = 0; x < _x; x++)
                wallPixels[new Vector2(x, _y - 1)] = wallPixel;
            for (int y = 0; y < _y; y++)
                wallPixels[new Vector2(0, y)] = wallPixel;
            for (int y = 0; y < _y; y++)
                wallPixels[new Vector2(_x - 1, y)] = wallPixel;
            wall.AddComponent(new ConsoleSprite(wallPixels));
            wall.AddComponent(new Transform(0, 0, 1));

            scene.AddGameObject(wall);
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

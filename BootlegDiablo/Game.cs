using System;
using GameEngine;
using System.Collections.Generic;
using System.Numerics;

namespace BootlegDiablo
{
    public class Game
    {
        // Should be gameObject
        private Player _player;

        // Render and Random
        private Render _render;
        private Random _rnd;

        // World dimensions
        private int _x = 160;
        private int _y = 40;

        // Frame duration in miliseconds
        private int _frameLength = 2000;

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
                new ConsoleRenderer(_x, _y, new ConsolePixel(' ')),
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
            _dungeon = new Dungeon(_rnd.Next(1, 10), _rnd);
            _scene.AddGameObject(_dungeon);

            // Render Start menu with options
            _render.StartMenu(out role);
            name = _render.AssignName();

            // Instantiate player
            _player = new Player(role, name);
            _scene.AddGameObject(_player);

            CreateWalls(_scene);

            _scene.GameLoop(_frameLength);
        }

        /// <summary>
        /// Add walls to the scene
        /// </summary>
        /// <param name="scene"> Accepts a Scene </param>
        private void CreateWalls(Scene scene)
        {
            GameObject go = scene.FindGameObjectByName("Dungeon");
            Dungeon dungeon = go as Dungeon;

            int index = 1;

            Dictionary<Vector2, ConsolePixel> wallPixels;
            // Foreach wall does this
            foreach (DungeonRoom room in dungeon.Rooms)
            {
                GameObject wallS = new GameObject("Walls" + index);

                ConsolePixel wallPixel =
                    new ConsolePixel('i', ConsoleColor.Yellow, ConsoleColor.DarkRed);
                wallPixels = new Dictionary<Vector2, ConsolePixel>();

                for (int x = 0; x < _x; x++)
                    wallPixels[new Vector2(x, 0)] = wallPixel;
                for (int x = 0; x < _x; x++)
                    wallPixels[new Vector2(x, _y - 1)] = wallPixel;
                for (int y = 0; y < _y; y++)
                    wallPixels[new Vector2(0, y)] = wallPixel;
                for (int y = 0; y < _y; y++)
                    wallPixels[new Vector2(_x - 1, y)] = wallPixel;
                wallS.AddComponent(new ConsoleSprite(wallPixels));
                wallS.AddComponent(new Transform(0, 0, 1));

                scene.AddGameObject(wallS);

                index++;
            }
        }
    }
}

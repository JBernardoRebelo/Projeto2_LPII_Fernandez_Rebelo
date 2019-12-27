using System;
using GameEngine;
using System.Collections.Generic;
using System.Numerics;

namespace BootlegDiablo
{
    public class Game
    {
        // Should be gameObject
        private GameObject _player;

        // Render and Random
        private Render _render;
        private Random _rnd;

        // World dimensions
        private int _x = 160;
        private int _y = 40;

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
                new ConsoleRenderer(_x, _y, new ConsolePixel(' ')),
                new CollisionHandler(_x, _y));


            // Instantiate render and random
            _render = new Render();
            _rnd = new Random(1);

            // Instantiate dungeon with number of rooms
            Dungeon _dungeon;
            _dungeon = new Dungeon(_rnd.Next(1, 10), _rnd);
            _scene.AddGameObject(_dungeon);

            CreateWalls(_scene);
        }

        /// <summary>
        /// Starts game by showing menu and instantiating player
        /// </summary>
        public void Start()
        {
            // Create quitter object
            GameObject quitter = new GameObject("Quitter");
            KeyObserver quitSceneKeyListener = new KeyObserver(new ConsoleKey[]
                { ConsoleKey.Escape });
            quitter.AddComponent(quitSceneKeyListener);
            quitter.AddComponent(new Quitter());
            _scene.AddGameObject(quitter);

            // Instance variables for player
            Role role;
            string name;

            // Render Start menu with options
            _render.StartMenu(out role);
            name = _render.AssignName();

            // Instantiate player
            char[,] playerSprite = { { 'O' } };
            _player = new Player(role, name);
            KeyObserver playerKeys = new KeyObserver(new ConsoleKey[]
            {
                ConsoleKey.W,
                ConsoleKey.A,
                ConsoleKey.S,
                ConsoleKey.D,
                ConsoleKey.C,
                ConsoleKey.P,
            });
            _player.AddComponent(playerKeys);
            _player.AddComponent(new PlayerController());
            _player.AddComponent(new Transform(10f, 10f, 2f));
            _player.AddComponent(new ConsoleSprite(
                playerSprite, ConsoleColor.Green, ConsoleColor.Yellow));

            _scene.AddGameObject(_player);

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
            Dictionary<Vector2, ConsolePixel> doorPixels;

            GameObject aux = null;
            // Foreach wall does this
            foreach (DungeonRoom room in dungeon.Rooms)
            {
                GameObject walls = new GameObject("Walls" + index);

                ConsolePixel wallPixel =
                    new ConsolePixel(' ', ConsoleColor.White, ConsoleColor.Red);
                wallPixels = new Dictionary<Vector2, ConsolePixel>();

                for (int x = 0; x < room.Dim.X; x++)
                {
                    wallPixels[new Vector2(x, 0)] = wallPixel;
                }
                for (int x = 0; x < room.Dim.X; x++)
                {
                    wallPixels[new Vector2(x, room.Dim.Y - 1)] = wallPixel;
                }
                for (int y = 0; y < room.Dim.Y; y++)
                {
                    wallPixels[new Vector2(0, y)] = wallPixel;
                }
                for (int y = 0; y < room.Dim.Y; y++)
                {
                    wallPixels[new Vector2(room.Dim.X - 1, y)] = wallPixel;
                }

                if (aux == null)
                {
                    walls.AddComponent(new ConsoleSprite(wallPixels));
                    walls.AddComponent(
                        new Transform(0, 0, 1f));
                    aux = walls;
                }
                else
                {
                    walls.AddComponent(new ConsoleSprite(wallPixels));
                    walls.AddComponent(
                        new Transform(_rnd.Next(Convert.ToInt32
                        (aux.GetComponent<Transform>().Pos.X), _x - 20),
                        _rnd.Next(0, _y - 20), 1f));

                    aux = walls;
                }

                scene.AddGameObject(walls);

                for (int i = 0; i < room.Doors.Length; i++)
                {
                    GameObject door = new GameObject("Door" + i + index);

                    ConsolePixel doorPixel = new ConsolePixel
                        ('D', ConsoleColor.Blue, ConsoleColor.Green);

                    doorPixels = new Dictionary<Vector2, ConsolePixel>();

                    door.AddComponent(new ConsoleSprite(doorPixels));
                    door.AddComponent(new Transform(0, 0, 2));

                    scene.AddGameObject(door);
                }

                index++;
            }
        }
    }
}

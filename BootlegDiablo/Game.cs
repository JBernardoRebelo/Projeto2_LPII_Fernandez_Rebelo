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
        private const int _x = 160;
        private const int _y = 40;

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
            char[,] playerSprite = { { 'O', '|' } };
            _player = new Player(role, name);
            KeyObserver playerKeys = new KeyObserver(new ConsoleKey[]
            {
                ConsoleKey.W,
                ConsoleKey.A,
                ConsoleKey.S,
                ConsoleKey.D,
                ConsoleKey.C,
                ConsoleKey.P,
                ConsoleKey.Spacebar
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
            GameObject aux = null;
            int index = 1;

            Dictionary<Vector2, ConsolePixel> wallPixels;
            Dictionary<Vector2, ConsolePixel> doorPixels;

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

                // First room walls
                if (aux == null)
                {
                    walls.AddComponent(new ConsoleSprite(wallPixels));
                    walls.AddComponent(
                        new Transform(0, 9, 1f));
                    aux = walls;
                }
                else
                {
                    // Look for door of prev room, join this room door with it
                    walls.AddComponent(new ConsoleSprite(wallPixels));
                    walls.AddComponent(
                        new Transform(
                        _rnd.Next(Convert.ToInt32(
                            aux.GetComponent<Transform>().Pos.X / 4),
                            _x - 30),
                        _rnd.Next(Convert.ToInt32(
                            aux.GetComponent<Transform>().Pos.Y / 2),
                            _y - 30), 1f));

                    aux = walls;
                }

                scene.AddGameObject(walls);

                // Display doors in room
                for (int i = 0; i < room.Doors.Length; i++)
                {
                    room.Doors[i].Name += i;
                    room.Doors[i].Name += index;

                    ConsolePixel doorPixel = new ConsolePixel
                        ('D', ConsoleColor.Blue, ConsoleColor.Green);

                    doorPixels = new Dictionary<Vector2, ConsolePixel>();

                    room.Doors[i].AddComponent(new ConsoleSprite(doorPixels));

                    //room.Doors[i].AddComponent(
                    //    new Transform(room.Doors[i].Transform.Pos.X,
                    //    room.Doors[i].Transform.Pos.Y,
                    //    room.Doors[i].Transform.Pos.Z));

                    // Debugs
                    room.Doors[i].AddComponent(new Transform(9, 10, 2));

                    //Console.Write(room.Doors[i].Transform.Pos.X);
                    //Console.Write(room.Doors[i].Transform.Pos.Y);
                    //Console.WriteLine(room.Doors[i].Transform.Pos.Z);

                    scene.AddGameObject(room.Doors[i]);
                }

                index++;
            }
        }
    }
}

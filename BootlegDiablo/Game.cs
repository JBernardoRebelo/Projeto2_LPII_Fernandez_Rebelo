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
        private const int _y = 41;

        // Frame duration in miliseconds
        private int _frameLength = 60;

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
            _rnd = new Random(5);

            // Instantiate dungeon with number of rooms
            Dungeon _dungeon;
            _dungeon = new Dungeon(_rnd.Next(1, 10), _rnd);
            _scene.AddGameObject(_dungeon);

            CreateDungeons(_scene);
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
            char[,] playerSprite = { { 'O'} };
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
                playerSprite, ConsoleColor.White, ConsoleColor.Blue));
            _player.AddComponent(new SpriteCollider());

            _scene.AddGameObject(_player);

            _scene.GameLoop(_frameLength);
        }

        /// <summary>
        /// Add walls to the scene
        /// </summary>
        /// <param name="scene"> Accepts a Scene </param>
        private void CreateDungeons(Scene scene)
        {
            GameObject go = scene.FindGameObjectByName("Dungeon");
            Dungeon dungeon = go as Dungeon;
            GameObject aux = null;
            int index = 1;

            Dictionary<Vector2, ConsolePixel> wallPixels;

            char[,] doors = { { ' ' } };
            char[,] enemy = { { '☠' } }; // ☠

            // Foreach wall does this
            foreach (DungeonRoom room in dungeon.Rooms)
            {
                GameObject walls = new GameObject("Walls" + index);

                ConsolePixel wallPixel =
                    new ConsolePixel(' ', ConsoleColor.White,
                    ConsoleColor.DarkYellow);
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
                    walls.AddComponent(new Transform(0, 9, 1f));
                    aux = walls;
                }
                else
                {
                    Transform auxTrans = aux.GetComponent<Transform>();

                    // Look for door of prev room, join this room door with it
                    walls.AddComponent(new ConsoleSprite(wallPixels));
                    walls.AddComponent(new Transform(
                        _rnd.Next((int)(auxTrans.Pos.X / 2), _x - 30),
                        _rnd.Next((int)(auxTrans.Pos.Y / 2), _y - 30), 1f));

                    aux = walls;
                }

                scene.AddGameObject(walls);

                Transform wallTrans = walls.GetComponent<Transform>();

                // Display doors in room
                for (int i = 0; i < room.Doors.Length; i++)
                {
                    // Assign door name
                    room.Doors[i].Name += i;
                    room.Doors[i].Name += index;

                    // Add sprite to door
                    room.Doors[i].AddComponent(new ConsoleSprite(
                        doors, ConsoleColor.White, ConsoleColor.Black));

                    if (i % 2 == 0)
                    {
                        // Add transform corresponding to the room it's in - X
                        room.Doors[i].AddComponent(new Transform(wallTrans.Pos.X,
                            wallTrans.Pos.Y + 2, 2));
                    }
                    else
                    {
                        // Add transform corresponding to the room it's in - Y
                        room.Doors[i].AddComponent(new Transform(wallTrans.Pos.X + 2,
                            wallTrans.Pos.Y, 2));
                    }

                    scene.AddGameObject(room.Doors[i]);
                }

                for (int i = 0; i < room.Enemies.Length; i++)
                {
                    room.Enemies[i].Name += i;
                    room.Enemies[i].Name += index;

                    room.Enemies[i].AddComponent(new ConsoleSprite(
                        enemy, ConsoleColor.White, ConsoleColor.Red));

                    // Debugs
                    room.Enemies[i].AddComponent(
                        new Transform(wallTrans.Pos.X + 2,
                        wallTrans.Pos.Y + 2, 2f));

                    //Console.Write(room.Doors[i].Transform.Pos.X);
                    //Console.Write(room.Doors[i].Transform.Pos.Y);
                    //Console.WriteLine(room.Doors[i].Transform.Pos.Z);

                    room.Enemies[i].AddComponent(new EnemyController(_rnd));

                    scene.AddGameObject(room.Enemies[i]);
                }

                index++;
            }
        }
    }
}

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
        private const int _x = 161;
        private const int _y = 41;

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
            _rnd = new Random();

            // Instantiate dungeon with number of rooms
            Dungeon _dungeon;
            _dungeon = new Dungeon(_rnd.Next(2, 10), _rnd);
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
                ConsoleKey.Spacebar
            });
            _player.AddComponent(playerKeys);
            _player.AddComponent(new PlayerController());
            _player.AddComponent(new Transform(5f, _y / 4 + 1, 2f));
            _player.AddComponent(new ConsoleSprite(
                playerSprite, ConsoleColor.White, ConsoleColor.Blue));
            //_player.AddComponent(new SpriteCollider());

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
            GameObject border;
            GameObject walls;
            GameObject aux = null;          // Get previous gameObject (walls)
            Transform wallTrans;            // Get walls Transform
            Transform auxTrans;             // Previous gameObject transform
            DungeonRoom auxRoom = null;     // Get previous gameObject (room)
            int index = 1;

            ConsolePixel borderPixel;

            Dictionary<Vector2, ConsolePixel> wallPixels;
            Dictionary<Vector2, ConsolePixel> borderPixels;

            // Element's sprites
            char[,] doors = { { ' ' } };
            char[,] enemy = { { '☠' } }; // ☠

            // Border of game
            border = new GameObject("Borders");
            borderPixel = new ConsolePixel('#');
            borderPixels = new Dictionary<Vector2, ConsolePixel>();

            for (int x = 0; x < _x; x++)
            {
                borderPixels[new Vector2(x, 0)] = borderPixel;
            }
            for (int x = 0; x < _x; x++)
            {
                borderPixels[new Vector2(x, _y - 1)] = borderPixel;
            }
            for (int y = 0; y < _y; y++)
            {
                borderPixels[new Vector2(0, y)] = borderPixel;
            }
            for (int y = 0; y < _y; y++)
            {
                borderPixels[new Vector2(_x - 1, y)] = borderPixel;
            }
            border.AddComponent(new ConsoleSprite(borderPixels));
            border.AddComponent(new Transform(0, 0, 1));
            scene.AddGameObject(border);

            // Foreach room create wall
            foreach (DungeonRoom room in dungeon.Rooms)
            {
                walls = new GameObject("Walls" + index);

                ConsolePixel wallPixel =
                    new ConsolePixel(' ', ConsoleColor.White,
                    ConsoleColor.DarkYellow);
                wallPixels = new Dictionary<Vector2, ConsolePixel>();

                // WALLS
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
                if (aux == null && auxRoom == null)
                {
                    walls.AddComponent(new ConsoleSprite(wallPixels));
                    walls.AddComponent(new Transform(1, _y / 6, 1f));
                    aux = walls;
                    auxRoom = room;
                }
                else
                {
                    float xdim;
                    auxTrans = aux.GetComponent<Transform>();


                    // X of room is taken from the previus walls
                    // and room dimensions
                    xdim = Math.Clamp(
                            auxTrans.Pos.X + auxRoom.Dim.X - 1, 0, _x - 2);

                    // Y of room is taken from the previus walls and doors
                    // In relation with the center of the current room
                    float ydim = auxTrans.Pos.Y + (auxRoom.Dim.Y / 2)
                        - (room.Dim.Y / 2);

                    // Make sure the room doesn't get out of bounds
                    if (auxTrans.Pos.X + auxRoom.Dim.X + room.Dim.X <= _x)
                    {
                        // Add the sprite and transform to assign position
                        walls.AddComponent(new ConsoleSprite(wallPixels));
                        walls.AddComponent(new Transform(xdim, ydim, 1f));
                    }
                    else
                    {
                        walls.AddComponent(new ConsoleSprite(wallPixels));
                        walls.AddComponent(new Transform(1,
                           _y / 2, 1f));
                    }

                    aux = walls;
                    auxRoom = room;
                }

                scene.AddGameObject(walls);

                wallTrans = walls.GetComponent<Transform>();

                // DOORS IN ROOM
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
                        room.Doors[i].AddComponent(
                            new Transform(wallTrans.Pos.X,
                            wallTrans.Pos.Y + (room.Dim.Y / 2), 2));
                    }
                    else
                    {
                        // Add transform corresponding to the room it's in - Y
                        room.Doors[i].AddComponent(
                            new Transform(wallTrans.Pos.X + (room.Dim.X / 2),
                            wallTrans.Pos.Y + room.Dim.Y - 1, 2));
                    }

                    scene.AddGameObject(room.Doors[i]);
                }

                // ENEMIES IN ROOM
                for (int i = 0; i < room.Enemies.Length; i++)
                {
                    room.Enemies[i].Name += i;
                    room.Enemies[i].Name += index;

                    room.Enemies[i].AddComponent(new ConsoleSprite(
                        enemy, ConsoleColor.White, ConsoleColor.Red));

                    // Debugs
                    room.Enemies[i].AddComponent(
                        new Transform(wallTrans.Pos.X + (room.Dim.X / 2),
                        wallTrans.Pos.Y + (room.Dim.Y / 2), 2f));

                    room.Enemies[i].AddComponent(new EnemyController(_rnd));

                    scene.AddGameObject(room.Enemies[i]);
                }

                index++;
            }
        }
    }
}

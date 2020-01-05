using System;
using GameEngine;
using System.Collections.Generic;
using System.Numerics;

namespace BootlegDiablo
{
    /// <summary>
    /// Game, is called by program, starts game, creates the player
    /// add objects to scene and calls it to start gameloop
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Render instance to use in this
        /// </summary>
        private Render _render;

        /// <summary>
        /// Random instance to use in this 
        /// and to be passed on to other classes
        /// </summary>
        private Random _rnd;

        // World dimensions
        /// <summary>
        /// X dimension of world
        /// </summary>
        private const int _x = 161;

        /// <summary>
        /// Y dimension of world
        /// </summary>
        private const int _y = 41;

        /// <summary>
        /// Frame duration in miliseconds
        /// </summary>
        private int _frameLength = 60;

        /// <summary>
        /// The game scene 
        /// </summary>
        private Scene _scene;

        /// <summary>
        /// Player instance to create
        /// </summary>
        private Player _player;

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

            // Instantiate dungeon with number of rooms
            Dungeon _dungeon;
            _dungeon = new Dungeon(_rnd.Next(2, 10), _rnd);
            _scene.AddGameObject(_dungeon);

            CreateDungeons(_scene);

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
                ConsoleKey.E,
                ConsoleKey.Spacebar
            });
            _player.AddComponent(playerKeys);
            _player.AddComponent(new PlayerController());
            _player.AddComponent(new Transform(5f, _y / 4 + 1, 2f));
            _player.AddComponent(new ConsoleSprite(
                playerSprite, ConsoleColor.White, ConsoleColor.Blue));
            _player.AddComponent(new ObjectCollider());

            _scene.AddGameObject(_player);

            _scene.GameLoop(_frameLength);

            if (_player == null)
            {
                // LOST
            }
            else
            {
                // WIN
                Console.WriteLine("YAHHHHH win WEOOOOOHHHH");
            }
        }

        /// <summary>
        /// Add components to objets that compose the dungeon
        /// </summary>
        /// <param name="scene"> Accepts a Scene to add the components to
        /// </param>
        private void CreateDungeons(Scene scene)
        {
            GameObject go = scene.FindGameObjectByName("Dungeon");
            Dungeon dungeon = go as Dungeon;
            GameObject walls;
            GameObject aux = null;          // Get previous gameObject (walls)
            Transform wallTrans;            // Get walls Transform
            Transform auxTrans;             // Previous gameObject transform
            DungeonRoom auxRoom = null;     // Get previous gameObject (room)
            int index = 1;

            Dictionary<Vector2, ConsolePixel> wallPixels;

            // Element's sprites
            char[,] doors = { { ' ' } };
            char[,] enemy = { { '☠' } }; // ☠

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
                    walls.AddComponent(new ObjectCollider(new Vector2(x, 0)));
                }
                for (int x = 0; x < room.Dim.X; x++)
                {
                    wallPixels[new Vector2(x, room.Dim.Y - 1)] = wallPixel;
                    walls.AddComponent(new ObjectCollider(
                        new Vector2(x, room.Dim.Y - 1)));
                }
                for (int y = 0; y < room.Dim.Y; y++)
                {
                    wallPixels[new Vector2(0, y)] = wallPixel;
                    walls.AddComponent(new ObjectCollider(new Vector2(0, y)));
                }
                for (int y = 0; y < room.Dim.Y; y++)
                {
                    wallPixels[new Vector2(room.Dim.X - 1, y)] = wallPixel;
                    walls.AddComponent(new ObjectCollider(
                        new Vector2(room.Dim.X - 1, y)));
                }

                // First room walls
                if (aux == null && auxRoom == null)
                {
                    walls.AddComponent(new ConsoleSprite(wallPixels));
                    walls.AddComponent(new Transform(1, _y / 6, 1f));
                    //walls.AddComponent(new ObjectCollider());
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
                        doors, ConsoleColor.White, ConsoleColor.Yellow));

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

                    room.Enemies[i].AddComponent(
                        new Transform(wallTrans.Pos.X + (room.Dim.X / 2) + i,
                        wallTrans.Pos.Y + (room.Dim.Y / 2)+i, 2f));

                    room.Enemies[i].AddComponent(new ObjectCollider());

                    room.Enemies[i].AddComponent(new EnemyController(_rnd));

                    scene.AddGameObject(room.Enemies[i]);
                }

                index++;
            }
        }
    }
}

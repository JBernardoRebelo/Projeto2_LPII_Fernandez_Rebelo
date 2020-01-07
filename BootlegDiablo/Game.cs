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
            _rnd = new Random(1);
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
                ConsoleKey.E,
                ConsoleKey.Spacebar
            });
            _player.AddComponent(playerKeys);
            _player.AddComponent(new PlayerController());
            _player.AddComponent(new Transform(5f, _y / 6 + 1, 2f));
            _player.AddComponent(new ConsoleSprite(
                playerSprite, ConsoleColor.White, ConsoleColor.Blue));
            _player.AddComponent(new ObjectCollider());

            _scene.AddGameObject(_player);

            _scene.GameLoop(_frameLength);

            if (_player.Life <= 0)
            {
                // LOST
            }
            else
            {
                // WIN
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
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

                float xdim;
                float ydim;
                int xpos;
                int xpos2;
                int ypos;
                int ypos2;

                // First room walls
                if (aux == null && auxRoom == null)
                {
                    xdim = 1;
                    ydim = _y / 6;

                    walls.AddComponent(new ConsoleSprite(wallPixels));
                    walls.AddComponent(new Transform(xdim, ydim, 1f));

                    xpos = (int)xdim;
                    xpos2 = (int)xdim;
                    ypos = (int)ydim;
                    ypos2 = (int)ydim;

                    // COLLIDERS
                    for (int x = 0; x < room.Dim.X; x++)
                    {
                        walls.AddComponent(new ObjectCollider(
                            new Vector2(xpos, ydim)));
                        xpos++;
                        //Console.Write($"Col {x}: {xpos}, {ydim}");
                    }
                    for (int x = 0; x < room.Dim.X; x++)
                    {
                        walls.AddComponent(new ObjectCollider(
                            new Vector2(xpos2, ydim + room.Dim.Y - 1)));
                        xpos2++;
                    }
                    for (int y = 0; y < room.Dim.Y; y++)
                    {
                        walls.AddComponent(new ObjectCollider(
                            new Vector2(xdim, ypos)));
                        ypos++;
                    }
                    for (int y = 0; y < room.Dim.Y; y++)
                    {
                        walls.AddComponent(new ObjectCollider(
                            new Vector2(xdim + room.Dim.X -1, ypos2)));
                        ypos2++;
                    }

                    aux = walls;
                    auxRoom = room;
                }
                else
                {
                    auxTrans = aux.GetComponent<Transform>();

                    // X of room is taken from the previus walls
                    // and room dimensions
                    xdim = Math.Clamp(
                            auxTrans.Pos.X + auxRoom.Dim.X - 1, 0, _x - 2);

                    // Y of room is taken from the previus walls and doors
                    // In relation with the center of the current room
                    ydim = auxTrans.Pos.Y + (auxRoom.Dim.Y / 2)
                        - (room.Dim.Y / 2);

                    // Make sure the room doesn't get out of bounds
                    if (auxTrans.Pos.X + auxRoom.Dim.X + room.Dim.X <= _x)
                    {
                        // Add the sprite and transform to assign position
                        walls.AddComponent(new ConsoleSprite(wallPixels));
                        walls.AddComponent(new Transform(xdim, ydim, 1f));

                        xpos = (int)xdim;
                        xpos2 = (int)xdim;
                        ypos = (int)ydim;
                        ypos2 = (int)ydim;

                        // COLLIDERS
                        for (int x = 0; x < room.Dim.X; x++)
                        {
                            walls.AddComponent(new ObjectCollider(
                                new Vector2(xpos, ydim)));
                            xpos++;
                        }
                        for (int x = 0; x < room.Dim.X; x++)
                        {
                            walls.AddComponent(new ObjectCollider(
                                new Vector2(xpos2, ydim + room.Dim.Y - 1)));
                            xpos2++;
                        }
                        for (int y = 0; y < room.Dim.Y; y++)
                        {
                            walls.AddComponent(new ObjectCollider(
                                new Vector2(xdim, ypos)));
                            ypos++;
                        }
                        for (int y = 0; y < room.Dim.Y; y++)
                        {
                            walls.AddComponent(new ObjectCollider(
                                new Vector2(xdim + room.Dim.X - 1, ypos2)));
                            ypos2++;
                        }
                    }
                    else
                    {
                        xdim = 1;
                        ydim = _y / 2;

                        walls.AddComponent(new ConsoleSprite(wallPixels));
                        walls.AddComponent(new Transform(xdim, ydim, 1f));

                        xpos = (int)xdim;
                        xpos2 = (int)xdim;
                        ypos = (int)ydim;
                        ypos2 = (int)ydim;

                        // COLLIDERS
                        for (int x = 0; x < room.Dim.X; x++)
                        {
                            walls.AddComponent(new ObjectCollider(
                                new Vector2(xpos, ydim)));
                            xpos++;
                        }
                        for (int x = 0; x < room.Dim.X; x++)
                        {
                            walls.AddComponent(new ObjectCollider(
                                new Vector2(xpos2, ydim + room.Dim.Y - 1)));
                            xpos2++;
                        }
                        for (int y = 0; y < room.Dim.Y; y++)
                        {
                            walls.AddComponent(new ObjectCollider(
                                new Vector2(xdim, ypos)));
                            ypos++;
                        }
                        for (int y = 0; y < room.Dim.Y; y++)
                        {
                            walls.AddComponent(new ObjectCollider(
                                new Vector2(xdim + room.Dim.X - 1, ypos2)));
                            ypos2++;
                        }
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
                    int enemyX = Convert.ToInt32
                        (wallTrans.Pos.X + (room.Dim.X / 2) + i);
                    int enemyY = Convert.ToInt32
                        (wallTrans.Pos.Y + (room.Dim.Y / 2));

                    room.Enemies[i].Name += i;
                    room.Enemies[i].Name += index;

                    room.Enemies[i].AddComponent(new ConsoleSprite(
                        enemy, ConsoleColor.White, ConsoleColor.Red));

                    room.Enemies[i].AddComponent(
                        new Transform(enemyX, enemyY, 2f));

                    room.Enemies[i].AddComponent(
                        new ObjectCollider());

                    room.Enemies[i].AddComponent(new EnemyController(_rnd));

                    scene.AddGameObject(room.Enemies[i]);
                }

                index++;
            }
        }
    }
}

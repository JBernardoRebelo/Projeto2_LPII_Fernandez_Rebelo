using System;
using GameEngine;
using System.Numerics;
using System.Collections.Generic;

namespace BootlegDiablo
{
    public class DungeonRoom : GameObject
    {
        // Vector2 for room dimension
        public Vector2 Dim { get; set; }

        // Collection of enemies
        public Enemy[] Enemies { get; private set; }

        // Doors in Room
        public DungeonDoor[] Doors { get; private set; }

        // Accepts a random seed to generate enemies and dimensions
        public DungeonRoom(Random rnd)
        {
            Dim = new Vector2(rnd.Next(10, 40), rnd.Next(2, 40));
            Enemies = new Enemy[rnd.Next(0, 5)];
            Doors = new DungeonDoor[rnd.Next(2, 4)];

            InstantiateEnemies();
            InstantiateDoors();

            Name = "Room";
        }

        // código morto
        public new void Update()
        {
            Dictionary<Vector2, ConsolePixel> wallPixels;

            GameObject wallS = new GameObject("Walls");

            ConsolePixel wallPixel =
                new ConsolePixel('i', ConsoleColor.Yellow, ConsoleColor.DarkRed);
            wallPixels = new Dictionary<Vector2, ConsolePixel>();

            for (int x = 0; x < Dim.X; x++)
            {
                wallPixels[new Vector2(x, 0)] = wallPixel;
            }
            for (int x = 0; x < Dim.X; x++)
            {
                wallPixels[new Vector2(x, Dim.Y - 1)] = wallPixel;
            }
            for (int y = 0; y < Dim.Y; y++)
            {
                wallPixels[new Vector2(0, y)] = wallPixel;
            }
            for (int y = 0; y < Dim.Y; y++)
            {
                wallPixels[new Vector2(Dim.X - 1, y)] = wallPixel;
            }
            wallS.AddComponent(new ConsoleSprite(wallPixels));
            wallS.AddComponent(new Transform(0, 0, 1));

            ParentScene.AddGameObject(wallS);
        }

        /// <summary>
        /// Instantiates doors in room
        /// </summary>
        private void InstantiateDoors()
        {
            DungeonDoor temp = null;

            // Instantiate doors
            for (int i = 0; i < Doors.Length; i++)
            {
                if (temp != null)
                {
                    if (temp.Transform.Pos.X > 0) // Door on the left
                    {
                        Doors[i] = new DungeonDoor
                            (0, Convert.ToInt32(Dim.Y) / 2);
                    }
                    if (temp.Transform.Pos.Y > 0) // Door on top
                    {
                        Doors[i] = new DungeonDoor
                            (Convert.ToInt32(Dim.X) / 2, 0);
                    }
                }

                temp = Doors[i];
            }
        }

        /// <summary>
        /// Instantiate enemies in array
        /// </summary>
        private void InstantiateEnemies()
        {
            // Instantiate enemies
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i] = new EnemySkeleton();
            }
        }
    }
}

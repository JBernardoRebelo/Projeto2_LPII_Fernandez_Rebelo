using System;
using GameEngine;
using System.Numerics;

namespace BootlegDiablo
{
    public class DungeonRoom : GameObject
    {
        /// <summary>
        /// Vector2 for room dimension
        /// </summary>
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

            InstantiateEnemies(rnd);
            InstantiateDoors();

            Name = "Room";
        }

        /// <summary>
        /// Instantiates doors in room
        /// </summary>
        private void InstantiateDoors()
        {
            // Instantiate doors
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i] = new DungeonDoor();
            }
        }

        /// <summary>
        /// Instantiate enemies in array
        /// </summary>
        private void InstantiateEnemies(Random random)
        {
            // Instantiate enemies
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i] = new EnemySkeleton(random);
            }
        }
    }
}

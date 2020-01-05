using System;
using GameEngine;
using System.Numerics;

namespace BootlegDiablo
{
    /// <summary>
    /// DungeonRoom, inherits from GameObject, Dungeon has a collection of this
    /// </summary>
    public class DungeonRoom : GameObject
    {
        /// <summary>
        /// Vector2 for room dimension
        /// </summary>
        public Vector2 Dim { get; private set; }

        /// <summary>
        /// Array of Enemy
        /// </summary>
        public Enemy[] Enemies { get; private set; }

        /// <summary>
        /// Array of Doors
        /// </summary>
        public DungeonDoor[] Doors { get; private set; }

        /// <summary>
        /// DungeonRoom Constructor, instantiates all enemies and doors
        /// in itself as it's position
        /// </summary>
        /// <param name="rnd"> Accepts a Random to use in dimensions,
        /// enemy and door creation </param>
        public DungeonRoom(Random rnd)
        {
            Dim = new Vector2(rnd.Next(10, 40), rnd.Next(4, 17));
            Enemies = new Enemy[rnd.Next(1, 5)];
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

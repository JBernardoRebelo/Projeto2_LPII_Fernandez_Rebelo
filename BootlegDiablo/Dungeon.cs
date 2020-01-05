using System;
using GameEngine;

namespace BootlegDiablo
{
    public class Dungeon : GameObject
    {
        // Collection of rooms in dungeon
        public DungeonRoom[] Rooms { get; private set; }

        // Accepts a seed to instantiate rooms
        public Dungeon(int nRooms, Random rnd)
        {
            Rooms = new DungeonRoom[nRooms];

            for (int i = 0; i < nRooms; i++)
            {
                Rooms[i] = new DungeonRoom(rnd);
                Rooms[i].AddComponent(new Transform(rnd.Next(3, 15),
                    rnd.Next(3, 50), 0));
            }
            Name = "Dungeon";
        }

        /// <summary>
        /// Dungeon update with base, checks if enemies exist,
        /// if not, terminate game
        /// </summary>
        public override void Update()
        {
            base.Update();

            if (CheckAliveDead())
            {
                // End game, victory screen
                ParentScene.Terminate();
            }
        }

        /// <summary>
        /// Go through all rooms in dungeon to check if everyone is dead
        /// </summary>
        /// <returns> Returns true if everyone is dead </returns>
        private bool CheckAliveDead()
        {
            int deadCount = 0;
            int totalEnemies = 0;

            // Check all rooms and all enemies
            for (int i = 0; i < Rooms.Length; i++)
            {
                for (int j = 0; j < Rooms[i].Enemies.Length; j++)
                {
                    if (Rooms[i].Enemies[j].HP <= 0)
                    {
                        // Is dead
                        deadCount++;
                    }
                    totalEnemies++;
                }
            }

            // If all enemies are dead
            if (deadCount >= totalEnemies)
            {
                // Empty dungeon
                return true;
            }

            return false;
        }
    }
}

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
        public DungeonDoor[] Doors { get; private set; }

        // Collection of enemies
        public Enemy[] Enemies { get; private set; }
        // Doors in Room

        // Accepts a random seed to generate enemies and dimensions
        public DungeonRoom(int seed)
        {
            // Debug
            Dim = new Vector2(5, 7);
            Enemies = new Enemy[2];
            DungeonDoor door1 = new DungeonDoor(5 / 2, 0);
            DungeonDoor door2 = new DungeonDoor(0, 7 / 2);
            // **

            // Add door to dungeonRoom
            Doors = new DungeonDoor[2] { door1, door2 };

            Name = "Room";
        }

        // Must have a renderable
    }
}

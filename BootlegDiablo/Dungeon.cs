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
                Rooms[i].AddComponent(new Transform(rnd.Next(1, 50), rnd.Next(1, 50), 0));
            }

            Name = "Dungeon";
        }
    }
}

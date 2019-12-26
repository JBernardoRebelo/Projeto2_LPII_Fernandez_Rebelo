using GameEngine;

namespace BootlegDiablo
{
    public class Dungeon : GameObject
    {
        // Collection of rooms in dungeon
        public DungeonRoom[] Rooms { get; private set; }

        // Accepts a seed to instantiate rooms
        public Dungeon(int seed)
        {
            int nRooms = seed;

            Rooms = new DungeonRoom[nRooms];

            for (int i = 0; i < nRooms; i++)
            {
                Rooms[i] = new DungeonRoom(seed);
            }

            Name = "Dungeon";
        }
    }
}

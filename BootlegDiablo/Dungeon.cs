using GameEngine;

namespace BootlegDiablo
{
    public class Dungeon : GameObject
    {
        // Collection of rooms in dungeon
        public DungeonRoom[] Rooms { get; private set; }

        // Accepts a seed to instantiate rooms
        public Dungeon(int nRooms)
        {
            Rooms = new DungeonRoom[nRooms];
            Name = "Dungeon";
        }
    }
}

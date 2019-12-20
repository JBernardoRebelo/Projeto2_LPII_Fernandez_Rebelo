using System.Collections.Generic;

namespace BootlegDiablo
{
    public class Dungeon
    {
        // Collection of rooms in dungeon
        public ICollection<DungeonRoom> rooms { get; set; }

        // Accepts a seed to instantiate rooms
        public Dungeon(int nRooms)
        {

        }
    }
}

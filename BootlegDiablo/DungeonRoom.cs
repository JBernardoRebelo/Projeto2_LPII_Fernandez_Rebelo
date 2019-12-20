using System.Numerics;
using System.Collections.Generic;

namespace BootlegDiablo
{
    public class DungeonRoom
    {
        // Vector2 for room dimension
        public Vector2 Dim { get; set; }

        // Collection of enemies
        public ICollection<Enemy> Enemies { get; set; }

        // Accepts a random seed to generate enemies and dimensions
        public DungeonRoom(int seed)
        {

        }
    }
}

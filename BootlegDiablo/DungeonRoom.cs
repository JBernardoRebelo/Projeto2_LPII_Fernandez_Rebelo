using GameEngine;
using System.Numerics;

namespace BootlegDiablo
{
    public class DungeonRoom : GameObject
    {
        // Vector2 for room dimension
        public Vector2 Dim { get; set; }

        // Collection of enemies
        public Enemy[] Enemies { get; private set; }

        // Accepts a random seed to generate enemies and dimensions
        public DungeonRoom()
        {
            // Debug
            Dim = new Vector2(5, 7);
            Enemies = new Enemy[2];
            // **
        }
    }
}

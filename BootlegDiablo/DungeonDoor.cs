using GameEngine;
using System.Numerics;

namespace BootlegDiablo
{
    public class DungeonDoor : GameObject
    {
        public Transform Transform { get; set; }

        /// <summary>
        /// Door constructor, accepts coordenates to be placed
        /// </summary>
        /// <param name="x"> x coordinate to be placed </param>
        /// <param name="y"> y coordinate </param>
        public DungeonDoor(int x, int y)
        {
            Transform = new Transform(x, y, 0);
            Name = "Door";
        }
    }
}
using GameEngine;

namespace BootlegDiablo
{
    /// <summary>
    /// DungeonDoor class, inherits from GameObject, rooms have an array of
    /// this to use
    /// </summary>
    public class DungeonDoor : GameObject
    {
        /// <summary>
        /// Door constructor, assigns name
        /// </summary>
        public DungeonDoor()
        {
            Name = "Door";
        }
    }
}
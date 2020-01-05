using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    /// <summary>
    /// Class that manages game object's sprite to be rendered on screen
    /// </summary>
    public abstract class RenderableComponent : Component
    {
        /// <summary>
        /// Collection of pixel that compose a game object's sprite and its 
        /// position
        /// </summary>
        public abstract 
            IEnumerable<KeyValuePair<Vector2, ConsolePixel>> Pixels 
        { get; }
    }
}

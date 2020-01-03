using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    public abstract class RenderableComponent : Component
    {
        public abstract 
            IEnumerable<KeyValuePair<Vector2, ConsolePixel>> Pixels 
        { get; }
    }
}

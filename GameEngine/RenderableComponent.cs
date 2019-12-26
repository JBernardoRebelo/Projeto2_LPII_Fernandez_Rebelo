using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    class RenderableComponent : Component
    {
        public IEnumerable<KeyValuePair<Vector2, ConsolePixel>> Pixels 
        { get; internal set; }
    }
}

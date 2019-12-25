using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    class RenderableComponent
    {
        public IEnumerable<KeyValuePair<Vector2, ConsolePixel>> Pixels 
        { get; internal set; }
    }
}

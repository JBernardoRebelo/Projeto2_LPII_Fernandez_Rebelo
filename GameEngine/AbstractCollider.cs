using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    public abstract class AbstractCollider : Component
    {
        public IEnumerable<Vector2> Occupied { get; set; }
    }
}

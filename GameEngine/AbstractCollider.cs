using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    /// <summary>
    /// Abstract class to be used by colliders
    /// </summary>
    public abstract class AbstractCollider : Component
    {
        /// <summary>
        /// IEnumerable o occupied positions
        /// </summary>
        public abstract IEnumerable<Vector2> Occupied { get; }
    }
}

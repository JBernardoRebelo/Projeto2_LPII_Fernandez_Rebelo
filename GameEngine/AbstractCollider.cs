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
        /// Collider position
        /// </summary>
        public abstract Vector2 ColPos { get; set; }
    }
}

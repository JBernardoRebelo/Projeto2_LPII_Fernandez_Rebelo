using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class SpriteCollider : AbstractCollider
    {
        /// <summary>
        /// 
        /// </summary>
        public override IEnumerable<Vector2> Occupied => null; // TODO

        /// <summary>
        /// 
        /// </summary>
        private ConsoleSprite sprite;

        /// <summary>
        /// 
        /// </summary>
        public override void Start()
        {
            sprite = ParentGameObject.GetComponent<ConsoleSprite>();
            if (sprite == null)
            {
                throw new InvalidOperationException(
                    $"The {nameof(SpriteCollider)} component " +
                    "requires a {nameof(ConsoleSprite)} component");
            }
        }

    }
}

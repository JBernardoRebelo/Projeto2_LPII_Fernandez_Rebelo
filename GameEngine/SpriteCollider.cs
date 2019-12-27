using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    public class SpriteCollider : AbstractCollider
    {
        public override IEnumerable<Vector2> Occupied => null; // TODO

        private ConsoleSprite sprite;

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

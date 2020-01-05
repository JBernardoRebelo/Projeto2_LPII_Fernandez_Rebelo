using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameEngine
{
    public class ObjectCollider : AbstractCollider
    {
        /// <summary>
        /// 
        /// </summary>
        public override Vector2 ColPos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Transform _transform;

        /// <summary>
        /// 
        /// </summary>
        public ObjectCollider() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        public ObjectCollider(Vector2 pos)
        {
            ColPos = pos;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Start()
        {
            _transform = ParentGameObject.GetComponent<Transform>();

            if (_transform == null)
            {
                throw new InvalidOperationException(
                    $"The {nameof(ObjectCollider)} component " +
                    $"requires a {nameof(Transform)} component");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Update()
        {
            ColPos = new Vector2(
                (int) _transform.Pos.X,
                (int) _transform.Pos.Y);
        }
    }
}

﻿using System;
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
        public override Transform PrevPos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Transform _transform;

        private readonly bool _update;

        /// <summary>
        /// 
        /// </summary>
        public ObjectCollider()
        {
            ColPos = new Vector2(0, 1);
            _update = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        public ObjectCollider(Vector2 pos, bool update = false)
        {
            ColPos = pos;
            _update = update;
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
            PrevPos = new Transform(ColPos.X, ColPos.Y, _transform.Pos.Z);

            if (_update)
            {
                ColPos = new Vector2(
                (int)_transform.Pos.X,
                (int)_transform.Pos.Y);
            }

            if (Colliding)
            {
                _transform = PrevPos;
                Colliding = false;
            }

        }
    }
}
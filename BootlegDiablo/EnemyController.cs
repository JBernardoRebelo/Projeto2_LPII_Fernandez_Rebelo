using System;
using System.Numerics;
using GameEngine;

namespace BootlegDiablo
{
    /// <summary>
    /// Class that is responsible for enemy actions
    /// </summary>
    public class EnemyController : Component
    {
        /// <summary>
        /// Enemy Transform information
        /// </summary>
        private Transform _transform;

        private Vector2 _prevPos;

        /// <summary>
        /// Parent game object information
        /// </summary>
        private Enemy _parent;

        /// <summary>
        /// Instance of random that is passed through in the constructor, 
        /// to be used for the choice of actions
        /// </summary>
        private Random _rndm;

        private ObjectCollider _collider;

        /// <summary>
        /// Constructor for the EnemyController
        /// </summary>
        /// <param name="rndm"> Random instance </param>
        public EnemyController(Random rndm)
        {
            _rndm = rndm;
        }

        /// <summary>
        /// Get information of parent game object
        /// </summary>
        public override void Start()
        {
            _transform = ParentGameObject.GetComponent<Transform>();
            _collider = ParentGameObject.GetComponent<ObjectCollider>();
            _parent = ParentGameObject as Enemy;
        }

        /// <summary>
        /// Method that controls enemy actions, such as movement and attack
        /// </summary>
        public override void Update()
        {
            int state = _rndm.Next(40);
            float x = _transform.Pos.X;
            float y = _transform.Pos.Y;


            if (!_collider.Colliding)
            {
                _prevPos = new Vector2(x, y);

                switch (state)
                {
                    // Enemy goes up
                    case 0:
                        y -= 1f;
                        break;

                    // Enemy goes down
                    case 1:
                        y += 1f;
                        break;

                    // Enemy goes right
                    case 2:
                        x += 1f;
                        break;

                    // Enemy goes left
                    case 3:
                        x -= 1f;
                        break;

                    // Use Enemy attack method
                    case 4:
                        _parent.Attack();
                        break;

                    case 5:
                        _parent.Attack();
                        break;

                    case 6:
                        _parent.Attack();
                        break;
                }

                // Make sure enemy doesn't get outside of dungeon area
                x = Math.Clamp(x, 1, ParentScene.xdim - 3);
                y = Math.Clamp(y, 1, ParentScene.ydim - 3);

                // Update enemy position
                _transform.Pos = new Vector3(x, y, _transform.Pos.Z);
            }

            else if (_collider.Colliding)
            {
                _collider.ColPos = new Vector2(_prevPos.X, _prevPos.Y);
                _transform.Pos = new Vector3(_prevPos, _transform.Pos.Z);
                _collider.Colliding = false;
            }
        }
    }
}

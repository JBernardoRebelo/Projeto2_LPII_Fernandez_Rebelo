using System;
using GameEngine;
using System.Numerics;

namespace BootlegDiablo
{
    /// <summary>
    /// Class to handle player inputs and movement
    /// </summary>
    public class PlayerController : Component
    {
        /// <summary>
        /// Class instance of Key Observer to store player Key Observer 
        /// component.
        /// </summary>
        private KeyObserver _keyObserver;

        /// <summary>
        /// Class variable of Transform to store player 
        /// Transform component's information.
        /// </summary>
        private Transform _transform;

        private Vector2 _prevPos;

        /// <summary>
        /// Class variable of Render to use said class methods.
        /// </summary>
        private Render _rndr;

        private Player _player;

        private ObjectCollider _collider;

        /// <summary>
        /// Initialize player controller
        /// </summary>
        public override void Start()
        {
            _player = ParentGameObject as Player;
            _keyObserver = _player.GetComponent<KeyObserver>();
            _transform = _player.GetComponent<Transform>();
            _collider = _player.GetComponent<ObjectCollider>();
            _rndr = new Render();
        }

        /// <summary>
        /// Override of Update method to check input every frame and alter 
        /// player's position.
        /// </summary>
        public override void Update()
        {
            // Get player position
            float x = _transform.Pos.X;
            float y = _transform.Pos.Y;

            if (!_collider.Colliding)
            {
                _prevPos = new Vector2(x, y);

                // Check what keys were pressed and update position accordingly
                foreach (ConsoleKey key in _keyObserver.GetCurrentKeys())
                {
                    switch (key)
                    {
                        // Player goes up
                        case ConsoleKey.W:
                            y -= 1;
                            break;

                        // Player goes down
                        case ConsoleKey.S:
                            y += 1;
                            break;

                        // Player goes right
                        case ConsoleKey.D:
                            x += 1;
                            break;

                        // Player goes left
                        case ConsoleKey.A:
                            x -= 1;
                            break;

                        // Show player's stats and equipment
                        case ConsoleKey.C:
                            _rndr.CharInformationScreen(_player);
                            break;

                        // Use player attack method
                        case ConsoleKey.Spacebar:
                            _player.Attack();
                            break;

                        // Open a door
                        case ConsoleKey.E:
                            _player.OpenDoor(out x, (int)x, out y, (int)y);
                            break;
                    }
                }
            }
            // Make sure player doesn't get outside of dungeon area
            x = Math.Clamp(x, 1, ParentScene.xdim - 3);
            y = Math.Clamp(y, 1, ParentScene.ydim - 3);

            // Show essential information
            _rndr.EssencialInfo(_player);

            // Update player position
            _transform.Pos = new Vector3(x, y, _transform.Pos.Z);

            if (_collider.Colliding)
            {
                _collider.ColPos = new Vector2(_prevPos.X, _prevPos.Y);
                _transform.Pos = new Vector3(_prevPos, _transform.Pos.Z);
                _collider.Colliding = false;
            }
        }
    }
}

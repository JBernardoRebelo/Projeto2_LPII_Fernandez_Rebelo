using System;
using GameEngine;
using System.Numerics;

namespace BootlegDiablo
{
    public class PlayerController : Component
    {
        private KeyObserver _keyObserver;
        private Transform _transform;
        private Player _player;
        private Render _rndr;

        // Update player in the current frame
        public new void Update()
        {
            // Get player position
            float x = _transform.Pos.X;
            float y = _transform.Pos.Y;

            // Check what keys were pressed and update position accordingly
            foreach (ConsoleKey key in _keyObserver.GetCurrentKeys())
            {
                switch (key)
                {
                    case ConsoleKey.W:
                        y -= 1;
                        break;
                    case ConsoleKey.S:
                        y += 1;
                        break;
                    case ConsoleKey.D:
                        x += 1;
                        break;
                    case ConsoleKey.A:
                        x -= 1;
                        break;
                    case ConsoleKey.C:
                        _rndr.CharInformationScreen(_player);
                        break;
                }
            }

            // Make sure player doesn't get outside of dungeon area
            x = Math.Clamp(x, 0, ParentScene.xdim - 3);
            y = Math.Clamp(y, 0, ParentScene.ydim - 3);

            // Attack check

            // Update player position
            _transform.Pos = new Vector3(x, y, _transform.Pos.Z);
        }
    }
}

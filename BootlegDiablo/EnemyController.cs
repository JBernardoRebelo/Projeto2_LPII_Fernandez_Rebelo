using System;
using System.Numerics;
using GameEngine;

namespace BootlegDiablo
{
    public class EnemyController : Component
    {
        private Transform _transform;

        private Random _rndm;

        public EnemyController(Random rndm)
        {
            _rndm = rndm;
        }

        public override void Start()
        {
            _transform = ParentGameObject.GetComponent<Transform>();
        }

        public override void Update()
        {
            int state = _rndm.Next(5);
            float x = _transform.Pos.X;
            float y = _transform.Pos.Y;
            Enemy parent = ParentGameObject as Enemy;

            switch (state)
            {
                case 0:
                    y -= 0.5f;
                    break;

                // Enemy goes down
                case 1:
                    y += 0.5f;
                    break;

                // Enemy goes right
                case 2:
                    x += 0.5f;
                    break;

                // Enemy goes left
                case 3:
                    x -= 0.5f;
                    break;

                // Use Enemy attack method
                case 4:
                    // Implement enemy attack
                    //parent.Attack();
                    break;
            }

            x = Math.Clamp(x, 1, ParentScene.xdim - 3);
            y = Math.Clamp(y, 1, ParentScene.ydim - 3);

            // Update player position
            _transform.Pos = new Vector3(x, y, _transform.Pos.Z);
        }
    }
}

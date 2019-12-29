using System;
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
                    y -= 1;
                    break;

                // Player goes down
                case 1:
                    y += 1;
                    break;

                // Player goes right
                case 2:
                    x += 1;
                    break;

                // Player goes left
                case 3:
                    x -= 1;
                    break;

                // Use player attack method
                case 4:
                    // Implement player attack
                    //parent.Attack();
                    break;
            }
        }
    }
}

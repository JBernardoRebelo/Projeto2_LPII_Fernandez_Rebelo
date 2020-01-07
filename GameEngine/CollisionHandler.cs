using System;
using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// Class 
    /// </summary>
    public class CollisionHandler
    {
        /// <summary>
        /// 
        /// </summary>
        private int xdim;

        /// <summary>
        /// 
        /// </summary>
        private int ydim;

        /// <summary>
        /// 
        /// </summary>
        private readonly GameObject[,] collisionMap;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xdim"> Game Scene</param>
        /// <param name="ydim"></param>
        public CollisionHandler(int xdim, int ydim)
        {
            this.xdim = xdim;
            this.ydim = ydim;
            collisionMap = new GameObject[xdim, ydim];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObjects"></param>
        public void Update(IEnumerable<GameObject> gameObjects)
        {
            Array.Clear(collisionMap, 0, collisionMap.Length);

            foreach (GameObject gObj in gameObjects)
            {
                if (gObj.IsCollidable)
                {
                    // Array de colliders e percorrê-los
                    foreach (Component component in gObj)
                    {
                        if (component is AbstractCollider)
                        {
                            AbstractCollider collider = 
                                component as AbstractCollider;

                            int x = (int)collider.ColPos.X;
                            int y = (int)collider.ColPos.Y;

                            // Check if object hasn't even got positioned
                            // correctly
                            if (x == 0 && y == 0)
                            {
                                Console.WriteLine(gObj.Name);
                                continue;
                            }

                            // Throw exception if any of these is out of bounds
                            if (x < 0 || x >= xdim || y < 0 || y >= ydim)
                            {
                                throw new InvalidOperationException(
                                    $"Out of bounds pixel at ({x},{y}) in game"
                                    + $" object '{gObj.Name}'");
                            }

                            // If position is occupied by another game object
                            if (collisionMap[x, y] != null)
                            {
                                collider.Colliding = true;
                            }
                            else
                            {
                                // Set coordinate as occupied by the current 
                                // game object
                                collisionMap[x, y] = gObj;
                            }
                        }
                    }

                }
            }
        }
    }
}

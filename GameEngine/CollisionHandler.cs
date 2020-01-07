using System;
using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// Class responsible to check for collisions between game objects
    /// </summary>
    public class CollisionHandler
    {
        /// <summary>
        /// Scene x dimension where CollisionHandler exists
        /// </summary>
        private int xdim;

        /// <summary>
        /// Scene y dimension where CollisionHandler exists
        /// </summary>
        private int ydim;

        /// <summary>
        /// Internal bi-dimensional array to store game objects positions
        /// </summary>
        private readonly GameObject[,] collisionMap;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="xdim"> Game Scene x dimension </param>
        /// <param name="ydim"> Game Scene y dimension </param>
        public CollisionHandler(int xdim, int ydim)
        {
            this.xdim = xdim;
            this.ydim = ydim;
            collisionMap = new GameObject[xdim, ydim];
        }

        /// <summary>
        /// Update method to handle colisions between objects in a scene
        /// </summary>
        /// <param name="gameObjects"> Collection of game objects </param>
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

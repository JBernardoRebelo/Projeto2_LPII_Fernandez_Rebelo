using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    /// <summary>
    /// 
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
        private readonly GameObject[,] prevCollisionMap;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xdim"></param>
        /// <param name="ydim"></param>
        public CollisionHandler(int xdim, int ydim)
        {
            this.xdim = xdim;
            this.ydim = ydim;
            collisionMap = new GameObject[xdim, ydim];
            prevCollisionMap = new GameObject[xdim, ydim];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObjects"></param>
        public void Update(IEnumerable<GameObject> gameObjects)
        {

            for (int y = 0; y < collisionMap.Length; y++)
            {
                for (int x = 0; x < collisionMap.Length; x++)
                {
                    prevCollisionMap[x, y] = collisionMap[x, y];
                }
            }

            Array.Clear(collisionMap, 0, collisionMap.Length);

            foreach (GameObject gObj in gameObjects)
            {
                if (gObj.IsCollidable)
                {
                    AbstractCollider collider = gObj.GetComponent<
                        AbstractCollider>();

                    Transform transform = gObj.GetComponent<Transform>();

                    int x = (int)(collider.ColPos.X);
                    int y = (int)(collider.ColPos.Y);

                    // Throw exception if any of these is out of bounds
                    if (x < 0 || x >= xdim || y < 0 || y >= ydim)
                    {
                        throw new InvalidOperationException(
                            $"Out of bounds pixel at ({x},{y}) in game"
                            + $" object '{gObj.Name}'");
                    }

                    // Throw exception if position in collision map already
                    // contains a game object
                    if (collisionMap[x, y] != null)
                    {
                        prevCollisionMap.GetValue(collisionMap[x, y].GetHashCode());
                        throw new InvalidOperationException(
                            "Unable to specify coordinate as occupied by "
                            + $"'{gObj.Name}' since it is previously "
                            + $"occupied by '{collisionMap[x, y].Name}'");
                    }
                    else
                    {
                        // Set coordinate as occupied by the current game object
                        collisionMap[x, y] = gObj;
                    }

                }
            }

            Array.Clear(prevCollisionMap, 0, prevCollisionMap.Length);

        }
    }
}

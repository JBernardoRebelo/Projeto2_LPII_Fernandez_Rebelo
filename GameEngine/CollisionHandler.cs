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
        /// <param name="xdim"></param>
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
            int x;
            int y;

            AbstractCollider collider;
            Transform transform;

            Array.Clear(collisionMap, 0, collisionMap.Length);

            foreach (GameObject gObj in gameObjects)
            {
                if (gObj.IsCollidable)
                {
                    collider = gObj.GetComponent<AbstractCollider>();
                    transform = gObj.GetComponent<Transform>();

                    // Array de colliders e percorrê-los

                    collider.Colliding = false;

                    x = (int)collider.ColPos.X;
                    y = (int)collider.ColPos.Y;

                    Vector2 gObjLeft = new Vector2(x - 1, y);
                    Vector2 gObjRight = new Vector2(x + 1, y);
                    Vector2 gObjDown = new Vector2(x, y + 1);
                    Vector2 gObjUp = new Vector2(x, y - 1);

                    if (x == 0 && y == 0)
                    {
                        Console.WriteLine(gObj.Name);
                        //break;
                    }

                    // Throw exception if any of these is out of bounds
                    if (x < 0 || x >= xdim || y < 0 || y >= ydim)
                    {
                        throw new InvalidOperationException(
                            $"Out of bounds pixel at ({x},{y}) in game"
                            + $" object '{gObj.Name}'");
                    }

                    if(collisionMap[x,y] != null)
                    {
                        collider.Colliding = true;

                        // Warn collider that it is colliding
                        Console.WriteLine(
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
        }
    }
}

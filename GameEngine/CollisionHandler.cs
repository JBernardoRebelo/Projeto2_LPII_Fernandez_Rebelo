using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    public class CollisionHandler
    {
        private int xdim, ydim;

        private readonly GameObject[,] collisionMap;

        public CollisionHandler(int xdim, int ydim)
        {
            this.xdim = xdim;
            this.ydim = ydim;
            collisionMap = new GameObject[xdim, ydim];
        }

        public void Update(IEnumerable<GameObject> gameObjects)
        {
            Array.Clear(collisionMap, 0, collisionMap.Length);
            foreach (GameObject gObj in gameObjects)
            {
                if (gObj.IsCollidable)
                {
                    AbstractCollider collider = gObj.GetComponent<
                        AbstractCollider>();
                    Transform position = gObj.GetComponent<Transform>();
                    foreach (Vector2 occupied in collider.Occupied)
                    {
                        int x = (int)(position.Pos.X + occupied.X);
                        int y = (int)(position.Pos.Y + occupied.Y);

                        // Throw exception if any of these is out of bounds
                        if (x < 0 || x >= xdim || y < 0 || y >= ydim)
                            throw new InvalidOperationException(
                                $"Out of bounds pixel at ({x},{y}) in game"
                                + $" object '{gObj.Name}'");

                        // Throw exception if position in collision map already
                        // contains a game object
                        if (collisionMap[x, y] != null)
                            throw new InvalidOperationException(
                                "Unable to specify coordinate as occupied by "
                                + $"'{gObj.Name}' since it is previously "
                                + $"occupied by '{collisionMap[x, y].Name}'");

                        // Set coordinate as occupied by the current game object
                        collisionMap[x, y] = gObj;
                    }
                }
            }

        }
    }
}

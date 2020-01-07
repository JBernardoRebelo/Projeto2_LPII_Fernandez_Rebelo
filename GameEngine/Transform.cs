using System.Numerics;

namespace GameEngine
{
    /// <summary>
    /// Class that deals with the game object's position
    /// </summary>
    public class Transform : Component
    {
        /// <summary>
        /// Property that contains position (x,y) and buffer order (z)
        /// </summary>
        public Vector3 Pos { get; set; }
        /// <summary>
        /// Constructor used to set a game object's position and render order
        /// </summary>
        /// <param name="x"> X position value </param>
        /// <param name="y"> Y postion value </param>
        /// <param name="z"> Z render buffer value </param>
        public Transform(float x, float y, float z)
        {
            Pos = new Vector3(x, y, z);
        }
    }
}

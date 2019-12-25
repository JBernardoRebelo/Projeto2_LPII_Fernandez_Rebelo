using System.Numerics;

namespace GameEngine
{
    class Transform
    {
        public Vector3 Pos { get; set; }

        public Transform()
        {
            Pos = new Vector3(0f, 0f, 0f);
        }

        public Transform(float x, float y, float z)
        {
            Pos = new Vector3(x, y, z);
        }
    }
}

using System;
using GameEngine;
using System.Numerics;

namespace BootlegDiablo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            /*
            // Call gameloop

            // Debug
            Render rnd = new Render();
            rnd.DisplayLogo();
            // **
            */

            Vector3 v3 = new Vector3(2);

            Console.WriteLine(v3);

            v3.Y = 9;

            Console.WriteLine(v3);

        }
    }
}

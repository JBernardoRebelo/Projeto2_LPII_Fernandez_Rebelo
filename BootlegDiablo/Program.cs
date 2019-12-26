using GameEngine;

namespace BootlegDiablo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            GameLoop _gameLoop = new GameLoop();

            // Call gameloop
            _gameLoop.Start();
        }
    }
}

namespace BootlegDiablo
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method, instantiates game to call it's start()
        /// </summary>
        private static void Main()
        {
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;

            Game _gameLoop = new Game();

            // Call gameloop
            _gameLoop.Start();
        }
    }
}

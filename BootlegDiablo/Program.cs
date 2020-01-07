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
            // Setup output encoding
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;

            // Create instance of game
            Game _gameLoop = new Game();

            // Call gameloop
            _gameLoop.Start();
        }
    }
}

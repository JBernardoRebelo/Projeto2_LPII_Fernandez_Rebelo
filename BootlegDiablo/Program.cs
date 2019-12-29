namespace BootlegDiablo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;

            Game _gameLoop = new Game();

            // Call gameloop
            _gameLoop.Start();
        }
    }
}

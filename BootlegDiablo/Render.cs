using System;
using System.IO;

namespace BootlegDiablo
{
    public class Render
    {
        public void DisplayLogo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(File.ReadAllText("LogoDiablo.txt"));
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}

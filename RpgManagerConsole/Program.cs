using Microsoft.Win32.SafeHandles;
using RpgManagerLibrary;

namespace RpgManagerConsole
{
    internal class Program
    {
        private static Character Character = default!;
        static void Main(string[] args)
        {
            Character = new Mage("Wim", 100, DateTime.Now, 9999, 10);

            Console.ForegroundColor = ConsoleColor.White;
            ConsoleHelper.DrawBox(Character.ToString(), doubleLines: true, fullWidth: true);
            DrawTitle();

            ConsoleHelper.DrawBox("Menu", padding: 3);
            Console.WriteLine("1 - Heal");
            Console.WriteLine("2 - Damage");
        }

        private static void DrawTitle()
        {
            Console.SetCursorPosition(2, 0);
            Console.Write("╡");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("RPG Manager");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("╞");
            Console.ResetColor();
            Console.SetCursorPosition(0, 5);
        }

        /// <summary>
        /// Print details van het personage (ToString) bovenaan de console.
        /// </summary>
        private static void ConsoleWriteStats()
        {
            Console.SetCursorPosition(0, 0);
            //Console.WriteLine(Character);
            ConsoleHelper.DrawBox(Character.ToString(), doubleLines: true);
        }
    }
}

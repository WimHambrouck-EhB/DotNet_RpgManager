using RpgManagerLibrary;

namespace RpgManagerConsole
{
    internal class Program
    {
        private static Character Character = default!;
        static void Main(string[] args)
        {
            Character = new() { 
                Name = "Wim",
                PowerLevel = 9999, // over 9000
            };

            Console.WriteLine(Character);
        }
    }
}

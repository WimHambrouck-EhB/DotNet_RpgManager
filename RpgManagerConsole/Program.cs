using RpgManagerLibrary;

namespace RpgManagerConsole
{
    internal class Program
    {
        private static Character Character = default!;
        static void Main(string[] args)
        {
            Character = new Warrior() { 
                Name = "Wim",
                PowerLevel = 9999, // over 9000
            };

            ((Warrior)Character).Weapons.Add("Sword");
            ((Warrior)Character).Weapons.Add("Shield");


            Console.WriteLine(Character);
        }
    }
}

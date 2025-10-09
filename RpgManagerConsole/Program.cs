using RpgManagerLibrary;

namespace RpgManagerConsole
{
    internal class Program
    {
        private static Character Character = default!;
        static void Main(string[] args)
        {
            Character = new Mage() { 
                Name = "Wim",
                PowerLevel = 9999, // over 9000
            };

            try
            {
                Character.Damage(1000);
            }
            catch (DamageTooHighException e)
            {
                Console.WriteLine("Something went wrong: " + e.Message);
            }


            Console.WriteLine(Character);
        }
    }
}

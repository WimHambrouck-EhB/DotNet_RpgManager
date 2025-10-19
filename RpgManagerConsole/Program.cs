using Microsoft.Win32.SafeHandles;
using RpgManagerLibrary;
using System.Xml;

namespace RpgManagerConsole
{
    internal class Program
    {
        private static Character Character = default!;
        static void Main(string[] args)
        {
            Character = new Mage("Wim", 100, DateTime.Now, 9999, 10);

            bool continueApp = true;
            while (continueApp)
            {
                Console.Clear();
                WriteHeader();

                ConsoleHelper.DrawBox("Menu");
                switch (ConsoleHelper.Menu(["Heal", "Damage"], "Quit"))
                {
                    case 1:
                        HealCharacter();
                        break;
                    case 2:
                        DamageCharacter();
                        break;
                    default:
                        continueApp = false;
                        break;
                }
            }

            Console.ResetColor();
            Console.WriteLine("Bye!");
        }

        private static void DamageCharacter()
        {
            bool continueLoop = true;

            while (continueLoop)
            {
                int damageAmount = ConsoleHelper.ReadInt("Enter amount of damage [1 - 1000]: ", 1, 1000);
                try
                {
                    Character.Damage(damageAmount);
                    continueLoop = false;
                }
                catch (DamageTooHighException e)
                {
                    ConsoleHelper.ShowError(e.Message);
                    char retry = ConsoleHelper.ReadChar("Do you want to try again? (Y/N): ");
                    Console.WriteLine();
                    if (retry != 'Y')
                    {
                        continueLoop = false;
                    }
                }
            }


        }

        private static void HealCharacter()
        {
            int healAmount = ConsoleHelper.ReadInt("Enter amount to heal [1 - 1000]: ", 1, 1000);
            Character.Heal(healAmount);
        }

        private static void DrawTitle()
        {
            Console.SetCursorPosition(2, 0);
            Console.Write("╡");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("RPG Manager");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("╞");
            Console.ResetColor();
            Console.SetCursorPosition(0, 5);
        }

        /// <summary>
        /// Print details van het personage (ToString) bovenaan de console.
        /// </summary>
        private static void WriteHeader()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleHelper.DrawBox(Character.ToString(), doubleLines: true, fullWidth: true);
            DrawTitle();
        }
    }
}

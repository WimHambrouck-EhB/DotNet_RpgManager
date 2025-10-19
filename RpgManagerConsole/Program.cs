using Microsoft.Win32.SafeHandles;
using RpgManagerLibrary;
using System.Threading;
using System.Xml;

namespace RpgManagerConsole
{
    internal class Program
    {
        private static Character CurrentCharacter = new Mage("Wim", 100, DateTime.Now, 9999, 10);
        private static readonly List<Character> Characters = [CurrentCharacter];
        static void Main(string[] args)
        {
            bool continueApp = true;
            while (continueApp)
            {
                Console.Clear();
                WriteHeader();

                ConsoleHelper.DrawBox("Menu");
                int keuze = ConsoleHelper.Menu(
                    [
                      "Heal",
                      "Damage",
                      "New Character",
                      "Switch Character"
                    ],
                    "Quit");

                switch (keuze)
                {
                    case 1:
                        HealCharacter();
                        break;
                    case 2:
                        DamageCharacter();
                        break;
                    case 3:
                        AddCharacter();
                        break;
                    case 4:
                        SwitchCharacter();
                        break;
                    default:
                        continueApp = false;
                        break;
                }
            }

            Console.ResetColor();
            Console.WriteLine("Bye!");
        }

        private static void SwitchCharacter()
        {
            Console.Clear();
            WriteHeader();

            ConsoleHelper.DrawBox("Switch Character", fullWidth: true);
            ConsoleHelper.DrawBox("Select a character to switch to", padding: 1);

            // Aanmaken van de lijst kan gemakkelijker met LINQ, maar dat is op dit punt in de cursus nog niet behandeld.
            // Ter referentie, volgende 5 regels kunnen vervangen worden door:
            //      List<string> characterNames = Characters.Select(c => $"{c.Name} ({c.CharacterType})").ToList();
            List<string> characterNames = [];
            foreach (Character character in Characters)
            {
                characterNames.Add($"{character.Name} ({character.CharacterType})");
            }

            int keuze = ConsoleHelper.Menu(characterNames, "Cancel");

            // OPGLET: De Menu-methode controleert op juiste invoer, als je rechtstreeks invoer van de Console zou lezen,
            // dan zou je hier extra validatie moeten toevoegen (keuze >=1 && keuze <= Characters.Count)
            // om ervoor te zorgen dat je niet buiten de grenzen van de lijst gaat.
            if (keuze != 0)
            {
                CurrentCharacter = Characters[keuze - 1];
            }
        }

        private static void AddCharacter()
        {
            ConsoleHelper.DrawBox("Create new Character");
            switch (ConsoleHelper.Menu(["Mage", "Warrior"], "Cancel"))
            {
                case 1:
                    NewMage();
                    break;
                case 2:
                    NewWarrior();
                    break;
                default:
                    return;
            }
        }

        private static void NewWarrior()
        {
            string warriorName = ConsoleHelper.ReadString("What should this fearsome warrior be called? ");
            Warrior warrior = new(warriorName, 100, DateTime.Now, 9999);

            string weapon = ConsoleHelper.ReadString("Add weapon (type weapon name or Q to quit): ");
            while (!weapon.Equals("Q", StringComparison.CurrentCultureIgnoreCase))
            {
                warrior.Weapons.Add(weapon);
                weapon = ConsoleHelper.ReadString("Add weapon: ");
            }
            CurrentCharacter = warrior;
            Characters.Add(CurrentCharacter);
        }

        private static void NewMage()
        {
            string mageName = ConsoleHelper.ReadString("What should this mystical mage be called? ");
            CurrentCharacter = new Mage(mageName, 100, DateTime.Now, 9999, 10);
            Characters.Add(CurrentCharacter);
        }

        private static void DamageCharacter()
        {
            bool continueLoop = true;

            while (continueLoop)
            {
                int damageAmount = ConsoleHelper.ReadInt("Enter amount of damage (1 - 1000, 0 to cancel) [0]: ", 1, 1000, 0);
                try
                {
                    CurrentCharacter.Damage(damageAmount);
                    continueLoop = false;
                }
                catch (DamageTooHighException e)
                {
                    ConsoleHelper.ShowError(e.Message);
                    char retry = ConsoleHelper.ReadChar("Do you want to try again? (Y/N) [N]: ");
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
            int healAmount = ConsoleHelper.ReadInt("Enter amount to heal (1 - 1000, 0 to cancel) [0]: ", 1, 1000);
            CurrentCharacter.Heal(healAmount);
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
            Console.SetCursorPosition(0, 4);
        }

        /// <summary>
        /// Print details van het personage (ToString) bovenaan de console.
        /// </summary>
        private static void WriteHeader()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleHelper.DrawBox(CurrentCharacter.ToString(), doubleLines: true, fullWidth: true);
            DrawTitle();
        }
    }
}

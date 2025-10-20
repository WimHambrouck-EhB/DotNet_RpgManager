using RpgManagerLibrary;

namespace RpgManagerConsole
{
    internal class Program
    {
        private static Player CurrentPlayer = new("Wim", "Hambrouck");
        private static Character CurrentCharacter = new Mage("Wim", 100, DateTime.Now, 9999, 10, CurrentPlayer);

        private static readonly List<Character> Characters = [CurrentCharacter];
        private static readonly List<Player> Players = [CurrentPlayer];

        static void Main(string[] args)
        {
            Players.Add(new Player("Jan", "Janssens"));

            Warrior warrior = new("ZARKAN, the DESTROYER!", 150, DateTime.Now, 100, Players.Last());
            warrior.Weapons.Add("Sword of a Thousand Truths");
            warrior.Weapons.Add("Sting");
            warrior.Weapons.Add("Aperture Science Handheld Portal Device");
            Characters.Add(warrior);


            bool continueApp = true;
            while (continueApp)
            {
                Console.Clear();
                ConsoleDraw.WriteHeader(CurrentCharacter, CurrentPlayer);

                ConsoleDraw.DrawBox("Menu");
                int keuze = ConsoleInput.Menu(
                    [
                      "Heal",
                      "Damage",
                      "New Character",
                      "Switch Character",
                      "New Player",
                      "Switch Player"
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
                    case 6:
                        SwitchPlayer();
                        break;
                    default:
                        continueApp = false;
                        break;
                }
            }

            Console.ResetColor();
            Console.WriteLine("Bye!");
        }

        private static void SwitchPlayer()
        {
            // same as SwitchCharacter but for players
            Console.Clear();
            ConsoleDraw.WriteHeader(CurrentCharacter, CurrentPlayer);
            ConsoleDraw.DrawBox("Switch Player", fullWidth: true);
            ConsoleDraw.DrawBox("Select a player to switch to", padding: 1);
            List<string> playerNames = [];
            foreach (Player player in Players)
            {
                playerNames.Add(player.ToString());
            }
            int keuze = ConsoleInput.Menu(playerNames, "Cancel");

            // OPGLET: De Menu-methode controleert op juiste invoer.
            //
            // Als je rechtstreeks invoer van de Console zou lezen,
            // dan zou je hier extra validatie moeten toevoegen (keuze >=1 && keuze <= PLayers.Count)
            // om ervoor te zorgen dat je niet buiten de grenzen van de lijst gaat.
            if (keuze != 0)
            {
                Player otherPlayer = Players[keuze - 1];

                // eerste personage van de gekozen speler zoeken
                // Noot: dit kan efficiënter met LINQ, maar dat is op dit punt in de cursus nog niet behandeld.
                foreach (Character character in Characters)
                {
                    if (character.Player == otherPlayer)
                    {
                        CurrentCharacter = character;
                        break;
                    }
                }

                // CurrentCharacter is niet veranderd, dus speler heeft nog geen personages
                if (CurrentCharacter.Player != otherPlayer)
                {
                    ConsoleInput.ShowError("The selected player has no characters yet. Please create a new character for this player first.");
                    Console.Write("Press <ENTER> to continue with current player...");
                    Console.ReadLine();
                }
                else
                {
                    CurrentPlayer = otherPlayer;
                }
            }
        }

        private static void SwitchCharacter()
        {
            Console.Clear();
            ConsoleDraw.WriteHeader(CurrentCharacter, CurrentPlayer);

            ConsoleDraw.DrawBox("Switch Character", fullWidth: true);
            ConsoleDraw.DrawBox("Select a character to switch to", padding: 1);

            // Aanmaken van de lijst kan gemakkelijker met LINQ, maar dat is op dit punt in de cursus nog niet behandeld.
            // Ter referentie, volgende 5 regels kunnen vervangen worden door:
            //      List<string> characterNames = Characters.Select(c => $"{c.Name} ({c.CharacterType})").ToList();
            List<string> characterNames = [];
            foreach (Character character in Characters)
            {
                // enkel personages van de huidige speler tonen
                // Noot: we willen effectief de ref vergelijken, dat is sneller en
                // speler mag immers meerdere personages hebben met dezelfde naam
                if (character.Player == CurrentPlayer)
                {
                    characterNames.Add($"{character.Name} ({character.CharacterType})");
                }
            }

            int keuze = ConsoleInput.Menu(characterNames, "Cancel");

            // OPGELET: De Menu-methode controleert op juiste invoer.
            //
            // Als je rechtstreeks invoer van de Console zou lezen,
            // dan zou je hier extra validatie moeten toevoegen (keuze >=1 && keuze <= Characters.Count)
            // om ervoor te zorgen dat je niet buiten de grenzen van de lijst gaat.
            if (keuze != 0)
            {
                CurrentCharacter = Characters[keuze - 1];
            }
        }

        private static void AddCharacter()
        {
            ConsoleDraw.DrawBox("Create new Character");
            switch (ConsoleInput.Menu(["Mage", "Warrior"], "Cancel"))
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
            string warriorName = ConsoleInput.ReadString("What should this fearsome warrior be called? ");
            Warrior warrior = new(warriorName, 100, DateTime.Now, 9999, CurrentPlayer);

            string weapon = ConsoleInput.ReadString("Add weapon (type weapon name or Q to quit): ");
            while (!weapon.Equals("Q", StringComparison.CurrentCultureIgnoreCase))
            {
                warrior.Weapons.Add(weapon);
                weapon = ConsoleInput.ReadString("Add weapon: ");
            }
            CurrentCharacter = warrior;
            Characters.Add(CurrentCharacter);
        }

        private static void NewMage()
        {
            string mageName = ConsoleInput.ReadString("What should this mystical mage be called? ");
            CurrentCharacter = new Mage(mageName, 100, DateTime.Now, 9999, 10, CurrentPlayer);
            Characters.Add(CurrentCharacter);
        }

        private static void DamageCharacter()
        {
            bool continueLoop = true;

            while (continueLoop)
            {
                int damageAmount = ConsoleInput.ReadInt("Enter amount of damage (1 - 1000, 0 to cancel) [0]: ", 1, 1000, 0);
                try
                {
                    CurrentCharacter.Damage(damageAmount);
                    continueLoop = false;
                }
                catch (DamageTooHighException e)
                {
                    ConsoleInput.ShowError(e.Message);
                    char retry = ConsoleInput.ReadChar("Do you want to try again? (Y/N) [N]: ");
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
            int healAmount = ConsoleInput.ReadInt("Enter amount to heal (1 - 1000, 0 to cancel) [0]: ", 1, 1000, 0);
            CurrentCharacter.Heal(healAmount);
        }
    }
}

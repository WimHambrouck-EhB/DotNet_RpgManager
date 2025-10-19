
namespace RpgManagerLibrary
{
    public abstract class Character
    {
        private const int MinimumHealth = -100;
        public string Name { get; set; }
        public decimal Health { get; private set; }
        public DateTime CreationDate { get; set; }
        public int PowerLevel { get; set; }

        public Player Player { get; set; }

        public abstract string CharacterType { get; }

        /// <summary>
        /// Instantiates a new Character with empty name, health of 100 and current date as creation date.
        /// </summary>
        public Character(Player player) : this(string.Empty, 100, player)
        {
        }

        /// <summary>
        /// Instantiates a new Character with given name, health, creation date and power level.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="creationDate"></param>
        /// <param name="powerLevel"></param>
        public Character(string name, decimal health, Player player, int powerLevel = 0, DateTime? creationDate = null)
        {
            Name = name;
            Health = health;
            CreationDate = creationDate ?? DateTime.Now;
            PowerLevel = powerLevel;
            Player = player;
        }

        /// <summary>
        /// Adds health to the character.
        /// </summary>
        /// <param name="amount">The amount of health to add. Must be positive.</param>
        public void Heal(decimal amount)
        {
            if (amount > 0)
            {
                Health += amount;
            }
        }

        /// <summary>
        /// Removes health from the character.
        /// </summary>
        /// <param name="amount">The amount of health to remove. Must be positive.</param>
        /// <exception cref="DamageTooHighException">If requested amount of damage would drop health below MinimumHealth.</exception>
        public void Damage(decimal amount)
        {
            if (amount > 0)
            {
                if (Health - amount < MinimumHealth)
                {
                    throw new DamageTooHighException($"Damage would subceed minimum health of character ({MinimumHealth})");
                }

                Health -= amount;
            }
        }


        public override string ToString()
        {
            // :N2 maakt dat healt wordt geformatteerd als nummer en max 2 cijfers na de komma laat zien
            // cf.: https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#numeric-format-specifier-n
            return $"{Name} – {CharacterType} {Environment.NewLine}" +
                $"Health: {Health:N2} / PowerLevel: {PowerLevel} / Created: {CreationDate.ToShortDateString()}";
        }
    }
}

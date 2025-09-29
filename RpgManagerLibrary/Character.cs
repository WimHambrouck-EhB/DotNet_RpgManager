using System.Security.AccessControl;

namespace RpgManagerLibrary
{
    public class Character
    {
        private const int MinimumHealth = -100;
        public string Name { get; set; }
        public decimal Health { get; private set; }
        public DateTime CreationDate { get; set; }
        public int PowerLevel { get; set; }

        /// <summary>
        /// Instantiates a new Character with empty name and current date as creation date.
        /// </summary>
        public Character()
        {
            Name = string.Empty;
            CreationDate = DateTime.Now;
        }

        /// <summary>
        /// Instantiates a new Character with given name, health, creation date and power level.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="creationDate"></param>
        /// <param name="powerLevel"></param>
        public Character(string name, decimal health, DateTime creationDate, int powerLevel)
        {
            Name = name;
            Health = health;
            CreationDate = creationDate;
            PowerLevel = powerLevel;
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
                if(Health - amount < MinimumHealth)
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
            return $"{Name} (Health: {Health:N2}, PowerLevel: {PowerLevel}, Created: {CreationDate})";
        }
    }
}

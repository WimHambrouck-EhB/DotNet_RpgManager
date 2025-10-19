namespace RpgManagerLibrary
{
    public class Warrior : Character
    {
        public List<string> Weapons { get; set; } = [];

        public Warrior()
        {
        }

        public Warrior(string name, decimal health, DateTime creationDate, int powerLevel) : base(name, health, powerLevel, creationDate)
        {
        }

        override public string ToString()
        {
            return $"{base.ToString()} / Weapons: [{string.Join(", ", Weapons)}]";
        }
    }
}

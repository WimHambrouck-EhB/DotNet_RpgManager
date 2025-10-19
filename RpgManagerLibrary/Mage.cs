namespace RpgManagerLibrary
{
    public class Mage : Character
    {
        public int ManaBoost { get; set; }

        public Mage()
        {
            
        }

        public Mage(string name, decimal health, DateTime creationDate, int powerLevel, int manaBoost) : base(name, health, powerLevel, creationDate)
        {
            ManaBoost = manaBoost;
        }

        override public string ToString()
        {
            return $"{base.ToString()} / ManaBoost: {ManaBoost}";
        }
    }
}

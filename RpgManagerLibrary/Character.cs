namespace RpgManagerLibrary
{
    public class Character
    {
        public string Name { get; set; } = string.Empty;
        public int Health { get; private set; }
        public DateTime CreationDate { get; set; }
        public int PowerLevel { get; set; }

        public Character()
        {
            CreationDate = DateTime.Now;
        }



        public override string ToString()
        {
            return $"{Name} (Health: {Health}, PowerLevel: {PowerLevel}, Created: {CreationDate})";
        }
    }
}

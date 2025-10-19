using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgManagerLibrary
{
    public class Player
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Player(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{LastName.ToUpperInvariant()}, {FirstName}";
        }
    }
}

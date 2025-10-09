using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RpgManagerLibrary
{
    public class Warrior : Character
    {
        public List<string> Weapons { get; set; } = [];

        public Warrior()
        {
        }

        public Warrior(string name, decimal health, DateTime creationDate, int powerLevel) : base(name, health, creationDate, powerLevel)
        {
        }
    }
}

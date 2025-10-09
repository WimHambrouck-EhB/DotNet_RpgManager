using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgManagerLibrary
{
    public class Mage : Character
    {
        public int ManaBoost { get; set; }

        public Mage()
        {
            
        }

        public Mage(string name, decimal health, DateTime creationDate, int powerLevel, int manaBoost) : base(name, health, creationDate, powerLevel)
        {
            ManaBoost = manaBoost;
        }
    }
}

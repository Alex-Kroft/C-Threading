using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyThreading
{
    public class Generator
    {
        private string name;
        public float powerSupply;

        public Generator(string Name, float currentSupply) {
            name = Name;
            powerSupply = currentSupply;
        }

        public void delegatePower(float wantedEnergy)
        {
            powerSupply -= wantedEnergy;
        }

        public void producePower(float neededEnergy)
        {
            powerSupply += neededEnergy;
        }
    }
}

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
        private float powerSupply = 0;
        public Generator(string Name) {
            name = Name;
        }

        public void delegatePower()
        {
        }

        public void producePower()
        {
            powerSupply++;
        }
    }
}

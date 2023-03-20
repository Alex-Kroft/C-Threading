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
            //Now I am feeling a bit lost about the idea. The city would be the one distributing energy,
            //but then do they need to be as separate packages to each house?
            //Or should the city have a float that stores the produced energy?
        }

        public void producePower(float neededEnergy)
        {
            powerSupply += neededEnergy;
        }
    }
}

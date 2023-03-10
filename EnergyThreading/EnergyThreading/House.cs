using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyThreading
{
    public class House
    {
        public int id { get; set; }
        public House() {
        }

        public float updateElecricityDemand()
        {
            float demand = 10;

            return demand;
        }

        public Boolean checkIfSatisfied()
        {
            return true;
        }
    }
}

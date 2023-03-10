using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyThreading
{
    public class House
    {
        int id;
        public House(int ID) {
            int id = ID;
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

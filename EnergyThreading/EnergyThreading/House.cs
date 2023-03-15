using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyThreading
{
    public class House
    {

        public float demand;
        public int id { get; set; }
        public House() {
            demand = 10;
        }

        public float updateElecricityDemand()
        {
            if(demand <= 9)
            {
                demand = 10;
            }

            return demand;
        }

        public Boolean checkIfSatisfied()
        {
            if (demand == 0)
            {
                return true;
            }
            else
            {
                return false;   
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyThreading
{
    public class House
    {
        public const float HouseBaseDemand = 10;
        public const float HouseDemandVariance = 5;
        public int id { get; set; }
        public float currentElectricity { get; set; }

        public House(int id) 
        {
            currentElectricity = 0;
            this.id = id;
        }

        public float updateElecricityDemand()
        {
            float demand = HouseBaseDemand;
            currentElectricity = 0;
            Random rnd = new Random();
            demand += rnd.Next(-HouseDemandVariance, HouseDemandVariance);
            return demand;
        }

        public Boolean checkIfSatisfied()
        {
            return true;
        }
    }
}

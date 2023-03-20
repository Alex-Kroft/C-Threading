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
        public const int HouseDemandVariance = 500;
        public int id { get; set; }
        public float currentElectricity { get; set; }
        public float currentDemand { get; set; }

        public House(int id) 
        {
            updateElecricityDemand();
            this.id = id;
        }

        public void updateElecricityDemand()
        {
            currentElectricity = 0;
            Random rnd = new Random();
            currentDemand = HouseBaseDemand + (rnd.Next(-HouseDemandVariance, HouseDemandVariance))/100;
        }

        public Boolean isSatisfied()
        {
            return currentElectricity == currentDemand;
        }
    }
}

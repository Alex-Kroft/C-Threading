using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyThreading
{
    public class City
    {
        public List<House> houses;
        public City() {
            houses = new List<House>();
            Generator generator = new Generator("Alex");
        }

        private void createHouse(House house)
        {
            var highestID = houses.Any() ? houses.Max(x => x.id) : 1;
            house.id = highestID + 1;
            houses.Add(house);
        
        }

        public void update()
        {

        }

        public void initialize()
        {

        }

        private void supplyDemand()
        {
            foreach(var house in houses)
            {

            }

        }

        public void loadContent()
        {

        }
    }
}

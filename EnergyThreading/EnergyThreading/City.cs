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
        private List<House> houses;
        private Generator generator;
        private bool singleThread;

        public City(int houseAmount, bool singleThread) {
            houses = new List<House>();
            createHouses(houseAmount);
            Generator generator = new Generator("Alex");
            this.singleThread = singleThread;
        }

        private void createHouses(int amount)
        {
            //Should this also be done via threading when multithreaded is chosen?
            for (int i = 0; i < amount; i++)
            {
                houses.Add(new House(i))
            }
        }

        public void update()
        {

        }

        public void initialize()
        {

        }

        private void supplyDemand()
        {

        }

        public void loadContent()
        {

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.Xml.Linq;

namespace EnergyThreading
{
    public class City
    {
        private List<House> houses;
        private Generator generator;
        private List<Thread> threads;
        private bool singleThread;

        private float storedEnergy;

        public City(int houseAmount, bool singleThread) {
            houses = new List<House>();
            createHouses(houseAmount);
            Generator generator = new Generator("Alex");
            threads = new List<Thread>();
            this.singleThread = singleThread;
            storedEnergy = 0;
        }

        private void amountOfHouses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public bool setSingleThread(bool value)
        {
            singleThread = value;
            return singleThread;
        }

        public bool getSingleThread { get { return singleThread; } }
        public List<House> getHouses()
        {
            return houses;
        }
        private void createHouses(int amount)
        {
            //Should this also be done via threading when multithreaded is chosen?
            for (int i = 0; i < amount; i++)
            {
                House house = new House(i);
                house.X = i * 50;
                house.Y = 0;
                house.size = 50;

                houses.Add(house);
                if (!singleThread)
                {
                    Thread thread = new Thread(supplyDemandSingleHouse);
                    thread.Start(i);
                    threads.Add(thread);
                }
            }
        }

        public void update()
        {

        }

        public void initialize()
        {

        }

        private void supplyDemandAllHouses()
        {

        }

        private void supplyDemandSingleHouse(object houseId)
        {
            House house = houses[(int)houseId];
            if (!house.isSatisfied())
            {
                //This is really weird I know, but not like we're actually moving electricity, or any kinds of packets, unless we actually start doing that
                //Otherwise, these operations are good for showing speed differences and how it handles the same variables having multiple reads and stuff.
                generator.producePower(house.currentDemand);
                generator.delegatePower(house.currentDemand);
                storedEnergy += house.currentDemand;
                house.currentElectricity = house.currentDemand;
                storedEnergy -= house.currentDemand;
            }
            Console.WriteLine(house.currentDemand);
        }

        public void loadContent()
        {

        }
    }
}

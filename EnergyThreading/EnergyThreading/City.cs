using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EnergyThreading
{
    public class City
    {
        private List<House> houses;
        private Generator generator;
        private List<Thread> threads;
        private bool singleThread;
        public float total;
        private float storedEnergy;
        private readonly object lockObject = new object();

        public City(int houseAmount, bool singleThread) {
            houses = new List<House>();
            createHouses(houseAmount);
            Generator generator = new Generator("Alex");
            threads = new List<Thread>();
            this.singleThread = false;
            storedEnergy = 0;
        }

        public List<House> getHouses()
        {
            return houses;
        }
        public void createHouses(int amount)
        {
            lock (houses) {
                for (int i = 0; i < amount; i++)
                {
                    House house = new House(i);
                    houses.Add(house);

                    if (!singleThread)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(supplyDemandSingleHouse), i);
                    }
                    else
                    {
                        supplyDemandSingleHouse(i);
                    }
                }
            }
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
        

        public void update()
        {

        }

        public void initialize()
        {

        }

        public void produceEnergyForHouses()
        {
            if (singleThread == true)
            {
                foreach (House house in houses)
                {
                    generator.producePower(house.currentDemand);
                }
            }
            else
            {
                List<Thread> threads = new List<Thread>();
                foreach (House house in houses)
                {
                    Thread t = new Thread(() => generator.producePower(house.currentDemand));
                    threads.Add(t);
                    t.Start();
                }
                foreach (Thread t in threads)
                {
                    t.Join();
                }
            }
        }

        private void supplyDemandSingleHouse(object houseId)
        {
            House house = houses[(int)houseId];
            if (!house.isSatisfied())
            {
                house.currentDemand += 50;

            }
            else
            {
                house.currentDemand += 45;
            }
            }


        public float calculateTotalDemand()
        {
            total = 0;

            if (singleThread == true)
            {
                foreach (House house in houses)
                {
                    total += house.currentDemand;
                }
            }
            else
            {
                CountdownEvent countdownEvent = new CountdownEvent(houses.Count);

                for (int i = 0; i < houses.Count; i++)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(addToTotalDemand), new object[] { houses[i], countdownEvent });
                }

                countdownEvent.Wait();
            }

            return total;
        }

        private void addToTotalDemand(object data)
        {
            House house = (House)((object[])data)[0];
            CountdownEvent countdownEvent = (CountdownEvent)((object[])data)[1];

            lock (lockObject)
            {
                total += house.currentDemand;
            }
            countdownEvent.Signal();
        }
        public void loadContent()
        {

        }
    }
}

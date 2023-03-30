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
        private List<House> houses { get; set; }
        public Generator generator = new Generator("Alex");                                                
        private List<Thread> threads;
        private bool singleThread { get; set; }
        public float total;
        public float storedEnergy;
        private readonly object lockObject = new object();
        private Semaphore semaphore = new Semaphore(4, 4); // Initialize a semaphore with a count of 4

        public City(int houseAmount, bool singleThread) {
            houses = new List<House>();
            createHouses(houseAmount);

            threads = new List<Thread>();
            this.singleThread = false;
            storedEnergy = 0;
        }

        public List<House> getHouses()
        {
            return houses;
        }

        public List<House> setHouses(int amount)
        {
            for (int i = 0; i < amount; i++) 
            {
                houses.Add(new House(i + (houses.LastOrDefault().id)));
            }
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

        public bool setSingleThread(bool value)
        {
            singleThread = value;
            return singleThread;
        }

        public bool getSingleThread { get { return singleThread; } }
        

        public void update()
        {

        }

        public void distributeEnergyToHouses()
        {
            if (singleThread == true)
            {
                if (houses != null && generator != null)
                {
                    foreach (House house in houses)
                    {
                        if (house != null && house.currentDemand != 0)
                        {
                            generator.delegatePower(house.currentDemand);
                            house.currentElectricity = house.currentDemand;
                            house.currentDemand = 0;
                        }
                    }
                }
            }
            else
            {
                if (houses != null && generator != null)
                {

                    Parallel.ForEach(houses, house =>
                    {
                        if (house != null && house.currentDemand != 0 && generator.powerSupply >= house.currentDemand)
                        {
                            semaphore.WaitOne(); // Wait for the semaphore to become available
                            lock (generator)
                            {
                                generator.delegatePower(house.currentDemand);
                            }
                            lock (house)
                            {
                                house.currentElectricity = house.currentDemand;
                                house.currentDemand = 0;
                            }
                            semaphore.Release(); // Release the semaphore
                        }
                    });
                }
            }
        }

        public void produceEnergyForHouses()
        {
            if (singleThread == true)
            {
                if (houses != null && generator != null)
                {
                    foreach (House house in houses)
                    {
                        if (house != null && house.currentDemand != 0)
                        {
                            generator.producePower(house.currentDemand);
                        }
                    }
                    storedEnergy = generator.powerSupply;
                }
            }
            else
            {
                if (houses != null && generator != null)
                {
                    List<Thread> threads = new List<Thread>();
                    foreach (House house in houses)
                    {
                        lock (lockObject)
                        {
                            if (house != null && house.currentDemand != 0)
                            {
                                Thread t = new Thread(() => generator.producePower(house.currentDemand));
                                threads.Add(t);
                                t.Start();
                            }
                        }
                    }
                    foreach (Thread t in threads)
                    {
                        t.Join();
                    }
                    storedEnergy = generator.powerSupply;
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

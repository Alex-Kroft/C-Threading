using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Generator generator { get; set; }                                                
        private List<Thread> threads;
        private bool singleThread { get; set; }
        public float totalDemand;
        public float storedEnergy;
        private readonly object lockObject = new object();
        private Semaphore semaphore = new Semaphore(4, 4); // Initialize a semaphore with a count of 4

        public City(int houseAmount, float availableSupply) {
            houses = new List<House>();
            createHouses(houseAmount);
            generator = new Generator("Alex", availableSupply);

            threads = new List<Thread>();
            this.singleThread = false;
        }

        public List<House> getHouses()
        {
            return houses;
        }

        public Generator getGenerator() 
        {
            return generator;
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
                 
                    int threadAmountToSplit = 5;    //How many threads to make
                    int housesPerThread = houses.Count / threadAmountToSplit;
                    int extraHouses = houses.Count % threadAmountToSplit;   //When the number is not divisible by threadAmountToSplit

                    //Splits all the houses into blocks, and stores the list of all the blocks in the "blocks" variable
                    List<List<House>> blocks = new List<List<House>>(); 
                    for (int i = 0; i < threadAmountToSplit; i++)
                    {
                        
                        List<House> cityBlock = new List<House>();
                        int start = i * housesPerThread;
                        int end = start + housesPerThread;

                        // assign extra houses to the last block
                        if (i == threadAmountToSplit - 1)
                        {
                            end += extraHouses;
                        }

                        for (int j = start; j < end; j++)
                        {
                            if (j < houses.Count) // make sure j is within the bounds of the houses list
                            {
                                cityBlock.Add(houses[j]);
                            }
                        }
                        blocks.Add(cityBlock);
                    }

                    foreach (List<House> cityBlock in blocks)
                    {
                        Thread t = new Thread(() =>
                        {
                            // produce power for all houses in the block
                            lock (lockObject) // acquire the lock before accessing the generator
                            {
                                foreach (House house in cityBlock)
                                {
                                    generator.producePower(house.currentDemand);
                                }
                            }

                        });
                        threads.Add(t);
                        t.Start();
                    }
                    

                    foreach (Thread t in threads)
                    {
                        t.Join();
                    }
                    lock (lockObject) // acquire the lock before updating storedEnergy
                    {
                        storedEnergy = generator.powerSupply;
                    }
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
            totalDemand = 0;

            if (singleThread == true)
            {
                foreach (House house in houses)
                {
                    totalDemand += house.currentDemand;
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

            return totalDemand;
        }

        private void addToTotalDemand(object data)
        {
            House house = (House)((object[])data)[0];
            CountdownEvent countdownEvent = (CountdownEvent)((object[])data)[1];

            lock (lockObject)
            {
                totalDemand += house.currentDemand;
            }
            countdownEvent.Signal();
        }
    }
}

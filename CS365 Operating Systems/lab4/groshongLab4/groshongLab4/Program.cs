using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace groshongLab4
{
    class SingleLaneBridge
    {
        public static void Main()
        {
            Bridge bridge = new Bridge();
            SharedID iD = new SharedID();
            Random rand = new Random();

            // Create a northbound farmers thread
            Thread Northbound = new Thread(new ThreadStart(NorthRun));
            // Create a southbound farmers thread
            Thread Southbound = new Thread(new ThreadStart(SouthRun));

            void NorthRun()
            {

                while (true)
                {
                    Farmer farmer = new Farmer(bridge, iD);
                    Thread th = new Thread(new ThreadStart(farmer.Run));
                    // Sets the farmers name
                    farmer.SetName("Northbound Farmer" );
                    th.Start();
                    try
                    {
                        // how long it will wait before creating another Northbound farmer
                        Thread.Sleep(rand.Next(0,1000) * 10);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }

            }
            void SouthRun()
            {
                while (true)
                {
                    Farmer farmer = new Farmer(bridge, iD);
                    Thread th = new Thread(new ThreadStart(farmer.Run));
                    // Sets the farmers name
                    farmer.SetName("Southbound Farmer");
                    th.Start();
                    try
                    {
                        // how long it will wait before creating another Southbound farmer
                        Thread.Sleep(rand.Next(0,1000) * 10);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            
        Northbound.Start();
        Southbound.Start();
        }
    }
    /// <summary>
    /// The bridge controlls the semaphore and the crossing times of the farmers
    /// </summary>
    class Bridge
    {
        private Semaphore signal;
        Random rand = new Random();


        public Bridge()
        {
            signal = new Semaphore(1,1);
        }
        public void crossBridge(Farmer farmer)
        {
            try
            {
                Console.WriteLine(farmer.GetName() + " [" + farmer.id + "]: "+"is waiting for signal to cross the bridge.");
                // aquire lock
                signal.WaitOne();
                Console.WriteLine(farmer.GetName() + " [" + farmer.id + "]: " + "is crossing the bridge.");

                // utilize a random number to set how it takes to cross the bridge 
                // sleep for that duration
                Thread.Sleep( rand.Next(0, 1000) * 10 );
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.ToString()); ;
            }
            finally
            {
                Console.WriteLine(farmer.GetName() + " [" + farmer.id + "]: " + "has crossed the bridge.\n" );
                // release lock
                signal.Release();
            }
        }
    }

    /// <summary>
    /// The farmer accepts a common ID and a common Bridge that it trys to cross while running.
    /// </summary>
    class Farmer
    {
        private string name;
        public int id { get; set; }
        private Bridge bridge;
        public Farmer(Bridge bridge, SharedID iD)
        {
            this.bridge = bridge;
            this.id = iD.GetID();
        }

        public void Run()
        {
            bridge.crossBridge(this);
        }

        public String GetName()
        {
            return name;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

    }

    #region Helpers
    class SharedID
    {
        int ID = 0;
        public int GetID()
        {
            ID++;
            return ID;
        }
    }
    #endregion
}

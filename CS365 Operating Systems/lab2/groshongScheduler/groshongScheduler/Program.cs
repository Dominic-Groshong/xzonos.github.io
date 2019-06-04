using System;
using System.Linq;
using System.Collections.Generic;

namespace groshongScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Program scheduler = new Program();
        }

        public Program()
        {
            RunProgram();
        }

        public void RunProgram()
        {
            // Read in the number of processes to simulate
            Console.WriteLine("Enter number of processes to simulate: \n");
            var input = Console.ReadLine();
            var time = Convert.ToInt32(input);
           
            // Create a list of processes
            var processes = createProcess(time);
            
            // sort processes by corresponding method
            var sjf = processes.OrderBy(x => x.Arival).ThenBy(x => x.Burst);
            var fcfs = processes.OrderBy(x => x.Arival);
           
            // Preform scheduling algorithem on each process list.
            Scheduler(fcfs, time, "First Come First Serve:");
            Scheduler(sjf, time, "Shortest Job First:");
        }

        #region Scheduler
        /// <summary>
        /// Mock a scheduler, taking in a sorted list and time partition.
        /// </summary>
        /// <param name="processes"></param>
        /// <param name="time"></param>
        public void Scheduler(IEnumerable<Process> processes, int time, string title)
        {

            // Varables
            var turnaroundCount = 0;
            var burstCount = 0;
            // Title
            Console.WriteLine( title + "\n" );
            // Algorithem start time
            int startTime = processes.First().Arival;

            foreach (var item in processes)
            {

                // Preform scheduling on processes
                item.Start = startTime;
                item.Completion = item.Start + item.Burst;
                item.Turnaround = item.Completion - item.Arival;
                item.Waiting = item.Turnaround - item.Burst;
                item.Response = item.Start - item.Arival;
                startTime = item.Completion + item.Arival;

                turnaroundCount += item.Turnaround;
                burstCount += item.Burst;

            }

            // Calculate Averages / CPU
            double averageTurnaround = (double)(turnaroundCount / processes.Count());
            double throughput = (double)(processes.Last().Completion / processes.Count()) / 1000;
            double CPU = (double)burstCount / processes.Last().Completion;

            // Print statements
            PrintList(processes);
            Console.WriteLine("\n");
            Console.WriteLine("CPU Utilization: " + CPU);
            Console.WriteLine("Average Thoughput: " + throughput + " seconds");
            Console.WriteLine("Average Turnaround: " + averageTurnaround + "\n");
        }
        #endregion

        // Place any helper methods within the following helper region.
        #region Helpers

        /// <summary>
        /// Create a list of processes with burst times between 2ms and 42ms
        /// </summary>
        /// <param name="time">The ammount of time the program will run.</param>
        /// <returns></returns>
        public List<Process> createProcess(int time)
        {
            // create list of jobs to add processes to.
            List<Process> jobs = new List<Process>();

            for (int i = 0; i < time; i++)
            {
                Process p = new Process
                {
                    Name = "P" + (i + 1),
                    Burst = Rand(2, 42),
                    Arival = Rand(time)
                };
                jobs.Add(p);
            }

            return jobs;
        }
        /// <summary>
        /// Print out the list of processes passed in.
        /// </summary>
        /// <param name="processes">The processes being passed in</param>
        public void PrintList(IEnumerable<Process> processes)
        {
            Console.WriteLine("{0,-8}{1,10}{2,10}{3,10}{4,10}{5,15}{6,12}{7,12}",
                              "Process",
                              "Arival",
                              "Burst",
                              "Start",
                              "Finish",
                              "Turnaround",
                              "Waiting",
                              "Response"
                             );
            foreach(var item in processes)
            {
                Console.WriteLine("{0,-8}{1,10}{2,10}{3,10}{4,10}{5,15}{6,12}{7,12}",
                                  item.Name,
                                  item.Arival,
                                  item.Burst,
                                  item.Start,
                                  item.Completion,
                                  item.Turnaround,
                                  item.Waiting,
                                  item.Response
                                 );
            }
        }

        // Generate a random number without inputs
        public int Rand()
        {
            Random random = new Random();
            return random.Next();
        }

        // Generate a random number up to a maximum
        public int Rand(int max)
        {
            Random random = new Random();
            return random.Next(max);
        }

        // Generate a random number between two numbers
        public int Rand(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        #endregion

        #region OLD
        // First Come First Serve Scheduling
        //#region FCFS
        ///// <summary>
        ///// Mock a first come first serve cpu sheduler
        ///// </summary>
        ///// <param name="processes"></param>
        ///// <param name="time"></param>
        //public void FCFS(List<Process> processes, int time)
        //{

        //    // Varables
        //    var sortedProcess = processes.OrderBy(x => x.Arival);
        //    var turnaroundCount = 0;
        //    var burstCount = 0;
        //    // Title
        //    Console.WriteLine("First Come First Serve: \n");
        //    // Algorithem start time
        //    int startTime = sortedProcess.First().Arival;

        //    foreach (var item in sortedProcess)
        //    {
        //        // Preform scheduling on processes
        //        item.Start = startTime;
        //        item.Completion = item.Start + item.Burst;
        //        item.Turnaround = item.Completion - item.Arival;
        //        item.Waiting = item.Turnaround - item.Burst;
        //        item.Response = item.Start - item.Arival;
        //        startTime = item.Completion + item.Arival;
        //        // Increase counts
        //        turnaroundCount += item.Turnaround;
        //        burstCount += item.Burst;
        //    }

        //    // Calculate Averages
        //    double averageTurnaround = (double)(turnaroundCount / sortedProcess.Count());
        //    double throughput = (double)(sortedProcess.Last().Completion / sortedProcess.Count()) / 1000;
        //    double CPU = (double)burstCount / sortedProcess.Last().Completion;

        //    // Print statements
        //    PrintList(sortedProcess);
        //    Console.WriteLine("\n");
        //    Console.WriteLine("CPU Utilization: " + CPU);
        //    Console.WriteLine("Average Thoughput: " + throughput + " seconds");
        //    Console.WriteLine("Average Turnaround: " + averageTurnaround + "\n");
        //}
        //#endregion
        //// Shortest Job First Scheduling
        //#region SJF
        ///// <summary>
        ///// Mock a shortest job first cpu sheduler
        ///// </summary>
        ///// <param name="processes"></param>
        ///// <param name="time"></param>
        //public void SJF(List<Process> processes, int time)
        //{

        //    // Varables
        //    var sortedProcess = processes.OrderBy(x => x.Burst);
        //    var turnaroundCount = 0;
        //    var burstCount = 0;
        //    // Title
        //    Console.WriteLine("Shortest Job First: \n");
        //    // Algorithem start time
        //    int startTime = sortedProcess.First().Arival;

        //    foreach (var item in sortedProcess)
        //    {

        //        // Preform scheduling on processes
        //        item.Start = startTime;
        //        item.Completion = item.Start + item.Burst;
        //        item.Turnaround = item.Completion - item.Arival;
        //        item.Waiting = item.Turnaround - item.Burst;
        //        item.Response = item.Start - item.Arival;
        //        startTime = item.Completion + item.Arival;

        //        turnaroundCount += item.Turnaround;
        //        burstCount += item.Burst;

        //    }

        //    // Calculate Averages / CPU
        //    double averageTurnaround = (double)(turnaroundCount / sortedProcess.Count());
        //    double throughput = (double)(sortedProcess.Last().Completion / sortedProcess.Count()) / 1000;
        //    double CPU = (double)burstCount / sortedProcess.Last().Completion;

        //    // Print statements
        //    PrintList(sortedProcess);
        //    Console.WriteLine("\n");
        //    Console.WriteLine("CPU Utilization: " + CPU);
        //    Console.WriteLine("Average Thoughput: " + throughput + " seconds");
        //    Console.WriteLine("Average Turnaround: " + averageTurnaround + "\n");
        //}
        //#endregion
        #endregion
    }
}

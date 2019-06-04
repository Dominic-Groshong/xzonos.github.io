# CS365 Operating System
## Lab 2 CPU Scheduling

#### Part I: Producer and consumer (multithread)
##### Creation
I created my CPU scheduler simulation using C#. As an object-oriented language, the first step was to create the process class (Fig 1.). This class would contain all the variables for keeping track of the various process states, namely: the burst, arrival, completion, start, turnaround, waiting, and response time for each process.

(Fig 1. Process Class)
```csharp
class Process
{
    // Process Variables
    public string Name { get; set; }
    public int Burst { get; set; }
    public int Arival { get; set; }
    public int Completion { get; set; }
    public int Start { get; set; }

    // Scheduling Criteria
    public int Turnaround { get; set; }
    public int Waiting { get; set; }
    public int Response { get; set; }
}
```

Next, I created several helper methods for generating random numbers (Fig 2.) to use for the burst and arrival time during the creation of each process. As stated in the lab document, the minimum burst time was 2ms, and as such, our maximum time was 42ms. Thus in process creation, I used the random methods for each process creation for assigning the burst and arrival values, process creation happened up to N times (Fig. 3)

(Fig 2. Random Number Helpers)
```csharp
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
```
(Figure 3: Create Processes)
```csharp
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
```
The next step was to create a scheduling simulation for SJF and FCFS. To start since FCFS and SJF only differ in the ordering of when jobs are performed, I started by creating a Scheduler method (Fig. 4) which would handle all my calculations on any IEnumerable passed into the method. Then for each item in that IEnumerable, I calculated the correct values for each variable using the descriptions from class. Once they were calculated, I could use the numbers to find the averages for the turnaround time, throughput, and CPU utilization. Once that was done, I used a print method for printing each item from the sorted list and the resulting averages.

(Fig 4. Scheduler Method)
```csharp
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
```

Using Linq, I sorted the Process list so they would be ordered by the Arrival variable for FCFS and created another IEnumerable in which I sorted the same list by Burst time. Once each was sorted, I could pass the resulting IEnumerable into the scheduler method. The results can be seen in Figure 5 & 6.

```csharp
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
```

##### Results for 10 proccesses
By analyzing the results we can see that the shortest job first was better across the board, from CPU Utilization and Turnaround time both being shorter then FCFS. The throughputs where the same on this particular test and when running it with other processes Throughput was generally within a few ms of each other. As we know SJF is an provably the optimal solution, providing the shortest wait times which we can see by looking at the waiting column, The downsides of SJF being the time taken by a process must be known by the CPU beforehand, which is not possible, plus longer processes will have more waiting time, eventually leading to starvation of these processes.

```cmd
First Come First Serve:

Process     Arival     Burst     Start    Finish     Turnaround     Waiting    Response
P1               0         3         0         3              3           0           0
P7               1        28         3        31             30           2           2
P9               3         4        32        36             33          29          29
P8               4        19        39        58             54          35          35
P2               5        21        62        83             78          57          57
P4               5        12        88       100             95          83          83
P6               6         6       105       111            105          99          99
P10              6        26       117       143            137         111         111
P3               7        37       149       186            179         142         142
P5               9        23       193       216            207         184         184


CPU Utilization: 0.828703703703704
Average Thoughput: 0.021 seconds
Average Turnaround: 92
```
```cmd
Shortest Job First:

Process     Arival     Burst     Start    Finish     Turnaround     Waiting    Response
P1               0         3         0         3              3           0           0
P7               1        28         3        31             30           2           2
P9               3         4        32        36             33          29          29
P8               4        19        39        58             54          35          35
P4               5        12        62        74             69          57          57
P2               5        21        79       100             95          74          74
P6               6         6       105       111            105          99          99
P10              6        26       117       143            137         111         111
P3               7        37       149       186            179         142         142
P5               9        23       193       216            207         184         184


CPU Utilization: 0.828703703703704
Average Thoughput: 0.021 seconds
Average Turnaround: 91
```



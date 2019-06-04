using System;

namespace CPUScheduler
{

    public class Process(){
        // Process Variables
        public int Burst { get; set; }
        public int Arival { get; set; }
        public int Completion { get; set; }

        // Scheduling Criteria
        public int Turnaround { get; set; }
        public int Waiting { get; set; } 
        public int Response { get; set; }

    }    

    private List<Process> Setup{
        var p1, p2, p3, p4, p5, p6, p7, p8, p9, p10 = new Process();
    
    }

    public static void Main(string[] args){

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace groshongScheduler
{
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
}

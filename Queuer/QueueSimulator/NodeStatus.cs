using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueSimulator
{
    public class NodeStatus
    {
        
        public int numberOfJobsInQueue { get; set; } //liczba zadan czekajacych w kolejce
        public List<bool> machineWorkingStatuses { get; set; }
    }
}

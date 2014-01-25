using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queuer
{
    class MachineDescription
    {
        public int Id { get; set; }
        public int SlotsNumber { get; set; }  
        public int QueueSize { get; set; }  
        public string QueueDiscipline { get; set; }
        public string StreamType { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
    }
}

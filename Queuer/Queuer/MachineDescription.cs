using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queuer
{
    public class MachineDescription
    {
        public MachineDescription()
        {
            Routes = new List<int>();
        }
        public int Id { get; set; }
        public int SlotsNumber { get; set; }  
        public int QueueSize { get; set; }  
        public string ServiceDiscipline { get; set; }
        public string ServiceType { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public List<int> Routes { get; set; }
    }
}

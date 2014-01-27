using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Queuer;

namespace QueueSimulator
{
    /*
     * Klasa odpowiedzialna za przeprwadzenie symylacji
     * 
     * obluga priorytetowej koejki operacji kolejki operacji
     * 
     * */
    public class Simulator
    {
        int SimulationTime;

        TaskGenerator generatorZadan;

        List<Node> nodes;
        
              
        
        public Simulator(List<MachineDescription> machinesInSystem){

            // tu prasujemy liste machinDescription do postaci listy nodów
            nodes = new List<Node>();

            foreach (MachineDescription md in machinesInSystem)
            {
                this.nodes.Add(new Node(md)); 
            }

            EventQueue eq = EventQueue.Instance;
        }


        public void runSimulator(int limitZadan = 0 ) // 
        {
            
        }


        public int getNumberOfNodes(){
            return nodes.Count;
        }
        public static void Main(String[] args)
        {
            
        }
        
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Queuer;

namespace QueueSimulator
{
    /*
     * Klasa odpowiedzialna za przeprwadzenie symylacji
     * 
     * obluga priorytetowej koejki operacji kolejki operacji
     * 
     * */
    class Simulator
    {
        int SimulationTime;

        TaskGenerator generatorZadan;
        List<MachineDescription> machines;
        Queue<String> zadania;
        PriorityQueue<int, Queue<SytemEvents>> kolejka;
        
        public Simulator(List<MachineDescription> machinesInSystem, List<MachineConnections> connections){

            SimulationTime = 0;
            //tworzymy obiekt generatora zadań

            int odstepyPomiedzyNowymiZadaniami = 60;
            
            generatorZadan = new TaskGenerator(odstepyPomiedzyNowymiZadaniami); // ten
            this.machines = machinesInSystem;


        }


        public void runSimulator()
        {
            // generator Tworzy nowe zadanie
            generatorZadan.getTask(SimulationTime);

            // tworzy sie nowye

        }

        static void Main(string[] args)
        {
            //(new Simulator(machinesList, connectionList)).runSimulator();

        }
    }
}

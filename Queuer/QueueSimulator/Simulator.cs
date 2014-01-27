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
    class Simulator
    {
        int SimulationTime;

        TaskGenerator generatorZadan;
        List<Machine> machines;
        
        PriorityQueue<int, Queue<SytemEvents>> kolejka;
        
        public Simulator(List<MachineDescription> machinesInSystem,TaskGenerator taskGenerator){

            SimulationTime = 0;
            //tworzymy obiekt generatora zadań

            //int odstepyPomiedzyNowymiZadaniami = 60;

            generatorZadan = taskGenerator;
            
            this.machines = Machine.GetMachines(machinesInSystem); // z opisow tworzy liste maszyn

            
        }


        public void runSimulator(int limitZadan = 0 ) // 
        {
            // generator Tworzy nowe zadanie
            Task firstTask =  generatorZadan.getTask(SimulationTime);
            generatorZadan.setLimit(1);
            // tworzy sie nowye

            while (true) // symulacja trwa do momentu zakonczenia przez jeden z waruków
            {

            }

        }


        
       
    }
}

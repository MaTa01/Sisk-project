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
        List<Node> machines;
        
        PriorityQueue<int, Queue<SytemEvents>> kolejka;

        
        
        public Simulator(List<MachineDescription> machinesInSystem,TaskGenerator taskGenerator){

            SimulationTime = 0;
            //tworzymy obiekt generatora zadań

            //int odstepyPomiedzyNowymiZadaniami = 60;

            generatorZadan = taskGenerator;
            
            

            
        }


        public void runSimulator(int limitZadan = 0 ) // 
        {
            // generator Tworzy nowe zadanie
            
            
            generatorZadan.setLimit(1);
            // tworzy sie nowye

            // tworzymy kolejke zadan
            PriorityQueue<int, SimulatorEvent> kolejkaOperacji = new PriorityQueue<int, SimulatorEvent>();

            Task firstTask = generatorZadan.getTask(SimulationTime);
            kolejkaOperacji.Enqueue(0, new SimulatorEvent(SimulatorEvent.TypZadania.newTask));

            while (!kolejkaOperacji.IsEmpty) // symulacja trwa do momentu zakonczenia przez jeden z waruków
            {
                SimulatorEvent e = kolejkaOperacji.Dequeue();
                switch (e.typ)
                {
                    case SimulatorEvent.TypZadania.newTask:
                        // nowe zadanie pojawia sie w systemie wiec dodajemy je do maszyny na liscie z id =1
                        break;

                }
            }
            Console.Write("asd");
        }

        public static void Main(String[] args)
        {
            Simulator s = new Simulator(null, null);
            s.runSimulator();
        }
        
       
    }
}

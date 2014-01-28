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

        EventQueue kolejkaKomunikatow;

        private List<NodeStatus> nodeStatusesList;
        

        
        public Simulator(List<MachineDescription> machinesInSystem){

            // tu prasujemy liste machinDescription do postaci listy nodów

            SimulationTime = 0;

            nodes = new List<Node>();

            generatorZadan = new TaskGenerator(120);

            foreach (MachineDescription md in machinesInSystem)
            {
                this.nodes.Add(new Node(md)); 
            }
            //setListOfNodeStatuses();

            kolejkaKomunikatow = EventQueue.Instance; // tworzymy kolejke komunikatów
        }


        public void runSimulator(int limitZadan = 1 ) // 
        {
            // sytem zaczyna prace generujac pierwsze pierwszy Event

            Event firstEventInSimulation = new Event(SimulationTime);

            Event actualEvent, tmpEvent ;

            Task temTask;

            firstEventInSimulation.setEventType(Event.EventType.NEW_TASK);

            kolejkaKomunikatow.addEvent(firstEventInSimulation);

            while (!kolejkaKomunikatow.isQueueEmpty()) // dopoki sa komunikaty
            {
                actualEvent = kolejkaKomunikatow.getEvent();
                SimulationTime = actualEvent.getTime();
                Node node,next_Node;;

                switch (actualEvent.getEventType())
                {
                    case Event.EventType.NEW_TASK: // pojawia sie nowy task w sytemie wiec:

                        if (generatorZadan.getTaskCounter() < limitZadan)
                        {

                            Task t = generatorZadan.getTask(SimulationTime);

                            // dodajemy go do noda z NodeType = 1 o ile mam miejsce w kolejsce
                            node = nodes.Find(n => (n.getNodeType() == 1 || n.getNodeType() == 0)); // wyszyujemy pierwszej, bedz jedynej maszyny - to mozna by uprosci aby zwekszyc zrozumienie
                            
                            if (!node.isBufferFull()) // w danym nodzie jest miejsce
                            {
                                if (node.isBufferEmpty()) // gdy kolejka bufora jest wolna trzba
                                {
                                    tmpEvent = new Event(actualEvent.getTime());
                                    tmpEvent.setEventType(Event.EventType.NEW_TASK_ON_MACHINE);
                                    tmpEvent.setMachineId(node.getNodeID());
                                    kolejkaKomunikatow.addEvent(tmpEvent);
                                }
                                node.addToBuffer(t);

                            }
                            
                        }// else nie generujemy kolejnych zdań i komunikatów
                        break;

                    case Event.EventType.NEW_TASK_ON_MACHINE: // pojawilo sie nowe zadanie w pustej kolejce na maszynie
                        node = nodes.Find(n => n.getNodeID() == actualEvent.getMachineId() ); // szukamy noda ktorego dotyczy event

                        if (node.isMoveFromBufferToMachinePossible())
                        {
                            
                            node.moveFromBufferToMachine(); // skoro przesunelismy z kolejki to generujemy zadanie
                            
                            tmpEvent = new Event(SimulationTime +node.getWorkTime());
                            tmpEvent.setEventType(Event.EventType.TASK_READY);
                            tmpEvent.setMachineId(node.getNodeID());

                            kolejkaKomunikatow.addEvent(tmpEvent);

                            if(!node.isBufferFull()) // jesli wzielismy zadnie i oproznilismy kolejke trzeba sprawdzic czy jakies zadanie nie czeka w swoim buforze
                            {
                                tmpEvent = new Event(SimulationTime);
                                tmpEvent.setEventType(Event.EventType.QUEUE_FREE);
                                tmpEvent.setMachineId(node.getNodeID());
                                kolejkaKomunikatow.addEvent(tmpEvent);
                            }


                        }
                        break;

                    case Event.EventType.TASK_READY:
                        {
                            node = nodes.Find(n => n.getNodeID() == actualEvent.getMachineId());

                            if (node.getNodeType() == 0 || node.getNodeType() == 3)
                            {
                                // jedyny albo ostatni node
                                // zadanie opuszcza system
                                Task x = node.getReadyTaskFromMachine(SimulationTime);
                                x.setEndTime(SimulationTime);
                            }

                            // mam lsite polaczen
                        
                            int i = 0;
                            float rand_number = (new Random()).Next(1000);
                            rand_number /= 1000;
                            List<Route> routes = node.getRoutes();
                            foreach (Route r in routes)
                            {
                                if (r.Probability < rand_number)
                                {
                                    i++;
                                    continue;
                                }
                                else{
                                    break;
                                }
                            }
                                                        
                        
                             // id noda
                            next_Node = nodes.Find(n => n.getNodeID() == routes.ElementAt(i).Destination);
                            if (!next_Node.isBufferFull()) // jesli bu
                            {
                                next_Node.addToBuffer( node.getReadyTaskFromMachine(SimulationTime) ); // przejscie 
                            }
                        
                        }
                        break;

                    case Event.EventType.FREE_BUFFER:

                        break;

                }

                // tutaj trzaba zaktualizowac liste NodeStatus


                NodeStatus ns;
                int j=0;
                for (int i = 0; i < nodes.Count; i++)
                {
                    ns = nodes.ElementAt(i).getNodeStatus();
                    
                    nodeStatusesList.ElementAt(i).numberOfJobsInQueue = ns.numberOfJobsInQueue;

                    // zostala aktualizacja maszyn
                }

            }
            
        }
        

        

        public int getNumberOfNodes(){
            return nodes.Count;
        }
        public static void Main(String[] args)
        {
            
        }
        
       
    }
}

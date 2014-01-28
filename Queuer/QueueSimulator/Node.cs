using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Queuer;

namespace QueueSimulator
{
    public class Node 
    {
        //nodeType - 0,1,2,3
        // 0- pojedynczy node
        // 1- poczatkowy
        // 2-srodkowy
        // 3- koncowy
        enum NodeType { OnlyOne = 0, First = 1, Middle = 2, Last = 3 };


        int BufferSize;
        Queue<Task> Buffer; // Domyslnie Fifo

        Task[] Slots;
        bool[] MachineBusyStatus;
        List<Route> routes;
        int NodeID;
        NodeType nodeType; // okresla node

        DistributionType serviceType;

        public List<Route> getRoutes()
        {
            return routes;
        }

        public Node(MachineDescription mDesc)
        {
            BufferSize = mDesc.QueueSize;
            var f =  mDesc.ServiceDiscipline;
            var x = mDesc.ServiceType;

            routes = mDesc.Routes;

            Buffer = new Queue<Task>(BufferSize);
            
            Slots = new Task[mDesc.SlotsNumber]; // ilosc slotow
            MachineBusyStatus = new bool[mDesc.SlotsNumber];

            for(int i=0;i<MachineBusyStatus.Length;i++)
            {
                MachineBusyStatus[i] = false; // czy zajety
            }

            NodeID = mDesc.Id;

            nodeType = (NodeType)mDesc.NodeType;

            

            //service dyspline - FIFO/LIFO/PS
            // service Type - jaki jest rozklad obslugi 
            
            // E - exponential - lambda
            // D - arbitralny - constant

            // typ servisu i jego parametr bedzie 
            // identyfikowal sposob generowania czasu zakonczenia prac

            switch (mDesc.ServiceType)
            {
                /*
                *  Traz mozemy wywolywac dla kazdej maszyny service.getTimeOfWork()
                */
                default:
                case "D":
                    serviceType = new D_constantDistribution(Int32.Parse(mDesc.ServiceTypeParameter));
                    break;
                case "E":
                    serviceType = new ExpDistribution(Double.Parse(mDesc.ServiceTypeParameter));
                    break;
            }

            

        }

        /* Funkcje Podstawowe Bufora*/
        public bool isBufferFull() // czy w buforze jest miejsce??
        {
            if (BufferSize <= 0)
            {
                return false;
            }
            else if (Buffer.Count<Task>() < BufferSize)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool isBufferEmpty()
        {
            return !(Buffer.Any());
        }

        public void addToBuffer(Task t){
            Buffer.Enqueue(t);
        }

        public Task getFromBuffer()
        {
            return Buffer.Dequeue();
        }


        
        /* funkcje podstawowe maszyn */

        public bool isAnyFreeMachine() // jestli jest wile maszyn to trzeba to sprawdzic
        {
            foreach (bool b in MachineBusyStatus)
            {
                if (b == true)
                {
                    continue;
                }
                else
                {
                    return true;
                }
                
            }
            return false;
        }

        public bool isAnyInMachine()
        {
            foreach (bool b in MachineBusyStatus)
            {
                if (b == true)
                    return true;
                else
                    continue;
            }
            return false;
        }

       

        public void addToMachine(Task t)
        {
            //if(isAnyFreeMachine()){ - to sprawdzamy pozom wyzej
            int i = 0;
            foreach (bool b in MachineBusyStatus)
            {
                if (b == false)
                { // maszyna jest wolna
                    Slots[i] = t;
                    MachineBusyStatus[i] = true;
                    break;
                }

                i++;
            }
        }

        public Task getReadyTaskFromMachine(int time) // oczywiscie czy cos jest na maszynie sprawdzAMY POZOM WYZEJ
        {   
            int i=0;
            foreach (bool b in MachineBusyStatus)
            {
                if (b == true)
                {
                    if (time == Slots[i].getReadyTime() || Slots[i].isReady())
                    {
                        MachineBusyStatus[i] = false;
                        return Slots[i];
                    }
                }
                i++;
            }
            return null; // ten return moze duzo niszczyc

        }

        public void markReadyTask(int time)
        {
            int i=0;
            foreach (bool b in MachineBusyStatus)
            {
                if (b == true)
                {
                    if (time == Slots[i].getReadyTime())
                    {
                        Slots[i].setReady();
                    }
                }
            }
        }

        /* Dodatkowe uzytki*/

        public bool isMoveFromBufferToMachinePossible(){
            return (this.isAnyFreeMachine() && !this.isBufferEmpty());
        }

        public void moveFromBufferToMachine(){
            this.addToMachine(this.getFromBuffer());
        }
        
        public NodeStatus getNodeStatus(){
            NodeStatus ns = new NodeStatus();
            ns.numberOfJobsInQueue = this.BufferSize;
            foreach(bool b in MachineBusyStatus){
                ns.machineWorkingStatuses.Add(b);
            }
            return ns;
        }

        public NodeStatus setNodeStatus()
        {
            NodeStatus ns = new NodeStatus();
            ns.numberOfJobsInQueue = this.BufferSize;
            foreach (bool b in MachineBusyStatus)
            {
                ns.machineWorkingStatuses.Add(b);
            }
            return ns;
        }

        public int getNodeType()
        {
            return (int)this.nodeType;
        }

        public int getNodeID()
        {
            return this.NodeID;
        }

        public int getWorkTime()
        {
            return (int)this.serviceType.getTimeOfWork();
            //jesli service type tu null
        }

        
    }
}

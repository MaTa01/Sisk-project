using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Queuer;

namespace QueueSimulator
{
    public class Node 
    {
        int BufferSize;
        Queue<Task> Buffer; // Domyslnie Fifo

        Task[] Slots;
        bool[] MachineBusyStatus;
        List<Route> routes;
        int NodeID;

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

            //nodeType - 0,1,2,3
            // 0- pojedynczy node
            // 1- poczatkowy
            // 2-srodkowy
            // 3- koncowy

            //service dyspline - FIFO/LIFO/PS
            // service Type - jaki jest rozklad obslugi 
            
            // E - exponential - lambda
            // D - arbitralny - constant


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
                    if (time == Slots[i].getReadyTime())
                    {
                        MachineBusyStatus[i] = false;
                        return Slots[i];
                    }
                }
                i++;
            }
            return null;

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
    }
}

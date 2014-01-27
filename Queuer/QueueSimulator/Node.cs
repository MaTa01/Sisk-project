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

        Task[] maszyny;
        bool[] machineBusyState;

        int NodeID;

        public Node(MachineDescription mDesc)
        {
            BufferSize = mDesc.QueueSize;
            Buffer = new Queue<Task>(BufferSize);
            
            maszyny = new Task[mDesc.SlotsNumber];
            machineBusyState = new bool[mDesc.SlotsNumber];

            for(int i=0;i<machineBusyState.Length;i++)
            {
                machineBusyState[i] = false; // czy zajety
            }

            NodeID = mDesc.Id;
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
            foreach (bool b in machineBusyState)
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
            foreach (bool b in machineBusyState)
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
            foreach (bool b in machineBusyState)
            {
                if (b == false)
                { // maszyna jest wolna
                    maszyny[i] = t;
                    machineBusyState[i] = true;
                    break;
                }

                i++;
            }
        }

        public Task getReadyTaskFromMachine(int time) // oczywiscie czy cos jest na maszynie sprawdzAMY POZOM WYZEJ
        {   
            int i=0;
            foreach (bool b in machineBusyState)
            {
                if (b == true)
                {
                    if (time == maszyny[i].getReadyTime())
                    {
                        machineBusyState[i] = false;
                        return maszyny[i];
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
       
    }
}

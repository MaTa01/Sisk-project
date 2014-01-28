using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueSimulator
{
    

    public class Event
    {
        public enum EventType { NEW_TASK, NEW_TASK_ON_MACHINE, TASK_READY, QUEUE_FREE, FREE_BUFFER }

        EventType eventType;
        int id;
        int time;
        int machineID; // jesli event dotyczy maszyny tu przechowujemy jej ID

        private Event()
        {
            this.id = (new Random()).Next(100000);
        }
        public Event(int id, int time){ // do testów
            this.id =id;
            this.time=time;
        }

        public Event(int time):this()
        {
            this.time = time;
        }

        public int getTime()
        {
            return this.time;
        }

        public int getID()
        {
            return this.id;
        }

        public void setEventType(EventType t)
        {
            eventType = t;
        }

        public EventType getEventType()
        {
            return this.eventType;
        }

        
        public void setMachineId(int id)
        {
            this.machineID = id;
        }

        public int getMachineId()
        {
            return this.machineID;
        }
    }
}

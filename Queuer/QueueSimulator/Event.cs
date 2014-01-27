using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueSimulator
{
    

    public class Event
    {
        int id;
        int time;

        public Event(int id, int time){
            this.id =id;
            this.time=time;
        }

        public int getTime()
        {
            return this.time;
        }

        public int getID()
        {
            return this.id;
        }
    }
}

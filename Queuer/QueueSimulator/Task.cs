using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueSimulator
{
    public class Task // to do implementacja taska

        // sa pytania co robic czy trzymac historie w tasku i przenosic go przez caly system czy poprostu tworzyc nowy task i usuwac stary jak tylko jest zdarzenie
    {
        private int SystemTime;
        private int ReadyTime;

        private bool Ready;
        private int endTime;

        public Task(int SystemTime)
        {
            // TODO: Complete member initialization
            this.SystemTime = SystemTime;
            this.ReadyTime = this.SystemTime + 60; // to pomaga testy przejsc ale bedzie potrzaba jakos to rozwazyc
            this.Ready = false;
        }


        public void setReadyTime(int time)
        {
            this.ReadyTime = time;
        }
        public int getReadyTime()
        {
            return ReadyTime;
        }

        public int getCreationTime()
        {
            return SystemTime;
        }



        public void setEndTime(int time)
        {
            this.endTime = time;
        }

        internal bool isReady()
        {
            return this.Ready;
        }

        internal void setReady()
        {
            throw new NotImplementedException();
        }
    }
}

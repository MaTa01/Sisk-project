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

        public Task(int SystemTime)
        {
            // TODO: Complete member initialization
            this.SystemTime = SystemTime;
            this.ReadyTime = this.SystemTime + 60;
        }

        public int getReadyTime()
        {
            return ReadyTime;
        }
    }
}

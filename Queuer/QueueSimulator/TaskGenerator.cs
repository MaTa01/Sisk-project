using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueSimulator
{
    // klasa odpwiedzialna za generowanie czazadania i czasu wygenerowania klejnego
    class TaskGenerator
    {
        private int  time;
       
        public TaskGenerator(int constantTime){ // generTOR BEDZIE generowal zadanie 
            time = constantTime;
        }

        public Task getTask(int SystemTime)
        {
            return new Task(SystemTime);
        }

        public int getTimeOfNewTAsk()
        {
            return time; // na razie na stalych czasach potem dodamy prawdopodobienstwo
        }
    }
}

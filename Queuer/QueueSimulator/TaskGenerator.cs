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
        private int taskLimit; // maksymalna ilosc zadan do wygenerowanai
        private int generatedTaskCounter;

        private TaskGenerator()
        {
            taskLimit = -1;
            time = 0;
            generatedTaskCounter = 0;
        }
        public TaskGenerator(int constantTime): this() { // generTOR BEDZIE generowal zadanie co okreslony odstep
            
            time = constantTime;
        }

        public Task getTask(int SystemTime) 
        {
            return new Task(0);
        }

        public int getTimeOfNewTAsk()
        {
            return time; // na razie na stalych czasach potem dodamy prawdopodobienstwo
        }

        public void setLimit(int limit)
        {
            taskLimit = limit;
        }
    }
}

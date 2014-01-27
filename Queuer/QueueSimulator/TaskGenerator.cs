using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueSimulator
{
    // klasa odpwiedzialna za generowanie czazadania i czasu wygenerowania klejnego


    // kalsa jest fabryka ktora tworzy zadania


    public class TaskGenerator
    {
        enum GeneratorType { Constant, Random }

        GeneratorType typGeneratora;
        int nextTaskTime;
        int lastTasktime;
        int constTime;
        int randTime;

        int taskCounter;


        private TaskGenerator(){
            nextTaskTime =0;
            lastTasktime =0;
            constTime =0;
            randTime =0;
            taskCounter = 0;
        }
        public TaskGenerator(int constant):this()
        {
            typGeneratora = GeneratorType.Constant;
            constTime = constant;

        }

        

        public Task getTask(){ // pierwszy task oznacza wzgledny czas systemu =0
            
            int _time;

            if (taskCounter == 0)
            {
                _time = 0;
            }
            else
            {
                _time = nextTaskTime;
            }
            
            Task t = new Task(_time); // tworzymy nowy task
            taskCounter++;

            lastTasktime = _time;
            nextEventTime();

            return t;

        }

       

        private void nextEventTime(){
            switch (typGeneratora)
            {
                case GeneratorType.Constant:
                    nextTaskTime = lastTasktime + constTime;
                    break;

                
            }
        }


        
    }
}

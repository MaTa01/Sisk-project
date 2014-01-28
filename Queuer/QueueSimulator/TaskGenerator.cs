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
        
        int randTime;

        DistributionType distribution;
        EventQueue eq;

        int taskCounter;


        private TaskGenerator(){
            nextTaskTime =0;
            lastTasktime =0;
           
            randTime =0;
            taskCounter = 0;
            eq = EventQueue.Instance;
            
        }
        public TaskGenerator(int constant):this()
        {
            

            distribution = new D_constantDistribution(constant);
        }

        public TaskGenerator(float expectedValue)
            : this()
        {
            
            
            distribution = new ExpDistribution(expectedValue);
        }

        public Task getTask(){ // pierwszy task oznacza wzgledny czas systemu =0
            
            double _time;

            if (taskCounter == 0)
            {
                _time = 0;
            }
            else
            {
                _time = nextTaskTime;
            }
            
            Task t = new Task((int)_time); // tworzymy nowy task
            taskCounter++;

            lastTasktime = (int)_time;
            
            nextEventTime();

            return t;

        }

        public Task getTask(int systemTime) // zwraca 
        {
            Task t = new Task(systemTime);
            //nextEventTime();

            //Event e = new Event(nextTaskTime);
            Event e = new Event((int)distribution.getTimeOfWork());
            e.setEventType(Event.EventType.NEW_TASK);

            
            eq.addEvent(e); // tu jest eq nullem
            taskCounter++;

            return t;
        }


        private void nextEventTime()
        {
            
            nextTaskTime = lastTasktime + (int)distribution.getTimeOfWork();
            
        }




        public int getTaskCounter()
        {
            return taskCounter;
        }
    }
}

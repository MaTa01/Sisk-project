using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueSimulator
{
    /* klasa zawierajaca typy rozknaz
     * */
    class SimulatorEvent
    {
        
        public SimulatorEvent( TypZadania t){
            typ = t;
        }

        public TypZadania typ;

        public enum TypZadania { getFromQueue, addToQueue, startWork, finishWork, newTask };

    }
}

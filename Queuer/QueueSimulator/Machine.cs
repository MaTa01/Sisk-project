using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queuer;

namespace QueueSimulator
{
    public class Machine
    {
        Queue<Task> kolejka;
        Discipline dyscyplina; // abstarakt pod ktory podepniemy rozklad prawdopodobeinstawa
        

        public Machine(MachineDescription desc) // uzuywamy opisu do stworzenia maszyny
        {
            kolejka = new Queue<Task>(desc.QueueSize);


        }

        public void addToMachine(Task t)
        {

        }

        public Task getfromMachine()
        {
            return new Task(0); // uwaga hardkod
        }

        void setDycipline(String discipline)
        {

        }
    }
}

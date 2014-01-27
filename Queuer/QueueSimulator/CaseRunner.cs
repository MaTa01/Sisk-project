using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Queuer;

namespace QueueSimulator
{
    class CaseRunner
    {
        public static void Main(String[] args)
        {
            CaseRunner.Lab1Zad1a();
        }

        public static void Lab1Zad1a()
        {
            List<MachineDescription> machines = new List<MachineDescription>();

            MachineDescription machineDesc = new MachineDescription();
            machineDesc.Id = 0;
            machineDesc.QueueSize = 0;
            machineDesc.SlotsNumber = 1;
            machineDesc.ServiceDiscipline = "FIFO";

            machines.Add(machineDesc);

            int odstepyPomiedzyNowymiZadaniami = 120;
            TaskGenerator generatorZadan = new TaskGenerator(odstepyPomiedzyNowymiZadaniami); // ten obiekt bedzie tworzyl nowe zadanie co jakis odstep czasu
            Simulator _testSimulator = new Simulator(machines, generatorZadan);

            int limitZadan = 1000;
            _testSimulator.runSimulator(limitZadan);
        }
    }
}

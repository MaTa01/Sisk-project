using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Queuer
{
    class FileReader
    {
        private string inputFileName;

        public string InputFileName
        {
            get { return inputFileName; }
            set { inputFileName = value; }
        }

        public FileReader()
        {
            inputFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\data\\machines.txt");
        }

        public List<MachineDescription> ReadFile()
        {
            List<MachineDescription> machineDescriptions = new List<MachineDescription>();

            using(StreamReader reader = new StreamReader(inputFileName))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    String[] machineParameters = line.Split(' ');
                    MachineDescription machineDescription = NextMachine(machineParameters);
                    machineDescriptions.Add(machineDescription);
                }
            }
            return machineDescriptions;
        }
        
        private MachineDescription NextMachine(String[] machineParams)
        {
            MachineDescription description = new MachineDescription();
            int id;
            if(Int32.TryParse(machineParams[0], out id))
            {
                description.Id = id;
            }
            else
            {
                description.Id = -1;
            }
            int slotsNumber;
            if(Int32.TryParse(machineParams[1], out slotsNumber))
            {
                description.SlotsNumber = slotsNumber;
            }
            else
            {
                description.SlotsNumber = -1;
            }
            int queueSize;
            if(Int32.TryParse(machineParams[2], out queueSize))
            {
                description.QueueSize = queueSize;
            }
            else
            {
                description.QueueSize = -1;
            }
            
            string  queueDiscipline = machineParams[3];
            Regex regex = new Regex(@"\w*");
            Match match = regex.Match(queueDiscipline);
            if(match.Success)
                description.QueueDiscipline = queueDiscipline;
            else
                description.QueueDiscipline = "INVALID_INPUT";

            string  streamType = machineParams[4];
            match = regex.Match(streamType);
            if(match.Success)
                description.StreamType = streamType;
            else
                description.StreamType = "INVALID_INPUT";

            int  coordinateX;
            if(Int32.TryParse(machineParams[5], out coordinateX))
            {
                description.CoordinateX = coordinateX;
            }
            else
            {
                description.CoordinateX = -1;
            }
            int  coordinateY;
            if(Int32.TryParse(machineParams[6], out coordinateY))
            {
                description.CoordinateY = coordinateY;
            }
            else
            {
                description.CoordinateY = -1;
            }
            return description;
        }
    }
}

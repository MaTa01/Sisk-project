using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Queuer
{
    public class FileReader
    {
        private string inputFileName;
        public bool InputError { get; set; }

        public string InputFileName
        {
            get { return inputFileName; }
            set { inputFileName = value; }
        }

        public FileReader()
        {
            inputFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\data\\machines.txt");
            InputError = true;
        }

        public List<MachineDescription> ReadFile()
        {
            List<MachineDescription> machineDescriptions = new List<MachineDescription>();
            InputError = true;
            using(StreamReader reader = new StreamReader(inputFileName))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    String[] machineParameters = line.Split(' ');
                    if (machineParameters.Length < 7)
                    {
                        InputError = true;
                        return null;
                    }
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
            InputError = false;
            // TODO trim endline spaces, because if they show up, they are treated as an another parameter
            if(Int32.TryParse(machineParams[0], out id))
            {
                description.Id = id;
            }
            else
            {
                description.Id = -1;
                InputError = true;
                return null;
            }
            int nodeType;
            if(Int32.TryParse(machineParams[1], out nodeType))
            {
                description.NodeType = nodeType;
            }
            else
            {
                description.NodeType = -1;
                InputError = true;
            }
            int slotsNumber;
            if(Int32.TryParse(machineParams[2], out slotsNumber))
            {
                description.SlotsNumber = slotsNumber;
            }
            else
            {
                description.SlotsNumber = -1;
                InputError = true;
            }
            int queueSize;
            if(Int32.TryParse(machineParams[3], out queueSize))
            {
                description.QueueSize = queueSize;
            }
            else
            {
                description.QueueSize = -1;
                InputError = true;
            }
            
            string  queueDiscipline = machineParams[4];
            Regex regex = new Regex(@"\w*");
            Match match = regex.Match(queueDiscipline);
            if (match.Success)
            {
                description.ServiceDiscipline = queueDiscipline;
            }
            else
            {
                description.ServiceDiscipline = "INVALID_INPUT";
                InputError = true;
            }

            string  serviceType = machineParams[5];
            match = regex.Match(serviceType);
            if (match.Success)
            {
                description.ServiceType = serviceType;
            }
            else
            {
                description.ServiceType = "INVALID_INPUT";
                InputError = true;
            }
            string  serviceTypeParameter = machineParams[6];
            match = regex.Match(serviceTypeParameter);
            if (match.Success)
            {
                description.ServiceTypeParameter = serviceTypeParameter;
            }
            else
            {
                description.ServiceType = "INVALID_INPUT";
                InputError = true;
            }

            int  coordinateX;
            if(Int32.TryParse(machineParams[7], out coordinateX))
            {
                description.CoordinateX = coordinateX;
            }
            else
            {
                description.CoordinateX = -1;
                InputError = true;
            }
            int  coordinateY;
            if(Int32.TryParse(machineParams[8], out coordinateY))
            {
                description.CoordinateY = coordinateY;
            }
            else
            {
                description.CoordinateY = -1;
                InputError = true;
            }
            int destination;
            double probability;
            for (int i = 9; i < machineParams.Length; i+=2)
            {
                bool destinationParsed;
                bool probabilityParsed;
                destinationParsed = Int32.TryParse(machineParams[i], out destination);
                probabilityParsed = Double.TryParse(machineParams[i + 1], NumberStyles.Number, CultureInfo.InvariantCulture, out probability);
                if(destinationParsed && probabilityParsed)
                {
                    description.Routes.Add(new Route
                    {
                        Destination = destination,
                        Probability = probability
                    });
                }
                else
                {
                    description.Routes = null;
                    InputError = true;
                }
            }
            return description;
        }
    }
}

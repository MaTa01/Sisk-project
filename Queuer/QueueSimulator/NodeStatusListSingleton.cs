using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueSimulator
{
    public class NodeStatusListSingleton
    {
        private static readonly List<NodeStatus> instance = new List<NodeStatus>();

        
        public static List<NodeStatus> Instance
        {
            get 
            {
                return instance;
            }
        }

        public void addNodeStatus(NodeStatus ns){
            instance.Add(ns);
        }
    }
}

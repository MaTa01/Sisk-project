using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueSimulator
{
    abstract class InsideQueue<T> : Queue<T>// kolejka ktora
    {
        public abstract void pushToQueue(Task t);
        public abstract Task popFromQueue();
    }
}

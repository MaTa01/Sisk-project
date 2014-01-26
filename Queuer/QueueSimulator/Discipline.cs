using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueSimulator
{
    /*klasa abstrakcyjan pozwoli dodac do kolejki dowolna dyscypline LIFO / FIFO
     */
    abstract class Discipline
    {
        int getTime();
    }
}

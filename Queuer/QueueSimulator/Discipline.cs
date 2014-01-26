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
        // Error	1	'QueueSimulator.Discipline.getTime()' must declare a body because it is not marked abstract, extern, or partial	D:\dev\cs\pwr\sisk\Sisk-project\Queuer\QueueSimulator\Discipline.cs	12	13	QueueSimulator
        abstract public int getTime();
    }
}

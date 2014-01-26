using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueSimulator
{
    class D_discipline : Discipline
    {
        int returnValue;
        public D_discipline(int D)
        {
            returnValue = D;
        }
        public int getTime()
        {
            return returnValue;
        }
    }
}

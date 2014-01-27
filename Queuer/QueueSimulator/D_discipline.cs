using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace QueueSimulator
{
    class D_discipline : Discipline
    {
        int returnValue;
        public D_discipline(int D)
        {
            returnValue = D;
        }
        override public int getTime()
        {
            return returnValue;
        }
    }
}

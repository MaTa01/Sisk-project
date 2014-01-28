using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueSimulator
{
    public class D_constantDistribution : DistributionType
    {

        double parameter;

        public D_constantDistribution(double parameter)  
        {
            this.parameter = parameter;
        }

        public override double getTimeOfWork()
        {
            return (this.parameter);
        }
    }
}

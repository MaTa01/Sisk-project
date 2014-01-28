using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;

namespace QueueSimulator
{
    public class ExpDistribution:DistributionType
    {
        double expectedValue;
        Exponential exponential;

        public ExpDistribution(double expectedValue)
        {
            this.expectedValue = expectedValue;
            exponential = new Exponential(1.0 /expectedValue);

        }

        public override double getTimeOfWork()
        {
            return GetNextExponential();
            
            
        }
            private double GetNextExponential()
            {
                return exponential.Sample();
            }
    }
}

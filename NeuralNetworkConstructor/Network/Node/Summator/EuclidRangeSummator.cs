using System;
using System.Linq;

namespace NeuralNetworkConstructor.Network.Node.Summator
{
    public class EuclidRangeSummator : ISummator
    {

        public double GetSum(ISlaveNode node)
        {
            return GetEuclidRange(node);
        }

        public static double GetEuclidRange(ISlaveNode node)
        {
            var sum = node.Synapses.Sum(s => Math.Pow(s.MasterNode.Output() - s.Weight, 2));
            return Math.Sqrt(sum);
        }

    }
}

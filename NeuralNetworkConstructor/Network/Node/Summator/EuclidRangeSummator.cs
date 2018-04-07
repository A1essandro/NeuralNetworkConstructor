using System;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Network.Node.Summator
{
    public class EuclidRangeSummator : ISummator
    {

        public double GetSum(ISlaveNode node)
        {
            return GetEuclidRange(node);
        }

        public async Task<double> GetSumAsync(ISlaveNode node)
        {
            return await GetEuclidRangeAsync(node);
        }

        public static double GetEuclidRange(ISlaveNode node)
        {
            var sum = node.Synapses.Sum(s => Math.Pow(s.MasterNode.Output() - s.Weight, 2));
            return Math.Sqrt(sum);
        }

        public async static Task<double> GetEuclidRangeAsync(ISlaveNode node)
        {
            var tasks = node.Synapses
                .Select(async s => Math.Pow(await s.MasterNode.OutputAsync().ConfigureAwait(false) - s.Weight, 2));
            var sum = (await Task.WhenAll(tasks).ConfigureAwait(false)).Sum();
            return Math.Sqrt(sum);
        }

    }
}

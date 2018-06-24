using System;
using System.Linq;
using System.Threading.Tasks;
using NeuralNetwork.Structure.Nodes;

namespace NeuralNetwork.Structure.Summators
{

    public class EuclidRangeSummator : ISummator
    {

        public async Task<double> GetSum(ISlaveNode node)
        {
            return await GetEuclidRange(node);
        }

        public async static Task<double> GetEuclidRange(ISlaveNode node)
        {
            var tasks = node.Synapses
                .Select(async s => Math.Pow(await s.MasterNode.Output().ConfigureAwait(false) - s.Weight, 2));
            var sum = (await Task.WhenAll(tasks).ConfigureAwait(false)).Sum();
            return Math.Sqrt(sum);
        }

    }
}

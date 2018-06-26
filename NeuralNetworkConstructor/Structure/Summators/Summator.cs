using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using NeuralNetworkConstructor.Structure.Nodes;

namespace NeuralNetworkConstructor.Structure.Summators
{

    [DataContract]
    public class Summator : ISummator
    {

        public async Task<double> GetSum(ISlaveNode node)
        {
            var tasks = node.Synapses.Select(async x => await x.Output().ConfigureAwait(false));
            return (await Task.WhenAll(tasks).ConfigureAwait(false)).Sum();
        }

    }
}

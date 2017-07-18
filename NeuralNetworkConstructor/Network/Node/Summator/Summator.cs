using System.Linq;

namespace NeuralNetworkConstructor.Network.Node.Summator
{
    public class Summator : ISummator
    {

        public double GetSum(ISlaveNode node)
        {
            return node.Synapses.Sum(x => x.Output());
        }

    }
}

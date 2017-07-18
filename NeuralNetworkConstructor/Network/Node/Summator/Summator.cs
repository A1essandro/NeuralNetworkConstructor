using System.Linq;

namespace NeuralNetworkConstructor.Network.Node.Summator
{
    public class Summator : ISummator
    {

        private readonly ISlaveNode _node;

        public Summator(ISlaveNode node)
        {
            _node = node;
        }

        public double GetSum()
        {
            return _node.Synapses.Sum(x => x.Output());
        }

    }
}

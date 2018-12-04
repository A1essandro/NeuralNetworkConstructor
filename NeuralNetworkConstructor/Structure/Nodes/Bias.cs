using System;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Structure.Nodes
{
    public sealed class Bias : IMasterNode, INotInputNode
    {

        public event Action<double> OnOutput;

        private const double VALUE = 1.0;

        public Task<double> Output()
        {
            OnOutput?.Invoke(VALUE);
            return Task.FromResult(VALUE);
        }

    }
}

using System;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Structure.Nodes
{
    public class Bias : IMasterNode, INotInputNode
    {
        public event Action<double> OnOutput;
        private const double VALUE = 1.0; 

        public async Task<double> Output()
        {
            return await Task.FromResult(VALUE).ConfigureAwait(false);
        }
    }
}

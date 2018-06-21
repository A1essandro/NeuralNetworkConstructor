using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Nodes
{
    public class Bias : IMasterNode, INotInputNode
    {
        public event Action<double> OnOutput;
        private const double VALUE = 1.0; 

        public double Output()
        {
            OnOutput?.Invoke(VALUE);

            return VALUE;
        }

        public Task<double> OutputAsync()
        {
            return Task.Run(() => VALUE);
        }
    }
}

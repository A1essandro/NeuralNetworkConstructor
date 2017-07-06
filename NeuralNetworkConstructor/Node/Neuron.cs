using NeuralNetworkConstructor.Node.ActivationFunction;
using NeuralNetworkConstructor.Node.Synapse;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkConstructor.Node
{
    public class Neuron : INode
    {

        private IActivationFunction _activationFunction;
        private double? _calculatedOutput = null;

        public ICollection<ISynapse> Synapses { get; private set; } = new List<ISynapse>();

        public Neuron(IActivationFunction function)
        {
            _activationFunction = function;
        }

        public Neuron(IActivationFunction function, ICollection<ISynapse> synapses)
            : this(function)
        {
            Synapses = synapses;
        }

        public void AddSynapse(ISynapse synapse)
        {
            Synapses.Add(synapse);
        }

        public double Output()
        {
            if (_calculatedOutput != null)
            {
                return _calculatedOutput.Value;
            }

            _calculatedOutput = Synapses.Sum(x => x.Output());
            return _calculatedOutput.Value;
        }

        public void Refresh()
        {
            _calculatedOutput = null;
        }
    }
}

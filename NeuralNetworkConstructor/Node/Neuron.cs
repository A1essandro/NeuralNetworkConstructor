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
        private ICollection<ISynapse> _synapses;

        public Neuron(IActivationFunction function)
        {
            _activationFunction = function;
        }

        public Neuron(IActivationFunction function, ICollection<ISynapse> synapses)
            : this(function)
        {
            _synapses = synapses;
        }

        public void AddSynapse(ISynapse synapse)
        {
            _synapses.Add(synapse);
        }

        public double Output()
        {
            if (_calculatedOutput != null)
            {
                return _calculatedOutput.Value;
            }

            _calculatedOutput = _synapses.Sum(x => x.Output());
            return _calculatedOutput.Value;
        }

        public void refresh()
        {
            _calculatedOutput = null;
        }
    }
}

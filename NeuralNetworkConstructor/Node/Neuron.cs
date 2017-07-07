using NeuralNetworkConstructor.Node.ActivationFunction;
using NeuralNetworkConstructor.Node.Synapse;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkConstructor.Node
{
    public class Neuron : INode
    {

        private readonly IActivationFunction _activationFunction;
        private double? _calculatedOutput;

        public ICollection<ISynapse> Synapses { get; } = new List<ISynapse>();

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

            _calculatedOutput = _activationFunction
                .Calculate(Synapses.Sum(x => x.Output()));
            return _calculatedOutput.Value;
        }

        /// <summary>
        /// Zeroing of output calculation
        /// Should be called after entry new Input data
        /// </summary>
        internal void Refresh()
        {
            _calculatedOutput = null;
        }
    }
}

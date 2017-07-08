using System.Collections.Generic;
using System.Linq;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System;

namespace NeuralNetworkConstructor.Network.Node
{
    public class Neuron : INode
    {

        private readonly Func<double, double> _activationFunction;
        private double? _calculatedOutput;

        public ICollection<ISynapse> Synapses { get; } = new List<ISynapse>();

        public Neuron(IActivationFunction function)
            : this(function.Calculate)
        {
        }

        public Neuron(Func<double, double> function)
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

            _calculatedOutput = _activationFunction(Synapses.Sum(x => x.Output()));
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

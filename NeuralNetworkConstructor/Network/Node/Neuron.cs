using System.Collections.Generic;
using System.Linq;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System;
using NeuralNetworkConstructor.Common;

namespace NeuralNetworkConstructor.Network.Node
{
    public class Neuron : ISlaveNode, IRefreshable
    {

        private readonly Func<double, double> _activationFunction;
        private double? _calculatedOutput;

        /// <summary>
        /// Collection of synapses to this node
        /// </summary>
        public ICollection<ISynapse> Synapses { get; } = new List<ISynapse>();

        public event Action<Neuron> OnOutputCalculated;

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

        /// <summary>
        /// Adding synapse from master node to this node
        /// </summary>
        /// <param name="synapse"></param>
        public void AddSynapse(ISynapse synapse)
        {
            Synapses.Add(synapse);
        }

        public virtual double Output()
        {
            if (_calculatedOutput != null)
            {
                return _calculatedOutput.Value;
            }

            _calculatedOutput = _activationFunction != null
                ? _activationFunction.Invoke(Synapses.Sum(x => x.Output()))
                : Synapses.Sum(x => x.Output());
            NotifyOutputCalculated();
            return _calculatedOutput.Value;
        }

        protected void NotifyOutputCalculated()
        {
            OnOutputCalculated?.Invoke(this);
        }

        /// <summary>
        /// Zeroing of output calculation
        /// Should be called after entry new Input data
        /// </summary>
        public void Refresh()
        {
            _calculatedOutput = null;
        }

    }
}

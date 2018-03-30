using System;
using System.Collections.Generic;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;
using NeuralNetworkConstructor.Network.Node.Summator;
using NeuralNetworkConstructor.Common;

namespace NeuralNetworkConstructor.Network.Node
{
    public class Neuron : ISlaveNode, IRefreshable
    {

        private double? _calculatedOutput;

        public ISummator Summator { get; }

        /// <summary>
        /// Collection of synapses to this node
        /// </summary>
        public ICollection<ISynapse> Synapses { get; } = new List<ISynapse>();

        public IActivationFunction Function { get; }

        public event Action<Neuron> OnOutputCalculated;
        public event Action<double> OnOutput;

        public Neuron(IActivationFunction function, ISummator summator = null)
        {
            Function = function;
            Summator = summator ?? new Summator.Summator();
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

            _calculatedOutput = Function != null
                ? Function.GetEquation(Summator.GetSum(this))
                : Summator.GetSum(this);
            NotifyOutputCalculated();
            OnOutput?.Invoke(_calculatedOutput.Value);

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

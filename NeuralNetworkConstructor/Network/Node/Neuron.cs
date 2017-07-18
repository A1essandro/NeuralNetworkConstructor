using System.Collections.Generic;
using System.Linq;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System;
using NeuralNetworkConstructor.Common;

namespace NeuralNetworkConstructor.Network.Node
{
    public class Neuron : ISlaveNode, IRefreshable<Neuron>
    {

        private double? _calculatedOutput;

        public ISummator Summator { get; }

        /// <summary>
        /// Collection of synapses to this node
        /// </summary>
        public ICollection<ISynapse> Synapses { get; } = new List<ISynapse>();

        public IActivationFunction Function { get; }

        public event Action<Neuron> OnOutputCalculated;

        public Neuron(IActivationFunction function)
        {
            Function = function;
            Summator = new StandartSummator(this);
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
                ? Function.GetEquation(Summator.GetSum())
                : Summator.GetSum();
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

        public class StandartSummator : ISummator
        {

            private readonly ISlaveNode _node;

            public StandartSummator(ISlaveNode node)
            {
                _node = node;
            }

            public double GetSum()
            {
                return _node.Synapses.Sum(x => x.Output());
            }

        }

    }
}

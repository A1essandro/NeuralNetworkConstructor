using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Summator;
using NeuralNetworkConstructor.Network.Node.Synapse;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Network.Node
{
    public class Neuron : ISlaveNode, IRefreshable
    {

        private double? _calculatedOutput;

        private readonly AsyncAutoResetEvent _waitHandle = new AsyncAutoResetEvent(true);

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
                OnOutput?.Invoke(_calculatedOutput.Value);
                return _calculatedOutput.Value;
            }

            _calculatedOutput = Function != null
                ? Function.GetEquation(Summator.GetSum(this))
                : Summator.GetSum(this);
            OnOutputCalculated?.Invoke(this);
            OnOutput?.Invoke(_calculatedOutput.Value);

            return _calculatedOutput.Value;
        }

        /// <summary>
        /// Zeroing of output calculation
        /// Should be called after entry new Input data
        /// </summary>
        public void Refresh()
        {
            _calculatedOutput = null;
        }

        public async Task<double> OutputAsync()
        {
            await _waitHandle.WaitAsync();
            if (_calculatedOutput != null)
            {
                _waitHandle.Set();
                OnOutput?.Invoke(_calculatedOutput.Value);
                return _calculatedOutput.Value;
            }

            var sum = await Summator.GetSumAsync(this);
            _calculatedOutput = Function != null
                ? Function.GetEquation(sum)
                : sum;
            _waitHandle.Set();

            OnOutputCalculated?.Invoke(this);
            OnOutput?.Invoke(_calculatedOutput.Value);

            return _calculatedOutput.Value;
        }
    }

    public class Neuron<TActivationFunction> : Neuron
        where TActivationFunction : IActivationFunction, new()
    {

        public Neuron()
            : this((ISummator)null)
        {
        }

        public Neuron(ISummator summator = null)
            : base(new TActivationFunction(), summator)
        {
        }

        public Neuron(ICollection<ISynapse> synapses)
            : base(new TActivationFunction(), synapses)
        {
        }
    }

}

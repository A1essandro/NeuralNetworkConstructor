﻿using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.ActivationFunctions;
using NeuralNetworkConstructor.Structure.Summators;
using NeuralNetworkConstructor.Structure.Synapses;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Structure.Nodes
{

    [DataContract]
    [KnownType(typeof(Summator))]
    [KnownType(typeof(Synapse))]
    [KnownType(typeof(Rectifier))]
    [KnownType(typeof(Logistic))]
    [KnownType(typeof(Linear))]
    [KnownType(typeof(Gaussian))]
    public class Neuron : ISlaveNode, IRefreshable
    {

        #region serialization data

        [DataMember]
        private readonly ISummator _summator;

        [DataMember]
        private readonly ICollection<ISynapse> _synapses = new List<ISynapse>();

        [DataMember]
        private IActivationFunction _actFunction;

        #endregion

        private double? _calculatedOutput;

        private AutoResetEvent _waitHandle = new AutoResetEvent(true);

        #region public properties

        /// <summary>
        /// Collection of synapses to this node
        /// </summary>
        public ICollection<ISynapse> Synapses => _synapses;

        public ISummator Summator => _summator;

        public IActivationFunction Function
        {
            get => _actFunction;
            set => _actFunction = value;
        }

        #endregion

        public event Action<Neuron> OnOutputCalculated;
        public event Action<double> OnOutput;

        #region ctors

        public Neuron()
        {
            _summator = new Summator();
        }

        public Neuron(IActivationFunction function, ISummator summator = null)
        {
            _actFunction = function;
            _summator = summator ?? new Summator();
        }

        public Neuron(IActivationFunction function, ICollection<ISynapse> synapses)
            : this(function)
        {
            _synapses = synapses;
        }

        #endregion

        #region IOutput

        public async Task<double> Output()
        {
            _waitHandle.WaitOne();
            if (_calculatedOutput != null)
            {
                _waitHandle.Set();
                OnOutput?.Invoke(_calculatedOutput.Value);
                return _calculatedOutput.Value;
            }

            var sum = await Summator.GetSum(this);
            _calculatedOutput = Function != null
                ? Function.GetEquation(sum)
                : sum;
            _waitHandle.Set();

            OnOutputCalculated?.Invoke(this);
            OnOutput?.Invoke(_calculatedOutput.Value);

            return _calculatedOutput.Value;
        }

        #endregion

        /// <summary>
        /// Adding synapse from master node to this node
        /// </summary>
        /// <param name="synapse"></param>
        public void AddSynapse(ISynapse synapse)
        {
            Synapses.Add(synapse);
        }

        /// <summary>
        /// Zeroing of output calculation
        /// Should be called after entry new Input data
        /// </summary>
        public Task Refresh()
        {
            _calculatedOutput = null;
            return Task.CompletedTask;
        }

        [OnDeserializing]
        private void Deserialize(StreamingContext ctx)
        {
            _waitHandle = new AutoResetEvent(true);
        }

    }

}

using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.ActivationFunctions;
using NeuralNetworkConstructor.Structure.Summators;
using NeuralNetworkConstructor.Structure.Synapses;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    [KnownType(typeof(AsIs))]
    public class Neuron : ISlaveNode, IRefreshable
    {

        #region serialization data

        [DataMember]
        private readonly ICollection<ISynapse> _synapses = new List<ISynapse>();

        [DataMember]
        private ISummator _summator;

        [DataMember]
        private IActivationFunction _actFunction;

        #endregion

        private static IActivationFunction DefaultActivationFunction = new AsIs();

        private static ISummator DefaultSummator = new Summator();

        private double? _calculatedOutput;

        private AutoResetEvent _waitHandle = new AutoResetEvent(true);

        #region public properties

        /// <summary>
        /// Collection of synapses to this node
        /// </summary>
        public ICollection<ISynapse> Synapses => _synapses;

        public ISummator Summator
        {
            get => _summator;
            set => _summator = value;
        }

        public IActivationFunction Function
        {
            get => _actFunction;
            set => _actFunction = value;
        }

        #endregion

        public event Action<double> OnOutput;

        #region ctors

        public Neuron()
            : this(DefaultActivationFunction, DefaultSummator)
        {
        }

        public Neuron(IActivationFunction function, ISummator summator = null)
        {
            _actFunction = function;
            _summator = summator ?? DefaultSummator;
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

            var sum = await _summator.GetSum(this);
            _calculatedOutput = _actFunction.GetEquation(sum);
            _waitHandle.Set();

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
            Contract.Requires(synapse != null, nameof(synapse));

            _synapses.Add(synapse);
        }

        /// <summary>
        /// Zeroing of output calculation
        /// Should be called after entry new Input data
        /// </summary>
        public void Refresh()
        {
            _calculatedOutput = null;
        }

        [OnDeserializing]
        private void Deserialize(StreamingContext ctx)
        {
            _waitHandle = new AutoResetEvent(true);
        }

    }

}

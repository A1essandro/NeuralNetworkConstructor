using System;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using System.Collections.Generic;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System.Linq;
using System.Diagnostics.Contracts;
using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Node.Summator;

namespace NeuralNetworkConstructor.Network.Node
{

    /// <summary>
    /// Node with memory for Elman networks or Jordan networks
    /// </summary>
    public class Context : ISlaveNode
    {

        private readonly Func<double, double> _activationFunction;
        private readonly Queue<double[]> _memory;
        private INode[] _masterNodes;
        private double[] _currentMemoryChunk;

        /// <summary>
        /// Collection of synapses to this node
        /// </summary>
        public ICollection<ISynapse> Synapses { get; }

        public ISummator Summator { get; }

        /// <summary>
        /// Delay between input and appropriate output
        /// </summary>
        public ushort Delay { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="delay"><see cref="Delay"/></param>
        /// <param name="synapses"><see cref="Synapses"/> Create empty list if null</param>
        /// <param name="summator"></param>
        public Context(Func<double, double> function, ushort delay = 1, 
            ICollection<ISynapse> synapses = null, ISummator summator = null)
        {
            _activationFunction = function;
            Synapses = synapses;
            _memory = new Queue<double[]>();
            Delay = delay;
            Summator = summator ?? new Summator.Summator(this);
            Synapses = synapses ?? new List<ISynapse>();
            _calculateMasterNeurons();
        }

        /// <summary>
        /// Calculates output with delay <see cref="Delay"/>
        /// </summary>
        /// <returns></returns>
        public double Output()
        {
            double output = 0;

            foreach (var mNode in _masterNodes.Where(n => !(n is IRefreshable<INode>)))
            {
                //TODO: if master node have master-relation only with Contexts nodes - this line out of reach:
                _currentMemoryChunk[Array.IndexOf(_masterNodes, mNode)] = mNode.Output();
            }

            if (_memory.Count == Delay)
            {
                output = _memory.Dequeue().Sum();
            }
            _memory.Enqueue(_currentMemoryChunk);

            _currentMemoryChunk = new double[_masterNodes.Length];
            return _activationFunction?.Invoke(output) ?? output;
        }

        /// <summary>
        /// Adding synapse from master node to this node
        /// </summary>
        /// <param name="synapse">Synapse adding to synapses <see cref="Synapses"/></param>
        public void AddSynapse(ISynapse synapse)
        {
            Contract.Requires(synapse != null, nameof(synapse));

            Synapses.Add(synapse);
            _calculateMasterNeurons();
        }

        public IActivationFunction Function { get; } = null;

        private void _onMasterNeuronCalculated(INode neuron)
        {
            var index = Array.IndexOf(_masterNodes, neuron);
            _currentMemoryChunk[index] = neuron.Output();
        }

        private void _calculateMasterNeurons()
        {
            _masterNodes = Synapses.Select(s => s.MasterNode).ToArray();
            foreach (var neuron in _masterNodes.Where(n => n is IRefreshable<INode>))
            {
                (neuron as IRefreshable<INode>).OnOutputCalculated += _onMasterNeuronCalculated;
            }
            _currentMemoryChunk = new double[_masterNodes.Length];
        }

    }
}

using System;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using System.Collections.Generic;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System.Linq;

namespace NeuralNetworkConstructor.Network.Node
{
    public class Context : Neuron
    {

        private readonly ushort _delay = 1;
        private readonly Queue<double[]> _memory;
        private readonly Neuron[] _masterNodes;
        private double[] _currentMemoryChunk;

        public Context(IActivationFunction function, ICollection<ISynapse> synapses, ushort delay = 1)
            : base(function, synapses)
        {
            _memory = new Queue<double[]>();
            _masterNodes = Synapses.Select(s => s.MasterNode as Neuron).ToArray();
            foreach(var neuron in _masterNodes)
            {
                neuron.OnOutputCalculated += _onMasterNeuronCalculated;
            }
            _delay = delay;
        }

        public override double Output()
        {
            double output = 0;
            _currentMemoryChunk = new double[_masterNodes.Length];
            if (_memory.Count == _delay)
            {
                output = _memory.Dequeue().Sum();
            }
            _memory.Enqueue(_currentMemoryChunk);

            return output;
        }

        private void _onMasterNeuronCalculated(Neuron neuron)
        {
            var index = Array.IndexOf(_masterNodes, neuron);
            _currentMemoryChunk[index] = neuron.Output();
        }

    }
}

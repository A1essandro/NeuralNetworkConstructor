using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Node;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NeuralNetworkConstructor.Network
{
    public class Network : INetwork
    {

        private INode[] _inputs;
        private ILayer _outputLayer;

        public ICollection<ILayer> Layers { get; private set; }

        public Network(ICollection<ILayer> layers)
        {
            Contract.Requires(layers != null, nameof(layers));
            Contract.Requires(layers.Count() >= 2, nameof(layers));

            _inputs = layers.First().Nodes.Where(n => n is IInput<double>).ToArray();
            _outputLayer = layers.Last();
            Layers = layers;
        }

        public IEnumerable<double> Output()
        {
            var result = new double[_outputLayer.Nodes.Count];

            var index = 0;
            foreach (var node in _outputLayer.Nodes)
            {
                result[index++] = node.Output();
            }
            return result;
        }

        public void Input(ICollection<double> input)
        {
            Contract.Requires(input != null, nameof(input));
            Contract.Requires(input.Count() == _inputs.Count(), nameof(input));

            foreach (Neuron neuron in Layers.SelectMany(l => l.Nodes).Where(n => n is Neuron))
            {
                neuron.Refresh();
            }

            var index = 0;
            foreach (var inputVal in input)
            {
                (_inputs[index++] as IInput<double>).Input(inputVal);
            }
        }
    }
}

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;

namespace NeuralNetworkConstructor.Network
{
    public class Network : INetwork
    {

        private readonly INode[] _inputs;
        private readonly ILayer _outputLayer;

        public ICollection<ILayer> Layers { get; }

        public Network(ICollection<ILayer> layers)
        {
            Contract.Requires(layers != null, nameof(layers));
            Contract.Requires(layers.Count >= 2, nameof(layers));
            Contract.Requires(layers.First().Nodes.Any(n => n is IInput<double>));

            _inputs = layers.First().Nodes.Where(n => n is IInput<double>).ToArray();
            _outputLayer = layers.Last();
            Layers = layers;
        }

        public Network(params ILayer[] layers)
            : this(layers.ToList())
        {

        }

        public IEnumerable<double> Output()
        {
            return _outputLayer.Nodes.Select(n => n.Output());
        }

        public void Input(ICollection<double> input)
        {
            Contract.Requires(input != null, nameof(input));
            Contract.Requires(input.Count == _inputs.Length, nameof(input));

            foreach (var node in Layers.SelectMany(l => l.Nodes).Where(n => n is Neuron))
            {
                (node as Neuron)?.Refresh();
            }

            var index = 0;
            foreach (var inputVal in input)
            {
                (_inputs[index++] as IInput<double>)?.Input(inputVal);
            }
        }
    }
}

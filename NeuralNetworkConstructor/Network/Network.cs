using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using System;

namespace NeuralNetworkConstructor.Network
{
    public class Network : INetwork
    {

        public event Action<IEnumerable<double>> OnOutput;
        public event Action<IEnumerable<double>> OnInput;

        private readonly IInputLayer _inputLayer;
        private readonly ILayer<INode> _outputLayer;

        public ICollection<ILayer<INode>> Layers { get; }

        public Network(IInputLayer inputLayer, ICollection<ILayer<INode>> layers)
        {
            Contract.Requires(layers != null, nameof(layers));
            Contract.Requires(layers.Count >= 1, nameof(layers));
            Contract.Requires(inputLayer.Nodes.Any(n => n is IInput));

            _inputLayer = inputLayer;
            _outputLayer = layers.Last();
            Layers = layers;
        }

        public Network(IInputLayer inputLayer, params ILayer<INode>[] layers)
            : this(inputLayer, layers.ToList())
        {

        }

        /// <summary>
        /// Start calculation for current input values and get result.
        /// </summary>
        /// <returns>Output value of each neuron in output-layer</returns>
        public IEnumerable<double> Output()
        {
            var result = _outputLayer.Nodes.Select(n => n.Output());
            OnOutput?.Invoke(result);

            return result;
        }

        /// <summary>
        /// Write input value to each input-neuron (<see cref="IInput{double}"/>) in input-layer.
        /// </summary>
        /// <param name="input"></param>
        public void Input(IEnumerable<double> input)
        {
            Contract.Requires(input != null, nameof(input));

            OnInput?.Invoke(input);

            Refresh();
            _inputLayer.Input(input);
        }

        public void Refresh()
        {
            foreach (IRefreshable layer in Layers)
            {
                layer.Refresh();
            }
        }

    }
}

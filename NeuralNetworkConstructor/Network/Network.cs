using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NeuralNetworkConstructor.Network
{
    public class Network : INetwork
    {

        public event Action<IEnumerable<double>> OnOutput;
        public event Action<IEnumerable<double>> OnInput;

        public IInputLayer InputLayer { get; internal set; }
        public ICollection<ILayer<INode>> Layers { get; }
        public ILayer<INode> OutputLayer { get => Layers.Last(); }

        public Network()
        {
            Layers = new List<ILayer<INode>>();
            InputLayer = new InputLayer();
        }

        public Network(IInputLayer inputLayer, ICollection<ILayer<INode>> layers)
        {
            Contract.Requires(layers != null, nameof(layers));
            Contract.Requires(layers.Count >= 1, nameof(layers));
            Contract.Requires(inputLayer.Nodes.Any(n => n is IInput));

            InputLayer = inputLayer;
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
            var result = OutputLayer.Nodes.Select(n => n.Output());
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
            InputLayer.Input(input);
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

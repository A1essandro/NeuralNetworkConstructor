using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Network
{

    [DataContract]
    [KnownType(typeof(InputLayer))]
    [KnownType(typeof(Layer.Layer))]
    public class Network : INetwork
    {

        [DataMember]
        private ICollection<ILayer<INode>> _layers;
        [DataMember]
        private IInputLayer _inputLayer;

        public event Action<IEnumerable<double>> OnOutput;
        public event Action<IEnumerable<double>> OnInput;

        public IInputLayer InputLayer => _inputLayer;
        public virtual ICollection<ILayer<INode>> Layers => _layers;
        public virtual ILayer<INode> OutputLayer => Layers.Last();

        public Network()
        {
            _layers = new List<ILayer<INode>>();
            _inputLayer = new InputLayer();
        }

        public Network(IInputLayer inputLayer, ICollection<ILayer<INode>> layers)
        {
            Contract.Requires(layers != null, nameof(layers));
            Contract.Requires(layers.Count >= 1, nameof(layers));
            Contract.Requires(inputLayer.Nodes.Any(n => n is IInput));

            _inputLayer = inputLayer;
            _layers = layers;
        }

        public Network(IInputLayer inputLayer, params ILayer<INode>[] layers)
            : this(inputLayer, layers.ToList())
        {
        }

        /// <summary>
        /// Start calculation for current input values and get result.
        /// </summary>
        /// <returns>Output value of each neuron in output-layer</returns>
        public virtual IEnumerable<double> Output()
        {
            var r = new double[] { 0.0, 0.0 };
            var i = 0;
            foreach (var n in OutputLayer.Nodes)
            {
                r[i++] = n.Output();
            }
            var result = OutputLayer.Nodes.Select(n => n.Output());
            OnOutput?.Invoke(result);

            return result;
        }

        /// <summary>
        /// Write input value to each input-neuron (<see cref="IInput{double}"/>) in input-layer.
        /// </summary>
        /// <param name="input"></param>
        public virtual void Input(IEnumerable<double> input)
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

        public virtual async Task<IEnumerable<double>> OutputAsync()
        {
            var tasks = OutputLayer.Nodes.Select(async n => await n.OutputAsync());
            var result = await Task.WhenAll(tasks).ConfigureAwait(false);

            OnOutput?.Invoke(result);

            return result;
        }

        public static T Clone<T>(T network) where T : Network
        {
            var serSettings = new DataContractSerializerSettings() { PreserveObjectReferences = true };
            var ser = new DataContractSerializer(typeof(Network), serSettings);
            using (var stream = new MemoryStream())
            {
                ser.WriteObject(stream, network);
                stream.Position = 0;
                return ser.ReadObject(stream) as T;
            }
        }

    }
}

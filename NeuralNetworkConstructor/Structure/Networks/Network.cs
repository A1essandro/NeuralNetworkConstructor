using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Networks
{

    [DataContract]
    [KnownType(typeof(InputLayer))]
    [KnownType(typeof(Layer))]
    public class Network : INetwork<double, double>
    {

        #region serialization data

        [DataMember]
        private ICollection<ILayer<INotInputNode>> _layers;
        [DataMember]
        private IInputLayer _inputLayer;

        #endregion

        public event Action<IEnumerable<double>> OnOutput;
        public event Action<IEnumerable<double>> OnInput;

        public IInputLayer InputLayer => _inputLayer;
        public virtual ICollection<ILayer<INotInputNode>> Layers => _layers;
        public virtual ILayer<INotInputNode> OutputLayer => Layers.Last();

        #region ctors

        public Network()
        {
            _layers = new List<ILayer<INotInputNode>>();
            _inputLayer = new InputLayer();
        }

        public Network(IInputLayer inputLayer, ICollection<ILayer<INotInputNode>> layers)
        {
            Contract.Requires(layers != null, nameof(layers));
            Contract.Requires(layers.Count >= 1, nameof(layers));
            Contract.Requires(inputLayer.Nodes.Any(n => n is IInputNode));

            _inputLayer = inputLayer;
            _layers = layers;
        }

        public Network(IInputLayer inputLayer, params ILayer<INotInputNode>[] layers)
            : this(inputLayer, layers.ToList())
        {
        }

        #endregion

        #region IOutputSet

        public virtual async Task<IEnumerable<double>> Output()
        {
            var result = await OutputLayer.Output().ConfigureAwait(false);

            OnOutput?.Invoke(result);

            return result;
        }

        #endregion

        /// <summary>
        /// Write input value to each input-neuron (<see cref="IInput{double}"/>) in input-layer.
        /// </summary>
        /// <param name="input"></param>
        public virtual async Task Input(IEnumerable<double> input)
        {
            Contract.Requires(input != null, nameof(input));

            OnInput?.Invoke(input);

            await Refresh().ConfigureAwait(false);
            await InputLayer.Input(input).ConfigureAwait(false);
        }

        public Task Refresh()
        {
            return Task.WhenAll(Layers.Select(l => l.Refresh()));
        }

        protected virtual T GetClone<T>() where T : Network
        {
            using (var stream = new MemoryStream())
            {
                var serSettings = new DataContractSerializerSettings() { PreserveObjectReferences = true };
                var ser = new DataContractSerializer(typeof(T), serSettings);
                ser.WriteObject(stream, this);
                stream.Position = 0;
                return ser.ReadObject(stream) as T;
            }
        }

        public static T Clone<T>(T network) where T : Network
        {
            return network.GetClone<T>();
        }

    }
}

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
        private ICollection<IReadOnlyLayer<INotInputNode>> _layers;
        [DataMember]
        private IReadOnlyLayer<IMasterNode> _inputLayer;

        #endregion

        public event Action<IEnumerable<double>> OnOutput;
        public event Action<IEnumerable<double>> OnInput;

        public IReadOnlyLayer<IMasterNode> InputLayer => _inputLayer;
        public virtual ICollection<IReadOnlyLayer<INotInputNode>> Layers => _layers;
        public virtual IReadOnlyLayer<INotInputNode> OutputLayer => Layers.Last();

        #region ctors

        public Network()
        {
            _layers = new List<IReadOnlyLayer<INotInputNode>>();
            _inputLayer = new InputLayer();
        }

        public Network(IReadOnlyLayer<IMasterNode> inputLayer, ICollection<IReadOnlyLayer<INotInputNode>> layers)
        {
            Contract.Requires(layers != null, nameof(layers));
            Contract.Requires(layers.Count >= 1, nameof(layers));
            Contract.Requires(inputLayer.Nodes.Any(n => n is IInputNode));

            _inputLayer = inputLayer;
            _layers = layers;
        }

        public Network(IReadOnlyLayer<IMasterNode> inputLayer, params IReadOnlyLayer<INotInputNode>[] layers)
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
        public virtual void Input(IEnumerable<double> input)
        {
            Contract.Requires(input != null, nameof(input));

            OnInput?.Invoke(input);

            Refresh();

            var inputNodes = _inputLayer.Nodes.OfType<IInputNode>().Where(x => !(x is Bias)).ToArray();
            var index = 0;
            foreach (var value in input)
            {
                inputNodes[index++].Input(value);
            }
        }

        public void Refresh() => Parallel.ForEach(Layers, l => l.Refresh());

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

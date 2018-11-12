using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Structure.Layers
{

    [DataContract]
    [KnownType(typeof(InputNode))]
    [KnownType(typeof(Bias))]
    public class InputLayer : IInputLayer
    {

        [DataMember]
        private IList<IMasterNode> _nodes = new List<IMasterNode>();

        public IList<IMasterNode> Nodes => _nodes;

        public InputLayer(IList<IMasterNode> nodes)
        {
            _nodes = nodes;
        }

        public InputLayer(params IMasterNode[] nodes)
            : this(nodes.ToList())
        {
        }

        public InputLayer(Func<IMasterNode> getter, ushort qty, params IMasterNode[] other)
        {
            for (var i = 0; i < qty; i++)
            {
                _nodes.Add(getter());
            }
            foreach (var node in other)
            {
                _nodes.Add(node);
            }
        }

        public event Action<IEnumerable<double>> OnInput;

        public Task Input(IEnumerable<double> input)
        {
            var inputNodes = Nodes.OfType<IInputNode>().Where(x => !(x is Bias)).ToArray();
            Contract.Requires(input.Count() == inputNodes.Length, nameof(input));

            OnInput?.Invoke(input);

            return Task.WhenAll(input.Select((value, index) => inputNodes[index].Input(value)));
        }

        public Task Refresh() => Task.WhenAll(Nodes?.OfType<IRefreshable>().Select(n => n.Refresh()));

    }
}

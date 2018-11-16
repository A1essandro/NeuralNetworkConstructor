using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Structure.Layers
{

    [DataContract]
    [KnownType(typeof(Neuron))]
    [KnownType(typeof(Bias))]
    public class Layer : ILayer<INotInputNode>
    {

        [DataMember]
        private List<INotInputNode> _nodes = new List<INotInputNode>();

        public IEnumerable<INotInputNode> Nodes => _nodes;

        public Layer()
        {
        }

        public Layer(IEnumerable<INotInputNode> nodes)
        {
            _nodes = nodes.ToList();
        }

        public Layer(params INotInputNode[] nodes)
            : this(nodes.AsEnumerable())
        {
        }

        public Layer(Func<INotInputNode> getter, ushort qty, params INotInputNode[] other)
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

        public Task Refresh() => Task.WhenAll(_nodes?.OfType<IRefreshable>().Select(n => n.Refresh()));

        public void Add(INotInputNode node)
        {
            _nodes.Add(node);
        }

    }
}

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
        protected List<INotInputNode> NodeList = new List<INotInputNode>();

        public IEnumerable<INotInputNode> Nodes => NodeList;

        public Layer(IEnumerable<INotInputNode> nodes)
        {
            NodeList = nodes.ToList();
        }

        public Layer(params INotInputNode[] nodes)
            : this(nodes.AsEnumerable())
        {
        }

        public Layer(Func<INotInputNode> getter, ushort qty, params INotInputNode[] other)
        {
            for (var i = 0; i < qty; i++)
            {
                NodeList.Add(getter());
            }
            foreach (var node in other)
            {
                NodeList.Add(node);
            }
        }

        public Task Refresh() => Task.WhenAll(NodeList?.OfType<IRefreshable>().Select(n => n.Refresh()));

    }
}

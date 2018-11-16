using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Nodes;

namespace NeuralNetworkConstructor.Structure.Layers
{

    [DataContract]
    public abstract class BaseLayer<TNode> : ILayer<TNode>
        where TNode : INode
    {

        [DataMember]
        protected List<TNode> NodeList = new List<TNode>();

        public IEnumerable<TNode> Nodes => NodeList.AsReadOnly();

        public BaseLayer()
        {
        }

        public BaseLayer(IEnumerable<TNode> nodes)
        {
            Contract.Assert(nodes != null, nameof(nodes));

            NodeList = nodes.ToList();
        }

        public BaseLayer(params TNode[] nodes)
            : this(nodes.AsEnumerable())
        {
        }

        public BaseLayer(Func<TNode> getter, int qty, params TNode[] other)
        {
            Contract.Assert(getter != null, nameof(getter));
            Contract.Assert(qty >= 0, nameof(qty));

            for (var i = 0; i < qty; i++)
            {
                NodeList.Add(getter());
            }
            foreach (var node in other)
            {
                NodeList.Add(node);
            }
        }

        public void Add(TNode node)
        {
            Contract.Assert(node != null, nameof(node));

            NodeList.Add(node);
        }

        public Task Refresh() => Task.WhenAll(NodeList.OfType<IRefreshable>().Select(n => n.Refresh()));

    }
}
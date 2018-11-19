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

        public event Action<IEnumerable<double>> OnOutput;

        public IEnumerable<TNode> Nodes => NodeList.AsReadOnly();

        public int NodesQuantity => NodeList.Count;

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

        public BaseLayer(Func<TNode> factory, int qty, params TNode[] other)
        {
            Contract.Assert(factory != null, nameof(factory));
            Contract.Assert(qty >= 0, nameof(qty));

            for (var i = 0; i < qty; i++)
            {
                NodeList.Add(factory());
            }
            foreach (var node in other)
            {
                NodeList.Add(node);
            }
        }

        public void AddNode(TNode node)
        {
            Contract.Assert(node != null, nameof(node));

            NodeList.Add(node);
        }

        public bool RemoveNode(TNode node)
        {
            Contract.Assert(node != null, nameof(node));

            return NodeList.Remove(node);
        }

        public void Refresh()
        {
            foreach (var n in NodeList.OfType<IRefreshable>())
            {
                n.Refresh();
            }
        }

        public async Task<IEnumerable<double>> Output()
        {
            var result = await Task.WhenAll(Nodes.Select(n => n.Output())).ConfigureAwait(false);

            OnOutput?.Invoke(result);

            return result;
        }

    }
}
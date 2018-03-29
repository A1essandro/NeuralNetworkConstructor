using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Common;

namespace NeuralNetworkConstructor.Network.Layer
{
    public class Layer : ILayer
    {

        public Layer(IList<INode> nodes)
        {
            Nodes = nodes;
        }

        public Layer(params INode[] nodes)
            : this(nodes.ToList())
        {
        }

        public Layer(Func<INode> getter, ushort qty, params INode[] other)
        {
            for (var i = 0; i < qty; i++)
            {
                Nodes.Add(getter());
            }
            foreach(var node in other)
            {
                Nodes.Add(node);
            }
        }

        public IList<INode> Nodes { get; } = new List<INode>();

        public void Refresh()
        {
            foreach (IRefreshable node in Nodes.Where(n => n is IRefreshable))
            {
                node.Refresh();
            }
        }

    }
}

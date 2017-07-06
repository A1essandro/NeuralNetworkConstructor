using System.Collections.Generic;
using NeuralNetworkConstructor.Node;
using System;
using System.Linq;

namespace NeuralNetworkConstructor
{
    public class Layer : ILayer
    {
        private Func<INode> p;

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
    }
}

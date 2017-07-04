using System.Collections.Generic;
using NeuralNetworkConstructor.Node;

namespace NeuralNetworkConstructor
{
    public class Layer : ILayer
    {
        public Layer(ICollection<INode> nodes)
        {
            Nodes = nodes;
        }

        public ICollection<INode> Nodes { get; }
    }
}

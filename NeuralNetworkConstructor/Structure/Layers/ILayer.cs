using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Structure.Layers
{

    public interface ILayer<out TNode> : IRefreshable
        where TNode : INode
    {

        IEnumerable<TNode> Nodes { get; }

    }

}
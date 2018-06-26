using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Structure.Layers
{

    public interface ILayer<TNode> : IRefreshable
        where TNode : INode
    {

        IList<TNode> Nodes { get; }

    }

}
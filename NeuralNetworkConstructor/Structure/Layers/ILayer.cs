using NeuralNetwork.Common;
using NeuralNetwork.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetwork.Structure.Layers
{

    public interface ILayer<TNode> : IRefreshable
        where TNode : INode
    {

        IList<TNode> Nodes { get; }

    }

}
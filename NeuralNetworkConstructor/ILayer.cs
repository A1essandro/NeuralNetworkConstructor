using NeuralNetworkConstructor.Node;
using System.Collections.Generic;

namespace NeuralNetworkConstructor
{
    public interface ILayer
    {

        IEnumerable<INode> Nodes { get; }

    }
}
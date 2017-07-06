using NeuralNetworkConstructor.Node;
using System.Collections.Generic;

namespace NeuralNetworkConstructor
{
    public interface ILayer
    {

        IList<INode> Nodes { get; }

    }
}
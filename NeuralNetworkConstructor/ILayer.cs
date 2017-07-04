using NeuralNetworkConstructor.Node;
using System.Collections.Generic;

namespace NeuralNetworkConstructor
{
    public interface ILayer
    {

        ICollection<INode> Nodes { get; }

    }
}
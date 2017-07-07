using System.Collections.Generic;
using NeuralNetworkConstructor.Network.Node;

namespace NeuralNetworkConstructor.Network.Layer
{
    public interface ILayer
    {

        IList<INode> Nodes { get; }

    }
}
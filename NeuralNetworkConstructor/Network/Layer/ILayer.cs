using System.Collections.Generic;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Common;

namespace NeuralNetworkConstructor.Network.Layer
{
    public interface ILayer : IRefreshable
    {

        IList<INode> Nodes { get; }

    }
}
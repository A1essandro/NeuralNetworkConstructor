using System.Collections.Generic;
using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;

namespace NeuralNetworkConstructor.Network
{
    public interface INetwork : IOutputSet, IInputSet, IRefreshable
    {

        ICollection<ILayer<INode>> Layers { get; }

    }
}

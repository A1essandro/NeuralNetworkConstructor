using System.Collections.Generic;
using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;

namespace NeuralNetworkConstructor.Network
{
    public interface INetwork : IOutput<IEnumerable<double>>, IInput<ICollection<double>>, IRefreshable
    {

        ICollection<ILayer<INode>> Layers { get; }

    }
}

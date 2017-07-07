using System.Collections.Generic;
using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Layer;

namespace NeuralNetworkConstructor.Network
{
    public interface INetwork : IOutput<IEnumerable<double>>, IInput<ICollection<double>>
    {
        ICollection<ILayer> Layers { get; }
    }
}

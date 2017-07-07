using System.Collections.Generic;
using NeuralNetworkConstructor.Common;

namespace NeuralNetworkConstructor.Network
{
    public interface INetwork : IOutput<IEnumerable<double>>, IInput<ICollection<double>>
    {
    }
}

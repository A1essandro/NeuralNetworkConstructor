using System.Collections.Generic;

namespace NeuralNetworkConstructor.Network
{
    public interface INetwork : IOutput<IEnumerable<double>>, IInput<ICollection<double>>
    {
    }
}

using System.Collections.Generic;
using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Node;

namespace NeuralNetworkConstructor.Network.Layer
{
    public interface IInputLayer : ILayer<IMasterNode>, IInput<IEnumerable<double>>
    {
    }

}

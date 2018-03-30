using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Node;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Network.Layer
{
    public interface IInputLayer : ILayer<IInputNode>, IInput<ICollection<double>>
    {
    }

}

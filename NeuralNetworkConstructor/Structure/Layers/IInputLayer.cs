using System.Collections.Generic;
using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Nodes;

namespace NeuralNetworkConstructor.Structure.Layers
{
    public interface IInputLayer : ILayer<IMasterNode>, IInput<IEnumerable<double>>
    {
    }

}

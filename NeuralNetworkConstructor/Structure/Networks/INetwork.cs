using NeuralNetwork.Common;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetwork.Networks
{

    public interface INetwork : IOutputSet, IInput<IEnumerable<double>>, IRefreshable
    {

        IInputLayer InputLayer { get; }

        ICollection<ILayer<INotInputNode>> Layers { get; }

        ILayer<INotInputNode> OutputLayer { get; }

    }
}

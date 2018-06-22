using NeuralNetwork.Common;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetwork.Networks
{

    public interface INetwork<TInput, TOutput> : IInput<IEnumerable<TInput>>, IOutput<IEnumerable<TOutput>>, IRefreshable
    {

        IInputLayer InputLayer { get; }

        ICollection<ILayer<INotInputNode>> Layers { get; }

        ILayer<INotInputNode> OutputLayer { get; }

    }
}

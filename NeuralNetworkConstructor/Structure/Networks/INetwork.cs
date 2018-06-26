using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Networks
{

    public interface INetwork : IRefreshable
    {

        IInputLayer InputLayer { get; }

        ICollection<ILayer<INotInputNode>> Layers { get; }

        ILayer<INotInputNode> OutputLayer { get; }

    }

    public interface INetwork<TInput, TOutput> : INetwork, IInput<IEnumerable<TInput>>, IOutput<IEnumerable<TOutput>>
    {

    }

}

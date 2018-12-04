using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Networks
{

    public interface INetwork : IRefreshable, IInput<IEnumerable<double>>, IOutput<IEnumerable<double>>
    {

        IReadOnlyLayer<IMasterNode> InputLayer { get; }

        ICollection<IReadOnlyLayer<INotInputNode>> Layers { get; }

        IReadOnlyLayer<INotInputNode> OutputLayer { get; }

    }

}

using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetworkConstructor.Factories.Layers;

namespace NeuralNetworkConstructor.Contract
{
    public interface IInnerLayerCreator<out TResult>
    {

        TResult AddInnerLayer(out ILayer<INotInputNode> layer, object context = default, ILayerFactory<ILayer<INotInputNode>, INotInputNode> factory = default);

    }
}
using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Nodes;

namespace NeuralNetworkConstructor.Factories.Layers
{

    public interface ILayerFactory<out TLayer, TNode>
        where TLayer : ILayer<TNode>
        where TNode : INode
    {

        TLayer Create(object context = default);

    }

}
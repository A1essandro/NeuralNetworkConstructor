using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetworkConstructor.Factories.Layers;
using NeuralNetworkConstructor.Generators;

namespace NeuralNetworkConstructor.Contract
{
    public interface ISimpleNetworkLayerCreator<out TResult>
    {

        TResult AddInputLayer(out ILayer<IMasterNode> inputLayer, object context = default, ILayerFactory<IInputLayer, IMasterNode> factory = default);

        TResult AddOutputLayer(out ILayer<INotInputNode> layer, object context = default, ILayerFactory<ILayer<INotInputNode>, INotInputNode> factory = default);

        TResult GenerateSynapses<TMasterLayerNode, TSlaveLayerNode>(ILayer<TMasterLayerNode> masterLayer, ILayer<TSlaveLayerNode> slaveLayer, ISynapseGenerator generator = default)
            where TMasterLayerNode : INode
            where TSlaveLayerNode : INotInputNode;

    }
}
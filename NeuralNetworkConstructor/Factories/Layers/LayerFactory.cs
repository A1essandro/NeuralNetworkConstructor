using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetwork.Structure.Layers;

namespace NeuralNetworkConstructor.Factories.Layers
{
    public class LayerFactory : ILayerFactory<ILayer<INotInputNode>, INotInputNode>
    {

        public ILayer<INotInputNode> Create(object context = default)
        {
            return new Layer();
        }

    }
}
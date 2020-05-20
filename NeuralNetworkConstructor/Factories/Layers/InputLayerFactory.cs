using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetwork.Structure.Layers;

namespace NeuralNetworkConstructor.Factories.Layers
{
    public class InputLayerFactory : ILayerFactory<IInputLayer, IMasterNode>
    {

        public IInputLayer Create(object context = default)
        {
            return new InputLayer();
        }

    }
}
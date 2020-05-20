using NeuralNetwork.Structure.Contract.Networks;

namespace NeuralNetworkConstructor.Factories.Network
{
    public class NetworkFactory : INetworkFactory<IMultilayerNetwork>
    {

        public IMultilayerNetwork Create(object context = default)
        {
            return new NeuralNetwork.Structure.Networks.Network();
        }

    }
}
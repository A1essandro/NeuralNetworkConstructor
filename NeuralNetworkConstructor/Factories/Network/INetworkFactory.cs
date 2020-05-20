using NeuralNetwork.Structure.Contract.Networks;

namespace NeuralNetworkConstructor.Factories.Network
{
    public interface INetworkFactory<out TNetwork>
        where TNetwork : ISimpleNetwork
    {

        TNetwork Create(object context = default);

    }
}
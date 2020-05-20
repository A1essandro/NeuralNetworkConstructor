using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Networks;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetworkConstructor.Factories.Network;
using NeuralNetworkConstructor.Factories.Synapses;
using NeuralNetworkConstructor.Generators;

namespace NeuralNetworkConstructor.Contract
{
    public interface INetworkCreator<TNetwork, out TResult>
        where TNetwork : ISimpleNetwork
    {

        TResult CreateNetwork(out TNetwork network, object context = default, INetworkFactory<TNetwork> factory = default);

    }
}
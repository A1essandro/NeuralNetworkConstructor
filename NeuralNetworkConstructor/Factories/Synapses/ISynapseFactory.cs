using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetwork.Structure.Contract.Synapses;

namespace NeuralNetworkConstructor.Factories.Synapses
{
    public interface ISynapseFactory
    {
        ISynapse Create(INode master, ISlaveNode slave, object context = default);
    }
}
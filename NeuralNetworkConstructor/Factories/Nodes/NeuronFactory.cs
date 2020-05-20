using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetwork.Structure.Nodes;

namespace NeuralNetworkConstructor.Factories.Nodes
{
    public class NeuronFactory : INodeFactory<ISlaveNode>
    {
        public ISlaveNode Create(object context = default)
        {
            return new Neuron();
        }
    }
}
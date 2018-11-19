using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;

namespace NeuralNetworkConstructor.Structure.Synapses.Factory
{
    public interface ISynapseGenerator<TSynapse>
        where TSynapse : ISynapse, new()
    {

        void Generate(IReadOnlyLayer<INode> masterLayer, IReadOnlyLayer<INotInputNode> slaveLayer);

    }
}
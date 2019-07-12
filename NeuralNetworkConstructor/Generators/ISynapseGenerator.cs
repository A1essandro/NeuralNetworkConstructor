using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;

namespace NeuralNetworkConstructor.Generators
{

    /// <summary>
    /// Generator of synapses between two layers
    /// </summary>
    /// <typeparam name="TSynapse"></typeparam>
    public interface ISynapseGenerator<TSynapse>
        where TSynapse : ISynapse, new()
    {

        /// <summary>
        /// Generate synapses between two layers
        /// </summary>
        /// <param name="masterLayer"></param>
        /// <param name="slaveLayer"></param>
        void Generate(IReadOnlyLayer<INode> masterLayer, IReadOnlyLayer<INotInputNode> slaveLayer);

    }
}
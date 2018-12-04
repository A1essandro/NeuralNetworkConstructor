using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;
using NeuralNetworkConstructor.Structure.Synapses;

namespace NeuralNetworkConstructor.Constructor.Generators
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
using System.Collections.Generic;
using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetwork.Structure.Contract.Synapses;

namespace NeuralNetworkConstructor.Generators
{

    /// <summary>
    /// Generator of synapses between two layers
    /// </summary>
    /// <typeparam name="TSynapse"></typeparam>
    public interface ISynapseGenerator
    {

        /// <summary>
        /// Generate synapses between two layers
        /// </summary>
        /// <param name="masterLayer"></param>
        /// <param name="slaveLayer"></param>
        IEnumerable<ISynapse> Generate<TMasterLayerNode, TSlaveLayerNode>(ILayer<TMasterLayerNode> masterLayer, ILayer<TSlaveLayerNode> slaveLayer)
            where TMasterLayerNode : INode
            where TSlaveLayerNode : INotInputNode;

    }
}
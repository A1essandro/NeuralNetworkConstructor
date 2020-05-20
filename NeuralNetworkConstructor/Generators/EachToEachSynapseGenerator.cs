using NeuralNetwork.Structure.Contract.Synapses;
using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Nodes;
using System.Linq;
using System.Collections.Generic;
using NeuralNetworkConstructor.Factories.Synapses;

namespace NeuralNetworkConstructor.Generators
{
    public class EachToEachSynapseGenerator : ISynapseGenerator
    {

        private readonly ISynapseFactory _factory;

        public EachToEachSynapseGenerator(ISynapseFactory factory)
        {
            _factory = factory;
        }

        public IEnumerable<ISynapse> Generate<TMasterLayerNode, TSlaveLayerNode>(ILayer<TMasterLayerNode> masterLayer, ILayer<TSlaveLayerNode> slaveLayer)
            where TMasterLayerNode : INode
            where TSlaveLayerNode : INotInputNode
        {
            foreach (var mNode in masterLayer.Nodes)
            {
                foreach (var sNode in slaveLayer.Nodes.OfType<ISlaveNode>())
                {
                    yield return _factory.Create(mNode, sNode);
                }
            }
        }

    }
}
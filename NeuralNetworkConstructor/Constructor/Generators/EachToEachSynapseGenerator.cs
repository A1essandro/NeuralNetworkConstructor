using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
using System;
using System.Linq;

namespace NeuralNetworkConstructor.Constructor.Generators
{
    public class EachToEachSynapseGenerator<TSynapse> : ISynapseGenerator<TSynapse>
        where TSynapse : ISynapse, new()
    {

        private readonly Random _random;

        public EachToEachSynapseGenerator() => _random = new Random();

        public EachToEachSynapseGenerator(Random random) => _random = random;

        public void Generate(IReadOnlyLayer<INode> masterLayer, IReadOnlyLayer<INotInputNode> slaveLayer)
        {
            foreach (var mNode in masterLayer.Nodes)
            {
                foreach (var sNode in slaveLayer.Nodes.OfType<ISlaveNode>())
                {
                    var weight = _getRandomWeight();
                    var synapse = new TSynapse();
                    synapse.Weight = weight;
                    synapse.MasterNode = mNode;

                    sNode.AddSynapse(synapse);
                }
            }
        }

        private double _getRandomWeight() => (_random.NextDouble() - 0.5) * 2;

    }
}
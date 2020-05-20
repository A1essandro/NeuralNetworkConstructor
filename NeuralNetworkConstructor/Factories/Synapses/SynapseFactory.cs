using System;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetwork.Structure.Contract.Synapses;
using NeuralNetwork.Structure.Synapses;

namespace NeuralNetworkConstructor.Factories.Synapses
{
    public class SynapseFactory : ISynapseFactory
    {

        private Random _random;

        public SynapseFactory(int seed)
        {
            _random = new Random(seed);
        }

        public SynapseFactory()
        {
            _random = new Random();
        }

        public ISynapse Create(INode master, ISlaveNode slave, object context = null)
        {
            return new Synapse(master, slave, _getRandomWeight());
        }

        private double _getRandomWeight() => (_random.NextDouble() - 0.5) * 2;

    }
}
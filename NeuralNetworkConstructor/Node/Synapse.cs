using System;

namespace NeuralNetworkConstructor.Node
{

    public class Synapse : ISynapse
    {

        public INode MasterNode { get; private set; }

        public double Weight { get; private set; }

        private static Random _random = new Random();
        private static double _randomWeight => (_random.NextDouble() - 0.5) * 2;

        public void ChangeWeight(double delta)
        {
            Weight += delta;
        }

        public Synapse(INode masterNode, double weight)
        {
            MasterNode = masterNode;
            Weight = weight;
        }

        public Synapse(INode masterNode)
            : this(masterNode, _randomWeight)
        {
            //empty
        }

    }
}

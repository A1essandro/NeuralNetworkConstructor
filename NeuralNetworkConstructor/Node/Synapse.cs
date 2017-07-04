using System;

namespace NeuralNetworkConstructor.Node
{

    public class Synapse : ISynapse
    {

        public INode MasterNode { get; }

        public double Weight { get; private set; }

        private static readonly Random Random = new Random();
        /// <summary>
        /// Random value from -1.0 to 1.0
        /// </summary>
        private static double RandomWeight => (Random.NextDouble() - 0.5) * 2;

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
            : this(masterNode, RandomWeight)
        {
            //empty
        }

    }
}

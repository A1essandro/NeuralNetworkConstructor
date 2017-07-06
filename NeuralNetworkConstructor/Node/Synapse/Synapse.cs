using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkConstructor.Node.Synapse
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

        /// <summary>
        /// Calculate output data from master node via weight
        /// </summary>
        /// <returns></returns>
        public double Output() => Weight * MasterNode.Output();

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

        public static class Generator
        {

            public static void EachToEach(ILayer master, ILayer slave)
            {
                foreach (var mNode in master.Nodes)
                {
                    foreach (Neuron sNode in slave.Nodes.Where(n => n is Neuron))
                    {
                        var synapse = new Synapse(mNode);
                        sNode.AddSynapse(synapse);
                    }
                }
            }

        }

    }
}

using System;
using System.Linq;
using NeuralNetworkConstructor.Network.Layer;
using System.Diagnostics.Contracts;

namespace NeuralNetworkConstructor.Network.Node.Synapse
{

    public class Synapse : ISynapse
    {

        /// <summary>
        /// "From" node
        /// </summary>
        public INode MasterNode { get; }

        public double Weight { get; private set; }

        private static readonly Random Random = new Random();
        /// <summary>
        /// Random value from -1.0 to 1.0
        /// </summary>
        private static double RandomWeight => (Random.NextDouble() - 0.5) * 2;

        /// <summary>
        /// Change weight 
        /// </summary>
        /// <param name="delta"></param>
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
            Contract.Requires(masterNode != null, nameof(masterNode));

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

            /// <summary>
            /// Generates synapses each to each nodes between layers 
            /// </summary>
            /// <param name="master"></param>
            /// <param name="slave"></param>
            public static void EachToEach(ILayer master, ILayer slave)
            {
                foreach (var mNode in master.Nodes)
                {
                    foreach (var sNode in slave.Nodes.Where(n => n is Neuron))
                    {
                        var synapse = new Synapse(mNode);
                        (sNode as Neuron)?.AddSynapse(synapse);
                    }
                }
            }

        }

    }
}

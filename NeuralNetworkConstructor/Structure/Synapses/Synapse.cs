using System;
using System.Linq;
using NeuralNetworkConstructor.Structure.Layers;
using System.Diagnostics.Contracts;
using NeuralNetworkConstructor.Common;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using NeuralNetworkConstructor.Structure.Nodes;

namespace NeuralNetworkConstructor.Structure.Synapses
{

    /// <summary>
    /// Synapse gets output from neuron-transmitter and convert the value via its weight.
    /// Result value gets neuron-reciever.
    /// </summary>
    [DataContract]
    public class Synapse : ISynapse
    {

        /// <summary>
        /// Node transmitter
        /// </summary>
        [DataMember]
        public INode MasterNode { get; set; }

        /// <summary>
        /// Current weight of synapse
        /// </summary>
        [DataMember]
        public double Weight { get; set; }

        public event Action<double> OnOutput;

        private static readonly Random Random = new Random();

        /// <summary>
        /// Random value from -1.0 to 1.0
        /// </summary>
        private static double RandomWeight => (Random.NextDouble() - 0.5) * 2;

        /// <summary>
        /// Change value of weight
        /// </summary>
        /// <param name="delta">Delta value for change</param>
        public void ChangeWeight(double delta)
        {
            Weight += delta;
        }

        public async Task<double> Output()
        {
            var result = Weight * await MasterNode.Output().ConfigureAwait(false);
            OnOutput?.Invoke(result);

            return result;
        }

        public Synapse()
        {
            Weight = RandomWeight;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterNode">Node-transmitter</param>
        /// <param name="weight">Initial weight. Random [-1, 1] if null</param>
        public Synapse(INode masterNode, double? weight = null)
        {
            Contract.Requires(masterNode != null, nameof(masterNode));

            MasterNode = masterNode;
            Weight = weight ?? RandomWeight;
        }

        /// <summary>
        /// Helper for easy generating
        /// </summary>
        public static class Generator
        {

            /// <summary>
            /// Generates synapses each to each nodes between layers 
            /// </summary>
            /// <param name="master"></param>
            /// <param name="slave"></param>
            public static void EachToEach<TNodeMaster, TNodeSlave>(ILayer<TNodeMaster> master, ILayer<TNodeSlave> slave, Func<double> weightGenerator = null)
                where TNodeMaster : INode
                where TNodeSlave : INode
            {
                foreach (INode mNode in master.Nodes)
                {
                    foreach (var sNode in slave.Nodes.OfType<Neuron>())
                    {
                        var weight = weightGenerator?.Invoke();
                        var synapse = new Synapse(mNode, weight);
                        sNode.AddSynapse(synapse);
                    }
                }
            }

        }

    }
}

using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Summator;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Network.Node
{

    /// <summary>
    /// Node that has synapses
    /// </summary>
    public interface ISlaveNode : INotInputNode
    {

        /// <summary>
        /// Collection of synapses to this node
        /// </summary>
        ICollection<ISynapse> Synapses { get; }

        ISummator Summator { get; }

        /// <summary>
        /// Adding synapse from master node to this node
        /// </summary>
        /// <param name="synapse"></param>
        void AddSynapse(ISynapse synapse);

        IActivationFunction Function { get; set; }

    }
}

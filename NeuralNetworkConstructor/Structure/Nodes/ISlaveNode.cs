using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Summators;
using NeuralNetwork.Structure.Synapses;
using System.Collections.Generic;

namespace NeuralNetwork.Structure.Nodes
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

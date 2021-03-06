﻿using NeuralNetworkConstructor.Structure.Nodes;
using NeuralNetworkConstructor.Common;

namespace NeuralNetworkConstructor.Structure.Synapses
{

    /// <summary>
    /// Synapse gets output from neuron-transmitter and convert the value via its weight.
    /// Result value gets neuron-reciever.
    /// </summary>
    public interface ISynapse : IOutput<double>
    {

        /// <summary>
        /// Change value of weight
        /// </summary>
        /// <param name="delta">Delta value for change</param>
        void ChangeWeight(double delta);

        /// <summary>
        /// Current weight of synapse
        /// </summary>
        double Weight { get; set; }

        /// <summary>
        /// Node transmitter
        /// </summary>
        INode MasterNode { get; set; }

    }
}

using NeuralNetworkConstructor.Constructor;
using NeuralNetworkConstructor.Network;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System.Linq;
using Xunit;

namespace Tests
{
    public class ConstructorTest 
    {

        [Fact]
        public void ConstructorNetwokrTest()
        {
            var network = new NetworkConstructor<Network>()
                .AddInputNodes<InputNode>("0_1", "0_2", "0_3")
                .AddInputNode<InputBias>("0_4")
                .AddLayer<Layer>("1")
                .AddNeuron<Neuron<Rectifier>>("1_1")
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron<Linear>>("1_2")
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron<Gaussian>>("1_3")
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron<Logistic>>("1_4")
                .AddSynapses<Synapse>()
                .AddNode<Bias>("1_5")
                .AddLayer<Layer>("2")
                .AddNeuron<Neuron<Rectifier>>("2_1")
                .AddSynapses<Synapse>("1")
                .AddNeuron<Neuron<Rectifier>>("2_2")
                .AddSynapses<Synapse>("1")
                .AddNeuron<Neuron<Rectifier>>("2_3")
                .AddSynapses<Synapse>("1")
                .Complete();

            Assert.Equal(4, network.InputLayer.Nodes.Count);
            Assert.Equal(2, network.Layers.Count);
            Assert.Equal(5, network.Layers.First().Nodes.Count);
        }

    }
}

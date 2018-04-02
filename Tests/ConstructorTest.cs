using NeuralNetworkConstructor.Constructor;
using NeuralNetworkConstructor.Learning;
using NeuralNetworkConstructor.Network;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class ConstructorTest 
    {

        private const double DELTA = 0.15;
        private const double THETA = 0.33;

        [Fact]
        public void ConstructorNetwokrStructureTest()
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

        [Fact]
        public void ConstructorNetwokrXORTest()
        {
            var network = new NetworkConstructor<Network>()
                .AddInputNodes<InputNode>("0_1", "0_2")
                .AddInputNode<InputBias>("0_4")
                .AddLayer<Layer>("1")
                .AddNeuron<Neuron<Logistic>>("1_1")
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron<Logistic>>("1_2")
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron<Logistic>>("1_3")
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron<Logistic>>("1_4")
                .AddSynapses<Synapse>()
                .AddNode<Bias>("1_5")
                .AddLayer<Layer>("2")
                .AddNeuron<Neuron<Logistic>>("2_1")
                .AddSynapses<Synapse>("1")
                .Complete();

            var teachKit = new Dictionary<double[], double[]>
            {
                { new double[] { 0, 1 }, new double[] { 1 } },
                { new double[] { 1, 0 }, new double[] { 1 } },
                { new double[] { 1, 1 }, new double[] { 0 } },
                { new double[] { 0, 0 }, new double[] { 0 } }
            };

            var strategy = new BackpropagationStrategy(THETA, DELTA, 10000);
            var learning = new Learning<KeyValuePair<double[], double[]>>(strategy, teachKit);
            learning.Learn(network);

            network.Input(new double[] { 1, 0 });
            var output = network.Output().First();
            Assert.True(Math.Abs(1 - output) < DELTA);

            network.Input(new double[] { 1, 1 });
            output = network.Output().First();
            Assert.True(Math.Abs(0 - output) < DELTA);

            network.Input(new double[] { 0, 0 });
            output = network.Output().First();
            Assert.True(Math.Abs(0 - output) < DELTA);

            network.Input(new double[] { 0, 1 });
            output = network.Output().First();
            Assert.True(Math.Abs(1 - output) < DELTA);
        }

    }
}

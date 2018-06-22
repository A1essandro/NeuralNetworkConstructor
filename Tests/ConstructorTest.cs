﻿using NeuralNetwork.Constructor;
using NeuralNetwork.Learning;
using NeuralNetwork.Networks;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Synapses;
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
            var network = new NetworkConstructor<Network, double, double>()
                .AddInputNodes<InputNode>("0_1", "0_2", "0_3")
                .AddInputNode<Bias>("0_4")
                .AddLayer<Layer>("1")
                .AddNeuron<Neuron>("1_1", new Rectifier())
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron>("1_2", new Linear())
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron>("1_3", new Gaussian())
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron>("1_4", new Logistic())
                .AddSynapses<Synapse>()
                .AddNode<Bias>("1_5")
                .AddLayer<Layer>("2")
                .AddNeuron<Neuron>("2_1", new Rectifier())
                .AddSynapses<Synapse>("1")
                .AddNeuron<Neuron>("2_2", new Rectifier())
                .AddSynapses<Synapse>("1")
                .AddNeuron<Neuron>("2_3", new Rectifier())
                .AddSynapses<Synapse>("1")
                .Complete();

            Assert.Equal(4, network.InputLayer.Nodes.Count);
            Assert.Equal(2, network.Layers.Count);
            Assert.Equal(5, network.Layers.First().Nodes.Count);
        }

        [Fact]
        public void ConstructorNetwokrXORTest()
        {
            var network = new NetworkConstructor<Network, double, double>()
                .AddInputNodes<InputNode>("0_1", "0_2")
                .AddInputNode<Bias>("0_4")
                .AddLayer<Layer>("1")
                .AddNeuron<Neuron>("1_1", new Logistic())
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron>("1_2", new Logistic())
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron>("1_3", new Logistic())
                .AddSynapses<Synapse>()
                .AddNeuron<Neuron>("1_4", new Logistic())
                .AddSynapses<Synapse>()
                .AddNode<Bias>("1_5")
                .AddLayer<Layer>("2")
                .AddNeuron<Neuron>("2_1", new Logistic())
                .AddSynapses<Synapse>("1")
                .Complete();

            var teachKit = new Dictionary<IEnumerable<double>, IEnumerable<double>>
            {
                { new double[] { 0, 1 }, new double[] { 1 } },
                { new double[] { 1, 0 }, new double[] { 1 } },
                { new double[] { 1, 1 }, new double[] { 0 } },
                { new double[] { 0, 0 }, new double[] { 0 } }
            };

            var strategy = new BackpropagationStrategy(THETA, DELTA, 10000);
            var learning = new Learning<double, double>(strategy, teachKit);
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

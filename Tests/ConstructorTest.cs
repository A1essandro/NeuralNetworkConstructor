﻿using NeuralNetworkConstructor.Constructor;
using NeuralNetworkConstructor.Learning;
using NeuralNetworkConstructor.Networks;
using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;
using NeuralNetworkConstructor.Structure.ActivationFunctions;
using NeuralNetworkConstructor.Structure.Synapses;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using NeuralNetworkConstructor.Learning.Samples;
using NeuralNetworkConstructor.Learning.Strategies;
using System.Threading.Tasks;

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
        public async Task ConstructorNetwokrXORTest()
        {
            var network = new NetworkConstructor<Network>()
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

            var samples = new List<ILearningSample<double, double>>
            {
                new LearningSample<double, double>(new double[] { 0, 1 }, new double[] { 1 }),
                new LearningSample<double, double>(new double[] { 1, 0 }, new double[] { 1 }),
                new LearningSample<double, double>(new double[] { 0, 0 }, new double[] { 0 }),
                new LearningSample<double, double>(new double[] { 1, 1 }, new double[] { 0 })
            };
            var strategy = new BackpropagationStrategy();
            var settings = new LearningSettings { Repeats = 10000, Theta = THETA };
            var learning = new Learning<Network, ILearningSample<double, double>>(network, strategy, settings);
            await learning.Learn(samples);

            await network.Input(new double[] { 1, 0 });
            var output = (await network.Output()).First();
            Assert.True(Math.Abs(1 - output) < DELTA);

            await network.Input(new double[] { 1, 1 });
            output = (await network.Output()).First();
            Assert.True(Math.Abs(0 - output) < DELTA);

            await network.Input(new double[] { 0, 0 });
            output = (await network.Output()).First();
            Assert.True(Math.Abs(0 - output) < DELTA);

            await network.Input(new double[] { 0, 1 });
            output = (await network.Output()).First();
            Assert.True(Math.Abs(1 - output) < DELTA);
        }

    }
}

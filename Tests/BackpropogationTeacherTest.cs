﻿using NeuralNetworkConstructor.Learning;
using NeuralNetworkConstructor.Networks;
using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;
using NeuralNetworkConstructor.Structure.ActivationFunctions;
using NeuralNetworkConstructor.Structure.Synapses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using NeuralNetworkConstructor.Learning.Samples;
using NeuralNetworkConstructor.Learning.Strategies;
using System.Threading;

namespace Tests
{
    public class BackpropogationTeacherTest
    {

        private const double DELTA = 0.15;
        private const double THETA = 0.33;

        [Fact]
        public async Task TestTeachXor()
        {
            IInputLayer inputLayer = new InputLayer(() => new InputNode(), 2, new Bias());
            var innerLayer = new Layer(() => new Neuron(new Logistic(0.888)), 3, new Bias());
            var outputLayer = new Layer(new Neuron(new Logistic(0.777)));

            Synapse.Generator.EachToEach(inputLayer, innerLayer);
            Synapse.Generator.EachToEach(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);
            var samples = new List<ILearningSample<double, double>>
            {
                new LearningSample<double, double>(new double[] { 0, 1 }, new double[] { 1 }),
                new LearningSample<double, double>(new double[] { 1, 0 }, new double[] { 1 }),
                new LearningSample<double, double>(new double[] { 0, 0 }, new double[] { 0 }),
                new LearningSample<double, double>(new double[] { 1, 1 }, new double[] { 0 })
            };

            await network.Input(new double[] { 1, 0 });
            var beforeLearning = (await network.Output()).First();

            var strategy = new BackpropagationStrategy();
            var settings = new LearningSettings { Repeats = 10000, Theta = THETA, ThetaFactorPerEpoch = 0.9999, ShuffleEveryEpoch = true };
            var learning = new Learning<Network, ILearningSample<double, double>>(network, strategy, settings);
            await learning.Learn(samples);

            await network.Input(new double[] { 1, 0 });
            var afterLearning = (await network.Output()).First();

            Assert.True(beforeLearning < afterLearning);

            await network.Input(new double[] { 1, 0 });
            var output = (await network.Output()).First();
            await network.Refresh();
            Assert.True(Math.Abs(1 - output) < DELTA);

            await network.Input(new double[] { 1, 1 });
            output = (await network.Output()).First();
            await network.Refresh();
            Assert.True(Math.Abs(0 - output) < DELTA);

            await network.Input(new double[] { 0, 0 });
            output = (await network.Output()).First();
            await network.Refresh();
            Assert.True(Math.Abs(0 - output) < DELTA);

            await network.Input(new double[] { 0, 1 });
            output = (await network.Output()).First();
            await network.Refresh();
            Assert.True(Math.Abs(1 - output) < DELTA);
        }

        [Fact]
        public async Task TestCancel()
        {
            IInputLayer inputLayer = new InputLayer(() => new InputNode(), 2, new Bias());
            var innerLayer = new Layer(() => new Neuron(new Logistic(0.888)), 3, new Bias());
            var outputLayer = new Layer(new Neuron(new Logistic(0.777)));

            Synapse.Generator.EachToEach(inputLayer, innerLayer);
            Synapse.Generator.EachToEach(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);
            var samples = new List<ILearningSample<double, double>>
            {
                new LearningSample<double, double>(new double[] { 0, 1 }, new double[] { 1 }),
                new LearningSample<double, double>(new double[] { 1, 0 }, new double[] { 1 }),
                new LearningSample<double, double>(new double[] { 0, 0 }, new double[] { 0 }),
                new LearningSample<double, double>(new double[] { 1, 1 }, new double[] { 0 })
            };

            var strategy = new BackpropagationStrategy();
            var settings = new LearningSettings { Repeats = 20000 };
            var learning = new Learning<Network, ILearningSample<double, double>>(network, strategy, settings);

            var cts = new CancellationTokenSource();
            var task = Task.Run(async () =>
            {
                await Task.Delay(1000);
                cts.Cancel();
            });

            await Assert.ThrowsAsync<OperationCanceledException>(async () => await learning.Learn(samples, cts.Token));
        }

        [Fact]
        public async Task TestTeachLite()
        {
            var inputLayer = new InputLayer(new InputNode(), new Bias());
            var innerLayer = new Layer(new Neuron(new Rectifier()));
            var outputLayer = new Layer(new Neuron(new Rectifier()));

            Synapse.Generator.EachToEach(inputLayer, innerLayer);
            Synapse.Generator.EachToEach(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);

            var samples = new List<ILearningSample<double, double>>
            {
                new LearningSample<double, double>(new double[] { 0 }, new double[] { 1 }),
            };

            var strategy = new BackpropagationStrategy();
            var settings = new LearningSettings { Repeats = 10000, Theta = THETA, ThetaFactorPerEpoch = 0.9995 };
            var learning = new Learning<Network, ILearningSample<double, double>>(network, strategy, settings);

            await learning.Learn(samples);

            await network.Input(new double[] { 1 });
            var output = (await network.Output()).First();
            Assert.True(Math.Abs(output) < DELTA);
        }
    }
}

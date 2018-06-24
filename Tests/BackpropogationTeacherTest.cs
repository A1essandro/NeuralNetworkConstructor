using NeuralNetwork.Learning;
using NeuralNetwork.Networks;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Synapses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using NeuralNetwork.Learning.Samples;
using NeuralNetwork.Learning.Strategies;

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

            var strategy = new BackpropagationStrategy(THETA, DELTA, 10000);
            var learning = new Learning<Network, ILearningSample<double, double>>(strategy, samples);
            await learning.Learn(network);

            network.Input(new double[] { 1, 0 });
            var output = (await network.Output()).First();
            network.Refresh();
            var Output = (await network.Output()).First();
            Assert.True(Math.Abs(1 - output) < DELTA);
            Assert.True(Math.Abs(1 - Output) < DELTA);

            network.Input(new double[] { 1, 1 });
            output = (await network.Output()).First();
            network.Refresh();
            Output = (await network.Output()).First();
            Assert.True(Math.Abs(0 - output) < DELTA);
            Assert.True(Math.Abs(0 - Output) < DELTA);

            network.Input(new double[] { 0, 0 });
            output = (await network.Output()).First();
            network.Refresh();
            Output = (await network.Output()).First();
            Assert.True(Math.Abs(0 - output) < DELTA);
            Assert.True(Math.Abs(0 - Output) < DELTA);

            network.Input(new double[] { 0, 1 });
            output = (await network.Output()).First();
            network.Refresh();
            Output = (await network.Output()).First();
            Assert.True(Math.Abs(1 - output) < DELTA);
            Assert.True(Math.Abs(1 - Output) < DELTA);
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

            var strategy = new BackpropagationStrategy(THETA, DELTA, ushort.MaxValue);
            var learning = new Learning<Network, ILearningSample<double, double>>(strategy, samples);

            await learning.Learn(network);

            network.Input(new double[] { 1 });
            var output = (await network.Output()).First();
            Assert.True(Math.Abs(output) < DELTA);
        }
    }
}

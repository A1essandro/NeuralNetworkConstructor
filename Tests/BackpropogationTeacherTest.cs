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
            network.Refresh();
            var outputAsync = (await network.OutputAsync()).First();
            Assert.True(Math.Abs(1 - output) < DELTA);
            Assert.True(Math.Abs(1 - outputAsync) < DELTA);

            network.Input(new double[] { 1, 1 });
            output = network.Output().First();
            network.Refresh();
            outputAsync = (await network.OutputAsync()).First();
            Assert.True(Math.Abs(0 - output) < DELTA);
            Assert.True(Math.Abs(0 - outputAsync) < DELTA);

            network.Input(new double[] { 0, 0 });
            output = network.Output().First();
            network.Refresh();
            outputAsync = (await network.OutputAsync()).First();
            Assert.True(Math.Abs(0 - output) < DELTA);
            Assert.True(Math.Abs(0 - outputAsync) < DELTA);

            network.Input(new double[] { 0, 1 });
            output = network.Output().First();
            network.Refresh();
            outputAsync = (await network.OutputAsync()).First();
            Assert.True(Math.Abs(1 - output) < DELTA);
            Assert.True(Math.Abs(1 - outputAsync) < DELTA);
        }

        [Fact]
        public void TestTeachLite()
        {
            var inputLayer = new InputLayer(new InputNode(), new Bias());
            var innerLayer = new Layer(new Neuron(new Rectifier()));
            var outputLayer = new Layer(new Neuron(new Rectifier()));

            Synapse.Generator.EachToEach(inputLayer, innerLayer);
            Synapse.Generator.EachToEach(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);

            var teachKit = new Dictionary<double[], double[]>
            {
                { new double[] { 0 }, new double[] { 1 } },
            };

            var strategy = new BackpropagationStrategy(THETA, DELTA, ushort.MaxValue);
            var learning = new Learning<KeyValuePair<double[], double[]>>(strategy, teachKit);

            learning.Learn(network);

            network.Input(new double[] { 1 });
            var output = network.Output().First();
            Assert.True(Math.Abs(output) < DELTA);
        }
    }
}

using NeuralNetworkConstructor.Network;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Summator;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class NetworkTest
    {

        [Fact]
        public async Task TestInput()
        {
            var inputLayer = new InputLayer(() => new InputNode(), 2, new InputBias());
            var innerLayer = new Layer(() => new Neuron(new Rectifier()), 3, new Bias());
            var outputLayer = new Layer(() => new Neuron(new Rectifier()), 2);

            Synapse.Generator.EachToEach(inputLayer, innerLayer);
            Synapse.Generator.EachToEach(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);

            network.Input(new[] { 0.1, 1.0 });

            var output1 = network.Output();
            var output2 = network.Output();
            network.Refresh();
            var outputAsync = await network.OutputAsync();

            Assert.Equal(output1.First(), output2.First());
            Assert.Equal(output1.First(), outputAsync.First());
        }

        [Fact]
        public void TestKohonenNetwork()
        {
            var inputLayer = new InputLayer(() => new InputNode(), 2);
            var outputLayer = new Layer(new Neuron(new Gaussian(), new EuclidRangeSummator()));

            Synapse.Generator.EachToEach(inputLayer, outputLayer);

            var network = new KohonenNetwork(inputLayer, outputLayer);
            var learning = new KohonenNetwork.SelfLearning(network);

            var input = new[] { 0.0, 1.0 };

            network.Input(input);
            var output1 = network.Output().First();

            learning.Learn(input, 0.33);

            network.Input(input);
            var output2 = network.Output().First();

            Assert.True(output1 < output2, $"{output1} > {output2}");
        }

        [Fact]
        public void TestEvents()
        {
            var inputLayer = new InputLayer(() => new InputNode(), 2, new InputBias());
            var outputLayer = new Layer(new Neuron(new Gaussian(), new EuclidRangeSummator()));

            Synapse.Generator.EachToEach(inputLayer, outputLayer);

            var network = new KohonenNetwork(inputLayer, outputLayer);

            var output = 0;
            var input = 0;
            network.OnOutput += (result) => { output++; };
            network.OnOutput += (result) => { output++; };
            network.OnInput += (result) => { input++; };
            network.OnInput += (result) => { input++; };

            network.Input(new double[] { 1, 0 });
            network.Output();

            Assert.True(output == 2);
            Assert.True(input == 2);
        }

        [Fact]
        public void TestClone()
        {
            var inputLayer = new InputLayer(() => new InputNode(), 2, new InputBias());
            var innerLayer = new Layer(() => new Neuron(new Rectifier()), 3, new Bias());
            var outputLayer = new Layer(() => new Neuron(new Rectifier()), 2);
            Synapse.Generator.EachToEach(inputLayer, innerLayer);
            Synapse.Generator.EachToEach(innerLayer, outputLayer);
            var network = new Network(inputLayer, innerLayer, outputLayer);

            var clone = Network.Clone(network);

            var input = new[] { 0.1, 1.0 };
            network.Input(input);
            clone.Input(input);
            var networkOutput = network.Output().ToArray();
            var cloneOutput = clone.Output().ToArray();

            Assert.NotEqual(network, clone);
            Assert.Equal((network.Layers.Last().Nodes.First() as ISlaveNode).Synapses.First().Weight,
                (clone.Layers.Last().Nodes.First() as ISlaveNode).Synapses.First().Weight);
            Assert.Equal(networkOutput.First(), cloneOutput.First());
            Assert.Equal(networkOutput.Last(), cloneOutput.Last());
        }

    }
}

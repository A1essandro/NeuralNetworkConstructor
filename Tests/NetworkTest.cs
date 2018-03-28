using NeuralNetworkConstructor.Network;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Summator;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System.Linq;
using Xunit;

namespace Tests
{
    public class NetworkTest
    {

        [Fact]
        public void TestInput()
        {
            var inputLayer = new Layer(() => new InputNode(), 2, new Bias());
            var innerLayer = new Layer(() => new Neuron(new Rectifier()), 3, new Bias());
            var outputLayer = new Layer(() => new Neuron(new Rectifier()), 2);

            Synapse.Generator.EachToEach(inputLayer, innerLayer);
            Synapse.Generator.EachToEach(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);

            network.Input(new[] { 0.1, 1.0 });

            var output1 = network.Output();
            var output2 = network.Output();

            Assert.Equal(output1.First(), output2.First());
        }

        [Fact]
        public void TestKohonenNetwork()
        {
            var inputLayer = new Layer(() => new InputNode(), 2);
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
    }
}

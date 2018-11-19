using NeuralNetworkConstructor.Networks;
using NeuralNetworkConstructor.Structure.ActivationFunctions;
using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;
using NeuralNetworkConstructor.Structure.Summators;
using NeuralNetworkConstructor.Structure.Synapses;
using NeuralNetworkConstructor.Structure.Synapses.Generators;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class NetworkTest
    {

        [Fact]
        public async Task TestInput()
        {
            var inputLayer = new InputLayer(() => new InputNode(), 2, new Bias());
            var innerLayer = new Layer(() => new Neuron(new Rectifier()), 3, new Bias());
            var outputLayer = new Layer(() => new Neuron(new Rectifier()), 2);

            var generator = new EachToEachSynapseGenerator<Synapse>();
            generator.Generate(inputLayer, innerLayer);
            generator.Generate(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);

            network.Input(new[] { 0.1, 1.0 });

            var output1 = await network.Output();
            var output2 = await network.Output();
            network.Refresh();
            var outputAsync = await network.Output();

            Assert.Equal(output1.First(), output2.First());
            Assert.Equal(output1.First(), outputAsync.First());
        }

        [Fact]
        public async Task TestEvents()
        {
            var inputLayer = new InputLayer(() => new InputNode(), 2, new Bias());
            var outputLayer = new Layer(new Neuron(new Gaussian(), new EuclidRangeSummator()));

            var generator = new EachToEachSynapseGenerator<Synapse>();
            generator.Generate(inputLayer, outputLayer);

            var network = new Network(inputLayer, outputLayer);

            var output = 0;
            var input = 0;
            network.OnOutput += (result) => { output++; };
            network.OnOutput += (result) => { output++; };
            network.OnInput += (result) => { input++; };
            network.OnInput += (result) => { input++; };

            network.Input(new double[] { 1, 0 });
            await network.Output();

            Assert.True(output == 2);
            Assert.True(input == 2);
        }

        [Fact]
        public async Task TestClone()
        {
            var inputLayer = new InputLayer(() => new InputNode(), 2, new Bias());
            var innerLayer = new Layer(() => new Neuron(new Rectifier()), 3, new Bias());
            var outputLayer = new Layer(() => new Neuron(new Logistic()), 2);

            var generator = new EachToEachSynapseGenerator<Synapse>();
            generator.Generate(inputLayer, innerLayer);
            generator.Generate(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);

            var clone = Network.Clone(network);

            var input = new[] { 0.1, 1.0 };
            network.Input(input);
            clone.Input(input);
            var networkOutput = (await network.Output()).ToArray();
            var cloneOutput = (await clone.Output()).ToArray();

            Assert.NotEqual(network, clone);
            Assert.Equal((network.Layers.Last().Nodes.First() as ISlaveNode).Synapses.First().Weight,
                (clone.Layers.Last().Nodes.First() as ISlaveNode).Synapses.First().Weight);
            Assert.Equal(networkOutput.First(), cloneOutput.First());
            Assert.Equal(networkOutput.Last(), cloneOutput.Last());
        }

    }
}

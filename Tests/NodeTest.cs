using Moq;
using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{

    public class NodeTest
    {

        private static Random Random => new Random();

        [Fact]
        public async Task TestInputNode()
        {
            var node = new InputNode();
            var value = Random.NextDouble();
            node.Input(value);

            Assert.Equal(value, await node.Output());
        }

        [Fact]
        public async Task TestBias()
        {
            Assert.Equal(1, await new Bias().Output());
        }

        [Fact]
        public async Task TestNeuron()
        {
            var synapse = new Mock<ISynapse>();
            synapse.Setup(x => x.Output()).ReturnsAsync(0.5);

            var func = new Mock<IActivationFunction>();
            func.Setup(x => x.GetEquation(It.IsAny<double>()))
                .Returns<double>(x => x);

            var neuron = new Neuron(func.Object, new[] { synapse.Object });
            Assert.Equal(0.5, await neuron.Output());
        }

        [Fact]
        public async Task TestContext()
        {
            var synapse = new Mock<ISynapse>();
            synapse.Setup(x => x.Output()).ReturnsAsync(0.5);

            var context = new Context(delay: 1);
            context.AddSynapse(synapse.Object);

            var secondCallResult = await synapse.Object.Output();
            Assert.Equal(0, await context.Output());
            Assert.Equal(0, await context.Output());
            context.Refresh();
            Assert.Equal(secondCallResult, await context.Output());
        }

        [Fact]
        public async Task TestEvents()
        {
            var neuron = new Neuron();
            var inputNeuron = new InputNode();
            neuron.AddSynapse(new Synapse(inputNeuron, 1));

            var output = 0;
            var input = 0;

            inputNeuron.OnOutput += (result) => { output++; };
            neuron.OnOutput += (result) => { output++; };
            neuron.OnOutput += (result) => { output++; };
            inputNeuron.OnInput += (result) => { input++; };
            inputNeuron.OnInput += (result) => { input++; };

            inputNeuron.Input(1);
            await neuron.Output();

            Assert.True(output == 3);
            Assert.True(input == 2);
        }

    }
}

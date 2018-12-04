using NeuralNetworkConstructor.Structure.Nodes;
using NeuralNetworkConstructor.Structure.ActivationFunctions;
using NeuralNetworkConstructor.Structure.Synapses;
using System;
using Xunit;
using System.Threading.Tasks;

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
            var synapse = new Synapse(new Bias(), 0.5);
            var neuron = new Neuron(new Rectifier(), new[] { synapse });
            Assert.Equal(0.5, await neuron.Output());
        }

        [Fact]
        public async Task TestContext()
        {
            var neuron = new Neuron();
            var input = new InputNode();
            neuron.AddSynapse(new Synapse(input, 1));

            var context = new Context(null, 1);
            var synapse = new Synapse(neuron, 0.5);
            context.AddSynapse(synapse);

            input.Input(1);
            var secondCallResult = await synapse.Output(); //Direct call
            Assert.Equal(0, await context.Output());
            Assert.Equal(0, await context.Output());
            context.Refresh();
            Assert.Equal(secondCallResult, await context.Output());
        }

        [Fact]
        public async Task TestEvents()
        {
            var neuron = new Neuron(new Rectifier());
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

using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Synapses;
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
        public async Task TestNeuronEvent()
        {
            var synapse = new Synapse(new Bias(), 0.5);
            var neuron = new Neuron(new Logistic(), new[] { synapse });

            var testBool = false;
            var testDouble = double.MinValue;

            neuron.OnOutputCalculated += (n) => { testBool = true; };
            neuron.OnOutputCalculated += async (n) => { testDouble = await n.Output(); };
            var output = await neuron.Output();

            Assert.True(testBool);
            Assert.Equal(output, testDouble);
        }

        [Fact]
        public async Task TestContext()
        {
            var neuron = new Neuron(new Rectifier());
            var input = new InputNode();
            neuron.AddSynapse(new Synapse(input, 1));

            var context = new Context((Func<double, double>)null, 1);
            context.AddSynapse(new Synapse(neuron));

            input.Input(1);
            await neuron.Output(); //Direct call
            Assert.Equal(0, await context.Output());
            await neuron.Output(); //Direct call
            Assert.Equal(1, await context.Output());
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

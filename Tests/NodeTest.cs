using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System;
using Xunit;

namespace Tests
{

    public class NodeTest
    {

        private static Random Random => new Random();

        [Fact]
        public void TestInputNode()
        {
            var node = new InputNode();
            var value = Random.NextDouble();
            node.Input(value);

            Assert.Equal(value, node.Output());
        }

        [Fact]
        public void TestBias()
        {
            Assert.Equal(1, new Bias().Output());
        }

        [Fact]
        public void TestNeuron()
        {
            var synapse = new Synapse(new Bias(), 0.5);
            var neuron = new Neuron(new Rectifier(), new[] { synapse });
            Assert.Equal(0.5, neuron.Output());
        }

        [Fact]
        public void TestNeuronEvent()
        {
            var synapse = new Synapse(new Bias(), 0.5);
            var neuron = new Neuron(new Logistic(), new[] { synapse });

            var testBool = false;
            var testDouble = double.MinValue;

            neuron.OnOutputCalculated += (n) => { testBool = true; };
            neuron.OnOutputCalculated += (n) => { testDouble = n.Output(); };
            var output = neuron.Output();

            Assert.True(testBool);
            Assert.Equal(output, testDouble);
        }

        [Fact]
        public void TestContext()
        {
            var neuron = new Neuron(new Rectifier());
            var input = new InputNode();
            neuron.AddSynapse(new Synapse(input, 1));

            var context = new Context((Func<double, double>) null, 1);
            context.AddSynapse(new Synapse(neuron));

            input.Input(1);
            neuron.Output(); //Direct call
            Assert.Equal(0, context.Output());
            neuron.Output(); //Direct call
            Assert.Equal(1, context.Output());
        }

        [Fact]
        public void TestEvents()
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
            neuron.Output();

            Assert.True(output == 3);
            Assert.True(input == 2);
        }
    }
}

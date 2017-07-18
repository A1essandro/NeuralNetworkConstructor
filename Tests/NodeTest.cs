using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;

namespace Tests
{
    [TestClass]
    public class NodeTest
    {

        private static Random Random => new Random();

        [TestMethod]
        public void TestInputNode()
        {
            var node = new InputNode();
            var value = Random.NextDouble();
            node.Input(value);

            Assert.AreEqual(value, node.Output());
        }

        [TestMethod]
        public void TestBias()
        {
            Assert.AreEqual(1, new Bias().Output());
        }

        [TestMethod]
        public void TestNeuron()
        {
            var synapse = new Synapse(new Bias(), 0.5);
            var neuron = new Neuron(new Rectifier(), new[] { synapse });
            Assert.AreEqual(0.5, neuron.Output());
        }

        [TestMethod]
        public void TestNeuronEvent()
        {
            var synapse = new Synapse(new Bias(), 0.5);
            var neuron = new Neuron(new Logistic(), new[] { synapse });

            var testBool = false;
            var testDouble = double.MinValue;

            neuron.OnOutputCalculated += (n) => { testBool = true; };
            neuron.OnOutputCalculated += (n) => { testDouble = n.Output(); };
            var output = neuron.Output();

            Assert.IsTrue(testBool);
            Assert.AreEqual(output, testDouble);
        }

        [TestMethod]
        public void TestContext()
        {
            var neuron = new Neuron(new Rectifier());
            var input = new InputNode();
            neuron.AddSynapse(new Synapse(input, 1));

            var context = new Context((Func<double, double>) null, 1);
            context.AddSynapse(new Synapse(neuron));

            input.Input(1);
            neuron.Output(); //Direct call
            Assert.AreEqual(0, context.Output());
            neuron.Output(); //Direct call
            Assert.AreEqual(1, context.Output());
        }
    }
}

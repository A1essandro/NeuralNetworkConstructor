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
            var neuron = new Neuron(new SimpleActivationFunction(), new[] { synapse });
            Assert.AreEqual(0.5, neuron.Output());
        }
    }
}

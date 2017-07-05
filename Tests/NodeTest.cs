using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkConstructor.Node;

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
    }
}

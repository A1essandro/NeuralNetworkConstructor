using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkConstructor.Node;

namespace Tests
{
    [TestClass]
    public class SynapseTest
    {
        [TestMethod]
        public void TestConstructorWithWeight()
        {
            var synapse = new Synapse(new Bias(), 0.5);

            Assert.AreEqual(0.5, synapse.Weight);
        }

        [TestMethod]
        public void TestConstructorWithRandomWeight()
        {
            var synapse1 = new Synapse(new Bias());
            var synapse2 = new Synapse(new Bias());

            Assert.AreNotEqual(synapse2.Weight, synapse1.Weight);
            Assert.IsTrue(Math.Abs(synapse1.Weight) <= 1);
        }
    }
}

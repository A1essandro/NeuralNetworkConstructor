using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkConstructor;
using System.Collections.Generic;
using System.Linq;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;

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

        [TestMethod]
        public void TestGeneratorEachToEach()
        {
            var master = new Layer(new List<INode> {
                new Bias(), new Neuron(new Rectifier())
            });
            var slave = new Layer(new List<INode> {
                new Neuron(new Rectifier()), new Neuron(new Rectifier())
            });

            var rand = new Random();
            Synapse.Generator.EachToEach(master, slave, () => rand.NextDouble() - 0.5);

            Assert.AreEqual(2, (slave.Nodes.First() as Neuron).Synapses.Count);
        }
    }
}

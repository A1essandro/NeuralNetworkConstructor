using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;
using NeuralNetworkConstructor.Structure.ActivationFunctions;
using NeuralNetworkConstructor.Structure.Synapses;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class SynapseTest
    {

        [Fact]
        public void TestConstructorWithWeight()
        {
            var synapse = new Synapse(new Bias(), 0.5);

            Assert.Equal(0.5, synapse.Weight);
        }

        [Fact]
        public void TestConstructorWithRandomWeight()
        {
            var synapse1 = new Synapse(new Bias());
            var synapse2 = new Synapse(new Bias());

            Assert.NotEqual(synapse2.Weight, synapse1.Weight);
            Assert.True(Math.Abs(synapse1.Weight) <= 1);
        }

        [Fact]
        public void TestGeneratorEachToEach()
        {
            var master = new Layer(new List<INotInputNode> {
                new Bias(), new Neuron(new Rectifier())
            });
            var slave = new Layer(new List<INotInputNode> {
                new Neuron(new Rectifier()), new Neuron(new Rectifier())
            });

            var rand = new Random();
            Synapse.Generator.EachToEach(master, slave, () => rand.NextDouble() - 0.5);

            Assert.Equal(2, (slave.Nodes.First() as Neuron).Synapses.Count);
        }
    }
}

using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;
using NeuralNetworkConstructor.Structure.ActivationFunctions;
using NeuralNetworkConstructor.Structure.Synapses;
using NeuralNetworkConstructor.Constructor.Generators;
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
            var synapse = new Synapse();

            Assert.Equal(0, synapse.Weight);
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

            var generator = new EachToEachSynapseGenerator<Synapse>(new Random());
            generator.Generate(master, slave);

            Assert.Equal(2, (slave.Nodes.First() as Neuron).Synapses.Count);
        }
    }
}

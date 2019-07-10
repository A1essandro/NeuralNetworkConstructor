using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
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

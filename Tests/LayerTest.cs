using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Networks;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
using NeuralNetworkConstructor.Constructor.Generators;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class LayerTest
    {

        [Fact]
        public async Task TestLayers2D()
        {
            var inputQty = 12;
            var inner0Qty = 6;
            var inner1Qty = 5;
            var outerQty = 3;
            var input = new Layer2D<IMasterNode>(() => new InputNode(), inputQty, 3, 4);
            var inner0 = new Layer2D<INotInputNode>(() => new Neuron(new Rectifier()), inner0Qty, 2, 3);
            var inner1 = new Layer(() => new Neuron(new Rectifier()), inner1Qty, new Bias());
            var outer = new Layer(() => new Neuron(new Rectifier()), outerQty);

            var generator = new EachToEachSynapseGenerator<Synapse>(new Random());
            generator.Generate(input, inner0);
            generator.Generate(inner0, inner1);
            generator.Generate(inner1, outer);

            var network = new Network(input, inner0, inner1, outer);
            network.Input(new double[] { 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 1 });

            var output0 = await network.Output();
            var output1 = await network.Output();

            Assert.Equal(output0.First(), output1.First());
        }

    }
}

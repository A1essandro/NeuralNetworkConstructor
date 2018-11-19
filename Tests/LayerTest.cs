using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;
using NeuralNetworkConstructor.Structure.ActivationFunctions;
using System;
using System.Linq;
using Xunit;
using NeuralNetworkConstructor.Structure.Layers.Projections;
using NeuralNetworkConstructor.Structure.Synapses;
using NeuralNetworkConstructor.Networks;
using System.Threading.Tasks;
using NeuralNetworkConstructor.Structure.Synapses.Generators;

namespace Tests
{
    public class LayerTest
    {

        [Fact]
        public void TestConstructorWithGenerator()
        {
            ushort neuronsQty = 15;
            var layer = new Layer(() => new Neuron(new Rectifier()), neuronsQty, new Bias());

            Assert.Equal(neuronsQty + 1, layer.Nodes.Count());
            Assert.IsType<Neuron>(layer.Nodes.ToArray()[5]);
            Assert.IsType<Bias>(layer.Nodes.Last());
        }

        [Fact]
        public void TestInputLayer()
        {
            ushort neuronsQty = 7;
            var layer = new InputLayer(() => new InputNode(), neuronsQty, new Bias());

            Assert.Equal(neuronsQty + 1, layer.Nodes.Count());
            Assert.IsType<InputNode>(layer.Nodes.ToArray()[5]);
            Assert.IsType<Bias>(layer.Nodes.Last());
            Assert.Throws<NullReferenceException>(() => (layer.Nodes.Last() as IInputNode).Input(1));
        }

        [Fact]
        public void TestInputLayer2D()
        {
            ushort neuronsQty = 12;
            var layer = new Layer2D<IMasterNode>(() => new InputNode(), neuronsQty, 3, 4);

            Assert.Equal(neuronsQty, layer.NodesQuantity);
        }

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

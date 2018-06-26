using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;
using NeuralNetworkConstructor.Structure.ActivationFunctions;
using System;
using System.Linq;
using Xunit;

namespace Tests
{
    public class LayerTest
    {

        [Fact]
        public void TestConstructorWithGenerator()
        {
            ushort neuronsQty = 15;
            var layer = new Layer(() => new Neuron(new Rectifier()), neuronsQty, new Bias());

            Assert.Equal(neuronsQty + 1, layer.Nodes.Count);
            Assert.IsType<Neuron>(layer.Nodes.ToArray()[5]);
            Assert.IsType<Bias>(layer.Nodes.Last());
        }

        [Fact]
        public void TestInputLayer()
        {
            ushort neuronsQty = 7;
            var layer = new InputLayer(() => new InputNode(), neuronsQty, new Bias());

            Assert.Equal(neuronsQty + 1, layer.Nodes.Count);
            Assert.IsType<InputNode>(layer.Nodes.ToArray()[5]);
            Assert.IsType<Bias>(layer.Nodes.Last());
            Assert.ThrowsAsync<NullReferenceException>(async () => await (layer.Nodes.Last() as IInputNode).Input(1));
        }

    }
}

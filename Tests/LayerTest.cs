using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
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
            Assert.IsType(typeof(Neuron), layer.Nodes.ToArray()[5]);
            Assert.IsType(typeof(Bias), layer.Nodes.Last());
        }
    }
}

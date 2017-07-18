using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;

namespace Tests
{
    [TestClass]
    public class LayerTest
    {
        [TestMethod]
        public void TestConstructorWithGenerator()
        {
            ushort neuronsQty = 15;
            var layer = new Layer(() => new Neuron(new Rectifier()), neuronsQty, new Bias());

            Assert.AreEqual(neuronsQty + 1, layer.Nodes.Count);
            Assert.IsInstanceOfType(layer.Nodes.ToArray()[5], typeof(Neuron));
            Assert.IsInstanceOfType(layer.Nodes.Last(), typeof(Bias));
        }
    }
}

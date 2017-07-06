using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkConstructor;
using NeuralNetworkConstructor.Node;
using NeuralNetworkConstructor.Node.ActivationFunction;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class LayerTest
    {
        [TestMethod]
        public void TestConstructorWithGenerator()
        {
            ushort neuronsQty = 15;
            var layer = new Layer(() => new Neuron(new SimpleActivationFunction()), neuronsQty, new Bias());

            Assert.AreEqual(neuronsQty + 1, layer.Nodes.Count);
            Assert.IsInstanceOfType(layer.Nodes.ToArray()[5], typeof(Neuron));
            Assert.IsInstanceOfType(layer.Nodes.Last(), typeof(Bias));
        }
    }
}

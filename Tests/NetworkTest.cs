using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkConstructor;
using NeuralNetworkConstructor.Network;
using System.Linq;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;

namespace Tests
{
    [TestClass]
    public class NetworkTest
    {
        [TestMethod]
        public void TestInput()
        {
            var inputLayer = new Layer(() => new InputNode(), 2, new Bias());
            var innerLayer = new Layer(() => new Neuron(new SimpleActivationFunction()), 3, new Bias());
            var outputLayer = new Layer(() => new Neuron(new SimpleActivationFunction()), 2);

            Synapse.Generator.EachToEach(inputLayer, innerLayer);
            Synapse.Generator.EachToEach(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);

            network.Input(new []{0.1, 1.0});

            var output1 = network.Output();
            var output2 = network.Output();

            Assert.AreEqual(output1.First(), output2.First());
        }
    }
}

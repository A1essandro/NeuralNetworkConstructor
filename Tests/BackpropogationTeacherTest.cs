using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkConstructor;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.Synapse;
using NeuralNetworkConstructor.Network;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Tests
{
    [TestClass]
    public class BackpropogationTeacherTest
    {
        [TestMethod]
        public void TestTeach()
        {
            var inputLayer = new Layer(() => new InputNode(), 2, new Bias());
            var innerLayer = new Layer(() => new Neuron(new Logistic(0.888)), 3, new Bias());
            var outputLayer = new Layer(new Neuron(new Logistic(0.777)));

            Synapse.Generator.EachToEach(inputLayer, innerLayer);
            Synapse.Generator.EachToEach(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);

            var teacher = new BackpropagationTeacher(network, 0.15);

            var teachKit = new Dictionary<double[], double[]>
            {
                { new double[] { 0, 1 }, new double[] { 1 } },
                { new double[] { 1, 0 }, new double[] { 1 } },
                { new double[] { 1, 1 }, new double[] { 0 } },
                { new double[] { 0, 0 }, new double[] { 0 } }
            };

            for (var i = 0; i < 3000; i++)
            {
                foreach (var t in teachKit)
                {
                    teacher.Teach(t.Key, t.Value);
                }
            }

            network.Input(new double[] { 1, 0 });
            var output = network.Output().First();
            Assert.AreEqual(1.0, Math.Round(output));
            Assert.IsTrue(output > 0.75);
        }
    }
}

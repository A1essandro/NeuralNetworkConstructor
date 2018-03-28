using NeuralNetworkConstructor;
using NeuralNetworkConstructor.Network;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class BackpropogationTeacherTest
    {

        [Fact]
        public void TestTeachXor()
        {
            var inputLayer = new Layer(() => new InputNode(), 2, new Bias());
            var innerLayer = new Layer(() => new Neuron(new Logistic(0.888)), 3, new Bias());
            var outputLayer = new Layer(new Neuron(new Logistic(0.777)));

            Synapse.Generator.EachToEach(inputLayer, innerLayer);
            Synapse.Generator.EachToEach(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);

            var teacher = new BackpropagationTeacher(network, 0.5);

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

            const double delta = 0.15;

            network.Input(new double[] { 1, 0 });
            var output = network.Output().First();
            Assert.True(Math.Abs(1 - output) < delta);

            network.Input(new double[] { 1, 1 });
            output = network.Output().First();
            Assert.True(Math.Abs(0 - output) < delta);

            network.Input(new double[] { 0, 0 });
            output = network.Output().First();
            Assert.True(Math.Abs(0 - output) < delta);

            network.Input(new double[] { 0, 1 });
            output = network.Output().First();
            Assert.True(Math.Abs(1 - output) < delta);
        }

        [Fact]
        public void TestTeachLite()
        {
            var inputLayer = new Layer(new InputNode());
            var innerLayer = new Layer(new Neuron(new Rectifier()));
            var outputLayer = new Layer(new Neuron(new Rectifier()));

            Synapse.Generator.EachToEach(inputLayer, innerLayer);
            Synapse.Generator.EachToEach(innerLayer, outputLayer);

            var network = new Network(inputLayer, innerLayer, outputLayer);

            var teacher = new BackpropagationTeacher(network, 0.15);

            var teachKit = new Dictionary<double[], double[]>
            {
                { new double[] { 0 }, new double[] { 1 } },
            };

            for (var i = 0; i < 3000; i++)
            {
                foreach (var t in teachKit)
                {
                    teacher.Teach(t.Key, t.Value);
                }
            }

            network.Input(new double[] { 1 });
            var output = network.Output().First();
            Assert.Equal(0, Math.Round(output));
            Assert.True(output < 0.15);
        }
    }
}

using System.Linq;
using NeuralNetwork.Structure.Contract.Networks;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetworkConstructor;
using Xunit;

namespace Tests
{
    public class ConstructorTest
    {

        [Fact]
        public void CreateTest()
        {
            new NetworkConstructor().CreateNetwork(out var network)
                .AddInputLayer(out var inputLayer)
                .AddNodes(3)
                .AddInnerLayer(out var firstInnerLayer)
                .AddNodes(4)
                .AddInnerLayer(out var secondInnerLayer)
                .AddNodes(3)
                .AddOutputLayer(out var outpulLayer)
                .AddNodes(2)
                .GenerateSynapses(inputLayer, firstInnerLayer)
                .GenerateSynapses(firstInnerLayer, secondInnerLayer)
                .GenerateSynapses(secondInnerLayer, outpulLayer);

            Assert.Equal(2, network.InnerLayers.Count);
            Assert.Equal(3, inputLayer.Nodes.Count());
            Assert.Equal(2, outpulLayer.Nodes.Count());
            Assert.Equal(4, firstInnerLayer.Nodes.Count());
            Assert.Equal(3, secondInnerLayer.Nodes.Count());
            Assert.Equal(inputLayer.Nodes.Count() * firstInnerLayer.Nodes.Count()
                + firstInnerLayer.Nodes.Count() * secondInnerLayer.Nodes.Count()
                + secondInnerLayer.Nodes.Count() * outpulLayer.Nodes.Count(), network.Synapses.Count());
        }

    }
}
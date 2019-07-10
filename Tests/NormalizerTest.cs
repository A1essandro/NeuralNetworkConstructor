using NeuralNetwork.Structure.Nodes;
using NeuralNetworkConstructor.Normalizer;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class NormalizerTest
    {

        [Fact]
        public void TestInputNode()
        {
            var normalizer = new Normalizer<int>(2);
            normalizer.Set(new int[] { 4500, 7000, 10000, 42422 });

            Assert.Equal(0, normalizer.Get(4500).Normalized);
            Assert.Equal(2, normalizer.Get(42422).Normalized);
        }

        [Fact]
        public void TestInputNode1()
        {
            var cities = new string[] { "London", "Liverpool", "Manchester" };
            var normalizer = new Normalizer<string>(city => Array.FindIndex(cities, w => w == city));
            normalizer.Set(cities);

            Assert.Equal(0, normalizer.Get(cities[0]).Normalized);
            Assert.Equal(1, normalizer.Get(cities[2]).Normalized);
        }

        [Fact]
        public async Task TestImplicitCast()
        {
            var normalizer = new Normalizer<int>(2);
            normalizer.Set(new int[] { 4500, 7000, 10000, 42422 });

            var node = new InputNode();
            node.Input(normalizer.Get(4500));

            Assert.Equal(0, await node.Output());
        }

        [Fact]
        public void TestVector()
        {
            var weights = new int[] { 26500, 29600, 57000, 18400, 188900 };
            var crew = new int[] { 4, 4, 5, 5, 6 };
            var normalizerWeights = new Normalizer<int>();
            normalizerWeights.Set(weights);
            var normalizerCrew = new Normalizer<int>();
            normalizerCrew.Set(crew);

            var vector = new NormalizersVector<int>(normalizerWeights, normalizerCrew);
            var normalized = vector.Get(new int[] { 18400, 6 });

            Assert.Equal<double>(0, normalized.First());
            Assert.Equal<double>(1, normalized.Last());
        }

    }
}
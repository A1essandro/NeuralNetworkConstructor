using System.Linq;
using System.Threading.Tasks;
using NeuralNetworkConstructor.Normalizer;
using Xunit;

namespace Tests
{
    public class NormalizerTest
    {

        [Fact]
        public void TestInputNode()
        {
            var normalizer = new Normalizer<int>();
            normalizer.Set(new int[] { 4500, 7000, 10000, 42422 });
            
            Assert.Equal(normalizer.Get(4500).Normalized, 0);
            Assert.Equal(normalizer.Get(42422).Normalized, 1);
        }

    }
}
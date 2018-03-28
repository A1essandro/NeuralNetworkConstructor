using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using Xunit;

namespace Tests
{
    public class ActivationFunctionTest
    {

        [Fact]
        public void TestSimpleFunction()
        {
            var foo = new Rectifier();
            var val = 0.75;

            Assert.Equal(val, foo.GetEquation(val));
            Assert.Equal(0, foo.GetEquation(-val));
        }
    }
}

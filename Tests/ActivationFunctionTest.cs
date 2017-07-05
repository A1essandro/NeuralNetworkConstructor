using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkConstructor.Node.ActivationFunction;

namespace Tests
{
    [TestClass]
    public class ActivationFunctionTest
    {
        [TestMethod]
        public void TestSimpleFunction()
        {
            var foo = new SimpleActivationFunction();
            var val = 0.75;

            Assert.AreEqual(val, foo.Calculate(val));
            Assert.AreEqual(0, foo.Calculate(-val));
        }
    }
}

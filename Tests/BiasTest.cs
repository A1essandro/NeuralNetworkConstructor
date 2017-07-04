using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkConstructor.Node;

namespace Tests
{
    [TestClass]
    public class BiasTest
    {
        [TestMethod]
        public void TestMethod()
        {
            Assert.AreEqual(1, new Bias().Output());
        }
    }
}

namespace NeuralNetworkConstructor.Network.Node.ActivationFunction
{

    /// <summary>
    /// Rectifier activation function.
    /// </summary>
    /// <remarks>
    /// Any negative number to alpha*x. Any positive number leaves unchanged.
    /// </remarks>
    public class Rectifier : IActivationFunction
    {

        private readonly double _alpha;

        public Rectifier(double alpha = 0)
        {
            _alpha = alpha;
        }

        public double GetEquation(double x) => x >= 0 ? x : _alpha * x;

        public double GetDerivative(double x) => x >= 0 ? 1 : _alpha;

    }
}

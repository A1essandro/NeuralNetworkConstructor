namespace NeuralNetworkConstructor.Network.Node.ActivationFunction
{
    /// <summary>
    /// Function y(x) = a*x
    /// </summary>
    public class Linear : IActivationFunction
    {

        private readonly double _multiplier;

        public Linear(double multiplier = 1)
        {
            _multiplier = multiplier;
        }

        public double GetEquation(double value) => _multiplier * value;

        public double GetDerivative(double value)
        {
            throw new System.NotImplementedException();
        }
    }
}

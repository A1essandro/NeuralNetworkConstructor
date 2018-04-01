namespace NeuralNetworkConstructor.Network.Node.ActivationFunction
{
    /// <summary>
    /// Function y(x) = a*x
    /// </summary>
    public class Linear : IActivationFunction
    {

        private readonly double _multiplier;

        public Linear()
            : this(1)
        {
        }

        public Linear(double multiplier)
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

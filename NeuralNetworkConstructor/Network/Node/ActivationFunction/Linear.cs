namespace NeuralNetworkConstructor.Network.Node.ActivationFunction
{
    /// <summary>
    /// Function y(x) = a*x
    /// </summary>
    public class Linear : IActivationFunction
    {

        private double _multiplier;

        public Linear(double multiplier = 1)
        {
            _multiplier = multiplier;
        }

        public double Calculate(double value) => _multiplier * value;

    }
}

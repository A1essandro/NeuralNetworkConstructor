namespace NeuralNetworkConstructor.Node.ActivationFunction
{
    public class SimpleActivationFunction : IActivationFunction
    {

        public double Calculate(double value) => value > 0 ? value : 0;

    }
}

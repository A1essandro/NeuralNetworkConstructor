namespace NeuralNetworkConstructor.Network.Node.ActivationFunction
{

    /// <summary>
    /// Rectifier activation function.
    /// </summary>
    /// <remarks>
    /// Any negative number to 0. Any positive number leaves unchanged.
    /// </remarks>
    public class Rectifier : IActivationFunction
    {

        public double Calculate(double value) => value > 0 ? value : 0;

    }
}

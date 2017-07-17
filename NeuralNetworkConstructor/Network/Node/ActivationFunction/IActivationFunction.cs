namespace NeuralNetworkConstructor.Network.Node.ActivationFunction
{
    public interface IActivationFunction
    {

        double GetEquation(double value);

        double GetDerivative(double value);

    }
}

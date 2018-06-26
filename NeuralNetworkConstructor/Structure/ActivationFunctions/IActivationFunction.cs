namespace NeuralNetworkConstructor.Structure.ActivationFunctions
{
    public interface IActivationFunction
    {

        double GetEquation(double x);

        double GetDerivative(double x);

    }
}

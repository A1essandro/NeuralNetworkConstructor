namespace NeuralNetwork.Structure.ActivationFunctions
{
    public interface IActivationFunction
    {

        double GetEquation(double value);

        double GetDerivative(double value);

    }
}

using System;

namespace NeuralNetworkConstructor.Network.Node.ActivationFunction
{
    public class Gaussian : IActivationFunction
    {

        public double GetDerivative(double x) => -2 * x * GetEquation(x);

        public double GetEquation(double x) => Math.Exp(-x * x);

    }
}

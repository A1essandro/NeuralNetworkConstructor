using System;
using NeuralNetworkConstructor.Structure.ActivationFunctions;

namespace NeuralNetworkConstructor.Structure.ActivationFunctions
{
    public class Hyperbolic : IActivationFunction
    {
        public double GetDerivative(double x) => (4 * Math.Exp(-2 * x)) / Math.Pow(1 + Math.Exp(-2 * x), 2);

        public double GetEquation(double x) => 2 / (1 + Math.Exp(-2 * x)) - 1;

    }
}
using System;
using NeuralNetwork.Structure.ActivationFunctions;

namespace NeuralNetworkConstructor.Structure.ActivationFunctions
{
    public class Hyperbolic : IActivationFunction
    {
        public double GetDerivative(double value) => (4 * Math.Exp(-2 * value)) / Math.Pow(1 + Math.Exp(-2 * value), 2);

        public double GetEquation(double value) => 2 / (1 + Math.Exp(-2 * value)) - 1;

    }
}
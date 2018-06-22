using System;
using NeuralNetwork.Structure.ActivationFunctions;

namespace NeuralNetworkConstructor.Structure.ActivationFunctions
{
    public class Quadratic : IActivationFunction
    {
        public double GetDerivative(double value) => 2 * value;

        public double GetEquation(double value) => Math.Pow(value, 2);

    }
}
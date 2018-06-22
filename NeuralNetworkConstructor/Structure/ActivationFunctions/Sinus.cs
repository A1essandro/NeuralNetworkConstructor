using System;
using NeuralNetwork.Structure.ActivationFunctions;

namespace NeuralNetworkConstructor.Structure.ActivationFunctions
{
    public class Sinus : IActivationFunction
    {
        public double GetDerivative(double value) => Math.Cos(value);

        public double GetEquation(double value) => Math.Sin(value);

    }
}
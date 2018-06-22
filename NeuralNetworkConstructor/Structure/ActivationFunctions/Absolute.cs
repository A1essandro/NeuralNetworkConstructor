using System;
using NeuralNetwork.Structure.ActivationFunctions;

namespace NeuralNetworkConstructor.Structure.ActivationFunctions
{
    public class Absolute : IActivationFunction
    {
        public double GetDerivative(double value) => value > 0 ? 1 : 0;

        public double GetEquation(double value) => Math.Abs(value);
        
    }
}
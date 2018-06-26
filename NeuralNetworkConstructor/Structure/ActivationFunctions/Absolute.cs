using System;
using NeuralNetworkConstructor.Structure.ActivationFunctions;

namespace NeuralNetworkConstructor.Structure.ActivationFunctions
{
    public class Absolute : IActivationFunction
    {

        public double GetDerivative(double x) => x > 0 ? 1 : 0;

        public double GetEquation(double x) => Math.Abs(x);
        
    }
}
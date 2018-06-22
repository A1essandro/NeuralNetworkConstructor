using System;
using NeuralNetwork.Structure.ActivationFunctions;

namespace NeuralNetworkConstructor.Structure.ActivationFunctions
{
    public class Quadratic : IActivationFunction
    {

        

        public double GetDerivative(double x) => 2 * x;

        public double GetEquation(double x) => Math.Pow(x, 2);

    }
}
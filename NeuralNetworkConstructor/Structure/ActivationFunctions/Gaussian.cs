using System;
using System.Runtime.Serialization;

namespace NeuralNetwork.Structure.ActivationFunctions
{

    [DataContract]
    public class Gaussian : IActivationFunction
    {

        public double GetDerivative(double value) => -2 * value * GetEquation(value);

        public double GetEquation(double value) => Math.Exp(-value * value);

    }
}

using System;
using System.Runtime.Serialization;

namespace NeuralNetwork.Structure.ActivationFunctions
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Logistic : IActivationFunction
    {

        [DataMember]
        private readonly double _param;
        private static double _equation(double x, double alpha) => 1 / (1 + Math.Exp(-alpha * x));

        public Logistic()
            : this(1)
        {
        }

        public Logistic(double param)
        {
            _param = param;
        }

        public double GetEquation(double value) => _equation(value, _param);

        public double GetDerivative(double value)
        {
            var func = _equation(value, 1);
            return _param * func * (1 - func);
        }

    }
}

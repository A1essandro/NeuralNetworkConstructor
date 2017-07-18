using System;

namespace NeuralNetworkConstructor.Network.Node.ActivationFunction
{

    /// <summary>
    /// 
    /// </summary>
    public class Logistic : IActivationFunction
    {

        private readonly double _param;
        private static double _equation(double x, double alpha) => 1 / (1 + Math.Exp(-alpha * x));

        public Logistic(double param = 1)
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

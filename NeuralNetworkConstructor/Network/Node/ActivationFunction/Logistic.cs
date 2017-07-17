using System;

namespace NeuralNetworkConstructor.Network.Node.ActivationFunction
{

    /// <summary>
    /// 
    /// </summary>
    public class Logistic : IActivationFunction
    {

        private readonly double _param;

        public Logistic(double param = 1)
        {
            _param = param;
        }

        public double GetEquation(double value) => 1 / (1 + Math.Exp(-_param * value));

        public double GetDerivative(double value)
        {
            var func = GetEquation(value);
            return func * (1 - func);
        } 

    }
}

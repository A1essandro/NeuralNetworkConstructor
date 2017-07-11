using System;

namespace NeuralNetworkConstructor.Network.Node.ActivationFunction
{
    public  class Logistic : IActivationFunction
    {

        private readonly double _param;

        public Logistic(double param = 1)
        {
            _param = param;
        }

        public double Calculate(double value) => 1 / (1 + Math.Exp(-_param * value));
    }
}

using System;

namespace NeuralNetworkConstructor.Network.Node
{
    public class Bias : INode
    {
        public event Action<double> OnOutput;

        public double Output()
        {
            OnOutput?.Invoke(1);

            return 1;
        }
    }
}

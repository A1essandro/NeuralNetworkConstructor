using System;

namespace NeuralNetworkConstructor.Network.Node
{
    public class InputBias : Bias, IInputNode
    {

        public event Action<double> OnInput;

        public void Input(double input)
        {
            throw new InvalidOperationException();
        }

    }
}

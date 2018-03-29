using System;
using NeuralNetworkConstructor.Common;

namespace NeuralNetworkConstructor.Network.Node
{
    public class InputNode : IInput<double>, INode
    {

        private double _data;

        public event Action<double> OnOutput;
        public event Action<double> OnInput;

        public void Input(double input)
        {
            OnInput?.Invoke(input);
            _data = input;
        }

        public double Output()
        {
            OnOutput?.Invoke(_data);
            return _data;
        }

    }
}

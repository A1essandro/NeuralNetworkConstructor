using NeuralNetworkConstructor.Common;

namespace NeuralNetworkConstructor.Network.Node
{
    public class InputNode : IInput<double>, INode
    {

        private double _data;

        public void Input(double input)
        {
            _data = input;
        }

        public double Output()
        {
            return _data;
        }

    }
}

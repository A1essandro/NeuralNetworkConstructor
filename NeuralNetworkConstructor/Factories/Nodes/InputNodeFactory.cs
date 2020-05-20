using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetwork.Structure.Nodes;

namespace NeuralNetworkConstructor.Factories.Nodes
{
    public class InputNodeFactory : INodeFactory<IInputNode>
    {
        public IInputNode Create(object context)
        {
            return new InputNode();
        }
    }
}
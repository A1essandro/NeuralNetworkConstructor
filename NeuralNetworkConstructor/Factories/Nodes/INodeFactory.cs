using NeuralNetwork.Structure.Contract.Nodes;

namespace NeuralNetworkConstructor.Factories.Nodes
{
    public interface INodeFactory<out TNode>
        where TNode : INode
    {

        TNode Create(object context = default);

    }
}
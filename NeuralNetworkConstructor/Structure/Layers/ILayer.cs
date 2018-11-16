using NeuralNetworkConstructor.Structure.Nodes;

namespace NeuralNetworkConstructor.Structure.Layers
{
    public interface ILayer<TNode> : IReadOnlyLayer<TNode>
        where TNode : INode
    {

        void Add(TNode node);

    }
}
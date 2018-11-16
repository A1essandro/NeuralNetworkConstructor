using NeuralNetworkConstructor.Structure.Nodes;

namespace NeuralNetworkConstructor.Structure.Layers
{
    public interface IEditableLayer<TNode>
        where TNode : INode
    {

        void Add(TNode node);

    }
}
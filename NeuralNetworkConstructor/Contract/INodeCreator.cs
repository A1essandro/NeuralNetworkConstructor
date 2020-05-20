using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetworkConstructor.Factories.Nodes;
using NeuralNetworkConstructor.Factories.Synapses;

namespace NeuralNetworkConstructor.Contract
{
    public interface INodeCreator<out TResult>
    {

        TResult AddNode(object context = default);

        TResult AddNodes(int quantity, object context = default);

        TResult AddNode(out INode node, object context = default);

        TResult AddNode<TNode>(out TNode node, INodeFactory<TNode> factory, object context = default) where TNode : INode;

        TResult AddSynapse(IMasterNode master, ISlaveNode slave, ISynapseFactory factory, object context = default);

    }
}
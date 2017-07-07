using NeuralNetworkConstructor.Common;

namespace NeuralNetworkConstructor.Network.Node.Synapse
{
    public interface ISynapse : IOutput<double>
    {

        void ChangeWeight(double delta);

        double Weight { get; }

        INode MasterNode { get; }

    }
}

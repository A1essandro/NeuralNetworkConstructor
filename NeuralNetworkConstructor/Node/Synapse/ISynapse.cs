namespace NeuralNetworkConstructor.Node.Synapse
{
    public interface ISynapse
    {

        void ChangeWeight(double delta);

        double Weight { get; }

        INode MasterNode { get; }

    }
}

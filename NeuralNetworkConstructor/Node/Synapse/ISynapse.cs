namespace NeuralNetworkConstructor.Node.Synapse
{
    public interface ISynapse : IOutput<double>
    {

        void ChangeWeight(double delta);

        double Weight { get; }

        INode MasterNode { get; }

    }
}

namespace NeuralNetworkConstructor.Structure.Layers.Projections
{
    public interface IProjection<TStructure>
    {

        TStructure Projection { get; }

    }
}
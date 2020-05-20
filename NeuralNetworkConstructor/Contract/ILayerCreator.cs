namespace NeuralNetworkConstructor.Contract
{
    public interface ILayerCreator : IInnerLayerCreator<ILayerCreator>, ISimpleNetworkLayerCreator<ILayerCreator>, INodeCreator<ILayerCreator>
    {

    }
}
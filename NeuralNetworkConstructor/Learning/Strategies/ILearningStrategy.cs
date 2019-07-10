using System.Threading.Tasks;
using NeuralNetwork.Structure.Networks;
using NeuralNetworkConstructor.Learning.Samples;

namespace NeuralNetworkConstructor.Learning.Strategies
{
    public interface ILearningStrategy<in TNetwork, in TSample>
        where TNetwork : INetwork
        where TSample : ISample
    {

        Task LearnSample(TNetwork network, TSample sample, double theta);

    }
}
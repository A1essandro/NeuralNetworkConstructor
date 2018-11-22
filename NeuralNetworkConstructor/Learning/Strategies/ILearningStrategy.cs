using System.Threading.Tasks;
using NeuralNetworkConstructor.Learning.Samples;
using NeuralNetworkConstructor.Networks;

namespace NeuralNetworkConstructor.Learning.Strategies
{
    public interface ILearningStrategy<in TNetwork, in TSample>
        where TNetwork : INetwork
        where TSample : ISample
    {

        Task LearnSample(TNetwork network, TSample sample, double theta);

    }
}
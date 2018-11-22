using System.Threading.Tasks;
using NeuralNetworkConstructor.Learning.Samples;
using NeuralNetworkConstructor.Networks;

namespace NeuralNetworkConstructor.Learning.Strategies
{
    public interface ILearningStrategy<in TNetwork>
        where TNetwork : INetwork
    {

        Task LearnSample(TNetwork network, ILearningSample sample, double theta);

    }
}
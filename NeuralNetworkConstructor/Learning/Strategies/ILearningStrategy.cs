using System.Threading.Tasks;
using NeuralNetwork.Learning.Samples;
using NeuralNetwork.Networks;

namespace NeuralNetwork.Learning.Strategies
{
    public interface ILearningStrategy<in TNetwork, in TSample>
        where TNetwork : INetwork
        where TSample : ISample
    {

        Task LearnSample(TNetwork network, TSample sample, double theta);

    }
}
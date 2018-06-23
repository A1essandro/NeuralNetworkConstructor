using NeuralNetwork.Learning.Samples;
using NeuralNetwork.Networks;

namespace NeuralNetwork.Learning.Strategies
{
    public interface ILearningStrategy<in TNetwork, in TSample>
        where TNetwork : INetwork
        where TSample : ISample
    {

        void LearnSample(TNetwork network, TSample sample);

        bool StopExpression(int epochIndex, int overallSamples);

        void OnEpochStart(int epochIndex);

    }
}
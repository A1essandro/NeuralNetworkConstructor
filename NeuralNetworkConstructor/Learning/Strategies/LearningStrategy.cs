using System.Collections.Generic;
using System.Threading.Tasks;
using NeuralNetwork.Learning.Samples;
using NeuralNetwork.Networks;

namespace NeuralNetwork.Learning.Strategies
{
    public abstract class LearningStrategy<TNetwork, TSample> : ILearningStrategy<TNetwork, TSample>
        where TNetwork : INetwork
        where TSample : ISample
    {

        public abstract Task LearnSample(TNetwork network, TSample sample);

        public abstract bool StopExpression(int epochIndex, int overallSamples);

        public virtual void OnEpochStart(int epochIndex)
        {
            //empty
        }

    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using NeuralNetworkConstructor.Learning.Samples;
using NeuralNetworkConstructor.Networks;

namespace NeuralNetworkConstructor.Learning.Strategies
{
    public abstract class LearningStrategy<TNetwork, TSample> : ILearningStrategy<TNetwork, TSample>
        where TNetwork : INetwork
        where TSample : ISample
    {

        public abstract Task LearnSample(TNetwork network, TSample sample, double theta);

    }
}
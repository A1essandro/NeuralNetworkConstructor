using System.Collections.Generic;
using System.Threading.Tasks;
using NeuralNetworkConstructor.Learning.Samples;
using NeuralNetworkConstructor.Networks;

namespace NeuralNetworkConstructor.Learning.Strategies
{
    public abstract class LearningStrategy<TNetwork> : ILearningStrategy<TNetwork>
        where TNetwork : INetwork
    {

        public abstract Task LearnSample(TNetwork network, ILearningSample sample, double theta);

    }
}
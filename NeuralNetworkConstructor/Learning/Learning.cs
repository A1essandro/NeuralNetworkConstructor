using NeuralNetwork.Learning.Samples;
using NeuralNetwork.Learning.Strategies;
using NeuralNetwork.Networks;
using System.Collections.Generic;

namespace NeuralNetwork.Learning
{
    public class Learning<TNetwork, TSample>
        where TNetwork : INetwork
        where TSample : ISample
    {

        private readonly ILearningStrategy<TNetwork, TSample> _strategy;
        private readonly IEnumerable<TSample> _learningSamples;

        public Learning(ILearningStrategy<TNetwork, TSample> strategy, IEnumerable<TSample> learningSamples)
        {
            _strategy = strategy;
            _learningSamples = learningSamples;
        }

        public void Learn(TNetwork network)
        {
            var overallSamples = 0;
            for (var epochIndex = 0; ; epochIndex++)
            {
                _strategy.OnEpochStart(epochIndex);
                foreach (var sample in _learningSamples)
                {
                    if (_strategy.StopExpression(epochIndex, overallSamples))
                        return;
                    _strategy.LearnSample(network, sample);
                    overallSamples++;
                }
            }
        }

    }
}

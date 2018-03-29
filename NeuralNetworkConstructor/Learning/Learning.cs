using NeuralNetworkConstructor.Network;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Learning
{
    public class Learning<T>
    {

        private readonly LearningStrategy<T> _strategy;
        private readonly IEnumerable<T> _learningSamples;

        public Learning(LearningStrategy<T> strategy, IEnumerable<T> learningSamples)
        {
            _strategy = strategy;
            _learningSamples = learningSamples;
        }

        public void Learn(INetwork network)
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

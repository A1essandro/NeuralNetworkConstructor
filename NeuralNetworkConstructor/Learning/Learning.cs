using NeuralNetwork.Networks;
using System.Collections.Generic;

namespace NeuralNetwork.Learning
{
    public class Learning<TInput, TOutput>
    {

        private readonly LearningStrategy<TInput, TOutput> _strategy;
        private readonly IEnumerable<KeyValuePair<IEnumerable<TInput>, IEnumerable<TOutput>>> _learningSamples;

        public Learning(LearningStrategy<TInput, TOutput> strategy, IEnumerable<KeyValuePair<IEnumerable<TInput>, IEnumerable<TOutput>>> learningSamples)
        {
            _strategy = strategy;
            _learningSamples = learningSamples;
        }

        public void Learn(INetwork<TInput, TOutput> network)
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

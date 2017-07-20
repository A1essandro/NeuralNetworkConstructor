using NeuralNetworkConstructor.Network;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Learning
{
    public class Learning<T>
    {

        LearningStrategy<T> _strategy;

        public Learning(LearningStrategy<T> strategy, IEnumerable<T> learningSamples)
        {
            _strategy = strategy;
        }

        public void Learn(INetwork network, IEnumerable<T> learningSamples)
        {
            var overallSamples = 0;
            for (var epochIndex = 0; ; epochIndex++)
            {
                _strategy.OnEpochStart(epochIndex);
                foreach (var sample in learningSamples)
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

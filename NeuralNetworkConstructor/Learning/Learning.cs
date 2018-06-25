using NeuralNetwork.Learning.Samples;
using NeuralNetwork.Learning.Strategies;
using NeuralNetwork.Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetwork.Learning
{
    public class Learning<TNetwork, TSample> : ILearning<TNetwork, TSample>
        where TNetwork : INetwork
        where TSample : ISample
    {

        public LearningSettings Settings { get; }

        private readonly TNetwork _network;
        private readonly ILearningStrategy<TNetwork, TSample> _strategy;
        private readonly LearningSettings _settings;

        public Learning(TNetwork network, ILearningStrategy<TNetwork, TSample> strategy, LearningSettings settings)
        {
            _network = network;
            _strategy = strategy;
            _settings = settings;
        }

        public async Task Learn(IEnumerable<TSample> samples)
        {
            var random = new Random();
            var theta = _settings.Theta;

            for (var epoch = 0; epoch < _settings.Repeats; epoch++)
            {
                if (_settings.ShuffleEveryEpoch)
                {
                    samples = samples.OrderBy(a => random.NextDouble());
                }

                await _learnEpoch(samples, theta).ConfigureAwait(false);

                theta *= _settings.ThetaFactorPerEpoch;
            }
        }

        private async Task _learnEpoch(IEnumerable<TSample> samples, double theta)
        {
            foreach (var sample in samples)
            {
                await _strategy.LearnSample(_network, sample, theta).ConfigureAwait(false);
            }
        }

    }
}

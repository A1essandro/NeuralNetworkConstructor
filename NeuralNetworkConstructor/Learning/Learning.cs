using NeuralNetworkConstructor.Learning.Samples;
using NeuralNetworkConstructor.Learning.Strategies;
using NeuralNetworkConstructor.Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Learning
{
    public class Learning<TNetwork> : ILearning<TNetwork, ILearningSample>
        where TNetwork : INetwork
    {

        public LearningSettings Settings { get; }

        private readonly TNetwork _network;
        private readonly ILearningStrategy<TNetwork, ILearningSample> _strategy;
        private readonly LearningSettings _settings;

        public Learning(TNetwork network, ILearningStrategy<TNetwork, ILearningSample> strategy, LearningSettings settings)
        {
            _network = network;
            _strategy = strategy;
            _settings = settings;
        }

        public async Task Learn(IEnumerable<ILearningSample> samples, CancellationToken ct = default(CancellationToken))
        {
            var theta = _settings.Theta;
            var random = new Random();
            var epochSamples = samples;

            for (var epoch = 0; epoch < _settings.Repeats; epoch++)
            {
                if (_settings.ShuffleEveryEpoch)
                {
                    var shuffleTask = Task.Run(() =>
                    {
                        epochSamples = samples.OrderBy(a => random.NextDouble());
                    });
                    await Task.WhenAll(shuffleTask, _learnEpoch(epochSamples, theta)).ConfigureAwait(false);
                }
                else
                {
                    await _learnEpoch(samples, theta).ConfigureAwait(false);
                }

                ct.ThrowIfCancellationRequested();
                theta *= _settings.ThetaFactorPerEpoch;
            }
        }

        private async Task _learnEpoch(IEnumerable<ILearningSample> samples, double theta)
        {
            foreach (var sample in samples)
            {
                await _strategy.LearnSample(_network, sample, theta).ConfigureAwait(false);
            }
        }

    }
}

using NeuralNetworkConstructor.Learning.Samples;
using NeuralNetworkConstructor.Learning.Strategies;
using NeuralNetworkConstructor.Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Learning
{
    public class Learning<TNetwork> : ILearning<ILearningSample>
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

        public Task Learn(IEnumerable<ILearningSample> samples, CancellationToken ct = default(CancellationToken))
        {
            if (_settings.ShuffleEveryEpoch)
            {
                return _learnWithShuffle(samples, ct);
            }
            else
            {
                return _learnWithoutShuffle(samples, ct);
            }
        }

        private async Task _learnWithoutShuffle(IEnumerable<ILearningSample> samples, CancellationToken ct)
        {
            var theta = _settings.InitialTheta;
            for (var epoch = 0; epoch < _settings.EpochRepeats; epoch++)
            {
                await _learnEpoch(samples, theta, ct).ConfigureAwait(false);
                theta *= _settings.ThetaFactorPerEpoch(epoch);
            }
        }

        private async Task _learnWithShuffle(IEnumerable<ILearningSample> samples, CancellationToken ct)
        {
            var theta = _settings.InitialTheta;
            var random = new Random();
            for (var epoch = 0; epoch < _settings.EpochRepeats; epoch++)
            {
                var epochSamples = samples.OrderBy(a => random.Next());
                await _learnEpoch(epochSamples, theta, ct).ConfigureAwait(false);

                theta *= _settings.ThetaFactorPerEpoch(epoch);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private async Task _learnEpoch(IEnumerable<ILearningSample> samples, double theta, CancellationToken ct)
        {
            foreach (var sample in samples)
            {
                ct.ThrowIfCancellationRequested();
                await _strategy.LearnSample(_network, sample, theta).ConfigureAwait(false);
            }
        }

    }
}

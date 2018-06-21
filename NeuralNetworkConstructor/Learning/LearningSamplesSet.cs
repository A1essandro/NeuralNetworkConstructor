using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Learning
{
    public class LearningSamplesSet
    {

        public KeyValuePair<double[], double[]>[] Samples { get; }
        public Func<KeyValuePair<double[], double[]>[], KeyValuePair<double[], double[]>> NextSearchStrategy { get; }

        public readonly Func<KeyValuePair<double[], double[]>[], KeyValuePair<double[], double[]>> NextSearchStrategyRandom = (samples) =>
        {
            var index = new Random().Next(0, samples.Count());
            return samples[index];
        };

        public LearningSamplesSet(KeyValuePair<double[], double[]>[] samples,
            Func<KeyValuePair<double[], double[]>[], KeyValuePair<double[], double[]>> nextSearchStrategy)
        {
            Samples = samples;
            NextSearchStrategy = nextSearchStrategy;
        }

    }
}

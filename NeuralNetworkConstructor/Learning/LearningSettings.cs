using System;

namespace NeuralNetworkConstructor.Learning
{
    public class LearningSettings
    {
        public const double DEFAULT_THETA = 0.15;

        public double InitialTheta { get; set; } = DEFAULT_THETA;
        public Func<int, double> ThetaFactorPerEpoch { get; set; } = epoch => 1.0;
        public bool ShuffleEveryEpoch { get; set; } = false;
        public int EpochRepeats { get; set; } = 1;

    }
}
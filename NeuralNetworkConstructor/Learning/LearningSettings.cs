namespace NeuralNetworkConstructor.Learning
{
    public class LearningSettings
    {
        public const double DEFAULT_THETA = 0.15;

        public double Theta { get; set; } = DEFAULT_THETA;
        public double ThetaFactorPerEpoch { get; set; } = 1.0;
        public bool ShuffleEveryEpoch { get; set; } = false;
        public int Repeats { get; set; } = 1;

    }
}
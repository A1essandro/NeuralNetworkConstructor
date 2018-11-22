using System.Collections.Generic;

namespace NeuralNetworkConstructor.Learning.Samples
{
    public class LearningSample : ILearningSample
    {

        public IEnumerable<double> Input { get; }

        public IEnumerable<double> Output { get; }

        public LearningSample(IEnumerable<double> input, IEnumerable<double> output)
        {
            Input = input;
            Output = output;
        }

    }
}
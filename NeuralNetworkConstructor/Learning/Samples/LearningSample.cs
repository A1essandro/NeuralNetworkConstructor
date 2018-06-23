using System.Collections.Generic;

namespace NeuralNetwork.Learning.Samples
{
    public class LearningSample<TInput, TOutput> : ILearningSample<TInput, TOutput>
    {

        public IEnumerable<TInput> Input { get; }

        public IEnumerable<TOutput> Output { get; }

        public LearningSample(IEnumerable<TInput> input, IEnumerable<TOutput> output)
        {
            Input = input;
            Output = output;
        }

    }
}
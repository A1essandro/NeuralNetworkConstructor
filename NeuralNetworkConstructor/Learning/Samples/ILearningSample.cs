using System.Collections.Generic;

namespace NeuralNetwork.Learning.Samples
{
    public interface ILearningSample<TInput, TOutput> : ISample
    {

        IEnumerable<TInput> Input { get; }

        IEnumerable<TOutput> Output { get; }

    }
}
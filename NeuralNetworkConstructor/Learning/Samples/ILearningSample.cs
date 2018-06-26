using System.Collections.Generic;

namespace NeuralNetworkConstructor.Learning.Samples
{
    public interface ILearningSample<TInput, TOutput> : ISample
    {

        IEnumerable<TInput> Input { get; }

        IEnumerable<TOutput> Output { get; }

    }
}
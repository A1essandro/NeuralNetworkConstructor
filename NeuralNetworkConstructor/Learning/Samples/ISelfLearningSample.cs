using System.Collections.Generic;

namespace NeuralNetworkConstructor.Learning.Samples
{
    public interface ISelfLearningSample<out T> : ISample
    {

        IEnumerable<T> Input { get; }

    }
}
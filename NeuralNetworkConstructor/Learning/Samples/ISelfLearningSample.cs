using System.Collections.Generic;

namespace NeuralNetwork.Learning.Samples
{
    public interface ISelfLearningSample<out T> : ISample
    {

        IEnumerable<T> Input { get; }

    }
}
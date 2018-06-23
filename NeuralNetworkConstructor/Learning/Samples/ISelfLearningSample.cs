using System.Collections.Generic;

namespace NeuralNetwork.Learning.Samples
{
    public interface ISelfLearningSample<T> : ISample
    {

        IEnumerable<T> Input { get; }

    }
}
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Learning.Samples
{
    public interface ISelfLearningSample : ISample
    {

        IEnumerable<double> Input { get; }

    }
}
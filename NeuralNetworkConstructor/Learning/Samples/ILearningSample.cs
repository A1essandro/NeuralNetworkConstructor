using System.Collections.Generic;

namespace NeuralNetworkConstructor.Learning.Samples
{
    public interface ILearningSample : ISample
    {

        IEnumerable<double> Input { get; }

        IEnumerable<double> Output { get; }

    }
}
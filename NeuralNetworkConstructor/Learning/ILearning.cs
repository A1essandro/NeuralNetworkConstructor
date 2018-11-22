using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NeuralNetworkConstructor.Learning.Samples;
using NeuralNetworkConstructor.Networks;

namespace NeuralNetworkConstructor.Learning
{
    public interface ILearning<TNetwork, TSample>
        where TNetwork : INetwork
        where TSample : ISample
    {
        Task Learn(IEnumerable<TSample> samples, CancellationToken ct = default(CancellationToken));
    }
}
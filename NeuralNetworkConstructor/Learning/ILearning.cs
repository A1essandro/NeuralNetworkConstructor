using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NeuralNetworkConstructor.Learning.Samples;

namespace NeuralNetworkConstructor.Learning
{
    public interface ILearning<in TSample>
        where TSample : ISample
    {
        Task Learn(IEnumerable<TSample> samples, CancellationToken ct = default(CancellationToken));
    }
}
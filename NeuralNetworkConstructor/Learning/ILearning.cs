using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Learning
{
    public interface ILearning<TNetwork, in TSample>
    {
        Task Learn(IEnumerable<TSample> samples, CancellationToken ct = default(CancellationToken));
    }
}
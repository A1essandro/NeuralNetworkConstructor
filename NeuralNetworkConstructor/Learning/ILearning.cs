using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NeuralNetworkConstructor.Learning.Samples;

namespace NeuralNetworkConstructor.Learning
{
    public interface ILearning<TNetwork>
    {
        Task Learn(IEnumerable<ILearningSample> samples, CancellationToken ct = default(CancellationToken));
    }
}
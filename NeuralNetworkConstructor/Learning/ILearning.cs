using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Learning
{
    public interface ILearning<TNetwork, in TSample>
    {
        Task Learn(IEnumerable<TSample> samples);
    }
}
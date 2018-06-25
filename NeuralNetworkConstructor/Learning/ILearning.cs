using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Learning
{
    public interface ILearning<TNetwork, TSample>
    {
        Task Learn(IEnumerable<TSample> samples);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Learning
{
    public interface ILearning<TNetwork, in TSample>
    {
        Task Learn(IEnumerable<TSample> samples);
    }
}
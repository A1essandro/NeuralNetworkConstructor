using System.Threading.Tasks;
using NeuralNetwork.Structure.Nodes;

namespace NeuralNetwork.Structure.Summators
{
    public interface ISummator
    {

        double GetSum(ISlaveNode node);

        Task<double> GetSumAsync(ISlaveNode node);

    }
}
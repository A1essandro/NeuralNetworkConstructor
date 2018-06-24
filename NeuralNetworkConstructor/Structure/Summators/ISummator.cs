using System.Threading.Tasks;
using NeuralNetwork.Structure.Nodes;

namespace NeuralNetwork.Structure.Summators
{
    public interface ISummator
    {

        Task<double> GetSum(ISlaveNode node);

    }
}
using System.Threading.Tasks;
using NeuralNetworkConstructor.Structure.Nodes;

namespace NeuralNetworkConstructor.Structure.Summators
{
    public interface ISummator
    {

        Task<double> GetSum(ISlaveNode node);

    }
}
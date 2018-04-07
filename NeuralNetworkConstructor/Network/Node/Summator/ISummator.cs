using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Network.Node.Summator
{
    public interface ISummator
    {

        double GetSum(ISlaveNode node);

        Task<double> GetSumAsync(ISlaveNode node);

    }
}
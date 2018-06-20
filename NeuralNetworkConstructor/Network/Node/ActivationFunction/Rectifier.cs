using System.Runtime.Serialization;

namespace NeuralNetworkConstructor.Network.Node.ActivationFunction
{

    /// <summary>
    /// Rectifier activation function.
    /// </summary>
    /// <remarks>
    /// Any negative number to alpha*x. Any positive number leaves unchanged.
    /// </remarks>
    [DataContract]
    public class Rectifier : IActivationFunction
    {

        [DataMember]
        private readonly double _alpha;

        public Rectifier()
            : this(0)
        {
        }

        public Rectifier(double alpha)
        {
            _alpha = alpha;
        }

        public double GetEquation(double x) => x >= 0 ? x : _alpha * x;

        public double GetDerivative(double x) => x >= 0 ? 1 : _alpha;

    }
}

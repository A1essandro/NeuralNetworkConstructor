using System.Runtime.Serialization;

namespace NeuralNetwork.Structure.ActivationFunctions
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

        public double GetEquation(double value) => value >= 0 ? value : _alpha * value;

        public double GetDerivative(double value) => value >= 0 ? 1 : _alpha;

    }
}

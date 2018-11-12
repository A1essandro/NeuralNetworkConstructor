using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Structure.Nodes
{

    [DataContract]
    public class InputNode : IInputNode
    {

        [DataMember]
        private double _data;

        public event Action<double> OnOutput;
        public event Action<double> OnInput;

        public Task Input(double input)
        {
            OnInput?.Invoke(input);
            _data = input;
            return Task.CompletedTask;
        }

        public Task<double> Output()
        {
            OnOutput?.Invoke(_data);
            return Task.FromResult(_data);
        }
    }
}

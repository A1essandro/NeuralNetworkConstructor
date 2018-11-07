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
            return Task.Run(() =>
            {
                OnInput?.Invoke(input);
                _data = input;
            });
        }

        public Task<double> Output()
        {
            return Task.Run(() =>
            {
                OnOutput?.Invoke(_data);
                return _data;
            });
        }
    }
}

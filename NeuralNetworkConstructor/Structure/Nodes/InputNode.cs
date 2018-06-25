using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Nodes
{

    [DataContract]
    public class InputNode : IInputNode
    {

        [DataMember]
        private double _data;

        public event Action<double> OnOutput;
        public event Action<double> OnInput;

        public async Task Input(double input)
        {
            await Task.Run(() =>
            {
                OnInput?.Invoke(input);
                _data = input;
            }).ConfigureAwait(false);

        }

        public async Task<double> Output()
        {
            return await Task.Run(() =>
            {
                OnOutput?.Invoke(_data);
                return _data;
            }).ConfigureAwait(false);
        }
    }
}

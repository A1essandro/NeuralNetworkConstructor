using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Structure.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Structure.Layers
{

    [DataContract]
    [KnownType(typeof(InputNode))]
    [KnownType(typeof(Bias))]
    public class InputLayer : BaseLayer<IMasterNode>, IInputLayer
    {

        public InputLayer(IEnumerable<IMasterNode> nodes)
            : base(nodes)
        {
        }

        public InputLayer(params IMasterNode[] nodes)
            : base(nodes.AsEnumerable())
        {
        }

        public InputLayer(Func<IMasterNode> getter, int qty, params IMasterNode[] other)
            : base(getter, qty, other)
        {
        }

        public event Action<IEnumerable<double>> OnInput;

        public void Input(IEnumerable<double> input)
        {
            var inputNodes = Nodes.OfType<IInputNode>().Where(x => !(x is Bias)).ToArray();
            Contract.Requires(input.Count() == inputNodes.Length, nameof(input));

            OnInput?.Invoke(input);

            var index = 0;
            foreach (var value in input)
            {
                inputNodes[index++].Input(value);
            }
        }

        private static Type[] GetKnownType() => new Type[] { typeof(BaseLayer<IMasterNode>) };
    }
}

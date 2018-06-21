using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Node;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;

namespace NeuralNetworkConstructor.Network.Layer
{

    [DataContract]
    [KnownType(typeof(InputNode))]
    [KnownType(typeof(Bias))]
    public class InputLayer : IInputLayer
    {

        [DataMember]
        private IList<IMasterNode> _nodes = new List<IMasterNode>();

        public IList<IMasterNode> Nodes => _nodes;

        public InputLayer(IList<IMasterNode> nodes)
        {
            _nodes = nodes;
        }

        public InputLayer(params IMasterNode[] nodes)
            : this(nodes.ToList())
        {
        }

        public InputLayer(Func<IMasterNode> getter, ushort qty, params IMasterNode[] other)
        {
            for (var i = 0; i < qty; i++)
            {
                _nodes.Add(getter());
            }
            foreach (var node in other)
            {
                _nodes.Add(node);
            }
        }

        public event Action<IEnumerable<double>> OnInput;

        public void Input(IEnumerable<double> input)
        {
            Contract.Requires(input.Count() == _getInputNodes().Count(), nameof(input));

            OnInput?.Invoke(input);

            var index = 0;
            var inputs = _getInputNodes().ToArray();

            foreach (var value in input)
            {
                inputs[index].Input(value);
                index++;
            }
        }

        public void Refresh()
        {
            foreach (IRefreshable node in Nodes?.Where(n => n is IRefreshable))
            {
                node.Refresh();
            }
        }

        private IEnumerable<IInput> _getInputNodes() => Nodes.Where(x => !(x is Bias)).Select(x => x as IInput);

    }
}

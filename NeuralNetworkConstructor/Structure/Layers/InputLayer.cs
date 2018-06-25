using NeuralNetwork.Common;
using NeuralNetwork.Structure.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Layers
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

        public async Task Input(IEnumerable<double> input)
        {
            Contract.Requires(input.Count() == _getInputNodes().Count(), nameof(input));

            OnInput?.Invoke(input);

            var inputs = _getInputNodes().ToArray();

            await Task.WhenAll(input.Select((value, index) => inputs[index].Input(value))).ConfigureAwait(false);
        }

        public async Task Refresh()
        {
            await Task.WhenAll(Nodes?.Where(n => n is IRefreshable)
                                .Select(n => n as IRefreshable)
                                .Select(n => n.Refresh())).ConfigureAwait(false);
        }

        private IEnumerable<IInputNode> _getInputNodes() => Nodes.Where(x => !(x is Bias)).Select(x => x as IInputNode);

    }
}

using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Node;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NeuralNetworkConstructor.Network.Layer
{
    public class InputLayer : IInputLayer
    {

        public InputLayer(IList<IInputNode> nodes)
        {
            Nodes = nodes;
        }

        public InputLayer(params IInputNode[] nodes)
            : this(nodes.ToList())
        {
        }

        public InputLayer(Func<IInputNode> getter, ushort qty, params IInputNode[] other)
        {
            for (var i = 0; i < qty; i++)
            {
                Nodes.Add(getter());
            }
            foreach (var node in other)
            {
                Nodes.Add(node);
            }
        }

        public IList<IInputNode> Nodes { get; } = new List<IInputNode>();

        public event Action<ICollection<double>> OnInput;

        public void Input(ICollection<double> input)
        {
            Contract.Requires(input.Count == _getInputNodes().Count(), nameof(input));

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

        private IEnumerable<IInputNode> _getInputNodes() => Nodes.Where(x => !(x is Bias));

    }
}

using NeuralNetwork.Common;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace NeuralNetwork.Structure.Layers
{

    [DataContract]
    [KnownType(typeof(Neuron))]
    [KnownType(typeof(Bias))]
    public class Layer : ILayer<INotInputNode>
    {

        [DataMember]
        private IList<INotInputNode> _nodes = new List<INotInputNode>();

        public IList<INotInputNode> Nodes => _nodes;

        public Layer()
        {
        }

        public Layer(IList<INotInputNode> nodes)
        {
            _nodes = nodes;
        }

        public Layer(params INotInputNode[] nodes)
            : this(nodes.ToList())
        {
        }

        public Layer(Func<INotInputNode> getter, ushort qty, params INotInputNode[] other)
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

        public void Refresh()
        {
            foreach (IRefreshable node in _nodes?.Where(n => n is IRefreshable))
            {
                node.Refresh();
            }
        }
    }
}

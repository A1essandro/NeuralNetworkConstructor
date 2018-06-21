using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace NeuralNetworkConstructor.Network.Layer
{

    [DataContract]
    [KnownType(typeof(Neuron))]
    [KnownType(typeof(Neuron<Linear>))]
    [KnownType(typeof(Neuron<Gaussian>))]
    [KnownType(typeof(Neuron<Logistic>))]
    [KnownType(typeof(Neuron<Rectifier>))]
    [KnownType(typeof(Bias))]
    public class Layer : ILayer<INode>
    {

        [DataMember]
        private IList<INode> _nodes = new List<INode>();

        public IList<INode> Nodes => _nodes;

        public Layer()
        {
        }

        public Layer(IList<INode> nodes)
        {
            _nodes = nodes;
        }

        public Layer(params INode[] nodes)
            : this(nodes.ToList())
        {
        }

        public Layer(Func<INode> getter, ushort qty, params INode[] other)
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

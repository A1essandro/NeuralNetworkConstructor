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
    [KnownType(typeof(Neuron))]
    [KnownType(typeof(Bias))]
    public class Layer : BaseLayer<INotInputNode>, ILayer<INotInputNode>
    {

        public Layer()
        {
        }

        public Layer(IEnumerable<INotInputNode> nodes)
            : base(nodes)
        {
        }

        public Layer(params INotInputNode[] nodes)
            : base(nodes.AsEnumerable())
        {
        }

        public Layer(Func<INotInputNode> getter, int qty, params INotInputNode[] other)
            : base(getter, qty, other)
        {
        }

        private static Type[] GetKnownType() => new Type[] { typeof(BaseLayer<INotInputNode>) };

    }
}

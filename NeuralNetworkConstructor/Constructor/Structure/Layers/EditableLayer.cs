using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeuralNetworkConstructor.Structure.Layers;
using NeuralNetworkConstructor.Structure.Nodes;

namespace NeuralNetworkConstructor.Constructor.Structure.Layers
{
    public class EditableLayer : Layer, IEditableLayer<INotInputNode>
    {

        public EditableLayer()
        {
        }

        public EditableLayer(IEnumerable<INotInputNode> nodes)
            : base(nodes)
        {
        }

        public EditableLayer(params INotInputNode[] nodes)
            : base(nodes.AsEnumerable())
        {
        }

        public EditableLayer(Func<INotInputNode> getter, ushort qty, params INotInputNode[] other)
            : base(getter, qty, other)
        {
        }

        public EditableLayer(Layer layer)
            : this(layer.Nodes)
        {
        }

        public void Add(INotInputNode node)
        {
            NodeList.Add(node);
        }

    }
}
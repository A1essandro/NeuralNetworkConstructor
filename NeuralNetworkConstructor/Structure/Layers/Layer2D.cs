using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NeuralNetworkConstructor.Structure.Layers.Projections;
using NeuralNetworkConstructor.Structure.Nodes;

namespace NeuralNetworkConstructor.Structure.Layers
{
    public class Layer2D<TNode> : BaseLayer<TNode>, IProjection<TNode[,]>
        where TNode : INode
    {

        private readonly int _w;
        private readonly int _h;

        public TNode[,] Projection { get; private set; }

        public Layer2D(IEnumerable<TNode> nodes, int sizeW, int sizeH)
            : base(nodes)
        {
            Contract.Assert(nodes.Count() == sizeH * sizeW, nameof(nodes));

            _w = sizeW;
            _h = sizeH;

            Projection = DistributeNodesToArray(nodes.ToArray(), _w, _h);
        }

        public Layer2D(Func<TNode> factory, int qty, int sizeW, int sizeH, params TNode[] other)
            : base(factory, qty, other)
        {
            _w = sizeW;
            _h = sizeH;

            Projection = DistributeNodesToArray(NodeList.ToArray(), _w, _h);
        }

        private static TNode[,] DistributeNodesToArray(TNode[] nodes, int sizeW, int sizeH)
        {
            var arr = new TNode[sizeW, sizeH];
            var index = 0;
            for (var x = 0; x < sizeW; x++)
            {
                for (var y = 0; y < sizeH; y++)
                {
                    arr[x, y] = nodes[index++];
                }
            }

            return arr;
        }

    }
}
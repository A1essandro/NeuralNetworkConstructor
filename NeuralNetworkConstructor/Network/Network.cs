using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkConstructor.Network
{
    public class Network : INetwork
    {

        public ICollection<ILayer> Layers { get; private set; }

        public Network(ICollection<ILayer> layers)
        {
            Layers = layers;
        }

        public IEnumerable<double> Output()
        {
            throw new NotImplementedException();
        }

        public void Input(ICollection<double> input)
        {
            if(input.Count != Layers.First().Nodes.Count)
                throw new ArgumentOutOfRangeException();
            throw new NotImplementedException();
        }
    }
}

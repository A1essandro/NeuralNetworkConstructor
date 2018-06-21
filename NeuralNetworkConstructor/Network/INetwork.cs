﻿using NeuralNetworkConstructor.Common;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Network
{

    public interface INetwork : IOutputSet, IInput<IEnumerable<double>>, IRefreshable
    {

        IInputLayer InputLayer { get; }

        ICollection<ILayer<INotInputNode>> Layers { get; }

        ILayer<INotInputNode> OutputLayer { get; }

    }
}

using NeuralNetworkConstructor.Network.Node;
using System;

namespace NeuralNetworkConstructor.Common
{

    /// <summary>
    /// Interface for refreshable members
    /// </summary>
    public interface IRefreshable<out T> where T : INode
    {

        event Action<T> OnOutputCalculated;

        void Refresh();

    }
}

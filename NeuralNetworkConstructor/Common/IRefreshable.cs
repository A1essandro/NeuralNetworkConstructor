using System;

namespace NeuralNetwork.Common
{

    /// <summary>
    /// Interface for refreshable members
    /// </summary>
    public interface IRefreshable
    {
        void Refresh();
    }

    /// <summary>
    /// Interface for refreshable members
    /// </summary>
    public interface IRefreshable<out T> : IRefreshable
    {
        event Action<T> OnOutputCalculated;
    }
}

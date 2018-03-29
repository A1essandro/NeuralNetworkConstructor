using System;

namespace NeuralNetworkConstructor.Common
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
    [Obsolete]
    public interface IRefreshable<out T> : IRefreshable
    {
        event Action<T> OnOutputCalculated;
    }
}

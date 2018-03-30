using System;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Common
{
    public interface IOutput<out T>
    {

        /// <summary>
        /// Getting output
        /// </summary>
        /// <returns>Calculated output</returns>
        T Output();

        event Action<T> OnOutput;

    }

    public interface IOutput : IOutput<double>
    {

    }

    public interface IOutputSet : IOutput<IEnumerable<double>>
    {

    }

}

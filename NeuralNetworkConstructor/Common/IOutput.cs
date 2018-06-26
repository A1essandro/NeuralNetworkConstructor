using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Common
{
    public interface IOutput<T>
    {

        /// <summary>
        /// Getting output
        /// </summary>
        /// <returns>Calculated output</returns>
        Task<T> Output();

        event Action<T> OnOutput;

    }

}

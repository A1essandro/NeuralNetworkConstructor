using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Common
{
    public interface IOutput<T>
    {

        /// <summary>
        /// Getting output
        /// </summary>
        /// <returns>Calculated output</returns>
        T Output();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<T> OutputAsync();

        event Action<T> OnOutput;

    }

}

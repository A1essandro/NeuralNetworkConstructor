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
        T Output();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<T> OutputAsync();

        event Action<T> OnOutput;

    }

    public interface IOutput : IOutput<double>
    {

    }

    public interface IOutputSet : IOutput<IEnumerable<double>>
    {

    }

}

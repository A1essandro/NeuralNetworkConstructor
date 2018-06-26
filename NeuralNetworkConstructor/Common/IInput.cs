using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Common
{
    public interface IInput<T>
    {

        /// <summary>
        /// Write input value
        /// </summary>
        /// <param name="input">Input value to write</param>
        Task Input(T input);

        event Action<T> OnInput;

    }

}

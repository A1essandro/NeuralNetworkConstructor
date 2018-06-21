using System;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Common
{
    public interface IInput<T>
    {

        /// <summary>
        /// Write input value
        /// </summary>
        /// <param name="input">Input value to write</param>
        void Input(T input);

        event Action<T> OnInput;

    }

}

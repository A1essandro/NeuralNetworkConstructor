namespace NeuralNetworkConstructor.Common
{
    public interface IInput<in T>
    {

        /// <summary>
        /// Write input value
        /// </summary>
        /// <param name="input">Input value to write</param>
        void Input(T input);

    }
}

namespace NeuralNetworkConstructor.Common
{
    public interface IOutput<out T>
    {

        /// <summary>
        /// Getting output
        /// </summary>
        /// <returns>Calculated output</returns>
        T Output();

    }
}

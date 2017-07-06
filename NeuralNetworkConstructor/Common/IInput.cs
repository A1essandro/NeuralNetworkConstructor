namespace NeuralNetworkConstructor.Common
{
    public interface IInput<in T>
    {

        void Input(T input);

    }
}

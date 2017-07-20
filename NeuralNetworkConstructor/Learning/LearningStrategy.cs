using NeuralNetworkConstructor.Network;

namespace NeuralNetworkConstructor.Learning
{
    public abstract class LearningStrategy<T>
    {
        public abstract void LearnSample(INetwork network, T sample);
        public abstract bool StopExpression(int epochIndex, int overallSamples);

        public virtual void OnEpochStart(int epochIndex)
        {
            //empty
        }

    }
}
using System.Collections.Generic;
using NeuralNetwork.Networks;

namespace NeuralNetwork.Learning
{
    public abstract class LearningStrategy<TInput, TOutput>
    {

        public abstract void LearnSample(INetwork<TInput, TOutput> network, KeyValuePair<IEnumerable<TInput>, IEnumerable<TOutput>> sample);

        public abstract bool StopExpression(int epochIndex, int overallSamples);

        public virtual void OnEpochStart(int epochIndex)
        {
            //empty
        }

    }
}
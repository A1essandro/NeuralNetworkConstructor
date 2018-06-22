﻿using NeuralNetwork.Networks;
using NeuralNetwork.Structure.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Learning
{
    public class BackpropagationStrategy : LearningStrategy<double, double>
    {
        private readonly double _maxCurrentEpochError;
        private bool _errorFlag = false;

        private readonly ushort _maxEpochRepeats;
        private readonly double _maxErrorDelta;
        private readonly double _force;
        private readonly Algorithm _algorithm;

        public BackpropagationStrategy(double force, double maxErrorDelta, ushort maxEpochRepeats = ushort.MaxValue)
        {
            _force = force;
            _maxErrorDelta = maxErrorDelta;
            _maxEpochRepeats = maxEpochRepeats;
            _maxCurrentEpochError = double.MaxValue;
            _algorithm = new Algorithm();
        }

        public override void LearnSample(INetwork<double, double> network, KeyValuePair<IEnumerable<double>, IEnumerable<double>> sample)
        {
            var input = sample.Key;
            var expectation = sample.Value;

            _algorithm.Teach(network, input, expectation, _force);
        }

        public override bool StopExpression(int epochIndex, int overallSamples)
        {
            return _errorFlag || epochIndex >= _maxEpochRepeats;
        }

        public override void OnEpochStart(int epochIndex)
        {
            if (_maxErrorDelta > _maxCurrentEpochError)
                _errorFlag = true;
        }


        private class Algorithm
        {

            public void Teach(INetwork<double, double> network, IEnumerable<double> input, IEnumerable<double> expectation, double force)
            {
                network.Input(input);
                var output = network.Output().ToArray();
                var outputLayer = true;
                var sigmas = new List<NeuronSigma>();

                foreach (var layer in network.Layers.Reverse())
                {
                    var oIndex = 0;
                    foreach (var node in layer.Nodes.Where(n => n is Neuron))
                    {
                        var neuron = (Neuron)node;

                        var sigma = outputLayer
                            ? SigmaCalcForOutputLayer(expectation.ToArray(), neuron, output, oIndex)
                            : SigmaCalcForInnerLayers(sigmas, neuron);

                        sigmas.Add(new NeuronSigma(neuron, sigma));
                        ChangeWeights(neuron, sigma, force);
                        oIndex++;
                    }
                    outputLayer = false;
                }
            }

            #region Private

            private static double SigmaCalcForInnerLayers(IEnumerable<NeuronSigma> sigmas, ISlaveNode neuron)
            {
                return GetDerivative(neuron) * GetChildSigmas(sigmas, neuron);
            }

            private static double SigmaCalcForOutputLayer(IReadOnlyList<double> expectation, ISlaveNode neuron, IReadOnlyList<double> output, int oIndex)
            {
                return GetDerivative(neuron) * (expectation[oIndex] - output[oIndex]);
            }

            private static double GetDerivative(ISlaveNode neuron)
            {
                var x = neuron.Summator.GetSum(neuron);
                var derivative = neuron.Function.GetDerivative(x);
                return derivative;
            }

            private void ChangeWeights(ISlaveNode neuron, double sigma, double force)
            {
                foreach (var synapse in neuron.Synapses)
                {
                    synapse.ChangeWeight(force * sigma * synapse.MasterNode.Output());
                }
            }

            private static double GetChildSigmas(IEnumerable<NeuronSigma> sigmas, INode neuron)
            {
                double sigma = 0;
                foreach (var neuronSigma in sigmas)
                {
                    foreach (var synapse in neuronSigma.Neuron.Synapses.Where(s => s.MasterNode == neuron))
                    {
                        sigma += synapse.Weight * neuronSigma.Sigma;
                    }
                }
                return sigma;
            }

            private class NeuronSigma
            {

                public NeuronSigma(Neuron neuron, double sigma)
                {
                    Neuron = neuron;
                    Sigma = sigma;
                }

                public Neuron Neuron { get; }
                public double Sigma { get; }
            }

            #endregion

        }
    }

}
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NeuralNetworkConstructor.Network;
using NeuralNetworkConstructor.Network.Node;

namespace NeuralNetworkConstructor
{
    public class BackpropagationTeacher
    {

        public BackpropagationTeacher(INetwork network, double theta)
        {
            _network = network;
            _theta = theta;
        }

        public void TeachOld(ICollection<double> input, ICollection<double> expectation)
        {
            _network.Input(input);
            var output = _network.Output().ToArray();
            var outputLayer = true;
            var sigmas = new List<NeuronSigma>();

            foreach (var layer in _network.Layers.Reverse())
            {
                var oIndex = 0;
                foreach (var node in layer.Nodes.Where(n => n is Neuron))
                {
                    var neuron = (Neuron)node;
                    var neuronOutput = neuron.Output();

                    var sigma = outputLayer
                        ? SigmaCalcForOutputLayer(expectation.ToArray(), neuronOutput, output, oIndex)
                        : SigmaCalcForInnerLayers(neuronOutput, sigmas, neuron);

                    sigmas.Add(new NeuronSigma(neuron, sigma));
                    ChangeWeights(neuron, sigma);
                    oIndex++;
                }
                outputLayer = false;
            }
        }

        public void Teach(ICollection<double> input, ICollection<double> expectation)
        {
            _network.Input(input);
            var expectationArr = expectation.ToArray();

            var oIndex = 0;
            foreach (ISlaveNode neuron in _network.Layers.Last().Nodes.Where(n => n is ISlaveNode))
            {
                var error = neuron.Output() - expectationArr[oIndex];
                _calculateNeuronWeights(neuron, error);
                oIndex++;
            }
        }

        #region Private

        private void _calculateNeuronWeights(ISlaveNode neuron, double error)
        {
            var weightDelta = error * neuron.Function.GetDerivative(neuron.Output());
            foreach (var synapse in neuron.Synapses)
            {
                synapse.ChangeWeight(-synapse.MasterNode.Output() * weightDelta * _theta);
                if (synapse.MasterNode is ISlaveNode)
                {
                    _calculateNeuronWeights(synapse.MasterNode as ISlaveNode, synapse.Weight * weightDelta);
                }
            }
        }

        private readonly INetwork _network;
        private readonly double _theta;

        private static double SigmaCalcForInnerLayers(double neuronOutput, IEnumerable<NeuronSigma> sigmas, INode neuron)
        {
            return neuronOutput * (1 - neuronOutput) * GetChildSigmas(sigmas, neuron);
        }

        private static double SigmaCalcForOutputLayer(IReadOnlyList<double> expectation, double neuronOutput, IReadOnlyList<double> output, int oIndex)
        {
            return neuronOutput * (1 - neuronOutput) * (expectation[oIndex] - output[oIndex]);
        }

        private void ChangeWeights(ISlaveNode neuron, double sigma)
        {
            foreach (var synapse in neuron.Synapses)
            {
                synapse.ChangeWeight(_theta * sigma * synapse.MasterNode.Output());
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

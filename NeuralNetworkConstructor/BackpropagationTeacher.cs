using System.Collections.Generic;
using System.Linq;
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

        public void Teach(ICollection<double> input, ICollection<double> expectation)
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

                    var sigma = outputLayer
                        ? SigmaCalcForOutputLayer(expectation.ToArray(), neuron, output, oIndex)
                        : SigmaCalcForInnerLayers(sigmas, neuron);

                    sigmas.Add(new NeuronSigma(neuron, sigma));
                    ChangeWeights(neuron, sigma);
                    oIndex++;
                }
                outputLayer = false;
            }
        }

        #region Private

        private readonly INetwork _network;
        private readonly double _theta;

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

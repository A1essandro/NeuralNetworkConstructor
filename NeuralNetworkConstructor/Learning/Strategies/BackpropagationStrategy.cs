using NeuralNetworkConstructor.Learning.Samples;
using NeuralNetworkConstructor.Networks;
using NeuralNetworkConstructor.Structure.Nodes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Learning.Strategies
{
    public class BackpropagationStrategy : LearningStrategy<INetwork<double, double>, ILearningSample<double, double>>
    {

        public override Task LearnSample(INetwork<double, double> network, ILearningSample<double, double> sample, double theta)
        {
            return _teach(network, sample.Input, sample.Output, theta);
        }

        private async Task _teach(INetwork<double, double> network, IEnumerable<double> input, IEnumerable<double> expectation, double force)
        {
            await network.Input(input);
            var output = (await network.Output().ConfigureAwait(false)).ToArray();
            var outputLayer = true;
            var sigmas = new List<NeuronSigma>();

            foreach (var layer in network.Layers.Reverse())
            {
                var oIndex = 0;
                foreach (var node in layer.Nodes.Where(n => n is Neuron))
                {
                    var neuron = (Neuron)node;

                    var sigmaTask = outputLayer
                        ? SigmaCalcForOutputLayer(expectation.ToArray(), neuron, output, oIndex)
                        : SigmaCalcForInnerLayers(sigmas, neuron);

                    var sigma = await sigmaTask.ConfigureAwait(false);

                    sigmas.Add(new NeuronSigma(neuron, sigma));
                    await ChangeWeights(neuron, sigma, force).ConfigureAwait(false);
                    oIndex++;
                }
                outputLayer = false;
            }
        }

        #region Private

        private static async Task<double> SigmaCalcForInnerLayers(IEnumerable<NeuronSigma> sigmas, ISlaveNode neuron)
        {
            var derivative = await GetDerivative(neuron).ConfigureAwait(false);
            return derivative * GetChildSigmas(sigmas, neuron);
        }

        private static async Task<double> SigmaCalcForOutputLayer(IReadOnlyList<double> expectation, ISlaveNode neuron, IReadOnlyList<double> output, int oIndex)
        {
            var derivative = await GetDerivative(neuron).ConfigureAwait(false);
            return derivative * (expectation[oIndex] - output[oIndex]);
        }

        private static async Task<double> GetDerivative(ISlaveNode neuron)
        {
            var x = await neuron.Summator.GetSum(neuron).ConfigureAwait(false);
            var derivative = neuron.Function.GetDerivative(x);
            return derivative;
        }

        private async Task ChangeWeights(ISlaveNode neuron, double sigma, double force)
        {
            foreach (var synapse in neuron.Synapses)
            {
                var masterNodeOutput = await synapse.MasterNode.Output().ConfigureAwait(false);
                synapse.ChangeWeight(force * sigma * masterNodeOutput);
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
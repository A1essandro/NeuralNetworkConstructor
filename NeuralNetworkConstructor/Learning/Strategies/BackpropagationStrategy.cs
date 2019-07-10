using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Networks;
using NeuralNetwork.Structure.Nodes;
using NeuralNetworkConstructor.Learning.Samples;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Learning.Strategies
{
    public class BackpropagationStrategy : ILearningStrategy<INetwork, ILearningSample>
    {

        public Task LearnSample(INetwork network, ILearningSample sample, double theta)
        {
            return _teach(network, sample.Input, sample.Output, theta);
        }

        #region Private

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private async Task _teach(INetwork network, IEnumerable<double> input, IEnumerable<double> expectation, double force)
        {
            network.Input(input);
            var output = (await network.Output().ConfigureAwait(false)).ToArray();
            var isOutputLayer = true;
            var sigmas = new List<NeuronSigma>();
            var expectationArr = expectation.ToArray();

            foreach (var layer in network.Layers.Reverse())
            {
                if (isOutputLayer)
                {
                    await CalculateSigmasForOutputLayer(sigmas, layer, force, output, expectationArr);

                    isOutputLayer = false;
                    continue;
                }

                foreach (var node in layer.Nodes.OfType<Neuron>())
                {
                    var sigma = await SigmaCalcForInnerLayers(sigmas, node).ConfigureAwait(false);

                    sigmas.Add(new NeuronSigma(node, sigma));
                    await ChangeWeights(node, sigma, force).ConfigureAwait(false);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task CalculateSigmasForOutputLayer(List<NeuronSigma> sigmas, IReadOnlyLayer<INotInputNode> layer, double force, double[] output, double[] expectationArr)
        {
            var oIndex = 0;
            foreach (var node in layer.Nodes.OfType<Neuron>())
            {
                var sigma = await SigmaCalcForOutputLayer(expectationArr, node, output, oIndex).ConfigureAwait(false);

                sigmas.Add(new NeuronSigma(node, sigma));
                await ChangeWeights(node, sigma, force).ConfigureAwait(false);
                oIndex++;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<double> SigmaCalcForInnerLayers(IEnumerable<NeuronSigma> sigmas, ISlaveNode neuron)
        {
            var derivative = await GetDerivative(neuron).ConfigureAwait(false);
            return derivative * GetChildSigmas(sigmas, neuron);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<double> SigmaCalcForOutputLayer(IReadOnlyList<double> expectation, ISlaveNode neuron, IReadOnlyList<double> output, int oIndex)
        {
            var derivative = await GetDerivative(neuron).ConfigureAwait(false);
            return derivative * (expectation[oIndex] - output[oIndex]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<double> GetDerivative(ISlaveNode neuron)
        {
            var x = await neuron.Summator.GetSum(neuron).ConfigureAwait(false);
            return neuron.Function.GetDerivative(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task ChangeWeights(ISlaveNode neuron, double sigma, double force)
        {
            var tasks = neuron.Synapses.Select(async synapse =>
            {
                var masterNodeOutput = await synapse.MasterNode.Output().ConfigureAwait(false);
                synapse.ChangeWeight(force * sigma * masterNodeOutput);
            });

            await Task.WhenAll(tasks);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [DebuggerDisplay("Neuron:{Neuron}; Sigma:{Sigma};")]
        private struct NeuronSigma
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
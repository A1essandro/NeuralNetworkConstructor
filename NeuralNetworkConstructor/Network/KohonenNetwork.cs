using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NeuralNetworkConstructor.Network
{
    public class KohonenNetwork : Network
    {

        public KohonenNetwork(ILayer inputLayer, ILayer outputLayer)
            : base(new List<ILayer> { inputLayer, outputLayer})
        {
            Contract.Requires(inputLayer != null, nameof(inputLayer));
            Contract.Requires(outputLayer != null, nameof(outputLayer));
        }

        public class SelfLearning
        {

            internal KohonenNetwork _network;

            public SelfLearning(KohonenNetwork network)
            {
                Contract.Requires(network != null, nameof(network));

                _network = network;
            }

            public void Learn(ICollection<double> input, double force)
            {
                Contract.Requires(force > 0, nameof(force));
                Contract.Requires(force <= 1, nameof(force));

                _network.Input(input);
                var output = _network.Output();

                var winnerIndex = Array.IndexOf(output.ToArray(), output.Max());
                var winner = _network.Layers.Last().Nodes[winnerIndex];

                foreach (var synapse in (winner as ISlaveNode).Synapses)
                {
                    synapse.ChangeWeight(force * (synapse.MasterNode.Output() - synapse.Weight));
                }
            }

        }

        public class SelfOrganizing
        {

            private SelfLearning _learning;
            private double _criticalRange;

            public SelfOrganizing(SelfLearning learning, double criticalRange)
            {
                Contract.Requires(learning != null, nameof(learning));
                Contract.Requires(criticalRange > 0, nameof(criticalRange));

                _learning = learning;
                _criticalRange = criticalRange;
            }

            public void Organize(ICollection<double> input, Func<ISlaveNode> creator, double force)
            {
                var outputLayerNodes = _learning._network.Layers.Last().Nodes;
                foreach (var neuron in outputLayerNodes)
                {
                    var euclidRange = GetEuclidRange(input.ToArray(), (neuron as ISlaveNode).Synapses);
                    if(euclidRange < _criticalRange)
                    {
                        _learning.Learn(input, force);
                        return;
                    }
                }

                var networkLayers = _learning._network.Layers;
                var newNode = creator();
                networkLayers.Last().Nodes.Add(newNode);
                _learning._network.Input(input); //write input data to nodes for read in loop
                foreach (var inputNode in networkLayers.First().Nodes)
                {
                    newNode.AddSynapse(new Synapse(inputNode, inputNode.Output()));
                }
            }

            private static double GetEuclidRange(double[] input, ICollection<ISynapse> synapses)
            {
                double subSqrt = 0;
                var index = 0;

                foreach (var synapse in synapses)
                {
                    subSqrt += Math.Pow(input[index++] - synapse.Weight, 2);
                }

                return Math.Sqrt(subSqrt);
            }

        }

    }
}

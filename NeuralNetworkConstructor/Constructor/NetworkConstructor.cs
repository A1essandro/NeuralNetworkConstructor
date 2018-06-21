using NeuralNetworkConstructor.Network;
using NeuralNetworkConstructor.Network.Layer;
using NeuralNetworkConstructor.Network.Node;
using NeuralNetworkConstructor.Network.Node.ActivationFunction;
using NeuralNetworkConstructor.Network.Node.Synapse;
using System;
using System.Collections.Generic;

namespace NeuralNetworkConstructor.Constructor
{
    public class NetworkConstructor<TNetwork>
        where TNetwork : INetwork, new()
    {

        private TNetwork _currentNetwork = new TNetwork();
        private ILayer<INode> _currentLayer;
        private ISlaveNode _currentNode;

        private readonly IDictionary<string, INode> _nodes = new Dictionary<string, INode>();
        private readonly IDictionary<string, ILayer<INode>> _layers = new Dictionary<string, ILayer<INode>>();

        private void _tryAddToDictionary<T>(IDictionary<string, T> dict, string key, T value)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = value;
                return;
            }

            throw new Exception();
        }

        public NetworkConstructor<TNetwork> AddLayer<TLayer>(string identity, bool withBias = false)
            where TLayer : ILayer<INode>, new()
        {
            var layer = new TLayer();
            _currentLayer = layer;
            _tryAddToDictionary(_layers, identity, layer);
            _currentNetwork.Layers.Add(layer);

            if (withBias)
            {
                _currentLayer.Nodes.Add(new Bias());
            }

            return this;
        }

        #region Nodes

        public NetworkConstructor<TNetwork> AddNode<TNode>(string identity)
            where TNode : INode, new()
        {
            var node = new TNode();
            _tryAddToDictionary(_nodes, identity, node);
            _currentLayer.Nodes.Add(node);

            return this;
        }

        public NetworkConstructor<TNetwork> AddNode(string identity, INode node)
        {
            _tryAddToDictionary(_nodes, identity, node);
            _currentLayer.Nodes.Add(node);

            return this;
        }

        public NetworkConstructor<TNetwork> AddInputNode<TNode>(string identity)
            where TNode : IInputNode, new()
        {
            var node = new TNode();
            _tryAddToDictionary(_nodes, identity, node);
            _currentNetwork.InputLayer.Nodes.Add(node);

            return this;
        }

        public NetworkConstructor<TNetwork> AddInputNodes<TNode>(params string[] identities)
            where TNode : IInputNode, new()
        {
            foreach(var identity in identities)
            {
                AddInputNode<TNode>(identity);
            }

            return this;
        }

        public NetworkConstructor<TNetwork> AddInputNode(string identity, IInputNode node)
        {
            _tryAddToDictionary(_nodes, identity, node);
            _currentNetwork.InputLayer.Nodes.Add(node);
            return this;
        }

        public NetworkConstructor<TNetwork> AddNeuron<TNode>(string identity, IActivationFunction func)
            where TNode : ISlaveNode, new()
        {
            var node = new TNode();
            node.Function = func;
            _currentNode = node;
            _tryAddToDictionary(_nodes, identity, node);
            _currentLayer.Nodes.Add(node);

            return this;
        }

        public NetworkConstructor<TNetwork> AddNeuron(string identity, ISlaveNode node)
        {
            _currentNode = node;
            _tryAddToDictionary(_nodes, identity, node);
            _currentLayer.Nodes.Add(node);

            return this;
        }

        #endregion

        #region Synapses

        public NetworkConstructor<TNetwork> AddSynapse<TSynapse>(string masterNodeIdentity, double? weight = null)
            where TSynapse : ISynapse, new()
        {
            var masterNode = _nodes[masterNodeIdentity];
            var synapse = new TSynapse
            {
                MasterNode = masterNode
            };

            if (weight.HasValue)
            {
                synapse.Weight = weight.Value;
            }

            _currentNode.Synapses.Add(synapse);

            return this;
        }

        public NetworkConstructor<TNetwork> AddSynapses<TSynapse>(string masterNodesLayer = null)
            where TSynapse : ISynapse, new()
        {
            if (masterNodesLayer == null)
            {
                _addSynapsesToInputLayer<TSynapse>();
            }
            else
            {
                ILayer<INode> layer = _layers[masterNodesLayer];
                foreach (var masterNode in layer.Nodes)
                {
                    var synapse = new TSynapse
                    {
                        MasterNode = masterNode
                    };

                    _currentNode.Synapses.Add(synapse);
                }
            }

            return this;
        }

        private void _addSynapsesToInputLayer<TSynapse>()
            where TSynapse : ISynapse, new()
        {
            foreach (var masterNode in _currentNetwork.InputLayer.Nodes)
            {
                var synapse = new TSynapse
                {
                    MasterNode = masterNode
                };

                _currentNode.Synapses.Add(synapse);
            }
        }

        #endregion

        public TNetwork Complete()
        {
            return _currentNetwork;
        }

    }
}

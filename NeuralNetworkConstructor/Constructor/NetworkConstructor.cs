﻿using NeuralNetwork.Networks;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Synapses;
using System;
using System.Collections.Generic;

namespace NeuralNetwork.Constructor
{
    public class NetworkConstructor<TNetwork, TInput, TOutput>
        where TNetwork : INetwork<TInput, TOutput>, new()
    {

        private readonly TNetwork _currentNetwork = new TNetwork();
        private ILayer<INotInputNode> _currentLayer;
        private ISlaveNode _currentNode;

        private readonly IDictionary<string, INode> _nodes = new Dictionary<string, INode>();
        private readonly IDictionary<string, ILayer<INotInputNode>> _layers = new Dictionary<string, ILayer<INotInputNode>>();

        private void _tryAddToDictionary<T>(IDictionary<string, T> dict, string key, T value)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = value;
                return;
            }

            throw new InvalidOperationException();
        }

        public NetworkConstructor<TNetwork, TInput, TOutput> AddLayer<TLayer>(string identity, bool withBias = false)
            where TLayer : ILayer<INotInputNode>, new()
        {
            var layer = new TLayer();
            _currentLayer = layer;
            _tryAddToDictionary<ILayer<INotInputNode>>(_layers, identity, layer);
            _currentNetwork.Layers.Add(layer);

            if (withBias)
            {
                _currentLayer.Nodes.Add(new Bias());
            }

            return this;
        }

        #region Nodes

        public NetworkConstructor<TNetwork, TInput, TOutput> AddNode<TNode>(string identity)
            where TNode : INotInputNode, new()
        {
            var node = new TNode();
            _tryAddToDictionary(_nodes, identity, node);
            _currentLayer.Nodes.Add(node);

            return this;
        }

        public NetworkConstructor<TNetwork, TInput, TOutput> AddNode(string identity, INotInputNode node)
        {
            _tryAddToDictionary(_nodes, identity, node);
            _currentLayer.Nodes.Add(node);

            return this;
        }

        public NetworkConstructor<TNetwork, TInput, TOutput> AddInputNode<TNode>(string identity)
            where TNode : IMasterNode, new()
        {
            var node = new TNode();
            _tryAddToDictionary(_nodes, identity, node);
            _currentNetwork.InputLayer.Nodes.Add(node);

            return this;
        }

        public NetworkConstructor<TNetwork, TInput, TOutput> AddInputNodes<TNode>(params string[] identities)
            where TNode : IInputNode, new()
        {
            foreach(var identity in identities)
            {
                AddInputNode<TNode>(identity);
            }

            return this;
        }

        public NetworkConstructor<TNetwork, TInput, TOutput> AddInputNode(string identity, IInputNode node)
        {
            _tryAddToDictionary(_nodes, identity, node);
            _currentNetwork.InputLayer.Nodes.Add(node);
            return this;
        }

        public NetworkConstructor<TNetwork, TInput, TOutput> AddNeuron<TNode>(string identity, IActivationFunction func)
            where TNode : ISlaveNode, new()
        {
            var node = new TNode();
            node.Function = func;
            _currentNode = node;
            _tryAddToDictionary(_nodes, identity, node);
            _currentLayer.Nodes.Add(node);

            return this;
        }

        public NetworkConstructor<TNetwork, TInput, TOutput> AddNeuron(string identity, ISlaveNode node)
        {
            _currentNode = node;
            _tryAddToDictionary(_nodes, identity, node);
            _currentLayer.Nodes.Add(node);

            return this;
        }

        #endregion

        #region Synapses

        public NetworkConstructor<TNetwork, TInput, TOutput> AddSynapse<TSynapse>(string masterNodeIdentity, double? weight = null)
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

        public NetworkConstructor<TNetwork, TInput, TOutput> AddSynapses<TSynapse>(string masterNodesLayer = null)
            where TSynapse : ISynapse, new()
        {
            if (masterNodesLayer == null)
            {
                _addSynapsesToInputLayer<TSynapse>();
            }
            else
            {
                var layer = _layers[masterNodesLayer];
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

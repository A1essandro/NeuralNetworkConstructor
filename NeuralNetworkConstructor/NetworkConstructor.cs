using System;
using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Networks;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetworkConstructor.Contract;
using NeuralNetworkConstructor.Factories.Layers;
using NeuralNetworkConstructor.Factories.Network;
using NeuralNetworkConstructor.Factories.Nodes;
using NeuralNetworkConstructor.Factories.Synapses;
using NeuralNetworkConstructor.Generators;

namespace NeuralNetworkConstructor
{
    public class NetworkConstructor : INetworkCreator<IMultilayerNetwork, ILayerCreator>, ILayerCreator
    {

        private IMultilayerNetwork _network;
        private dynamic _currentLayer = null;

        public ILayerCreator CreateNetwork(out IMultilayerNetwork network, object context = default, INetworkFactory<IMultilayerNetwork> factory = default)
        {
            if (factory == default)
                factory = new NetworkFactory();

            _network = factory.Create(context);
            _currentLayer = null;
            network = _network;

            return this;
        }

        public ILayerCreator AddInputLayer(out ILayer<IMasterNode> inputLayer, object context = default, ILayerFactory<IInputLayer, IMasterNode> factory = default)
        {
            if (factory == default)
                factory = new InputLayerFactory();

            inputLayer = factory.Create(context);
            _network.InputLayer = inputLayer;
            _currentLayer = inputLayer;

            return this;
        }

        public ILayerCreator AddOutputLayer(out ILayer<INotInputNode> layer, object context = default, ILayerFactory<ILayer<INotInputNode>, INotInputNode> factory = default)
        {
            if (factory == default)
                factory = new LayerFactory();

            layer = factory.Create(context);
            _network.OutputLayer = layer;
            _currentLayer = layer;

            return this;
        }

        public ILayerCreator AddInnerLayer(out ILayer<INotInputNode> layer, object context = default, ILayerFactory<ILayer<INotInputNode>, INotInputNode> factory = default)
        {
            if (!(_network is IMultilayerNetwork))
                throw new InvalidOperationException($"Network is not {nameof(IMultilayerNetwork)}");

            var network = _network as IMultilayerNetwork;

            if (factory == default)
                factory = new LayerFactory();

            layer = factory.Create(context);
            network.AddInnerLayer(factory.Create(context));
            _currentLayer = layer;

            return this;
        }

        public ILayerCreator AddNode(object context = default)
        {
            AddNode(out var _, context);

            return this;
        }

        public ILayerCreator AddNode(out INode node, object context = default)
        {
            if (_currentLayer is IInputLayer)
            {
                AddNode<IInputNode>(out var inputNode, new InputNodeFactory(), context);
                node = inputNode;
            }
            else
            {
                AddNode<INotInputNode>(out var neuron, new NeuronFactory(), context);
                node = neuron;
            }

            return this;
        }

        public ILayerCreator AddNode<TNode>(out TNode node, INodeFactory<TNode> factory, object context = default)
            where TNode : INode
        {
            node = factory.Create(context);
            _currentLayer.AddNode(node);

            return this;
        }

        public ILayerCreator AddNodes(int quantity, object context = null)
        {
            for (int i = 0; i < quantity; i++)
            {
                AddNode(context);
            }

            return this;
        }

        public ILayerCreator AddSynapse(IMasterNode master, ISlaveNode slave, ISynapseFactory factory, object context = default)
        {
            _network.AddSynapse(factory.Create(master, slave, context));

            return this;
        }

        public ILayerCreator GenerateSynapses<TMasterLayerNode, TSlaveLayerNode>(ILayer<TMasterLayerNode> masterLayer, ILayer<TSlaveLayerNode> slaveLayer, ISynapseGenerator generator = default)
            where TMasterLayerNode : INode
            where TSlaveLayerNode : INotInputNode
        {
            if (generator == default)
            {
                generator = new EachToEachSynapseGenerator(new SynapseFactory());
            }

            foreach (var synapse in generator.Generate(masterLayer, slaveLayer))
            {
                _network.AddSynapse(synapse);
            }

            return this;
        }

    }
}

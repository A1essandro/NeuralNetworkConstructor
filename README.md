# NeuralNetworkConstructor

[![Build status](https://ci.appveyor.com/api/projects/status/dsyj7gu551sg2jey?svg=true)](https://ci.appveyor.com/project/A1essandro/neuralnetworkconstructor)

[![Quality gate status](https://sonarcloud.io/api/badges/measure?key=neural-network-constructor&metric=alert_status)](https://sonarqube.com/dashboard/index/neural-network-constructor)
[![Reliability Rating](https://sonarcloud.io/api/badges/measure?key=neural-network-constructor&metric=reliability_rating)](https://sonarqube.com/dashboard/index/neural-network-constructor)
[![Security Rating](https://sonarcloud.io/api/badges/measure?key=neural-network-constructor&metric=security_rating)](https://sonarqube.com/dashboard/index/neural-network-constructor)
[![Maintainability Rating](https://sonarcloud.io/api/badges/measure?key=neural-network-constructor&metric=sqale_rating)](https://sonarqube.com/dashboard/index/neural-network-constructor)
[![Issues](https://sonarcloud.io/api/badges/measure?key=neural-network-constructor&metric=violations)](https://sonarqube.com/dashboard/index/neural-network-constructor)
[![Public documented API](https://sonarcloud.io/api/badges/measure?key=neural-network-constructor&metric=public_documented_api_density)](https://sonarqube.com/dashboard/index/neural-network-constructor)

See also [similar realization on PHP](https://github.com/A1essandro/neural-network)

## Specification

#### Network

Interface [implementation](https://github.com/A1essandro/NeuralNetworkConstructor/tree/master/NeuralNetworkConstructor/Network) of `INetwork` is a container comprising layers (`ILayer`) interconnected by synapses (`Synapse`).

#### Layers

Interface [implementations](https://github.com/A1essandro/NeuralNetworkConstructor/tree/master/NeuralNetworkConstructor/Network/Layer) of `ILayer` are formal groups of `INode`.

#### Nodes

`INode` - [neurons](https://github.com/A1essandro/NeuralNetworkConstructor/blob/master/NeuralNetworkConstructor/Network/Node/Neuron.cs), [input-neurons](https://github.com/A1essandro/NeuralNetworkConstructor/blob/master/NeuralNetworkConstructor/Network/Node/InputNode.cs), [bias](https://github.com/A1essandro/NeuralNetworkConstructor/blob/master/NeuralNetworkConstructor/Network/Node/Bias.cs) etc.

#### Synapses

`Synapse` - is a connection between two nodes (`INode`). [Synapse](https://github.com/A1essandro/NeuralNetworkConstructor/tree/master/NeuralNetworkConstructor/Network/Node/Synapse) takes the result (call `Output()`) of leading node 
(implementation `INode` interface), it output is handled by the [activation function](https://github.com/A1essandro/NeuralNetworkConstructor/tree/master/NeuralNetworkConstructor/Network/Node/ActivationFunction) (`IActivationFunction` or `Func<double, double>`), 
the result is transmitted to the slave node.

## Contribute

Contributions to the package are always welcome!

## License

The code base is licensed under the MIT license.
namespace NeuralNetworkConstructor.Normalizer
{
    public struct NormalizedValue<TValue>
    {

        public TValue Value { get; }

        public double Normalized { get; }

        public NormalizedValue(TValue value, double normalized)
        {
            Value = value;
            Normalized = normalized;
        }

        public static implicit operator double(NormalizedValue<TValue> v) => v.Normalized;

    }
}
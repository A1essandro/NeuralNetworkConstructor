using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NeuralNetworkConstructor.Normalizer
{
    public class NormalizersVector<T> : IEnumerable<Normalizer<T>>
    {

        public IEnumerable<Normalizer<T>> Normalizers => _normalizers;

        private Normalizer<T>[] _normalizers;

        public NormalizersVector(IEnumerable<Normalizer<T>> normalizers)
        {
            _normalizers = normalizers.ToArray();
        }

        public NormalizersVector(params Normalizer<T>[] normalizers)
        {
            _normalizers = normalizers;
        }

        public IEnumerator<Normalizer<T>> GetEnumerator() => Normalizers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Normalizers.GetEnumerator();

        public IEnumerable<NormalizedValue<T>> Get(IEnumerable<T> values)
        {
            if (values.Count() != _normalizers.Length)
                throw new ArgumentOutOfRangeException();

            return values.Zip(_normalizers, (v, n) => n.Get(v));
        }

    }
}
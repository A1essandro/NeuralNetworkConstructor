using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Normalizer
{
    public class Normalizer<T>
    {

        private static readonly Func<T, int> DefaultConverter = x => x.GetHashCode();

        private readonly ConcurrentDictionary<T, NormalizedValue<T>> _values = new ConcurrentDictionary<T, NormalizedValue<T>>();
        private readonly int _factor = 1;
        private readonly Func<T, int> _converter = DefaultConverter;

        #region ctor

        public Normalizer()
        {}

        public Normalizer(int factor)
        {
            _factor = factor;
        }

        public Normalizer(Func<T, int> converter)
        {
            _converter = converter;
        }

        public Normalizer(Func<T, int> converter, int factor)
        {
            _converter = converter;
            _factor = factor;
        }

        #endregion

        public virtual void Set(IEnumerable<T> values)
        {
            var dict = values.Distinct().ToDictionary(_converter, x => x)
                .OrderBy(x => x.Key);

            var min = (double)dict.First().Key;
            var max = (double)dict.Last().Key;

            Parallel.ForEach(dict, item => _calculateConvertibleItem(item, min, max));
        }

        public NormalizedValue<T> Get(T value)
        {
            if (_values.ContainsKey(value))
            {
                return _values[value];
            }

            var dict = _values.ToDictionary(x => _converter(x.Key), x => x)
                .OrderBy(x => x.Key);

            var min = (double)dict.First().Key;
            var max = (double)dict.Last().Key;

            _calculateConvertibleItem(new KeyValuePair<int, T>(_converter(value), value), min, max);

            return _values[value];
        }

        private void _calculateConvertibleItem(KeyValuePair<int, T> value, double min, double max)
        {
            var normalized = _factor * (value.Key - min) / (max - min);
            var item = new NormalizedValue<T>(value.Value, normalized);
            _values.TryAdd(value.Value, item);
        }

    }
}
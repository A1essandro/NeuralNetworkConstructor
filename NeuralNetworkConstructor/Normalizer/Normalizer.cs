using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetworkConstructor.Normalizer
{
    public class Normalizer<T>
    {

        private ConcurrentDictionary<T, NormalizedValue<T>> _values = new ConcurrentDictionary<T, NormalizedValue<T>>();

        public virtual void Set(IEnumerable<T> values)
        {
            var dict = values.ToDictionary(x => x.GetHashCode(), x => x)
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

            var dict = _values.ToDictionary(x => x.GetHashCode(), x => x)
                .OrderBy(x => x.Key);

            var min = (double)dict.First().Key;
            var max = (double)dict.Last().Key;

            _calculateConvertibleItem(new KeyValuePair<int, T>(value.GetHashCode(), value), min, max);

            return _values[value];
        }

        private void _calculateConvertibleItem(KeyValuePair<int, T> value, double min, double max)
        {
            var normalized = (value.Key - min) / (max - min);
            var item = new NormalizedValue<T>(value.Value, normalized);
            _values.TryAdd(value.Value, item);
        }
    }
}
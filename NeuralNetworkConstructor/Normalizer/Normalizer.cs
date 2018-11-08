using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkConstructor.Normalizer
{
    public class Normalizer<T>
    {

        private Dictionary<T, NormalizedValue<T>> _values = new Dictionary<T, NormalizedValue<T>>();

        public void Set(IEnumerable<T> values)
        {
            var firstValue = values.FirstOrDefault();
            if (firstValue is IConvertible)
            {
                IConvertible max, min;
                var typedValues = values.Select(x => x as IConvertible);
                max = typedValues.Max(x => x);
                min = typedValues.Min(x => x);

                _calculateConvertible(typedValues.Distinct(), min.ToDouble(null), max.ToDouble(null));
                return;
            }

            foreach (var value in values.Distinct())
            {
                //TODO
            }
        }

        public NormalizedValue<T> Get(T value)
        {
            if (_values.ContainsKey(value))
            {
                return _values[value];
            }

            if (value is IConvertible)
            {
                IConvertible max, min;
                var typedValues = _values.Select(x => x.Value as IConvertible);
                max = typedValues.Max(x => x);
                min = typedValues.Min(x => x);

                _calculateConvertibleItem(value as IConvertible, min.ToDouble(null), max.ToDouble(null));

                return _values[value];
            }

            return default(NormalizedValue<T>); //TODO
        }

        private void _calculateConvertible(IEnumerable<IConvertible> values, double min, double max)
        {
            foreach (var value in values)
            {
                _calculateConvertibleItem(value, min, max);
            }
        }

        private void _calculateConvertibleItem(IConvertible value, double min, double max)
        {
            var tvalue = (T)value;
            var normalized = (value.ToDouble(null) - min) / (max - min);
            var item = new NormalizedValue<T>(tvalue, normalized);
            _values.Add(tvalue, item);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessWho
{
    public class RandomTable<T>
    {
        readonly RandomFunc _getRandom;
        readonly IList<KeyValuePair<int, T>> _valuePairs = new List<KeyValuePair<int, T>>();
        int _totalCumulativeFrequency;

        public RandomTable(RandomFunc getRandom)
        {
            _getRandom = getRandom;
        }

        public void Add(T item, int frequency)
        {
            _totalCumulativeFrequency += frequency;
            _valuePairs.Add(new KeyValuePair<int, T>(_totalCumulativeFrequency, item));
        }

        T Next()
        {
            var index = _getRandom(_totalCumulativeFrequency);

            var result = _valuePairs
                .SkipWhile(vp => vp.Key <= index)
                .Select(vp => vp.Value)
                .FirstOrDefault();
            return result;
        }

        public IEnumerable<T> Sequence
        {
            get
            {
                while (true)
                    yield return Next();
            }
        }
    }

    public delegate int RandomFunc(int maxValue);
}
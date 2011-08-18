using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessWho
{
    public class NameGenerator
    {
        readonly Random _random;
        readonly int _maxGivenNames;

        readonly RandomTable<string> _maleTable;
        readonly RandomTable<string> _femaleTable;
        readonly RandomTable<string> _surnameTable;

        public NameGenerator() : this (new RandomNameConfiguration()) { }

        public NameGenerator(RandomNameConfiguration configuration)
        {
            _random = configuration.CreateRandom();
            _maleTable = new RandomTable<string>(_random);
            _femaleTable = new RandomTable<string>(_random);
            _surnameTable = new RandomTable<string>(_random);

            _maxGivenNames = configuration.MaxGivenNames;

            var nameFiles = configuration.GetCensusDataFiles();
            var convention = configuration.GetFileTypeConvention();

            foreach (var nameData in nameFiles.Where(convention.ContainsMaleNames).SelectMany(f => f.NameData))
                _maleTable.Add(nameData.Name, nameData.Frequency);

            foreach (var nameData in nameFiles.Where(convention.ContainsFemaleNames).SelectMany(f => f.NameData))
                _femaleTable.Add(nameData.Name, nameData.Frequency);

            foreach (var nameData in nameFiles.Where(convention.ContainsSurnames).SelectMany(f => f.NameData))
                _surnameTable.Add(nameData.Name, nameData.Frequency);
        }

        public GeneratedName NextName()
        {
            return _random.Next(0, 1000) > 500 ? NextFemaleName() : NextMaleName();
        }

        public GeneratedName NextMaleName()
        {
            var givenNameCount = _random.Next(1, _maxGivenNames);
            
            var names = _maleTable.Sequence
                .Take(givenNameCount)
                .Concat(_surnameTable.Sequence.Take(1));

            return new GeneratedName(names.ToArray(), Gender.Male);
        }

        public GeneratedName NextFemaleName()
        {
            var givenNameCount = _random.Next(1, _maxGivenNames);
            var names = _femaleTable.Sequence
                .Take(givenNameCount)
                .Concat(_surnameTable.Sequence.Take(1));

            return new GeneratedName(names.ToArray(), Gender.Female);
        }

        private class RandomTable<T>
        {
            readonly Random _random;
            readonly IList<KeyValuePair<int, T>> _valuePairs = new List<KeyValuePair<int, T>>();
            int _totalCumulativeFrequency = 0;

            public RandomTable(Random random)
            {
                _random = random;
            } 

            public void Add(T item, int frequency)
            {
                _totalCumulativeFrequency += frequency;
                _valuePairs.Add(new KeyValuePair<int, T>(_totalCumulativeFrequency, item));                
            }

            T Next()
            {
                var index = _random.Next(0, _totalCumulativeFrequency);

                return _valuePairs
                    .SkipWhile(vp => vp.Key < index)
                    .Select(vp => vp.Value)
                    .FirstOrDefault();                
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
    }
}
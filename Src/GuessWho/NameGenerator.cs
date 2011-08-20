using System;
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
            _maleTable = new RandomTable<string>(_random.Next);
            _femaleTable = new RandomTable<string>(_random.Next);
            _surnameTable = new RandomTable<string>(_random.Next);

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
    }
}
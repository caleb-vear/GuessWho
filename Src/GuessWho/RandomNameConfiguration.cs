using System;
using System.Collections.Generic;

namespace GuessWho
{
    public class RandomNameConfiguration
    {
        int? _randomSeed;
        ICensusDataFileProvider _censusFileProvider = new EmbededCensusDataFileProvider(typeof(GeneratedName).Assembly);
        INameFileTypeConvention _nameFileTypeConvention = new DefaultNameFileTypeConvention();

        public int MaxGivenNames { get; private set; }

        public RandomNameConfiguration()
        {
            MaxGivenNames = 3;
        }

        public Random CreateRandom()
        {
            return _randomSeed.HasValue ? new Random(_randomSeed.Value) : new Random();
        }

        public IEnumerable<CensusDataFile> GetCensusDataFiles()
        {
            return _censusFileProvider.GetCensusFiles();
        }

        public INameFileTypeConvention GetFileTypeConvention()
        {
            return _nameFileTypeConvention;
        }

        public RandomNameConfiguration Seed(int seed)
        {
            _randomSeed = seed;
            return this;
        }

        public RandomNameConfiguration MaximumGivenNames(int maxGivenNameCount)
        {
            MaxGivenNames = maxGivenNameCount;
            return this;
        }

        public RandomNameConfiguration CensusDataFileProvider(ICensusDataFileProvider provider)
        {
            _censusFileProvider = provider;
            return this;
        }

        public RandomNameConfiguration NameFileTypeConvention(INameFileTypeConvention fileTypeConvention)
        {
            _nameFileTypeConvention = fileTypeConvention;
            return this;
        }
    }
}
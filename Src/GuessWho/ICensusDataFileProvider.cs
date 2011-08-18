using System.Collections.Generic;

namespace GuessWho
{
    public interface ICensusDataFileProvider
    {
        IEnumerable<CensusDataFile> GetCensusFiles();
    }
}
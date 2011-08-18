using System.Collections.Generic;

namespace GuessWho
{
    public class CensusDataFile
    {
        public string Filename { get; private set; }
        public IEnumerable<CensusNameData> NameData { get; private set; }

        public CensusDataFile(string filename, IEnumerable<CensusNameData> nameData)
        {
            Filename = filename;
            NameData = nameData;
        }
    }
}
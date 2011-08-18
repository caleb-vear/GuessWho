using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GuessWho
{
    public class EmbededCensusDataFileProvider :ICensusDataFileProvider
    {
        readonly Assembly _assembly;
        
        public EmbededCensusDataFileProvider(Assembly assembly)
        {
            _assembly = assembly;
        }

        public IEnumerable<CensusDataFile> GetCensusFiles()
        {
            return _assembly
                .GetManifestResourceNames()
                .Where(filename => filename.EndsWith(".cnd"))
                .Select(ReadCensusFile)
                .ToList();            
        }

        CensusDataFile ReadCensusFile(string filename)
        {
            using (var resourceStream = _assembly.GetManifestResourceStream(filename))
            {
                var serializer = new CensusNameDataFileSerializer();

                return new CensusDataFile(filename, serializer.Deserialize(resourceStream).ToArray());
            }
        }
    }
}
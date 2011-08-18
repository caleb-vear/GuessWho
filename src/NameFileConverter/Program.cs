using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessWho.NameFileConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage GuessWho.NameFileConverter [search directory path]");
                return;
            }

            var sourceFiles = Directory.GetFiles(args.Length == 1 ? args[0] : ".", "dist.*.txt");

            if (sourceFiles.Length == 0)
            {
                Console.WriteLine("No files found to convert");
            }

            Console.WriteLine("Converting {0} files", sourceFiles.Length);
            Parallel.ForEach(sourceFiles, ConvertFile);
            Console.WriteLine("Conversion complete");
            Console.ReadLine();
        }

        static void ConvertFile(string inFilePath)
        {
            var outFilePath = Path.ChangeExtension(inFilePath, "cnd");
            ConvertFile(inFilePath, outFilePath);
        }

        static void ConvertFile(string inFilePath, string outFilePath)
        {
            using (var inFile = File.OpenRead(inFilePath))
            using (var outFile = File.OpenWrite(outFilePath))
            {
                var nameFileParser = new NameFileParser();
                var binSerializer = new CensusNameDataFileSerializer();

                var names = nameFileParser.Parse(inFile);
                binSerializer.Serialize(outFile, names);
            }

            Console.WriteLine("{0} -> {1} conversion completed", Path.GetFileName(inFilePath), Path.GetFileName(outFilePath));
        }
    }
}

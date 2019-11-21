using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GuessWho
{
    public class NameFileParser
    {
        private readonly CultureInfo _culture;

        public NameFileParser(CultureInfo culture)
        {
            _culture = culture ?? throw new ArgumentNullException(nameof(culture));
        }

        public NameFileParser()
            : this(CultureInfo.InvariantCulture)
        { }

        public IEnumerable<CensusNameData> Parse(Stream nameFile)
        {
            using (var reader = new StreamReader(nameFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var lineValues = line.Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    yield return new CensusNameData
                                     {
                                         Name = lineValues[0],
                                         Frequency = (short)(decimal.Parse(lineValues[1], _culture) * 1000),
                                     };
                }
            }
        }
    }
}
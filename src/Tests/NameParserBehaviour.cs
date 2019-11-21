using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shouldly;

namespace GuessWho.Tests
{
    [TestFixture]
    public class NameParserBehaviour
    {
        private string _nameFileText;
        private string _italianNameFileText;

        [SetUp]
        public void Given()
        {
            _nameFileText = new StringBuilder()
                .Append("SMITH          1.006  1.006      1\n")
                .Append("JOHNSON        0.810  1.816      2\n")
                .Append("WILLIAMS       0.699  2.515      3\n")
                .ToString();

            _italianNameFileText = new StringBuilder()
                .Append("ROSSI          1,012  1,012      1\n")
                .Append("BIANCHI        0,567  1,478      2\n")
                .Append("VERDI          0,111  2,345      3\n")
                .ToString();
        }

        [Test]
        public void ParseShouldReturnTheCorrectCountOfNames()
        {
            var parser = new NameFileParser();

            using (var nameFileStream = new MemoryStream(Encoding.ASCII.GetBytes(_nameFileText)))
            {
                var names = parser.Parse(nameFileStream);
                names.Count().ShouldBe(3);
            }
        }

        [Test]
        public void ParseShouldReadNamesCorrectly()
        {
            var parser = new NameFileParser();

            using (var nameFileStream = new MemoryStream(Encoding.ASCII.GetBytes(_nameFileText)))
            {
                var names = parser.Parse(nameFileStream);
                var expectedNames = new[] { "SMITH", "JOHNSON", "WILLIAMS" };
                names
                    .Select(n => n.Name)
                    .ToArray()
                    .ShouldBe(expectedNames);
            }
        }

        [Test]
        public void ParseShouldReadFrequencyCorrectly()
        {
            var parser = new NameFileParser();

            using (var nameFileStream = new MemoryStream(Encoding.ASCII.GetBytes(_nameFileText)))
            {
                var names = parser.Parse(nameFileStream);

                var expectedFrequencies = new short[] { 1006, 810, 699 };
                names
                    .Select(n => n.Frequency)
                    .ToArray()
                    .ShouldBe(expectedFrequencies);
            }
        }

        [Test]
        public void ParseShouldReadFrequencyCorrectlyUsingSpecificCulture()
        {
            var parser = new NameFileParser(CultureInfo.GetCultureInfo("it"));

            using (var nameFileStream = new MemoryStream(Encoding.ASCII.GetBytes(_italianNameFileText)))
            {
                var names = parser.Parse(nameFileStream);

                var expectedFrequencies = new short[] { 1012, 567, 111 };
                names
                    .Select(n => n.Frequency)
                    .ToArray()
                    .ShouldBe(expectedFrequencies);
            }
        }
    }
}
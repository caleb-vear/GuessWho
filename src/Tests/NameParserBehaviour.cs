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
        MemoryStream _nameFileStream;

        [SetUp]
        public void Given()
        {
            var nameFileText = new StringBuilder()
                .Append("SMITH          1.006  1.006      1\n")
                .Append("JOHNSON        0.810  1.816      2\n")
                .Append("WILLIAMS       0.699  2.515      3\n")
                .ToString();

            _nameFileStream = new MemoryStream(Encoding.ASCII.GetBytes(nameFileText));
        }

        [Test]
        public void ParseShouldReturnTheCorrectCountOfNames()
        {
            var parser = new NameFileParser();

            var names = parser.Parse(_nameFileStream);
            names.Count().ShouldBe(3);
        }

        [Test]
        public void ParseShouldReadNamesCorrectly()
        {
            var parser = new NameFileParser();
            var names = parser.Parse(_nameFileStream);

            var expectedNames = new[] { "SMITH", "JOHNSON", "WILLIAMS" };
            names
                .Select(n => n.Name)
                .ToArray()
                .ShouldBe(expectedNames);
        }

        [Test]
        public void ParseShouldReadFrequencyCorrectly()
        {
            var parser = new NameFileParser();
            var names = parser.Parse(_nameFileStream);

            var expectedFrequencies = new short[] { 1006, 810, 699 };

            names
                .Select(n => n.Frequency)
                .ToArray()
                .ShouldBe(expectedFrequencies);
        }
    }
}
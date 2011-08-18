using System.IO;
using NUnit.Framework;
using Shouldly;

namespace GuessWho.Tests
{
    [TestFixture]
    public class CensusNameDataFileSerializerBehaviour
    {
        CensusNameData[] _nameData;

        [SetUp]
        public void Given()
        {
            _nameData = new[]
                            {
                                new CensusNameData {Name = "SMITH", Frequency = 1006},
                                new CensusNameData {Name = "JOHNSON", Frequency = 810},
                                new CensusNameData {Name = "WILLIAMS", Frequency = 699},
                            };
        }

        [Test]
        public void ShouldBeAbleToRoudTrip()
        {
            var stream = new MemoryStream();
            var serializer = new CensusNameDataFileSerializer();

            serializer.Serialize(stream, _nameData);

            stream.Position = 0;
            var loadedNames = serializer.Deserialize(stream);

            loadedNames.ShouldBe(_nameData);
        }
    }
}
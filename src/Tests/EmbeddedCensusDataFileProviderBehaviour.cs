using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace GuessWho.Tests
{
    [TestFixture]
    public class EmbeddedCensusDataFileProviderBehaviour
    {
        [Test]
        public void ShouldFindEmbededFiles()
        {
            var subject = new EmbededCensusDataFileProvider(GetType().Assembly);

            var censusFiles = subject.GetCensusFiles().ToArray();

            censusFiles.Count().ShouldBe(2);
            censusFiles.ShouldContain(file => file.Filename.EndsWith("dist.top1000.male.first.cnd"));
            censusFiles.ShouldContain(file => file.Filename.EndsWith("dist.top1000.female.first.cnd"));
        }

        [Test]
        public void ShouldDeserializeNames()
        {
            var subject = new EmbededCensusDataFileProvider(GetType().Assembly);

            var censusFiles = subject.GetCensusFiles().ToArray();

            foreach (var censusDataFile in censusFiles)
                censusDataFile.NameData.Count().ShouldBe(1000);
        }
    }
}
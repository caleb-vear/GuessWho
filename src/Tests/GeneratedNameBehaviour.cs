using NUnit.Framework;
using Shouldly;

namespace GuessWho.Tests
{
    [TestFixture]
    public class GeneratedNameBehaviour
    {
        [Test]
        public void ShouldReturnNamesInCorrectCase()
        {
            var names = new[] {"JOHN", "joe", "James"};

            var generatedName = new GeneratedName(names, Gender.Male);

            generatedName.AllNames.ShouldBe(new [] {"John", "Joe", "James"});
        }
    }
}
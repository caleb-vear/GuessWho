using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace GuessWho.Tests
{
    [TestFixture]
    public class RandomTableBehaviour
    {
        [Test]
        public void ShouldReturnItemsWithTheFrequencySpecified()
        {
            var randomIncrement = 0;
            var randomTable = new RandomTable<int>(maxValue => randomIncrement++ % maxValue);

            randomTable.Add(5, 7);
            randomTable.Add(3, 2);
            randomTable.Add(7, 1);

            var results = randomTable
                .Sequence
                .Take(10)
                .ToArray();

            results.Count(r => r == 5).ShouldBe(7);
            results.Count(r => r == 3).ShouldBe(2);
            results.Count(r => r == 7).ShouldBe(1);
        }
    }
}
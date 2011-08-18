using System.Collections.Generic;

namespace GuessWho
{
    public static class RandomNameExtensions
    {
        public static IEnumerable<GeneratedName> Names(this NameGenerator random)
        {
            while (true)
                yield return random.NextName();
        }

        public static IEnumerable<GeneratedName> MaleNames(this NameGenerator random)
        {
            while (true)
                yield return random.NextMaleName();
        }

        public static IEnumerable<GeneratedName> FemaleNames(this NameGenerator random)
        {
            while (true)
                yield return random.NextFemaleName();
        }
    }
}
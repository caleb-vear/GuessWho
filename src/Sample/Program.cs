using System;
using System.Linq;
using GuessWho;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var randomName = new NameGenerator();

            const int takeCount = 5;

            Console.WriteLine("First {0} male names", takeCount);
            foreach (var name in randomName.MaleNames().Take(takeCount))
                Console.WriteLine(name);

            Console.WriteLine();

            Console.WriteLine("First {0} female names", takeCount);
            foreach (var name in randomName.FemaleNames().Take(takeCount))
                Console.WriteLine(name);

            Console.WriteLine();

            Console.WriteLine("First {0} all names", takeCount);
            foreach (var name in randomName.Names().Take(takeCount))
                Console.WriteLine(name);

            Console.ReadLine();
        }
    }
}

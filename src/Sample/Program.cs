using System;
using System.Linq;

namespace GuessWho.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var randomName = new NameGenerator();

            Console.WriteLine("First 10 male names");
            foreach (var name in randomName.MaleNames().Take(10))
                Console.WriteLine(name);

            Console.WriteLine();

            Console.WriteLine("First 10 female names");
            foreach (var name in randomName.FemaleNames().Take(10))
                Console.WriteLine(name);

            Console.WriteLine();

            Console.WriteLine("First 10 all names");
            foreach (var name in randomName.Names().Take(10))
                Console.WriteLine(name);

            Console.ReadLine();
        }
    }
}

Guess Who
=========

Guess who is a library for generating realistic sample identity information.  Version 1 is limited to just names.
Contributors who would like to assist in adding other types of identity information are welcome.

Some ideas for future versions

* Realistic addresses, configurable to different countries
* Realistic phone numbers
* Other personal details such as date of birth

Where are the names from?
-------------------------

The list of names and their relative frequency was taken from the US census data.
This information is publicly available [here](http://www.census.gov/genealogy/names/names_files.html).
The frequency data of the names to produce the names is used to produce more of the common names and less of the more obscure ones.

These name files come embedded in the assembly, but if you would like to you can implemented your own
custom _ICensusDataFileProvider_ to load them from some other location.

How can I get it?
-----------------

GuessWho is available on nuget.  Two version have been published 
[GuessWho](http://nuget.org/List/Packages/GuessWho) and 
[GuessWho.Top1000](http://nuget.org/List/Packages/GuessWho.Top1000).

GuessWho.Top1000 is a cut down version that only includes 1,000 of each type of name.
This saves quite a lot of space as the full version assembly ways in at 919KB compared to 45KB for the cut down version.

If you're a [Mercurial](http://mercurial.selenic.com/) users you might like head over to the project's 
[bitbucket repository](https://bitbucket.org/calebvear/guesswho) if you're already at the bitbucket repository, but looking
for a git one head over to the [github repository](https://github.com/caleb-vear/GuessWho).

How do I use it?
----------------

First of all create a new instance of NameGenerator class.

```c#

	var randomName = new NameGenerator();
	
```

You can then get new names by using any of the following methods.

```c#

	var boysName = randomName.NextMaleName();
	var girlsName = randomName.NextFemaleName();
	var whoKnowsName = randomName.NextName();
	
```

If you use the _NextName()_ method you can find out what gender the name is using the _Gender_ property.

```c#

	if (whoKnowsName.Gender == Gender.Male)
		Console.WriteLine("It's a boy!");
	else
		Console.WriteLine("It's a girl!");
		
```

For convenience there are also extension methods which allow you to _IEnumerable<GeneratedName>'s_ of names.

```c#

	const int takeCount = 2;

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
		
```

Sample output
-------------

> First 2 male names  
>     Steve Ashcraft  
>     Christopher Timothy Bischoff
>
> First 2 female names  
>     Denise Jan Dellinger  
>     Brigida Carpenter
>
> First 2 all names  
>     Ivan Brent Duffey  
>     Christy Mildred Clark

Advanced configuration
----------------------

If you would like a little bit more control over how names are generated you can use the RandomNameConfiguration class.

```c#
	
	var configuration = new RandomNameConfiguration()
		.Seed(1337) // Specify a custom seed so you can generate repeatable results
		.MaximumGivenNames(10) // Hey some people have more names than others the default is 3
		.CensusDataFileProvider(new CustomCensusDataFileProvider()) // Determine how the name files are loaded
		.NameFileTypeConvention(new CustomConvention()); // Control which files are treated as male or female

	var randomName = new NameGenerator(configuration);
	
```
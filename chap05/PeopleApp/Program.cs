using System.Reflection.Metadata.Ecma335;
using Packt.Shared;

ConfigureConsole(); // Sets current culture to "en-US"
// Alternatives:
// ConfigureConsole(useComputerCulture: true); // Use your culture.
// ConfigureConsole(culture: "fr-FR"); // Use French culture.

Person bob = new(); // C#9 or later
bob.Name = "Bob Smith";

bob.Born = new DateTimeOffset(
    year: 1965, month: 12, day: 22,
    hour: 16, minute: 28, second: 0,
    offset: TimeSpan.FromHours(-5));  //US Eastern Standard Time
// WriteLine(bob);  Implicit call to ToString()  WriteLine(bob.ToString()) does the same thing

WriteLine(format: "{0} was born on {1:D}.", arg0: bob.Name, arg1: bob.Born);

/*

The format code for arg1 is one of the standard date and time formats. D means
a long date format and d would mean a short date format. You can learn more
about standard date and time format codes at the following link: https://learn.
microsoft.com/en-us/dotnet/standard/base-types/standard-date-andtime-format-strings.

*/
bob.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;
WriteLine(format: "{0}'s favorite wonder is {1}. It's integer is {2}.\n\n",
    arg0 : bob.Name, arg1: bob.FavoriteAncientWonder, arg2: (int)bob.FavoriteAncientWonder);

bob.BucketList =
    WondersOfTheAncientWorld.HangingGardensOfBabylon | WondersOfTheAncientWorld.MausoleumOfHalicarnassus;
    // bob.BucketList = (WondersOfTheAncientWorld)18
    
WriteLine($"{bob.Name}'s bucket list is {bob.BucketList}\n\n");

// Works with all verisons of C#
Person alfred = new Person();
alfred.Name = "Alfred";
bob.Children.Add(alfred);

// Works with C# 3 and later
bob.Children.Add(new Person { Name = "Bella" });

// Works with C# 9 and later
bob.Children.Add(new() { Name = "Zoe" });

WriteLine($"{bob.Name} has {bob.Children.Count} children:");
for (int childIndex = 0; childIndex < bob.Children.Count; childIndex++)
{
    WriteLine($"> {bob.Children[childIndex].Name}");
}
WriteLine("\n");

Person alice = new()
{
    Name = "Alice Jones",
    Born = new(1998, 3, 7, 16, 28, 0, TimeSpan.Zero)
};

WriteLine(format: "{0} was born on {1:D}.", arg0: alice.Name, arg1: alice.Born); // short date format
using System.Net;
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
    
WriteLine($"{bob.Name}'s bucket list is {bob.BucketList}");
WriteLine($"{bob.Name} is a {Person.Species}.");
WriteLine($"{bob.Name} was born on {bob.HomePlanet}.\n");
bob.WriteToConsole();
WriteLine(bob.GetOrigin());
WriteLine(bob.SayHello());
WriteLine(bob.SayHelloTo("Elvis"));
WriteLine(bob.OptionalParameters(3));
WriteLine(bob.OptionalParameters(3, "Jump!", 98.5));
WriteLine(bob.OptionalParameters(3, number:  52.7, command:  "Hide!"));
WriteLine(bob.OptionalParameters(3,"Poke!", active:  false));
WriteLine();


// Works with all versions of C#
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

WriteLine(format: "{0} was born on {1:D}.\n\n", arg0: alice.Name, arg1: alice.Born); // short date format

#region Using the BankAccount class 
BankAccount.InterestRate = .012M; // stores a shared value in static field

BankAccount jonesAccount = new();
jonesAccount.AccountName = "Mrs. Jones";
jonesAccount.Balance = 2400M;
WriteLine(format:  "{0} earned {1:C} interest.", // {C} uses currency format
    arg0:  jonesAccount.AccountName,
    arg1:  jonesAccount.Balance * BankAccount.InterestRate);

BankAccount gerrierAccount = new();
gerrierAccount.AccountName = "Ms. Gerrier";
gerrierAccount.Balance = 98;
WriteLine(format:  "{0} earned {1:C} interest.\n",
    arg0:  gerrierAccount.AccountName,
    arg1:  gerrierAccount.Balance * BankAccount.InterestRate);
#endregion

#region Using the Book class
/*
Book book = new()
{
    Isbn = "978-1803237800",
    Title = "C# 12 and .NET 8 - Modern Cross-Platform Development Fundamentals"
};
*/
Book book = new(isbn: "978-1803237800", 
    title: "C# 12 and .NET 8 - Modern Cross-Platform Development Fundamentals")
{
    Author = "Mark J Price",
    PageCount = 821
};
WriteLine("{0}: {1} written by {2} has {3:N0} pages.\n",
    book.Isbn,
    book.Title,
    book.Author,
    book.PageCount);
#endregion

#region Using the Person class with the constructor
Person blankPerson = new();

WriteLine(format: "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.\n",
    blankPerson.Name,
    blankPerson.HomePlanet,
    blankPerson.Instantiated);

Person gunny = new(initialName: "Gunny", homePlanet: "Mars");

WriteLine(format: "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.\n",
    gunny.Name,
    gunny.HomePlanet,
    gunny.Instantiated);
#endregion

#region Using the PassingParameters method
int a = 10;
int b = 20;
int c = 30;
int d = 40;

WriteLine($"a = {a}, b = {b}, c = {c}, d = {d}.\n");

bob.PassingParameters(a, b, ref c, out d);
WriteLine($"After: a = {a}, b = {b}, c = {c}, d = {d}.\n");

int e = 50;
int f = 60;
int g = 70;

WriteLine($"Before: e={e}, f={f}, g={g}, h doesn't exist yet!");

// Simplified C# 7 or later syntax for the out parameter.
bob.PassingParameters(e, f, ref g, out int h);
WriteLine($"After: e = {e}, f = {f}, g = {g}, h = {h}\n");

(string, int) fruit = bob.GetFruit();
WriteLine($"{fruit.Item1}, {fruit.Item2} there are.");
WriteLine($"There are {fruit.Item2} {fruit.Item1}.\n");
#endregion
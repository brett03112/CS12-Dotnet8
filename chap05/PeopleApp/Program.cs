using Fruit = (string Name, int Number); // Aliasing a tuple
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
bob.FavoriteAncientWonder = (WondersOfTheAncientWorld)32;
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

// Without an aliased tuple type
var fruitName = bob.GetNamedFruit(); 
WriteLine($"There are {fruitName.Number} {fruitName.Name}.\n");

// With an aliased tuple type
Fruit namedFruit = bob.GetNamedFruit();
WriteLine($"There are {namedFruit.Number} {namedFruit.Name}.\n");

// Deconstructed tuple
(string fruitName1, int fruitNumber) = bob.GetFruit();
WriteLine($"Deconstructed Tuple: {fruitName1}, {fruitNumber}.");

var thing1 = ("Neville", 4);
WriteLine($"{thing1.Item1} has {thing1.Item2} children.");

var thing2 = (bob.Name, bob.Children.Count);
WriteLine($"{thing2.Item1} has {thing2.Item2} children.\n");

var (name1, dob1) = bob; // Implicitly calls the Deconstruct method
WriteLine($"Deconstructed person: {name1}, {dob1}.");

var (name2, dob2, fav2) = bob;
WriteLine($"Deconstructed person: {name2}, {dob2}, {fav2}.\n");

// Using the Factorial function
int number = 8;
try
{
    WriteLine($"{number}! is {Person.Factorial(number):N0}.\n");
}
catch (Exception ex)
{
    WriteLine($"{ex.GetType()} says: {ex.Message} number was {number}.\n");
}

Person sam = new()
{
    Name = "Sam",
    Born = new(1969, 6, 25, 0, 0, 0, TimeSpan.Zero)
};

WriteLine(sam.Origin);
WriteLine(sam.Greeting);
WriteLine(sam.Age);
WriteLine();

sam.Children.Add(new() { Name = "Charlie", Born = new(2010,3,18,0,0,0,TimeSpan.Zero) });
sam.Children.Add(new() { Name = "Ella", Born = new(2020,12,24,0,0,0,TimeSpan.Zero) });

// Get using Children List
WriteLine($"Sam's first child is {sam.Children[0].Name}.");
WriteLine($"Sam's second child is {sam.Children[1].Name}.\n");

// Get using the int indexer
WriteLine($"Sam's first child is {sam[0].Name}.");
WriteLine($"Sam's second child is {sam[1].Name}.\n");

// Get using the string indexer
WriteLine($"Sam's child named Ella is {sam["Ella"].Age} years old.\n");

sam.FavoriteIceCream = "Chocolate Fudge";
WriteLine($"Sam's favorite ice-cream flavor is {sam.FavoriteIceCream}.\n");
string color = "Red";

try
{
    sam.FavoritePrimaryColor = color;
    WriteLine($"Sam's favorite primary color is {sam.FavoritePrimaryColor}.\n");
}
catch (Exception ex)
{
    WriteLine($"Tried to set {0} to '{1}':  {2}", 
        nameof(sam.FavoritePrimaryColor), color, ex.Message);
}

// An array containing a mix of passenger types
Passenger[] passengers = {
    new FirstClassPassenger {AirMiles = 1_419, Name = "Suman"},
    new FirstClassPassenger { AirMiles = 16_562, Name = "Lucy"},
    new BusinessClassPassenger {Name = "Janice"},
    new CoachClassPassenger {CarryOnKG = 25.7, Name = "Dave"},
    new CoachClassPassenger {CarryOnKG = 0, Name = "Amit"},
};

foreach (Passenger passenger in passengers)
{
    decimal flightCost = passenger switch
    {
        /*  C# 8 Syntax
        FirstClassPassenger p when p.AirMiles > 35_000 => 1_500M,
        FirstClassPassenger p when p.AirMiles > 15_000 => 1_700M,
        FirstClassPassenger _                          => 2_000M,
        */
        // C# 9 or later syntax
        FirstClassPassenger p => p.AirMiles switch
        {
            > 35_000 => 1_500M,
            > 15_000 => 1_700M,
            _        => 2_000M
        },
        BusinessClassPassenger                         => 1_000M,
        CoachClassPassenger p when p.CarryOnKG < 10.0  => 500M,
        CoachClassPassenger                            => 650M,
        _                                              => 850M
    };
    WriteLine($"Flight costs {flightCost:C} for {passenger}");
}

ImmutablePerson jeff = new()
{
    FirstName = "Jeff",
    LastName = "Winger"
};
//jeff.FirstName = "Jess";
WriteLine();
ImmutableVehicle car = new()
{
    Brand = "Mazda MX-5 RF",
    Color = "Soul Red Crystal Metallic",
    Wheels = 4
};
ImmutableVehicle repaintedCar = car
    with { Color = "Polymetal Grey Metallic"};

WriteLine($"Original car color was {car.Color}.");
WriteLine($"New car color is {repaintedCar.Color}.\n");

AnimalClass ac1 = new() {Name = "Rex"};
AnimalClass ac2 = new() {Name = "Rex"};

WriteLine($"ac1 == ac2: {ac1 == ac2}.\n"); // False due to reference equality

AnimalRecord ar1 =  new() {Name = "Rex"};
AnimalRecord ar2 =  new() {Name = "Rex"};

WriteLine($"ar1 == ar2: {ar1 == ar2}.\n"); // True due to value equality

ImmutableAnimal oscar = new("Oscar", "Labrador");
var (who, what) = oscar; // Calls  the Deconstruct method
WriteLine($"{who} is a {what}.\n");

HeadSet vp = new("Apple", "Vision Pro");
WriteLine($"{vp.ProductName} is made by {vp.Manufacturer}.\n");

HeadSet holo = new();
WriteLine($"{holo.ProductName} is made by {holo.Manufacturer}.\n");

HeadSet mq = new() { Manufacturer = "Meta", ProductName = "Quest 3"};
WriteLine($"{mq.ProductName} is made by {mq.Manufacturer}.\n");
#endregion
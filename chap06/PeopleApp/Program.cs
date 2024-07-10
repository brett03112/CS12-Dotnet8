using Packt.Shared;

/*
Categories of custom types and their capabilities are summarized in the following table:

Type                    Instantiation   Inheritance     Equality    Memory
___________________________________________________________________________
class                   Yes             Single          Reference   Heap
sealed class            Yes             None            Reference   Heap
abstract class          No              Single          Reference   Heap
record or record class  Yes             Single          Value       Heap
struct or record struct Yes             None            Value       Stack
interface               No              Multiple        Reference   Heap

_____________________________________________________________________________
• A sealed class does not support inheritance.

• An abstract class does not allow instantiation with new.

• A record class uses value equality instead of reference equality.

• A struct or record struct does not support inheritance, it uses value equality instead of
    reference equality, and its state is stored in stack memory.

• An interface does not allow instantiation with new and supports multiple inheritance.

*/

Person harry = new()
{
    Name = "Harry",
    Born = new(year: 2001, month: 3, day: 25,
        hour: 0, minute: 0, second: 0, offset: TimeSpan.Zero)
};

harry.WriteToConsole();

// Implementing functionality using methods
Person lamech = new() { Name = "Lamech" };
Person adah = new() { Name = "Adah" };
Person zillah = new() { Name = "Zillah" };

// Call the instance method to marry Lamech and Adah
lamech.Marry(adah);


// Call the static method to marry Lamech and Zillah
//Person.Marry(lamech, zillah);

if (lamech + zillah)
{
    WriteLine($"{lamech.Name} and {zillah.Name} successfully got married.");
}

lamech.OutputSpouses();
adah.OutputSpouses();
zillah.OutputSpouses();

// Call the instance method to make a baby
Person baby1 = lamech.ProcreateWith(adah);
baby1.Name = "Jabal";
WriteLine($"{baby1.Name} was born on {baby1.Born}");

// Call the static method to make a baby
Person baby2 = Person.Procreate(zillah, lamech);
baby2.Name = "Tubalcain";

// Use the * operator to "multiply"
Person baby3 = lamech * adah;
baby3.Name = "Jubal";

Person baby4 = zillah * lamech;
baby4.Name = "Naamah";

adah.WriteChildrenToConsole();
zillah.WriteChildrenToConsole();
lamech.WriteChildrenToConsole();

for (int i = 0; i < lamech.Children.Count; i++)
{
    WriteLine(format: " {0}'s child #{1} is named \"{2}\".", arg0: lamech.Name, arg1: i, arg2: lamech.Children[i].Name);
}
WriteLine();
WriteLine();

#region Generics
// Non-generic lookup collection
// Is slow and has bugs
System.Collections.Hashtable lookupObject = new();
lookupObject.Add(key: 1, value: "Alpha");
lookupObject.Add(key: 2, value: "Beta");
lookupObject.Add(key: 3, value: "Gamma");
lookupObject.Add(key: harry, value: "Delta");

int key = 2; // Look up the value that has 2 as its key

WriteLine(format: "Key{0} has value: {1}", arg0: key, arg1: lookupObject[key]);

// Look up the value that has harry as its key
WriteLine(format: "Key{0} has value: {1}\n", arg0: harry, arg1: lookupObject[harry]);

// Define a generic lookup collection
Dictionary<int, string> lookupIntString = new();
lookupIntString.Add(key: 1, value: "Alpha");
lookupIntString.Add(key: 2, value: "Beta");
lookupIntString.Add(key: 3, value: "Gamma");
lookupIntString.Add(key: 4, value: "Delta");

key = 3;
WriteLine(format: "Key {0} has a value: {1}.\n", arg0: key, arg1: lookupIntString[key]);


#endregion

#region Using Delegates
// Assign the method to the Shout delegate
harry.Shout += Harry_Shout; // must use '+' if "event" is stated before the EventHandler in Person.cs
harry.Shout += Harry_Shout_2;

// Call the Poke method that eventually raises the Shout event.
harry.Poke();
harry.Poke();
harry.Poke();
harry.Poke();
WriteLine();
#endregion

#region Interfaces
Person?[] people = 
{
    null,
    new() { Name = "Simon"},
    new() { Name = "Jenny"},
    new() { Name = "Adam" },
    new() { Name = null },
    new() { Name = "Richard" },
};

OutputPeopleNames(people, "Initial list of people:");

//Array.Sort(people);

/*
This time, when we sort the people array, we explicitly ask the sorting algorithm to use the
PersonComparer type instead so that the people are sorted with the shortest names first, like Adam,
and the longest names last, like Richard, and when the lengths of two or more names are equal they
are sorted alphabetically, like Jenny and Simon.*/

Array.Sort(people, new PersonComparer());


OutputPeopleNames(people,
    "After sorting using PersonComparer's IComparer implementation: ");


WriteLine();
#endregion

#region Boxing and Unboxing

int a = 3;
int b = 3;
WriteLine($"a:  {a}, b: {b}");
WriteLine($"a == b: {a == b}\n");


Person p1 = new() { Name = "Kevin" };
Person p2 = new() { Name = "Kevin" };
WriteLine($"p1: {p1}, p2: {p2}");
WriteLine($"p1.Name: {p1.Name}, p2.Name: {p2.Name}");
WriteLine($"p1 == p2: {p1 == p2}\n"); // false due to not pointing to the same object

Person p3 = p1;
WriteLine($"p3: {p3}");
WriteLine($"p3.Name: {p3.Name}");
WriteLine($"p1 == p3: {p1 == p3}\n"); // true due to pointing to the same object

// String is the only class referece type implemented to act like a value type for equality comparisons
WriteLine($"p1.Name : {p1.Name}, p2.Name: {p2.Name}");
WriteLine($"p1.Name == p2.Name: {p1.Name == p2.Name}\n");

/*
You can do the same as string with your classes to override the equality operator == to return true,
even if the two variables are not referencing the same object (the same memory address on the heap)
but, instead, their fields have the same values. However, that is beyond the scope of this book.
*/

#endregion

#region Struct types

DisplacementVector dv1 = new(3, 5);
DisplacementVector dv2 = new(-2, 7);

DisplacementVector dv3 = dv1 + dv2;
WriteLine($"({dv1.X}, {dv1.Y}) + ({dv2.X}, {dv2.Y}) = ({dv3.X}, {dv3.Y})\n");

DisplacementVector dv4 = new();
WriteLine($"({dv4.X}, {dv4.Y})\n");

DisplacementVector dv5 = new(3, 5);
WriteLine($"dv1.Equals(dv5): {dv1.Equals(dv5)}");
WriteLine($"dv1 == dv5: {dv1 == dv5}\n"); // cannot use '==' with struct types

#endregion

#region Inheritance

Employee john = new()
{
    Name = "John Jones",
    Born = new(year: 1998, month: 7, day: 28, hour: 0, minute: 0, second: 0, offset: TimeSpan.Zero)
};
john.WriteToConsole();

WriteLine(john.ToString());

john.EmployeeCode = "JJ001";
john.HireDate = new(year: 2014, month: 11, day: 23);
WriteLine($"{john.Name} was hired on {john.HireDate:yyyy-MM-dd}.\n");

Employee aliceInEmployee = new() { Name = "Alice", EmployeeCode = "AA123" };

Person aliceInPerson = aliceInEmployee;
aliceInEmployee.WriteToConsole();
aliceInPerson.WriteToConsole();
WriteLine(aliceInEmployee.ToString());
WriteLine(aliceInPerson.ToString());
/*
Variable type   Member modifier     Method executed     In class
______________________________________________________________________
Person                              WriteToConsole      Person
Employee        new                 WriteToConsole      Employee
Person          virtual             ToString            Employee
Employee        override            ToString            Employee
*/

//Employee explicitAlice = aliceInPerson; will give an error


// To check if an object is an instance of a class, use the 'is' operator
// 
if (aliceInPerson is Employee explicitAlice)
{
    WriteLine($"{nameof(aliceInPerson)} is an Employee.");
    
    //Employee explicitAlice = (Employee)aliceInPerson; // explicit cast
}

Employee? aliceAsEmployee = aliceInPerson as Employee;

if (aliceAsEmployee is not null)
{
    WriteLine($"{nameof(aliceAsEmployee)} is an Employee.\n");
}

try
{
    john.TimeTravel(when: new(1999, 12, 31));
    john.TimeTravel(when: new(1950, 12, 25));
}
catch (PersonException ex)
{
    WriteLine(ex.Message);
}
#endregion

#region using Regular Expressions and extension methods

string email1 = "pamela@test.com";
string email2 = "ian&test.com";

WriteLine("{0} is a valid email address: {1}", arg0: email1, arg1: StringExtensions.IsValidEmail(email1));
WriteLine("{0} is a valid email address: {1}", arg0: email2, arg1: StringExtensions.IsValidEmail(email2));
WriteLine();

//using an extension method.  Uses a simpler syntax
WriteLine("{0} is a valid email address: {1}", arg0: email1, arg1: email1.IsValidEmail());
WriteLine("{0} is a valid email address: {1}", arg0: email2, arg1: email2.IsValidEmail());
#endregion

#region Mutable and immutable record types
C1 c1 = new() { Name = "Bob" }; // mutable
c1.Name = "Bill";

C2 c2 = new(Name: "Bob"); // immutable
//c2.Name = "Bill";

S1 s1 = new() { Name = "Bob" }; // mutable
s1.Name = "Bill";

S2 s2 = new() { Name = "Bob" }; // mutable
s2.Name = "Bill";

S3 s3 = new() { Name = "Bob" }; // immutable
//s3.Name = "Bill";

/* Reviewing illustrative code from Chapter 6:
________________________________________________________________________________________________________________________________________
• To simplify the code, I have left out access modifiers like private and public.

• Instead of normal brace formatting, to save vertical space I have put all the method implementation in one statement, for example:
void M1() { // implementation  }

• Using “I” as a prefix for interfaces is a convention, not a requirement. It is useful to highlight

interfaces using this prefix, since only interfaces support multiple inheritance.
___________________________________________________________________________________________________________________________________________

// These are both "classic" interfaces in that they are pure contracts.
// They have no functionality, just the signatures of members that
// must be implemented.

interface IAlpha
{
    // A method that must be implemented in any type that implements
    // this interface.

    void M1();
}
interface IBeta
{
    void M2(); // Another method.
}

// A type (a struct in this case) implementing an interface.
// ": IAlpha" means Gamma promises to implement all members of IAlpha.

struct Gamma : IAlpha
{
    void M1() {  implementation  }
}
// A type (a class in this case) implementing two interfaces.

class Delta : IAlpha, IBeta
{
    void M1() { implementation  }
    void M2() { implementation  }
}

// A sub class inheriting from a base aka super class.
// ": Delta" means inherit all members from Delta.

class Episilon : Delta
{
    // This can be empty because this inherits M1 and M2 from Delta.
    // You could also add new members here.
}
// A class with one inheritable method and one abstract method
// that must be implemented in sub classes. A class with at least
// one abstract member must be decorated with the abstract keyword
// to prevent instantiation.

abstract class Zeta
{
    // An implemented method would be inherited.
    void M3() { implementation }
    // A method that must be implemented in any type that inherits
    // this abstract class.

    abstract void M4();
}

// A class inheriting the M3 method from Zeta but it must provide
// an implementation for M4.

class Eta : Zeta
{
    void M4() { implementation }
}
// In C# 8 and later, interfaces can have default implementations
// as well as members that must be implemented.
// Requires: .NET Standard 2.1, .NET Core 3.0 or later.

interface ITheta
{
    void M3() { implementation }
    void M4();
}
// A class inheriting the default implementation from an interface
// and must provide an implementation for M4.

class Iota : ITheta
{
    void M4() { implementation }
}

*/

#endregion
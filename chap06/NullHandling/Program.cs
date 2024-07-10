using Packt.Shared; // to use Address

int thisCannotBeNull = 4;
//thisCannotBeNull = null; // this will throw a NullReferenceException cs0037 compiler error

WriteLine(thisCannotBeNull);

int? thisCanBeNull = null;

WriteLine(thisCanBeNull);

WriteLine(thisCanBeNull.GetValueOrDefault());

thisCanBeNull = 7;

WriteLine(thisCanBeNull);

WriteLine(thisCanBeNull.GetValueOrDefault());

// the actual type of int? is Nullable<int>
Nullable<int> thisCouldbeNull = null;
thisCouldbeNull = 9;

WriteLine(thisCouldbeNull);
/*
Initialism          Meaning                         Description
__________________________________________________________________________________________________________________________________
NRT                 Nullable Reference Type         A compiler feature introduced with C# 8 and enabled by
                                                    default in new projects with C# 10, which performs static
                                                    analysis of your code at design time and shows warnings
                                                    of potential misuse of null values for reference types.
___________________________________________________________________________________________________________________________________                                                    
NRE                 NullReferenceException          An exception thrown at runtime when dereferencing a
                                                    null value, aka accessing a variable or member on an
                                                    object that is null.
____________________________________________________________________________________________________________________________________
ANE                 ArgumentNullException           An exception thrown at runtime by a method, property,
                                                    or indexer invocation when an argument or value is null,
                                                    and when the business logic determines that it is not valid.

*/

/*
To enable or disable nullable references in projects use:
<PropertyGroup>
...
    <Nullable>disable</Nullable>(or enable)
</PropertyGroup>

or... at the file level

#nullable disable

#nullable enable

*/

/*
Other Compiler Warnings
_______________________________________________________________________

Code    Description
___________________________________________________________________________________________________
CS8600  Converting a null literal or a possible null value to a non-nullable type.
CS8601  A possible null reference assignment.
CS8602  A dereference of a possibly null reference.
CS8603  A possible null reference return.
CS8604  A possible null reference argument for a parameter.
CS8618  A non-nullable field ‘<field_name>' must contain a non-null value when exiting a
            constructor. Consider declaring the field as nullable.
CS8625  Cannot convert a null literal to a non-nullable reference type.
CS8655  The switch expression does not handle some null inputs (it is not exhaustive).
*/

Address address = new(city: "London")
{
    Building = null,
    Street = null!,
    Region = null!
};

WriteLine(address.Building?.Length);

if (address.Street is not null)
{
    WriteLine(address.Street.Length);// this will throw a NullReferenceException
}

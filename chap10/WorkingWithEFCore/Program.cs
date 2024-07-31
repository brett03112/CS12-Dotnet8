using Northwind.EntityModels;

#region Defining the Northwind database context class and querying the database


//using NorthwindDb db = new();
//WriteLine($"Provider: {db.Database.ProviderName}");
// Disposes the db context

ConfigureConsole();
WriteLine("");

//QueryingCategories();
WriteLine("");

//FilterIncludes();
WriteLine("");

#endregion

#region Using EFCore Conventions to define a @model 

/*
• The name of a table is assumed to match the name of a DbSet<T> property in the DbContext
class, for example, Products.

• The names of the columns are assumed to match the names of properties in the entity model
class, for example, ProductId.

• The string .NET type is assumed to be a nvarchar type in the database.

• The int .NET type is assumed to be an int type in the database.

• Conventions often aren’t enough to completely map the classes to the database objects. A simple way
of making your model smarter is to apply annotation attributes.
*/

/*
COMMON ATTRIBUTES RECOGNIZED BY EF CORE
______________________________________________________________________________________________
Attribute                               Description

[Required]                              Ensures the value is not null. In .NET 8, it has a
                                        DisallowAllDefaultValues parameter to prevent value types having
                                        their default value. For example, an int cannot be 0.

[StringLength(50)]                      Ensures the value is up to 50 characters in length.

[Column(TypeName =                      Specifies the column type and column name used in the table.
"money",
Name = "UnitPrice")]

[RegularExpression(expression)]         Ensures the value matches the specified regular expression.

[EmailAddress]                          Ensures the value contains one @ symbol, but not as
                                        the first or last character. It does not use a regular expression.

[Range(1, 10)]                          Ensures a double, int, or string value within a
                                        specified range. New in .NET 8 are the parameters
                                        MinimumIsExclusive and MaximumIsExclusive.

[Length(10, 20)]                        Ensures a string or collection is within a specified
                                        length range, for example, minimum 10 characters or
                                        items, maximum 20 characters or items.

[Base64String]                          Ensures the value is a well-formed Base64 string.

[AllowedValues]                         Ensures value is one of the items in the params array
                                        of objects. For example, "alpha", "beta", "gamma", or
                                        1, 2, 3.

[DeniedValues]                          Ensures value is not one of the items in the params
                                        array of objects. For example, "alpha", "beta",
                                        "gamma", or 1, 2, 3.
*/

#endregion

#region Querying EF Core models

//QueryingProducts();
WriteLine("");

#endregion

#region Logging EF Core models

/*
Good Practice: By default, EF Core logging will exclude any data that is sensitive. You
can include this data by calling the EnableSensitiveDataLogging method, especially
during development. You should disable it before deploying to production. You can also
call EnableDetailedErrors.
*/

//GettingOneProduct();
WriteLine("");

QueryingWithLike();
WriteLine("");

//GetRandomProduct();
WriteLine("");
#endregion

#region Loading and tracking patterns with EF Core
/*
There are three loading patterns that are commonly used with EF Core:
_______________________________________________________________________
• Eager loading: Load data early.

• Lazy loading: Load data automatically just before it is needed.

• Explicit loading: Load data manually
*/


#endregion
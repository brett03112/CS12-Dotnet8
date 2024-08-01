using Microsoft.VisualBasic;
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

//QueryingWithLike();
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
//LazyLoadingWithNoTracking();
WriteLine("");

#endregion

#region Modifying data with EF Core

/*
• C for Create
• R for Retrieve (or Read)
• U for Update
• D for Delete
*/

// add entities
/*
var resultAdd = AddProduct(categoryId: 6, ProductName: "Bob's Burgers", price: 500M, stock: 72);

if (resultAdd.affected == 1)
{
    WriteLine($"Add product successful with ID: {resultAdd.productId}.");
}

ListProducts(productIdsToHighlight: new[] { resultAdd.productId });
*/
/*
| 1   | Chai                                |   $18.00 |    39 |False |
| 2   | Chang                               |   $19.00 |    17 |False |
| 3   | Aniseed Syrup                       |   $10.00 |    13 |False |
| 4   | Chef Anton's Cajun Seasoning        |   $22.00 |    53 |False |
| 6   | Grandma's Boysenberry Spread        |   $25.00 |   120 |False |
| 7   | Uncle Bob's Organic Dried Pears     |   $30.00 |    15 |False |
| 8   | Northwoods Cranberry Sauce          |   $40.00 |     6 |False |
| 10  | Ikura                               |   $31.00 |    31 |False |
| 11  | Queso Cabrales                      |   $21.00 |    22 |False |
| 12  | Queso Manchego La Pastora           |   $38.00 |    86 |False |
| 13  | Konbu                               |    $6.00 |    24 |False |
| 14  | Tofu                                |   $23.25 |    35 |False |
| 15  | Genen Shouyu                        |   $15.50 |    39 |False |
| 16  | Pavlova                             |   $17.45 |    29 |False |
| 18  | Carnarvon Tigers                    |   $62.50 |    42 |False |
| 19  | Teatime Chocolate Biscuits          |    $9.20 |    25 |False |
| 20  | Sir Rodney's Marmalade              |   $81.00 |    40 |False |
| 21  | Sir Rodney's Scones                 |   $10.00 |     3 |False |
| 22  | Gustaf's Knäckebröd                 |   $21.00 |   104 |False |
| 23  | Tunnbröd                            |    $9.00 |    61 |False |
| 25  | NuNuCa Nuß-Nougat-Creme             |   $14.00 |    76 |False |
| 26  | Gumbär Gummibärchen                 |   $31.23 |    15 |False |
| 27  | Schoggi Schokolade                  |   $43.90 |    49 |False |
| 30  | Nord-Ost Matjeshering               |   $25.89 |    10 |False |
| 31  | Gorgonzola Telino                   |   $12.50 |     0 |False |
| 32  | Mascarpone Fabioli                  |   $32.00 |     9 |False |
| 33  | Geitost                             |    $2.50 |   112 |False |
| 34  | Sasquatch Ale                       |   $14.00 |   111 |False |
| 35  | Steeleye Stout                      |   $18.00 |    20 |False |
| 36  | Inlagd Sill                         |   $19.00 |   112 |False |
| 37  | Gravad lax                          |   $26.00 |    11 |False |
| 38  | Côte de Blaye                       |  $263.50 |    17 |False |
| 39  | Chartreuse verte                    |   $18.00 |    69 |False |
| 40  | Boston Crab Meat                    |   $18.40 |   123 |False |
| 41  | Jack's New England Clam Chowder     |    $9.65 |    85 |False |
| 43  | Ipoh Coffee                         |   $46.00 |    17 |False |
| 44  | Gula Malacca                        |   $19.45 |    27 |False |
| 45  | Rogede sild                         |    $9.50 |     5 |False |
| 46  | Spegesild                           |   $12.00 |    95 |False |
| 47  | Zaanse koeken                       |    $9.50 |    36 |False |
| 48  | Chocolade                           |   $12.75 |    15 |False |
| 49  | Maxilaku                            |   $20.00 |    10 |False |
| 50  | Valkoinen suklaa                    |   $16.25 |    65 |False |
| 51  | Manjimup Dried Apples               |   $53.00 |    20 |False |
| 52  | Filo Mix                            |    $7.00 |    38 |False |
| 54  | Tourtière                           |    $7.45 |    21 |False |
| 55  | Pâté chinois                        |   $24.00 |   115 |False |
| 56  | Gnocchi di nonna Alice              |   $38.00 |    21 |False |
| 57  | Ravioli Angelo                      |   $19.50 |    36 |False |
| 58  | Escargots de Bourgogne              |   $13.25 |    62 |False |
| 59  | Raclette Courdavault                |   $55.00 |    79 |False |
| 60  | Camembert Pierrot                   |   $34.00 |    19 |False |
| 61  | Sirop d'érable                      |   $28.50 |   113 |False |
| 62  | Tarte au sucre                      |   $49.30 |    17 |False |
| 63  | Vegie-spread                        |   $43.90 |    24 |False |
| 64  | Wimmers gute Semmelknödel           |   $33.25 |    22 |False |
| 65  | Louisiana Fiery Hot Pepper Sauce    |   $21.05 |    76 |False |
| 66  | Louisiana Hot Spiced Okra           |   $17.00 |     4 |False |
| 67  | Laughing Lumberjack Lager           |   $14.00 |    52 |False |
| 68  | Scottish Longbreads                 |   $12.50 |     6 |False |
| 69  | Gudbrandsdalsost                    |   $36.00 |    26 |False |
| 70  | Outback Lager                       |   $15.00 |    15 |False |
| 71  | Flotemysost                         |   $21.50 |    26 |False |
| 72  | Mozzarella di Giovanni              |   $34.80 |    14 |False |
| 73  | Röd Kaviar                          |   $15.00 |   101 |False |
| 74  | Longlife Tofu                       |   $10.00 |     4 |False |
| 75  | Rhönbräu Klosterbier                |    $7.75 |   125 |False |
| 76  | Lakkalikööri                        |   $18.00 |    57 |False |
| 77  | Original Frankfurter grüne Soße     |   $13.00 |    32 |False |
| 78  | Bob's Burgers                       |  $500.00 |    72 |False |
*/

/*
updating entities

var resultUpdate = IncreaseProductPrice(productNameStartsWith: "Bob", amount: 20M);

if (resultUpdate.affected == 1)
{
    WriteLine($"Increase price success for ID: {resultUpdate.productId}.");
}

ListProducts(productIdsToHighlight: new[] { resultUpdate.productId });
*/
/*
WriteLine("About to delete all products whose name starts with 'Bob'.");
Write("Press Enter to continue or any other key to exit:  ");
if (ReadKey(intercept: true).Key == ConsoleKey.Enter)
{
    int deleted = DeleteProducts(productNameStartsWith: "Bob");
    WriteLine($"{deleted} product(s) were deleted.");
}
else
{
    WriteLine("Delete was canceled.");
}
*/
/*
For example, to delete all products, call the ExecuteDelete or ExecuteDeleteAsync method on any
table, as shown in the following code:

    await db.Products.ExecuteDeleteAsync();

The preceding code would execute an SQL statement in the database, as shown in the following code:

    DELETE FROM Products

To delete all products that have a unit price greater than 50, use the following code:

    await db.Products
        .Where(product => product.UnitPrice > 50)
        .ExecuteDeleteAsync();

The preceding code would execute an SQL statement in the database, as shown in the following code:

    DELETE FROM Products p WHERE p.UnitPrice > 50

*/
/*
To update all products that are not discontinued to increase their unit price by 10% due to inflation,
use the following code:

    await db.Products
        .Where(product => !product.Discontinued)
        .ExecuteUpdateAsync(s => s.SetProperty(
            p => p.UnitPrice, // Selects the property to update.
            p => p.UnitPrice * 0.1)); // Sets the value to update it to.

*/
/*
var resultUpdateBetter = IncreaseProductPricesBetter(
    productNameStartsWith: "Bob", amount: 20M);

if (resultUpdateBetter.affected > 0)
{
    WriteLine("Increase product price successfull.");
}

ListProducts(productIdsToHighlight: resultUpdateBetter.productIds);
*/

WriteLine("About to delete all products whose name starts with 'Bob'.");
Write("Press Enter to continue or any other key to exit:  ");
if (ReadKey(intercept: true).Key == ConsoleKey.Enter)
{
    int deleted = DeleteProductsBetter(productNameStartsWith: "Bob");
    WriteLine($"{deleted} product(s) were deleted.");
}
else
{
    WriteLine("Delete was canceled.");
}
#endregion
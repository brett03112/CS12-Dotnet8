using System.ComponentModel.DataAnnotations; // To use [Required]
using System.ComponentModel.DataAnnotations.Schema; // To use [Column]


namespace Northwind.EntityModels;

public class Product
{
    public int ProductId { get; set; } // The primary key

    [Required]
    [StringLength(40)]
    public string ProductName { get; set; } = null!;

    // Property name is different from the column name
    [Column("UnitPrice", TypeName = "money")]
    public decimal? Cost { get; set; }

    [Column("UnitsInStock")]
    public short? Stock { get; set; }

    public bool Discontinued { get; set; }

    // These two properties define the foreign key relationship
    // to the Category table
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    /*
    • The Product class will be used to represent a row in the Products table, which has
    ten columns.

    • You do not need to include all columns from a table as properties of a class. We will only
    map six properties: ProductId, ProductName, UnitPrice, UnitsInStock, Discontinued,
    and CategoryId.

    • Columns that are not mapped to properties cannot be read or set using the class instances. 
    If you use the class to create a new object, then the new row in the table will
    have NULL or some other default value for the unmapped column values in that row.
    You must make sure that those missing columns are optional or have default values set
    by the database or an exception will be thrown at runtime. In this scenario, the rows
    already have data values and I have decided that I do not need to read those values in
    this application.

    • We can rename a column by defining a property with a different name, like Cost, and
    then decorating the property with the [Column] attribute and specifying its column
    name, like UnitPrice.
    
    • The final property, CategoryId, is associated with a Category property that will be
    used to map each product to its parent category.

    */
}
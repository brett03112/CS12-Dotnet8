using System.ComponentModel.DataAnnotations.Schema; // To use [Column]

namespace Northwind.EntityModels;

public class Category
{
    // These properties map to columns in the database
    public int CategoryId { get; set; } // The primary key
    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "ntext")]
    public string? Description { get; set; }

    // Defines a navigation property for related rows
    public virtual ICollection<Product> Products{ get; set; } = new HashSet<Product>();
        // To enable developers to add products to a Category, we must
        // initialize the navigation property to an empty collection.
        // This also avoids an exception if we get a member like Count.
    
    /*
        • The Category class will be in the Northwind.EntityModels namespace.

        • The CategoryId property follows the primary key naming convention, so it will be
        mapped to a column marked as the primary key with an index.

        • The CategoryName property maps to a column that does not allow database NULL values
        so it is a non-nullable string, and to disable nullability warnings, we have assigned the
        null-forgiving operator.

        • The Description property maps to a column with the ntext data type instead of the
        default mapping for string values to nvarchar.
        
        • We initialize the collection of Product objects to a new, empty HashSet. A hash set is
        more efficient than a list because it is unordered. If you do not initialize Products, then
        it will be null and if you try to get its Count then you will get an exception.
    */
}

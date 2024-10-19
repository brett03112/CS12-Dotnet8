using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

/*
This class definition represents a Category entity in a database, with properties and attributes that define 
its structure and relationships.

Here's a breakdown of what each part of the class does:

    [Index("CategoryName", Name = "CategoryName")]: Creates an index on the CategoryName column in the database 
    for faster querying.

    [Key] public int CategoryId { get; set; }: Defines the primary key of the table, a unique identifier for each category.
    
    [Required] [Column(TypeName = "nvarchar (15)")] [StringLength(15)] public string CategoryName { get; set; } = null!;: 
        Defines a required string property CategoryName with a maximum length of 15 characters.
    
    [Column(TypeName = "ntext")] public string? Description { get; set; }: Defines an optional string property 
        Description that can store large amounts of text.
    
    [Column(TypeName = "image")] public byte[]? Picture { get; set; }: Defines an optional byte array property Picture 
        that can store image data.
    
    [InverseProperty("Category")] public virtual ICollection<Product> Products { get; set; } = new List<Product>();: 
        Defines a navigation property Products that represents a collection of Product entities related to this category, 
        with the inverse property being the Category property on the Product entity.
        
Note that this class is marked as partial, indicating that it may be extended or modified by other parts of the codebase.
*/
[Index("CategoryName", Name = "CategoryName")]
public partial class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar (15)")]
    [StringLength(15)]
    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "ntext")]
    public string? Description { get; set; }

    [Column(TypeName = "image")]
    public byte[]? Picture { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

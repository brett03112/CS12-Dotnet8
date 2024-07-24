﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.AutoGen;

[Index("CategoryName", Name = "CategoryName")]
public partial class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Column(TypeName = "nvarchar (15)")]
    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "ntext")]
    public string? Description { get; set; }

    [Column(TypeName = "image")]
    public byte[]? Picture { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    /*
        • It decorates the entity class with the [Index] attribute, which was introduced in EF
        Core 5. This indicates properties that should have an index when using the Code First
        approach to generate a database at runtime. Since we are using Database First with
        an existing database, this is not needed. But if we wanted to recreate a new, empty
        database from our code, then this information would be needed.

        • The table name in the database is Categories but the dotnet-ef tool uses the Humanizer 
        third-party library to automatically singularize the class name to Category, which
        is a more natural name when creating a single entity that represents a row in the table.

        • The entity class is declared using the partial keyword so that you can 
        create a matching partial class for adding additional code. This allows you to rerun the tool and
        regenerate the entity class without losing that extra code.

        • The CategoryId property is decorated with the [Key] attribute to indicate that it is the
        primary key for this entity. The data type for this property is int for SQL Server and long
        for SQLite. We did not do this because we followed the naming primary key convention.

        • The CategoryName property is decorated with the [Column(TypeName = "nvarchar
        (15)")] attribute, which is only needed if you want to generate a database from the
        model.

        • We chose not to include the Picture column as a property because this is a binary
        object that we will not use in our console app.
        
        • The Products property uses the [InverseProperty] attribute to define the foreign key
        relationship to the Category property on the Product entity class, and it initializes the
        collection to a new empty list.
    */
}

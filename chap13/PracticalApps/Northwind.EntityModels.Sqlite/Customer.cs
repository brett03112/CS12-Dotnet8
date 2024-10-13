using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;
/*
This C# code defines a Customer class, which represents a customer entity in a database.

The class has several properties, such as CustomerId, CompanyName, ContactName, Address, etc., 
each with specific data type and length constraints.

The [Index] attributes on the class level specify that the City, CompanyName, PostalCode, and 
Region columns should be indexed in the database for faster querying.

The [Key] attribute on the CustomerId property indicates that it is the primary key of the table.

The [Required] attribute on the CompanyName property means that this field cannot be null.

The [Column] and [StringLength] attributes specify the data type and length of each column in the database.

The [RegularExpression] attribute on the CustomerId property ensures that it only contains 5 uppercase letters.

The [InverseProperty] attribute on the Orders property establishes a relationship between the Customer 
and Order entities, indicating that a customer can have multiple orders.

*/
[Index("City", Name = "City")]
[Index("CompanyName", Name = "CompanyNameCustomers")]
[Index("PostalCode", Name = "PostalCodeCustomers")]
[Index("Region", Name = "Region")]
public partial class Customer
{
    [Key]
    [Column(TypeName = "nchar (5)")]
    [StringLength(5)]
    [RegularExpression("[A-Z]{5}")]
    public string CustomerId { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar (40)")]
    [StringLength(40)]
    public string CompanyName { get; set; } = null!;

    [Column(TypeName = "nvarchar (30)")]

    [StringLength(30)]
    public string? ContactName { get; set; }

    [Column(TypeName = "nvarchar (30)")]

    [StringLength(30)]
    public string? ContactTitle { get; set; }

    [Column(TypeName = "nvarchar (60)")]

    [StringLength(60)]
    public string? Address { get; set; }

    [Column(TypeName = "nvarchar (15)")]

    [StringLength(15)]
    public string? City { get; set; }

    [Column(TypeName = "nvarchar (15)")]

    [StringLength(15)]
    public string? Region { get; set; }

    [Column(TypeName = "nvarchar (10)")]

    [StringLength(10)]
    public string? PostalCode { get; set; }

    [Column(TypeName = "nvarchar (15)")]

    [StringLength(15)]
    public string? Country { get; set; }

    [Column(TypeName = "nvarchar (24)")]

    [StringLength(24)]
    public string? Phone { get; set; }

    [Column(TypeName = "nvarchar (24)")]

    [StringLength(24)]
    public string? Fax { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

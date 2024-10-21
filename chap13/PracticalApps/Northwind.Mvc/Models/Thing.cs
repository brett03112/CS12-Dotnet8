using System.ComponentModel.DataAnnotations; // To use [Range] [Required] [EmailAddress]

namespace Northwind.Mvc.Models;


/* The `public record Thing` declaration is creating a C# record type named `Thing`. In C#, a record is
a reference type that is similar to a class but is primarily used for immutable data. In this case,
the `Thing` record has three properties: `Id`, `Color`, and `Email`. Each property has specific data
annotations applied to it: `[Range(1, 10)]` for the `Id` property to specify a range of valid
values, `[Required]` for the `Color` property to indicate that it cannot be null or empty, and
`[EmailAddress]` for the `Email` property to ensure it is a valid email address format. */
public record Thing(
    [Range(1, 10)] int? Id,
    [Required] string? Color,
    [EmailAddress] string? Email
);
using System.Diagnostics.CodeAnalysis; // To use [SetsRequiredMembers]

namespace Packt.Shared;

public class Book
{
    // Needs .NET 7 or later as well as C# 11 or later
    public required string? Isbn;
    public required string? Title;

    public string? Author;
    public int PageCount;

    public Book() { }  //Constructor for use with object initializer syntax

    /// <summary>
    /// Initializes a new instance of the <see cref="Book"/> class with the specified ISBN and title.
    /// </summary>
    /// <param name="isbn">The ISBN of the book.</param>
    /// <param name="title">The title of the book.</param>
    [SetsRequiredMembers]
    public Book(string? isbn, string title)
    {
        Isbn = isbn;
        Title = title;        
    }
}

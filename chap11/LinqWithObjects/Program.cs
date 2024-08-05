
// A string array is a sequence that implements IEnumerable<string>
string[] names = { "Michael", "Pam", "Jim", "Dwight",
    "Angela", "Kevin", "Toby", "Creed" };

//DeferredExecution(names);
FilteringUsingWhere(names);

/*
LINQ components
LINQ has several parts; some are required, and some are optional:
___________________________________________________________________________________________
• Extension methods (required): These include examples such as Where, OrderBy, and Select.
These are what provide the functionality of LINQ.

• LINQ providers (required): These include LINQ to Objects for processing in-memory objects,
LINQ to Entities for processing data stored in external databases and modeled with EF Core,
and LINQ to XML for processing data stored as XML. These providers are the part of LINQ that
executes LINQ expressions in a way specific to different types of data.

• Lambda expressions (optional): These can be used instead of named methods to simplify LINQ
queries, for example, for the conditional logic of the Where method for filtering.

• LINQ query comprehension syntax (optional): These include C# keywords like from, in, where,
orderby, descending, and select. These are aliases for some of the LINQ extension methods,
and their use can simplify the queries you write, especially if you already have experience
with other query languages, such as Structured Query Language (SQL).

Building LINQ expressions with the Enumerable class
______________________________________________________________________________________________________
The LINQ extension methods, such as Where and Select, are appended by the Enumerable static class
to any type, known as a sequence, that implements IEnumerable<T>. A sequence contains zero, one,
or more items.

For example, an array of any type implements the IEnumerable<T> class, where T is the type of item
in the array. This means that all arrays support LINQ to query and manipulate them.

All generic collections, such as List<T>, Dictionary<TKey, TValue>, Stack<T>, and Queue<T>, 
implement IEnumerable<T>, so they can be queried and manipulated with LINQ too.
*/

/*
Method(s)                               Description
____________________________________________________________________________________________________
First, FirstOrDefault, Last,            Get the first or last item in the sequence or throw an exception, or
LastOrDefault                           return the default value for the type, for example, 0 for an int and
                                        null for a reference type, if there is not a first or last item.
                               
Where                                   Return a sequence of items that match a specified filter.

Single, SingleOrDefault                 Return an item that matches a specific filter or throw an exception,or return the default
                                        value for the type if there is not exactly one match.

ElementAt,                              Return an item at a specified index position or throw an exception,
ElementAtOrDefault                      or return the default value for the type if there is not an item at that
                                        position. Introduced in .NET 6 are overloads that can be passed an
                                        Index instead of an int, which is more efficient when working with
                                        Span<T> sequences.

Select, SelectMany                      Project items into a different shape, that is, a different type, and
                                        flatten a nested hierarchy of items.

OrderBy, OrderByDescending,
ThenBy, ThenByDescending                Sort items by a specified field or property.

Order, OrderDescending                  Sort items by the item itself.

Reverse                                 Reverse the order of the items.

GroupBy, GroupJoin, Join                Group and/or join two sequences.

Skip, SkipWhile                         Skip a number of items; or skip when an expression is true.

Take, TakeWhile                         Take a number of items, or take items while an expression is true.
                                        Introduced in .NET 6 is an overload that can be passed a Range, for
                                        example, Take(range: 3..^5), meaning take a subset starting
                                        3 items in from the start and ending 5 items in from the end, or
                                        instead of Skip(4) you could use Take(4..).

Aggregate, Average, Count,
LongCount, Max, Min, Sum                Calculate aggregate values.

TryGetNonEnumeratedCount                Count() checks if a Count property is implemented on the
                                        sequence and returns its value, or it enumerates the entire
                                        sequence to count its items. Introduced in .NET 6, this method only
                                        checks for Count; if it is missing, it returns false and sets the out
                                        parameter to 0 to avoid a potentially poor-performing operation.

All, Any, Contains                      Return true if all or any of the items match the filter, or if the
                                        sequence contains a specified item.

Cast<T>                                 Cast items into a specified type. It is useful to convert non-generic
                                        objects in to a generic type in scenarios where the compiler would
                                        otherwise complain.

OfType<T>                               Remove items that do not match a specified type.

Distinct                                Remove duplicate items.

Except, Intersect, Union                Perform operations that return sets. Sets cannot have duplicate
                                        items. Although the inputs can be any sequence and so the inputs
                                        can have duplicates, the result is always a set.

DistinctBy, ExceptBy,                   Allow the comparison to be performed on a subset of the items
IntersectBy, UnionBy, MinBy,            rather than all the items. For example, instead of removing
MaxBy                                   duplicates with Distinct by comparing an entire Person object,
                                        you could remove duplicates with DistinctBy by comparing just
                                        their LastName and DateOfBirth.

Chunk                                   Divide a sequence into sized batches. The size parameter specified
                                        the number of items in each chunk. The last chunk will contain the
                                        remaining items and could be less than size.

Append, Concat, Prepend                 Perform sequence-combining operations.

Zip                                     Perform a match operation on two or three sequences based on
                                        the position of items, for example, the item at position 1 in the first
                                        sequence matches the item at position 1 in the second sequence.

ToArray, ToList,                        Convert the sequence into an array or collection. These are the only
ToDictionary, ToHashSet,                extension methods that force the execution of a LINQ expression
ToLookup                                immediately rather than wait for deferred execution, which you will
                                        learn about shortly.
*/

/*
            ENUMERABLE METHODS
____________________________________________________________________________________________________________
Method              Description

Empty<T>            Returns an empty sequence of the specified type T. It is useful for passing an empty
                    sequence to a method that requires an IEnumerable<T>.

Range               Returns a sequence of integers from the start value with count items. For example,
                    Enumerable.Range(start: 5, count: 3) would contain the integers 5, 6, and 7.

Repeat              Returns a sequence that contains the same element repeated count times. For
                    example, Enumerable.Repeat(element: "5", count: 3) would contain the string
                    values "5", "5", and "5".
*/

#region Working with spans, indexes, and ranges

/* INDEXES
// Two ways to define the same index, 3 in from the start.

        Index i1 = new(value: 3); // Counts from the start
        Index i2 = 3; // Using implicit int conversion operator.

// Two ways to define the same index, 5 in from the end.

        Index i3 = new(value: 5, fromEnd: true);
        Index i4 = ^5; // Using the caret ^ operator.

RANGES
Range r1 = new(start: new Index(3), end: new Index(7));

Range r2 = new(start: 3, end: 7); // Using implicit int conversion.

Range r3 = 3..7; // Using C# 8.0 or later syntax.

Range r4 = Range.StartAt(3); // From index 3 to last index.

Range r5 = 3..; // From index 3 to last index.

Range r6 = Range.EndAt(3); // From index 0 to index 3.

Range r7 = ..3; // From index 0 to index 3.

*/

string name  = "Samantha Jones";

// Getting the length of the first and last name
int lengthOfFirst = name.IndexOf(' ');
int lengthOfLast = name.Length - lengthOfFirst - 1;

// Using Substring
string firstname = name.Substring(startIndex: 0, length: lengthOfFirst);
string lastname = name.Substring(startIndex: lengthOfFirst + 1, length: lengthOfLast);
WriteLine($"First: {firstname}, Last: {lastname}");

// Using Span
ReadOnlySpan<char> nameAsSpan = name.AsSpan();
ReadOnlySpan<char> firstnameSpan = nameAsSpan[0..lengthOfFirst];
ReadOnlySpan<char> lastnameSpan = nameAsSpan[^lengthOfLast..];

WriteLine($"First: {firstnameSpan}, Last: {lastnameSpan}");
#endregion
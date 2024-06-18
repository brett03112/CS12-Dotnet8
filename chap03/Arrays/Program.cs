#region Arrays

string[] names; // This can reference any size of array of strings

//allocate memory for four strings in an array
names = new string[4];

//store items at these index positions
names[0] = "Kate";
names[1] = "Jack";
names[2] = "Rebecca";
names[3] = "Tom";

// Loop through the names
for (int i = 0; i < names.Length; i++)
{
    WriteLine($"{names[i]} is at position {i}");
}

// alternative syntax for creating and intializing an array
string[] names2 = new string[] { "Kate", "Jack", "Rebecca", "Tom" };

for (int i = 0; i < names2.Length; i++)
{
    WriteLine($"{names2[i]} is at position {i}");
}
#endregion

#region muliti-dimensional arrays

string[,] grid1 =
{   
    { "Alpha", "Beta", "Gamma", "Delta"},
    { "Anne", "Ben", "Charlie", "Doug" },
    { "Aardvark", "Bear", "Cat", "Dog" }
};
WriteLine($"1st dimension, lower bound: {grid1.GetLowerBound(0)}");
WriteLine($"1st dimension, upper bound: {grid1.GetUpperBound(0)}");
WriteLine($"2nd dimension, lower bound: {grid1.GetLowerBound(1)}");
WriteLine($"2nd dimension, upper bound: {grid1.GetUpperBound(1)}\n");

for (int row = 0; row <= grid1.GetUpperBound(0); row++)
{
    for (int col = 0; col <= grid1.GetUpperBound(1); col++)
    {
        WriteLine($"Row {row}, Column {col}: {grid1[row, col]}");
    }
}

WriteLine();
/*
// Alternative syntax for declaring and allocating memory
// for a multi-dimensional array.
string[,] grid2 = new string[3,4]; // Allocate memory.
grid2[0, 0] = "Alpha"; // Assign values.
grid2[0, 1] = "Beta";
// And so on.
grid2[2, 3] = "Dog";
*/
#endregion

#region Jagged arrays
string[][] jagged = // An array of string arrays.
{
    new[] { "Alpha", "Beta", "Gamma" },
    new[] { "Anne", "Ben", "Charlie", "Doug" },
    new[] { "Aardvark", "Bear" }
};

WriteLine("Upper bound of the array of arrays is: {0}",
    jagged.GetUpperBound(0));

for (int array = 0; array <= jagged.GetUpperBound(0); array++)
{
    WriteLine("Upper bound of array {0} is: {1}",
    arg0: array,
    arg1: jagged[array].GetUpperBound(0));
}

for (int row = 0; row <= jagged.GetUpperBound(0); row++)
{
    for (int col = 0; col <= jagged[row].GetUpperBound(0); col++)
    {
        WriteLine($"Row {row}, Column {col}: {jagged[row][col]}");
    }
}
#endregion

#region List pattern matching with arrays
/*
Example                 Description

[]                      Matches an empty array or collection.

[..]                    Matches an array or collection with any number of items, including zero, so
                            [..]must come after [] if you need to switch on both.

[_]                     Matches a list with any single item.

[int item1] or|         Matches a list with any single item and can use the value in the return
[var item1]                 expression by referring to item1.

[7, 2]                  Matches exactly a list of two items with those values in that order.

[_, _]                  Matches a list with any two items.

[var item1, var         Matches a list with any two items and can use the values in the return
item2]                      expression by referring to item1 and item2.

[_, _, _]               Matches a list with any three items.

[var item1, ..]         Matches a list with one or more items. Can refer to the value of the first
                            item in its return expression by referring to item1.

[var firstItem, ..,     Matches a list with two or more items. Can refer to the value of the first and
var lastItem]               last item in its return expression by referring to firstItem and lastItem.

[.., var lastItem]      Matches a list with one or more items. Can refer to the value of the last item
                            in its return expression by referring to lastItem.
*/

int[] sequentialNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
int[] oneTwoNumbers = { 1, 2 };
int[] oneTwoTenNumbers = { 1, 2, 10 };
int[] oneTwoThreeTenNumbers = { 1, 2, 3, 10 };
int[] primeNumbers = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };
int[] fibonacciNumbers = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };
int[] emptyNumbers = { }; // Or use Array.Empty<int>()
int[] threeNumbers = { 9, 7, 5 };
int[] sixNumbers = { 9, 7, 5, 4, 2, 10 };
WriteLine($"{nameof(sequentialNumbers)}: {CheckSwitch(sequentialNumbers)}");
    
WriteLine($"{nameof(oneTwoNumbers)}: {CheckSwitch(oneTwoNumbers)}");

WriteLine($"{nameof(oneTwoTenNumbers)}: {CheckSwitch(oneTwoTenNumbers)}");

WriteLine($"{nameof(oneTwoThreeTenNumbers)}: {CheckSwitch(oneTwoThreeTenNumbers)}");

WriteLine($"{nameof(primeNumbers)}: {CheckSwitch(primeNumbers)}");

WriteLine($"{nameof(fibonacciNumbers)}: {CheckSwitch(fibonacciNumbers)}");

WriteLine($"{nameof(emptyNumbers)}: {CheckSwitch(emptyNumbers)}");

WriteLine($"{nameof(threeNumbers)}: {CheckSwitch(threeNumbers)}");

WriteLine($"{nameof(sixNumbers)}: {CheckSwitch(sixNumbers)}");

static string CheckSwitch(int[] values) => values switch
{
    [] => "Empty array",
    [1, 2, _, 10] => "Contains 1, 2, any single number, 10.",
    [1, 2, .., 10] => "Contains 1, 2, any range including empty, 10.",
    [1, 2] => "Contains 1 then 2.",
    [int item1, int item2, int item3] =>
        $"Contains {item1} then {item2} then {item3}.",
    [0, _] => "Starts with 0, then one other number.",
    [0, ..] => "Starts with 0, then any range of numbers.",
    [2, .. int[] others] => $"Starts with 2, then {others.Length} more numbers.",   
    [..] => "Any items in any order.",
};

#endregion
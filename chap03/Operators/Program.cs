#region exploring unary operators
int a = 3;
int b = a++;
WriteLine($"a is {a}, b is {b}\n");

int c = 3;
int d = ++c;
WriteLine($"c is {c}, d is {d}\n");

int e = 11;
int f = 3;
WriteLine($"e is {e}, f is {f}\n");
WriteLine($"e + f = {e + f}");
WriteLine($"e - f = {e - f}");
WriteLine($"e * f = {e * f}");
WriteLine($"e / f = {e / f}");
WriteLine($"e % f = {e % f}\n");

double g = 11.0;
WriteLine($"g is {g:N1}, f is {f}");
WriteLine($"g / f = {g / f:N1}\n");

int r = 6;
r += 3; // p = p + 3
r -= 3; // p = p - 3
r /= 3; // p = p / 3
r *= 4; // p = p * 4
#endregion

#region null coalescing
WriteLine("Enter your name: ");
string? authorName = ReadLine(); // prompt user for input
int maxLength = authorName?.Length ?? 30; // maxLength = authorName.Length if not null, else 30

authorName ??= "unknown"; // if authorName is null, set it to "unknown"
#endregion

#region logical operators
//logical operators
bool p = true;
bool q = false;

WriteLine($"AND  | p     | q     ");
WriteLine($"p    | {p & p,-5} | {p & q,-5} ");
WriteLine($"q    | {q & p,-5} | {q & q,-5} ");
WriteLine();

WriteLine($"OR   | p     | q     ");
WriteLine($"p    | {p | p,-5} | {p | q,-5} ");
WriteLine($"q    | {q | p,-5} | {q | q,-5} ");
WriteLine();

WriteLine($"XOR  | p     | q     ");
WriteLine($"p    | {p ^ p,-5} | {p ^ q,-5} ");
WriteLine($"q    | {q ^ p,-5} | {q ^ q,-5} ");
WriteLine();

// Note that DoStuff() returns true
WriteLine($"p & DoStuff() = {p & DoStuff()}");
WriteLine($"q & DoStuff() = {q & DoStuff()}\n\n");

WriteLine();
WriteLine($"p && DoStuff() = {p && DoStuff()}");
WriteLine($"q && DoStuff() = {q && DoStuff()}\n");// Note that DoStuff() returns false and nothing is printed from DoStuff()
#endregion

#region Bitwise and Binary Shift Operators
int x = 10;
int y = 6;

WriteLine($"Expression  |  Decimal  |  Binary");
WriteLine($"---------------------------------");
WriteLine($"x           | {x,7} | {x:B8}");
WriteLine($"y           | {y,7} | {y:B8}");
WriteLine($"x & y       | {x & y,7} | {x & y:B8}");
WriteLine($"x | y       | {x | y,7} | {x | y:B8}");
WriteLine($"x ^ y       | {x ^ y,7} | {x ^ y:B8}");
// Left-shift x by three bit columns.
WriteLine($"x << 3      | {x << 3,7} | {x << 3:B8}");
// Multiply x by 8.
WriteLine($"x * 8       | {x * 8,7} | {x * 8:B8}");
// Right-shift y by one bit column.
WriteLine($"y >> 1      | {y >> 1,7} | {y >> 1:B8}\n");
#endregion

#region miscellaneous operators

int age = 50;
WriteLine($"The {nameof(age)} variable uses {sizeof(int)} bytes of memory.\n");

// How many operators in the following statement?
char firstDigit = age.ToString()[0];
// There are four operators:
// = is the assignment operator
// . is the member access operator
// () is the invocation operator
// [] is the indexer access operator
#endregion



static bool DoStuff()
{
    WriteLine("I'm doing some stuff");
    return true;
}
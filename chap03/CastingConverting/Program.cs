using static System.Convert; // To use the ToInt32 method
using static System.Console;
using System.Globalization;

#region implicitly casting
int a = 10;
double b = a; // an int can be safely cast to a double
WriteLine($"a is{a} and b is {b}");

double c = 9.8;
int d = (int) c; // compiler gives an error if you do not explicitly cast
/*
Error: (7,9): error CS0266: Cannot implicitly convert type 'double' to
'int'. An explicit conversion exists (are you missing a cast?)
*/
WriteLine($"c is {c} and d is {d}\n");

long e = 10;
int f =   (int) e;
WriteLine($"e is {e:N0} and f is {f:N0}\n");

e = long.MaxValue;
f = (int) e;
WriteLine($"e is {e:N0} and f is {f:N0}\n");

e = 5_000_000_000;
f = (int) e;
WriteLine($"e is {e:N0} and f is {f:N0}\n");
#endregion

#region Negative numbers in binary
/*
    Negative aka signed numbers
    use the first bit to represent negativity. If the bit is 0 (zero), then it is a positive number. If the bit is 1
    (one), then it is a negative number.
*/
WriteLine("{0,12} {1,34}", "Decimal", "Binary\n");// 12 and ,34 mean right-align within those column widths. :B32 means
                                                //    format as binary padded with leading zeros to a width of 32.
WriteLine("{0,12} {0,34:B32}\n", int.MaxValue);

for (int i = 8; i >= -8; i--)
{
    WriteLine("{0,12} {0,34:B32}\n", i);
}
WriteLine("{0,12} {0,34:B32}\n", int.MinValue);
#endregion

#region Using the Convert class
// Using the ToInt32 method
double g = 9.8;
int h = ToInt32(g);
WriteLine($"g is {g} and h is {h}"); // converting rounds up to the nearest integer.  While casting rounds down.

int j = (int) g;
WriteLine($"g is {g} and j is {j}"); // rounds down to the nearest integer
#endregion

#region Using the Convert class larger example
/*
• It always rounds toward zero if the decimal part is less than the midpoint .5.
• It always rounds away from zero if the decimal part is more than the midpoint .5.
• It will round away from zero if the decimal part is the midpoint .5 and the non-decimal part is
        odd, but it will round toward zero if the non-decimal part is even.
*/
double[,] doubles = {
    { 9.49, 9.5, 9.51 },
    { 10.49, 10.5, 10.51 },
    { 11.49, 11.5, 11.51 },
    { 12.49, 12.5, 12.51 } ,
    { -12.49, -12.5, -12.51 },
    { -11.49, -11.5, -11.51 },
    { -10.49, -10.5, -10.51 },
    { -9.49, -9.5, -9.51 }
};
WriteLine($"| double | ToInt32 | double | ToInt32 | double | ToInt32 |");

for (int x = 0; x < 8; x++)
{
    for (int y = 0; y < 3; y++)
    {
        Write($"| {doubles[x, y], 6} | {ToInt32(doubles[x, y]), 7}");
    }

    WriteLine("|");
}
WriteLine();
#endregion

#region Using the Round method

foreach (double n in doubles)
{
    WriteLine(format: "Math.Round({0}, 0 MidpointRounding.AwayFromZero) = {1}", arg0: n,
        arg1: Math.Round(value: n, digits: 0, mode: MidpointRounding.AwayFromZero));
}
#endregion

#region Converting to a String

int number = 12;
WriteLine(number.ToString());
bool boolean = true;
WriteLine(boolean.ToString());
DateTime now = DateTime.Now;
WriteLine(now.ToString());
object me = new();
WriteLine(me.ToString());

// Converting to a string from binary
// Allocate an array of 128 bytes
byte[] binaryObject = new byte[128];

// Populate the array with random bytes
Random.Shared.NextBytes(binaryObject);

WriteLine("Binary object as bytes: ");
for (int index = 0; index < binaryObject.Length; index++)
{
    Write($"{binaryObject[index]:X2} ");
}
WriteLine();

// Convert the array to Base64 string and output as text
string encoded = ToBase64String(binaryObject); 
WriteLine($"Binary as Base64: {encoded}\n");
#endregion

#region Parsing from Strings to numbers or dates and times

// Set the current culture to make sure date parsing works
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

int friends = int.Parse("27");
DateTime birthday = DateTime.Parse("4 June 1980");
WriteLine($"I have {friends} friends to invite to my party.");
WriteLine($"My birthday is {birthday}.");
WriteLine($"My birthday is {birthday:D}.\n");

#endregion

#region Using the TryParse method
/*
    Unhandled Exception: System.FormatException: Input string was not in a
    correct format.
*/
//  int count = int.Parse("abc");

// using TryParse
Write("How many eggs are there? ");
string? input = ReadLine();
if (int.TryParse(input, out int count))
{
    WriteLine($"There are {count} eggs.\n");
}
else
{
    WriteLine("I could not parse that input.\n");
}
// a method that might throw an exception
int number1 = int.Parse("123");

// The Try equivalent of the method
bool success = int.TryParse("abc", out int number2);

// Trying to create a Uri for a Web API
bool success1 = Uri.TryCreate("https://localhost:5000/api/customers", UriKind.Absolute, out Uri? serviceUrl);
WriteLine($"The service URL is {serviceUrl}\n"); 

#endregion

using Extensions.Methods; // ToWords Extension Method
using System.Numerics; // For BigInteger

Write("Enter a number up to twenty one digits long: ");
string? input = ReadLine();
if (input is null) return;

if (input.Length > 21)
{
    WriteLine("The number is too large.");
    return;
}

BigInteger number = BigInteger.Parse(input);

WriteLine($"{number:N0} in words is {number.ToWords()}");
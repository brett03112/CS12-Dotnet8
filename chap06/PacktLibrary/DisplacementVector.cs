namespace Packt.Shared;

/*
• The type is declared using struct instead of class.

• It has two int properties, named X and Y, that will auto-generate two private fields with
    the same data type, which will be allocated on the stack.

• It has a constructor to set initial values for X and Y.

• It has an operator to add two instances together that returns a new instance of the type,
    with X added to X, and Y added to Y:
*/

public record struct DisplacementVector
/*
A record struct has all the same benefits over a record class that a struct has over a class. One
difference between record struct and record class declared using primary constructor syntax is
that record struct is not immutable, unless you also apply the readonly keyword to the record
struct declaration. A struct does not implement the == and != operators, but they are automatically
implemented with a record struct.
*/
{
    public int X { get; set; }
    public int Y { get; set; }

    public DisplacementVector(int initialX, int initialY)
    {
        X = initialX;
        Y = initialY;
    }
    /// <summary>
    /// operator to add two instances
    /// </summary>
    /// <param name="vector1"></param>
    /// <param name="vector2"></param>
    /// <returns>new(vector1.X + vector2.X, vector1.Y + vector2.Y)</returns>
    public static DisplacementVector operator +(DisplacementVector vector1, DisplacementVector vector2)
    {
        return new(vector1.X + vector2.X, vector1.Y + vector2.Y);
    }
}
using InheritanceExercise;

public class Square : Shape
{
    public new double Height { get; set; }
    public new double Width { get; set; }

    /// <summary>
    /// Initializes a new instance of the Square class.
    /// </summary>
    /// <param name="sideLength">The length of the side of the square.</param>
    public Square(double sideLength)
    {
        Height = Width = sideLength;
    }        

    /// <summary>
    /// Returns the area of the square
    /// </summary>
    /// <returns>sideLength * sideLength (Height * Width)</returns>
    public override double Area()
    {
        return Width * Height;
    }

}
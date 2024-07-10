using InheritanceExercise;

public class Rectangle : Shape
{
    public new double Width { get; set; }
    public new double Height { get; set; }

    /// <summary>
    /// Returns the area of the rectangle
    /// </summary>
    /// <returns>Area = Width * Height</returns>
    public override double Area()
    {
        return (double) Width * Height;
    }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }
}





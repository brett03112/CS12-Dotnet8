using InheritanceExercise;

public class Circle : Shape
{
    public new double Height { get; set; }
    public new double Width { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Circle"/> class with the specified radius.
    /// </summary>
    /// <param name="radius">The radius of the circle.</param>
    public Circle(double radius)
    {
        Height = Width = radius;
    }
    /// <summary>
    /// Returns the area of the circle
    /// </summary>
    /// <returns>Math.PI * radius * radius</returns>
    public override double Area()
    {
        return Math.PI * Height * Width;
    }
}
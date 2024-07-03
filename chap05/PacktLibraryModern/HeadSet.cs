namespace Packt.Shared;

public class HeadSet(string? manufacturer, string? productName)
{
    public string? Manufacturer { get; init; } = manufacturer;
    public string? ProductName { get; init; } = productName;

    // Default parameterless constructor calls the primary constructor
    public HeadSet() : this("Microsoft", "HoloLens") { }
}
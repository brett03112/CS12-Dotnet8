namespace Packt.Shared;

public class AnimalClass
{
    public string? Name { get; set; }
}

public record AnimalRecord
{
    public string? Name { get; init; }
}
/*

    The following is an example of an immutable record with a Deconstruct method.

public record ImmutableAnimal
{
    public string? Name { get; init; }
    public string? Species { get; init; }

    public ImmutableAnimal(string? name, string? species)
    {
        Name = name;
        Species = species;
    }

    public void Deconstruct(out string? name, out string? species)
    {
        name = Name;
        species = Species;
    }
    */

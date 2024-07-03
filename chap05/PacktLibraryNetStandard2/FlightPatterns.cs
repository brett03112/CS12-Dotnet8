// All classes in this file will be defined in the Packt.Shared namespace.
namespace Packt.Shared;

public class Passenger
{
    public string? Name { get; set; }
}

public class BusinessClassPassenger : Passenger
{
    public override string ToString()
    {
        return $"Business Class: {Name}"; // base.ToString();
    }
}

public class FirstClassPassenger : Passenger
{
    public int AirMiles { get; set; }

    public override string ToString()
    {
        return $"First Class with {AirMiles:N0} air miles: {Name}";
    }
}

public class CoachClassPassenger : Passenger
{
    public double CarryOnKG { get; set; }

    public override string ToString()
    {
        return $"Coach Class with {CarryOnKG:N2} kg of carry on: {Name}";
    }
}
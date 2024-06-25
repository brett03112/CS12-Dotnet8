using static System.Console;

namespace CallStackExceptionHandlingLib;

public class Processor
{
    public static void Gamma() // public to it can be called from outside
    {
        WriteLine("In Gamma");
        Delta();
    }

    private static void Delta() // private to it cannot be called from outside
    {
        WriteLine("In Delta");
        File.OpenText("bad file path");
    }
}

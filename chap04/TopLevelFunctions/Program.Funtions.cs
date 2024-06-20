using static System.Console;

// do not define a namespace so this class goes in the default empty namespace
// just like the auto-generated partial Program class

partial class Program
{
    static void WhatsMyNamespace()
    {
        WriteLine("Nmaspace of Program class: {0}",
            arg0: typeof(Program).Namespace ?? "null");
    }
}